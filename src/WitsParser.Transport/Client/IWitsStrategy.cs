#region copyright
// <copyright file="IWitsStrategy.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>
#endregion
namespace WitsParser.Transport.Client
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines startegy to exchange data through WITS protocol
    /// </summary>
    public interface IWitsStrategy
    {
        /// <summary>
        /// Send WITS message
        /// </summary>
        /// <param name="sentence">WITS sentence <see cref="WitsSentence"/></param>
        void SendWitsSentence(WitsParser.Wits.WitsSentence sentence);

        /// <summary>
        /// Send WITS message collection
        /// </summary>
        /// <param name="sentences">WITS sentence collection <see cref="WitsSentence"/></param>
        void SendWitsSentenceCollection(IEnumerable<WitsParser.Wits.WitsSentence> sentences);
    }
}