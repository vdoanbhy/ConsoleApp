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
        public static async Task Detect(string testString)
        {


            // 1. Language Detection

            Storage.Current = new OnlineRepositoryStorage(new DiskStorage("catalyst-models"));
            //Console.WriteLine("Loading models... This might take a bit longer the first time you run this sample, as the models have to be downloaded from the online repository");
            var cld2LanguageDetector = await LanguageDetector.FromStoreAsync(Language.Any, Version.Latest, "");
            //var fastTextLanguageDetector = await FastTextLanguageDetector.FromStoreAsync(Language.Any, Version.Latest, "");
            
            //var doc = new Document(testString);
            //fastTextLanguageDetector.Process(doc);

            var doc2 = new Document(testString);
            //var doc2 = new Document("Familie Müller plant ihren Urlaub. Sie geht in ein Reisebüro und lässt sich von einem Angestellten beraten.");
            cld2LanguageDetector.Process(doc2);
            Console.WriteLine($"CLD2\t{doc2.Language}");




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
