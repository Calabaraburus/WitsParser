#region copyright
// <copyright file="WitsRecord.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>
#endregion
namespace WitsParser.Wits
{
    using System;

    /// <summary>
    /// Represents wits record
    /// </summary>
    public struct WitsRecord
    {
        private const int GroupIdStartIndex = 0;
        private const int IdStartIndex = 2;
        private const int GroupIdLength = 2;
        private const int IdLength = 2;
        private const int DataStartIndex = GroupIdLength + IdLength;

        /// <summary>
        /// Group id
        /// </summary>
        public string GroupId { get; private set; }

        /// <summary>
        /// Record id
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Record data
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// Parse WITS record
        /// </summary>
        /// <param name="input">input data string</param>
        /// <returns>Returns deserialized WITS record</returns>
        public static WitsRecord Parse(string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentNullException("input can't be null or empty");

            input.Trim();
            if (input.Length < DataStartIndex) throw new ParseException("Wits record length is less then identificator length");

            try
            {
                WitsRecord word = new WitsRecord()
                {
                    GroupId = input.Substring(GroupIdStartIndex, GroupIdLength),
                    Id = input.Substring(IdStartIndex, IdLength),
                    Data = input.Substring(DataStartIndex, input.Length - DataStartIndex)
                };

                return word;
            }
            catch (Exception ex)
            {
                throw new ParseException("WITS parse", ex);
            }
        }

        /// <summary>Returns the fully qualified type name of this instance.</summary>
        /// <returns>A <see cref="System.String" /> containing a fully qualified type name.</returns>
        public override string ToString()
        {
            return GroupId + Id + Data;
        }
    }
}