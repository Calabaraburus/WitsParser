#region copyright
// <copyright file="WitsClient.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>
#endregion
namespace WitsParser.Transport.Client
{
    using NLog;
    using System;
    using System.Collections.Generic;
    using WitsParser.Wits;

    /// <summary>
    /// WITS TCP client implementation
    /// </summary>
    public class WitsClient : IWitsClient, IDisposable
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly WitsLevel _Level;

        private readonly IClient _client;
        private readonly IWitsStrategy _witsStrategy;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tcpConnectionString">Connection string</param>
        /// <param name="port">Port</param>
        public WitsClient(string tcpConnectionString, int port) :
            this(new TcpIpClient(tcpConnectionString, port), WitsLevel.Level0)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tcpConnectionString">Connection string</param>
        /// <param name="port">Port</param>
        /// <param name="level">Wits level</param>
        public WitsClient(string tcpConnectionString, int port, WitsLevel level) :
            this(new TcpIpClient(tcpConnectionString, port), level)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Client</param>
        /// <param name="level">Wits level type</param>
        public WitsClient(IClient client, WitsLevel level)
        {
            _Level = level;

            _client = client;
            var witsFactory = new WitsStrategyFactory();
            _witsStrategy = witsFactory.GetStartegy(level, _client);
        }

        /// <summary>
        /// Send Wits sentence
        /// </summary>
        /// <param name="sentence">Wits sentence</param>
        public void SendWitsSentence(WitsSentence sentence)
        {
            try
            {
                _witsStrategy.SendWitsSentence(sentence);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Ошибка ассинхронного подключения");
            }
        }

        /// <summary>
        /// Send WITS sentence collection
        /// </summary>
        /// <param name="sentences">WITS sentence</param>
        public void SendWitsSentenceCollection(IEnumerable<WitsSentence> sentences)
        {
            try
            {
                _witsStrategy.SendWitsSentenceCollection(sentences);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Ошибка ассинхронного подключения");
            }
        }

        /// <summary>
        /// Connect to WITS server
        /// </summary>
        public void Connect()
        {
            _client.Connect();
        }

        /// <summary>
        /// Close client
        /// </summary>
        public void Close()
        {
            _client.Close();
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