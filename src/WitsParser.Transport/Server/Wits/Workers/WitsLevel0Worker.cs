#region copyright

// <copyright file="WitsLevel0Worker.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>

#endregion copyright

namespace WitsParser.Transport.Server.Wits.Workers
{
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Wits worker
    /// </summary>
    public class WitsLevel0Worker : ISimpleWorker
    {
        private static Logger _Logger = LogManager.GetCurrentClassLogger();
        private Task _longTask;
        private CancellationTokenSource _cancelTokenSource;
        readonly IEnumerable<IPostProcessing> _postproc;
        const int BufferLength = 8192;

        public WitsLevel0Worker(IEnumerable<IPostProcessing> postproc = null)
        {
            this._postproc = postproc;
        }

        /// <summary>
        /// Start worker
        /// </summary>
        /// <param name="client">Tcp client</param>
        public void Start(TcpClient client)
        {
            _Logger.Info("Start worker " + nameof(WitsLevel0Worker));

            _cancelTokenSource = new CancellationTokenSource();
            var cancelToken = _cancelTokenSource.Token;

            _longTask = new Task(() =>
              {
                  // Get a stream object for reading and writing
                  var stream = client.GetStream();

                  int localCount;
                  Byte[] bytes = new Byte[BufferLength];

                  // Loop to receive all the data sent by the client.
                  while ((localCount = stream.Read(bytes, 0, bytes.Length)) != 0)
                  {
                      if (cancelToken.IsCancellationRequested) break;

                      // Translate data bytes to a ASCII string.
                      //                      data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                      var data = new byte[localCount];

                      Array.Copy(bytes, 0, data, 0, localCount);

                      if (_postproc != null)
                      {
                          foreach (var proc in _postproc)
                          {
                              if (proc != null)
                              {
                                  proc.ProcessData(data);
                              }
                          }
                      }
                  }
              }, TaskCreationOptions.LongRunning);

            _longTask.Start();
        }

        /// <summary>
        /// Stop worker
        /// </summary>
        public void Stop()
        {
            if (_cancelTokenSource != null) _cancelTokenSource.Cancel();
        }
    }
}