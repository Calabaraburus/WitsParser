
using NUnit.Framework;

namespace NUnit.DataTests
{
    using WitsParser.Wits;
    using wh = WitsHelpers;
    [TestFixture]
    public class WitsTests
    {
        [Test]
        public void SerializeWitsSentenceMethod()
        {
            var sent = wh.CreateDefaultWitsSentence();
            Assert.AreEqual(wh.WitsString, sent.ToString());
        }

        [Test]
        public void DeserializeWitsSentenceMethod()
        {
            var sentBase = wh.CreateDefaultWitsSentence();
            var sent = WitsSentence.Parse(wh.WitsString);
            Assert.AreEqual(sentBase.Records, sent.Records);
        }

        [Test]
        public void SerializeWitsWordMethod()
        {
            var word = wh.CreateWitsWord();
            Assert.AreEqual(wh.WordString, word.ToString());
        }

        [Test]
        public void DeserializeWitsWordMethod()
        {
            var wordBase = wh.CreateWitsWord();
            Assert.AreEqual(wordBase.GroupId, wh.WordGroupId);
            Assert.AreEqual(wordBase.Id, wh.WordId);
            Assert.AreEqual(wordBase.Data, wh.WordData);
        }
    }
}