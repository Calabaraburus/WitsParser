#region copyright
// <copyright file="TcpIpClient.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>
#endregion
namespace WitsParser.Transport.Client
{
    using NLog;
    using System;
    using System.Net.Sockets;
    using System.Threading;

    /// <summary>
    /// Implements TcpIp client
    /// </summary>
    public class TcpIpClient : IClient, IDisposable
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly int _Port;
        private readonly string _ConnectionString;
        private TcpClient _client;
        private static ManualResetEvent TimeoutObject = new ManualResetEvent(false);
        private const int TimeoutPeriod = 5000;

        /// <summary>
        /// Tcp client
        /// </summary>
        public TcpClient Client { get { return _client; } }

        /// <summary>
        /// Return true, if connected to server, false otherwise
        /// </summary>
        public bool IsConnected { get { return _client.Connected; } }

        /// <summary>
        /// Event should be raise when connected to server
        /// </summary>
        public event EventHandler Connected;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tcpConnectionString">Connection string</param>
        /// <param name="port">Port number</param>
        public TcpIpClient(string tcpConnectionString, int port)
        {
            _ConnectionString = tcpConnectionString;
            _Port = port;
            _client = new TcpClient();
        }

        /// <summary>
        /// Connect to server
        /// </summary>
        public void Connect()
        {
            try
            {
                TimeoutObject.Reset();
                _client.BeginConnect(_ConnectionString, _Port, ConnectCallback, this);
                TimeoutObject.WaitOne(TimeoutPeriod);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Ошибка подключения");
                throw ex;
            }
        }

        private void ConnectCallback(IAsyncResult asyncResult)
        {
            var witsClient = asyncResult.AsyncState as TcpIpClient;
            try
            {
                if (witsClient.Client.Client != null)
                {
                    witsClient.Client.EndConnect(asyncResult);
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Ошибка ассинхронного подключения");
            }
            finally
            {
                TimeoutObject.Set();
            }
        }

        /// <summary>
        /// Start asynch data send
        /// </summary>
        /// <param name="buffer">Buffer</param>
        /// <param name="callback">Callback</param>
        /// <param name="state">State object</param>
        /// <returns>Return async result</returns>
        IAsyncResult IClient.BeginSend(byte[] buffer, AsyncCallback callback, object state)
        {
            try
            {
                return Client.Client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, callback, this);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex);

                throw ex;
            }
        }

        /// <summary>
        /// Stop async send
        /// </summary>
        /// <param name="asyncResult">Async result</param>
        /// <returns>Returns </returns>
        int IClient.EndSend(IAsyncResult asyncResult)
        {
            try
            {
                return Client.Client.EndSend(asyncResult);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex);

                throw ex;
            }
        }

        #region OnConnected

        /// <summary>
        /// Triggers the Connected event.
        /// </summary>
        public virtual void OnConnected()
        {
            EventHandler handler = Connected;
            if (handler != null)
                handler(null/*this*/, new EventArgs());
        }

        #endregion OnConnected

        /// <summary>
        /// Close connection
        /// </summary>
        public void Close()
        {
            Client.Close();
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

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}