using Catalyst;
using Catalyst.Models;
using Mosaik.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Version = Mosaik.Core.Version;
using P = Catalyst.PatternUnitPrototype;
using System.Text;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    class EntityRecognition
    {
        Spotter spotter1, spotter2;
       

        public static async Task PatternSpotter(string testString)
        {
            
            Storage.Current = new OnlineRepositoryStorage(new DiskStorage("catalyst-models"));

            //Console.WriteLine("Loading models... This might take a bit longer the first time you run this sample, as the models have to be downloaded from the online repository");
            var nlp = await Pipeline.ForAsync(Language.English);
            nlp.Add(await AveragePerceptronEntityRecognizer.FromStoreAsync(language: Language.English, version: Version.Latest, tag: "WikiNER"));
            var isApattern = new PatternSpotter(Language.English, 0, tag: "is-a-pattern", captureTag: "IsA");
            isApattern.NewPattern(
                "Is+Noun",
                mp => mp.Add(
                    new PatternUnit(P.Single().WithToken("is").WithPOS(PartOfSpeech.VERB)),
                    new PatternUnit(P.Multiple().WithPOS(PartOfSpeech.NOUN, PartOfSpeech.PROPN, PartOfSpeech.AUX, PartOfSpeech.DET, PartOfSpeech.ADJ))
            ));
            nlp.Add(isApattern);

            var doc = new Document(testString, Language.English);

            nlp.ProcessSingle(doc);
           
            PrintDocumentEntities(doc);

        }

        public Pipeline Initializer() {
            spotter1 = new Spotter(Language.Any, 0, "Addresses of Atheist Organizations", "Country");
            spotter2 = new Spotter(Language.Any, 0, "Resource Type", "Resources");
            spotter1.Data.IgnoreCase = true; //In some cases, it might be better to set it to false, and only add upper/lower-case exceptions as required
            spotter2.Data.IgnoreCase = true;

            spotter1.AddEntry("USA");
            spotter1.AddEntry("United Kingdom");
            spotter1.AddEntry("Germany"); //entries can have more than one word, and will be automatically tokenized on whitespace
            spotter2.AddEntry("Books -- Fiction");
            spotter2.AddEntry("Books -- Non-fiction");
            spotter2.AddEntry("Net Resources");
            var nlp = Pipeline.TokenizerFor(Language.English);

            nlp.Add(spotter1); //When adding a spotter model, the model propagates any exceptions on tokenization to the pipeline's tokenizer
            nlp.Add(spotter2);

            return nlp;
        }



        public void Spotter(string testString)
        {
            ////Another way to perform entity recognition is to use a gazeteer-like model. For example, here is one for capturing a set of programing languages


            var doc = new Document(testString, Language.English);

            var a = Initializer();

            a.ProcessSingle(doc);

            PrintDocumentEntities(doc);
        }
        private static void PrintDocumentEntities(IDocument doc)
        {
            //Console.WriteLine($"Input text:\n\t'{doc.Value}'\n\nTokenized Value:\n\t'{doc.TokenizedValue(mergeEntities: true)}'\n\nEntities: \n{string.Join("\n", doc.SelectMany(span => span.GetEntities()).Select(e => $"\t{e.Value} [{e.EntityType.Type}]"))}");
            Console.WriteLine($"Entities: \n{string.Join("\n", doc.SelectMany(span => span.GetEntities()).Select(e => $"\t{e.Value} [{e.EntityType.Type}]"))}");
        }
    }
}
