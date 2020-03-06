namespace TestServer
{
    using System;
    using System.ServiceModel;
    using WitsParser.Transport.Server;
    using WitsParser.Transport.Server.Wits;
    using WitsParser.Wits;

    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                using (var host = new WitsHost(6666, WitsLevel.Level0,
                    new WitsPostProcessorSimple[]
                    {
                        new WitsPostProcessorSimple((sentences)=>
                        {
                            foreach (var sentence in sentences)
                            {
                                Console.WriteLine(sentence);
                            }
                        })
                    }))
                {
                    host.Start();

                    Pause();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        public static void Pause()
        {
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}