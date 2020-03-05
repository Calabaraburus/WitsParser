namespace NUnit.DataTests
{
    using NUnit.Framework;
    using WitsParser.Wits;

    public static class WitsHelpers
    {
        public const string WitsString = "&&\n01021000\n01031000\n!!";
        public const string WordId = "12";
        public const string WordGroupId = "10";
        public const string WordData = "1231231";
        public const string WordString = WordGroupId + WordId + WordData;

        public static WitsSentence CreateDefaultWitsSentence()
        {
            var sent = new WitsSentence();
            sent.Records.Add(WitsRecord.Parse("01021000"));
            sent.Records.Add(WitsRecord.Parse("01031000"));
            return sent;
        }

        public static WitsRecord CreateWitsWord()
        {
            return WitsRecord.Parse(WordString);
        }
    }
}
