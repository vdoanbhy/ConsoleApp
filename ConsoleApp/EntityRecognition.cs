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
 
        public static void Spotter()
        {
            int i = 1;
            string connectrionStrPar = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

            //Another way to perform entity recognition is to use a gazeteer-like model. For example, here is one for capturing a set of programing languages
            var spotter = new Spotter(Language.Any, 0, "Addresses of Atheist Organizations", "Country");
            var spotter2 = new Spotter(Language.Any, 0, "Resource Type", "Resources");
            spotter.Data.IgnoreCase = true; //In some cases, it might be better to set it to false, and only add upper/lower-case exceptions as required
            spotter2.Data.IgnoreCase = true;

            spotter.AddEntry("USA");
            spotter.AddEntry("United Kingdom");
            spotter.AddEntry("Germany"); //entries can have more than one word, and will be automatically tokenized on whitespace
            spotter2.AddEntry("Books -- Fiction");
            spotter2.AddEntry("Books -- Non-fiction");
            spotter2.AddEntry("Net Resources");

            var nlp = Pipeline.TokenizerFor(Language.English);
            nlp.Add(spotter); //When adding a spotter model, the model propagates any exceptions on tokenization to the pipeline's tokenizer
            nlp.Add(spotter2);
            
            var docAboutProgramming = new Document(File.Process(File.FetchData(connectrionStrPar, i)), Language.English);

            nlp.ProcessSingle(docAboutProgramming);
            
            PrintDocumentEntities(docAboutProgramming);
        }
        private static void PrintDocumentEntities(IDocument doc)
        {
            Console.WriteLine($"Input text:\n\t'{doc.Value}'\n\nTokenized Value:\n\t'{doc.TokenizedValue(mergeEntities: true)}'\n\nEntities: \n{string.Join("\n", doc.SelectMany(span => span.GetEntities()).Select(e => $"\t{e.Value} [{e.EntityType.Type}]"))}");
        }
    }
}
