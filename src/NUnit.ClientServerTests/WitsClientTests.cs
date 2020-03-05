using WitsParser.Transport.Client;

namespace NUnit.ClientServerTests
{
    using NUnit.DataTests;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading;
    using WitsParser.Transport.Server;
    using WitsParser.Transport.Server.Wits;
    using WitsParser.Wits;

    [TestFixture]
    public class WitsClientTests
    {
        [Test]
        public void SendWitsSentence()
        {
            var baseSent = WitsHelpers.CreateDefaultWitsSentence();
            WitsSentence sentenceResult = null;

            using (var host = new WitsHost(6666, WitsLevel.Level0,
                new WitsPostProcessorSimple[]
                {
                    new WitsPostProcessorSimple((sentences)=>
                    {
                        sentenceResult=sentences.First();
                    })
                }))
            {
                host.Start();

                using (var client = new WitsClient("127.0.0.1", 6666))
                {
                    client.Connect();
                    client.SendWitsSentence(baseSent);
                    Thread.Sleep(2000);
                }
            }

            Assert.AreEqual(baseSent.ToString(), sentenceResult.ToString());
        }
    }
}