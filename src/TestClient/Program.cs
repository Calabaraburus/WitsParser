using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WitsParser.Transport.Client;

namespace TestClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var terminate = false;
            var parser = new WitsParser.Wits.WitsDataSerializer();
            Task.Run(() =>
            {
                using (var client = new WitsClient("127.0.0.1", 6666))
                {
                    client.Connect();
                    using (var sReader = new StreamReader("WitsTest.txt"))
                    {
                        var fillSentence = false;
                        var sent = "";
                        while (!sReader.EndOfStream && !terminate)
                        {
                            var line = sReader.ReadLine();
                            Console.WriteLine(line);

                            if (fillSentence) sent += line + "\n";

                            if (line == "&&")
                            {
                                fillSentence = true;
                                sent = line + "\n";
                            }

                            if (line == "!!")
                            {
                                fillSentence = false;

                               // sent += line;
                                var bytes = Encoding.ASCII.GetBytes(sent);
                                client.SendWitsSentence(parser.BytesToSentence(bytes));
                                Thread.Sleep(200);
                            }


                        }
                    }
                }
            });

            Console.ReadKey();

            terminate = true;
        }
    }
}