#region copyright

// <copyright file="WitsHost.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>

#endregion copyright

namespace WitsParser.Transport.Server.Wits
{
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using WitsParser.Wits;

    /// <summary>
    /// Wits TCP host
    /// </summary>
    public class WitsHost : IHost, IDisposable
    {
        private const int WaitForNextConnectionTime = 100;
        private static Logger _Logger = LogManager.GetCurrentClassLogger();

        private TcpListener _listener;
        private ManualResetEvent _stopEvent = new ManualResetEvent(false);
        private List<ISimpleWorker> _Workers = new List<ISimpleWorker>();
        private object _SyncAddingClientsObj = new object();
        private WorkersFactory _WorkersFactory;
        private readonly WitsLevel _WitsLevel;
        private readonly IEnumerable<IPostProcessing> _postprocessors;

        /// <summary>
        /// Is host run
        /// </summary>
        public bool IsRun { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">Port</param>
        /// <param name="level">Wits Level</param>
        public WitsHost(int port, WitsLevel level)
        {
            _WitsLevel = level;
            _WorkersFactory = new WorkersFactory();
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">Port</param>
        /// <param name="level">Wits Level</param>
        /// <param name="Postprocessors">List of postprocessors</param>
        public WitsHost(int port, WitsLevel level, IEnumerable<IPostProcessing> postprocessors) : this(port, level)
        {
            this._postprocessors = postprocessors;
        }

        /// <summary>
        /// Start
        /// </summary>
        public void Start()
        {
            _listener.Start();
            _stopEvent.Reset();

            try
            {
                _listener.BeginAcceptTcpClient(acceptTcpCallBack, _listener);
            }
            catch (Exception ex)
            {
                _Logger.Log(LogLevel.Error, ex, "BeginAcceptTcpClient call error");
                throw ex;
            }
        }

        private void acceptTcpCallBack(IAsyncResult ar)
        {
            if (_stopEvent.WaitOne(WaitForNextConnectionTime)) return;

            var server = (TcpListener)ar.AsyncState;

            try
            {
                var client = server.EndAcceptTcpClient(ar);

                CreateClientWorker(client);
            }
            catch (Exception ex)
            {
                _Logger.Log(LogLevel.Error, ex, "EndAcceptTcpClient call error");
            }

            try
            {
                server.BeginAcceptTcpClient(acceptTcpCallBack, server);
            }
            catch (Exception ex)
            {
                _Logger.Log(LogLevel.Error, ex, "BeginAcceptTcpClient call error");

                IsRun = false;
            }
        }

        private void CreateClientWorker(TcpClient client)
        {
            var worker = _WorkersFactory.GetWitsWorker(_WitsLevel, _postprocessors);

            worker.Start(client);

            lock (_SyncAddingClientsObj)
            {
                _Workers.Add(worker);
            }
        }

        /// <summary>
        /// Stop
        /// </summary>
        public void Stop()
        {
            lock (_SyncAddingClientsObj)
            {
                foreach (var item in _Workers)
                {
                    item.Stop();
                }
            }

            _stopEvent.Set();
        }

        /// <summary>
        /// Close
        /// </summary>
        public void Close()
        {
            Stop();
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Close();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}