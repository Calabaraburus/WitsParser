#region copyright
// <copyright file="IWitsDataSerializer.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>
#endregion
namespace WitsParser.Wits
{
    /// <summary>
    /// Defines wits data serializer
    /// </summary>
    public interface IWitsDataSerializer
    {
        /// <summary>
        /// Serialize  <see cref="WitsSentence"/> collection to byte array
        /// </summary>
        /// <param name="sentenceColection">WITS sentence collection</param>
        /// <returns>Returns serialized WITS sentence collection</returns>
        byte[] SentenceCollectionToBytes(System.Collections.Generic.IEnumerable<WitsSentence> sentenceColection);

        /// <summary>
        /// Deserialize byte array to <see cref="WitsSentence"/>
        /// </summary>
        /// <param name="sentencesInBytes">WITS data byte array</param>
        /// <returns>returns collection of <see cref="WitsSentence"/></returns>
        System.Collections.Generic.IEnumerable<WitsSentence> BytesToSentenceCollection(byte[] sentencesInBytes);

        /// <summary>
        /// Deserialize string to <see cref="WitsSentence"/> collection
        /// </summary>
        /// <param name="witsData">WITS data string</param>
        /// <returns>Deserialized wits data of <see cref="WitsSentence"/> collection</returns>
        System.Collections.Generic.IEnumerable<WitsSentence> StringToSentenceCollection(string witsData);

        /// <summary>
        /// Convert byte array to <see cref="WitsSentence"/>
        /// </summary>
        /// <param name="sentenceInBytes">Wits sentence as byte array</param>
        /// <returns>Returns deserialized WITS sentence</returns>
        WitsSentence BytesToSentence(byte[] sentenceInBytes);

        /// <summary>
        /// Serialize <see cref="WitsSentence"/> to byte array
        /// </summary>
        /// <param name="sentence">Wits sentence  <see cref="WitsSentence"/></param>
        /// <returns>Returns byte array</returns>
        byte[] SentenceToBytes(WitsSentence sentence);
    }
}