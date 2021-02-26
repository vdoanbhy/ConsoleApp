using System;
using System.IO;
using System.Text;
using Catalyst;
using Catalyst.Models;
using Mosaik.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Version = Mosaik.Core.Version;
using P = Catalyst.PatternUnitPrototype;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    public static class LanguageDetection
    {
        public static async Task Detect()
        {

            int i = 1;
            string connectrionStrPar = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            //Console.WriteLine(File.Process(File.FetchData(connectrionStrPar, i)));

            // 1. Language Detection
            Console.OutputEncoding = Encoding.UTF8;

            //ApplicationLogging.SetLoggerFactory(LoggerFactory.Create(lb => lb.AddConsole()));
            Storage.Current = new OnlineRepositoryStorage(new DiskStorage("catalyst-models"));
            Console.WriteLine("Loading models... This might take a bit longer the first time you run this sample, as the models have to be downloaded from the online repository");
            var cld2LanguageDetector = await LanguageDetector.FromStoreAsync(Language.Any, Version.Latest, "");
            //var fastTextLanguageDetector = await FastTextLanguageDetector.FromStoreAsync(Language.Any, Version.Latest, "");
            //foreach (var (lang, text) in LanguageData.LongSamples)
            //{
            //    //var doc = new Document(File.Process(File.FetchData(connectrionStrPar, i)));
            //    //fastTextLanguageDetector.Process(doc);

            //    var doc2 = new Document(File.Process(File.FetchData(connectrionStrPar, i)));
            //    cld2LanguageDetector.Process(doc2);

            //    //Console.WriteLine(text);
            //    //Console.WriteLine($"Actual:\t{lang}\nFT:\t{doc.Language}\nCLD2\t{doc2.Language}");
            //    Console.WriteLine($"Actual:\t{lang}\t\nCLD2\t{doc2.Language}");
            //    Console.WriteLine();
            //}
            var doc2 = new Document(File.Process(File.FetchData(connectrionStrPar, i)));
            //var doc2 = new Document("Familie Müller plant ihren Urlaub. Sie geht in ein Reisebüro und lässt sich von einem Angestellten beraten.");
            cld2LanguageDetector.Process(doc2);
            Console.WriteLine($"\nCLD2\t{doc2.Language}");




            //// You can also access all predictions via the Predict method:
            //var allPredictions = fastTextLanguageDetector.Predict(new Document(LanguageData.LongSamples[Language.Spanish]));

            //Console.WriteLine($"\n\nTop 10 predictions and scores for the Spanish sample:");
            //foreach (var kv in allPredictions.OrderByDescending(kv => kv.Value).Take(10))
            //{
            //    Console.WriteLine($"{kv.Key.ToString().PadRight(40)}\tScore: {kv.Value:n2}");
            //}

        }
    }
}
