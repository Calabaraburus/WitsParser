#region copyright
// <copyright file="WitsSentence.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>
#endregion
namespace WitsParser.Wits
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents collection of WITS records
    /// </summary>
    public class WitsSentence
    {
        public const string SentenceBegining = "&&";
        public const string SentenceEnding = "!!";
        private const int INT_MinWordsInSentence = 3;

        /// <summary>
        /// Records collection
        /// </summary>
        public IList<WitsRecord> Records { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WitsSentence()
        {
            Records = new List<WitsRecord>();
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return SentenceBegining + "\n" + string.Join("\n", Records) + "\n" + SentenceEnding;
        }

        /// <summary>
        /// Parse WITS sentence from string
        /// </summary>
        /// <param name="input">String data</param>
        /// <returns>Return parsed sentence <see cref="WitsSentence"/>/></returns>
        public static WitsSentence Parse(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input)) throw new Exception("input can't be null null");
                input.Trim();
                string[] strWords = input.Split('\n');
                if (strWords.Length < INT_MinWordsInSentence) throw new Exception("WITS sentence has no records");
                if (strWords.First().Trim() != SentenceBegining) throw new Exception(@"WITS sentence start symbols (&&) not found");
                if (strWords.Where((s) => s == SentenceEnding).FirstOrDefault() == null) throw new Exception(@"WITS sentence ending not found (!!)");

                var sentence = new WitsSentence();
                foreach (var strWord in strWords)
                {
                    if (strWord.StartsWith(SentenceBegining))
                    {
                        continue;
                    }

                    if (strWord.StartsWith(SentenceEnding))
                    {
                        break;
                    }

                    sentence.Records.Add(WitsRecord.Parse(strWord));
                }
                return sentence;
            }
            catch (Exception ex)
            {
                throw new ParseException("Error while parsing WITS sentence", ex);
            }
        }
    }
}