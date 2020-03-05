#region copyright
// <copyright file="WitsLevel0Strategy.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>
#endregion
namespace WitsParser.Transport.Client.WitsStrategies
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using WitsParser.Wits;

    public class WitsLevel0Strategy : IWitsStrategy
    {
        private const int waitToReconnectTime = 3000;
        private readonly IClient _Sender;
        private ManualResetEvent _connectWaitHandlle = new ManualResetEvent(false);
        private object _SynchObj = new object();

        public bool IsWaitForConnection { get; private set; }

        public WitsLevel0Strategy(IClient sender)
        {
            _Sender = sender;
        }

        /// <summary>
        /// Send WITS message
        /// </summary>
        /// <param name="sentence">WITS sentence <see cref="WitsSentence"/></param>
        public void SendWitsSentence(WitsSentence sentence)
        {
            SendWitsSentenceCollection(new List<WitsSentence>() { sentence });
        }

        /// <summary>
        /// Send WITS message collection
        /// </summary>
        /// <param name="sentences">WITS sentence collection <see cref="WitsSentence"/></param>
        public void SendWitsSentenceCollection(System.Collections.Generic.IEnumerable<WitsSentence> sentences)
        {
            var serializer = new WitsDataSerializer();
            var data = serializer.SentenceCollectionToBytes(sentences);

            if (!_Sender.IsConnected)
            {
                IsWaitForConnection = true;

                _Sender.Connect();
                IsWaitForConnection = false;
            }

            _Sender.BeginSend(data, SendCallback, _Sender);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                IClient client = (IClient)ar.AsyncState;

                int bytesSent = client.EndSend(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}