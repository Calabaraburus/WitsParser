#region copyright
// <copyright file="WitsDataSerializer.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>
#endregion
namespace WitsParser.Wits
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// WITS serializer. Serilize\deserialize wits data from\to ASCII string or byte array
    /// </summary>
    public class WitsDataSerializer : IWitsDataSerializer
    {
        private Encoding _encoding = new ASCIIEncoding();

        /// <summary>
        /// Serialize <see cref="WitsSentence"/> to byte array
        /// </summary>
        /// <param name="sentence">Wits sentence  <see cref="WitsSentence"/></param>
        /// <returns>Returns byte array</returns>
        public byte[] SentenceToBytes(WitsSentence sentence)
        {
            return _encoding.GetBytes(sentence.ToString());
        }

        /// <summary>
        /// Convert byte array to <see cref="WitsSentence"/>
        /// </summary>
        /// <param name="sentenceInBytes">Wits sentence as byte array</param>
        /// <returns>Returns deserialized WITS sentence</returns>
        public WitsSentence BytesToSentence(byte[] sentenceInBytes)
        {
            return WitsSentence.Parse(_encoding.GetString(sentenceInBytes));
        }

        /// <summary>
        /// Serialize  <see cref="WitsSentence"/> collection to byte array
        /// </summary>
        /// <param name="sentenceColection">WITS sentence collection</param>
        /// <returns>Returns serialized WITS sentence collection</returns>
        public byte[] SentenceCollectionToBytes(IEnumerable<WitsSentence> sentenceColection)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in sentenceColection)
            {
                sb.Append(item.ToString());
            }

            return _encoding.GetBytes(sb.ToString());
        }

        /// <summary>
        /// Deserialize byte array to <see cref="WitsSentence"/>
        /// </summary>
        /// <param name="sentencesInBytes">WITS data byte array</param>
        /// <returns>returns collection of <see cref="WitsSentence"/></returns>
        public IEnumerable<WitsSentence> BytesToSentenceCollection(byte[] sentencesInBytes)
        { return StringToSentenceCollection(_encoding.GetString(sentencesInBytes)); }

        /// <summary>
        /// Deserialize string to <see cref="WitsSentence"/> collection
        /// </summary>
        /// <param name="witsData">WITS data string</param>
        /// <returns>Deserialized wits data of <see cref="WitsSentence"/> collection</returns>
        public IEnumerable<WitsSentence> StringToSentenceCollection(string witsData)
        {
            List<WitsSentence> wSent = new List<WitsSentence>();

            int startInd = 0;
            int stopInd = 0;
            while (true)
            {
                startInd = witsData.IndexOf("&&", stopInd);
                if (startInd < 0)
                    break;
                stopInd = witsData.IndexOf("!!", startInd);
                if (stopInd < 0)
                    break;
                wSent.Add(WitsSentence.Parse(witsData.Substring(startInd, stopInd + 2 - startInd)));
            }

            return wSent;
        }
    }
}