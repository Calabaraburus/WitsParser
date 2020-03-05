#region copyright

// <copyright file="ISimpleWorker.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>

#endregion copyright

namespace WitsParser.Transport.Server
{
    using System;
    using System.Collections.Generic;
    using WitsParser.Wits;

    /// <summary>
    /// Post processor with convertor
    /// </summary>
    public class WitsPostProcessorSimple : IPostProcessing
    {
        private readonly Action<IEnumerable<WitsSentence>> _action;
        private WitsDataSerializer _serializer = new WitsDataSerializer();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action">Action for external sentence interpretation</param>
        public WitsPostProcessorSimple(Action<IEnumerable<WitsSentence>> action)
        {
            this._action = action;
        }

        /// <summary>
        /// Porcess data
        /// </summary>
        /// <param name="data">Data</param>
        void IPostProcessing.ProcessData(byte[] data)
        {
            var sentences = _serializer.BytesToSentenceCollection(data);
            _action?.Invoke(sentences);
        }
    }
}