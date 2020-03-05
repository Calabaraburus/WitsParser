#region copyright

// <copyright file="IWitsClient.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>

#endregion copyright

namespace WitsParser.Transport.Client
{
    using System.Collections.Generic;
    using WitsParser.Wits;

    /// <summary>
    /// Defines Wits client
    /// </summary>
    public interface IWitsClient
    {
        /// <summary>
        /// Send WITS sentence collection
        /// </summary>
        /// <param name="sentences">WITS sentence</param>
        void SendWitsSentenceCollection(IEnumerable<WitsSentence> sentences);

        /// <summary>
        /// Send Wits sentence
        /// </summary>
        /// <param name="sentence">Wits sentence</param>
        void SendWitsSentence(WitsSentence sentence);

        /// <summary>
        /// Connect to WITS server
        /// </summary>
        void Connect();

        /// <summary>
        /// Close client
        /// </summary>
        void Close();
    }
}