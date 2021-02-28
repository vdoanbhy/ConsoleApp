using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalyst;
using Catalyst.Models;
using Mosaik.Core;


namespace ConsoleApp
{
    class LatentDirichletAllocation
    {
        public static async Task LDAllocation()
        {
            Storage.Current = new OnlineRepositoryStorage(new DiskStorage("catalyst-models"));
            var (train, test) = await Corpus.Reuters.GetAsync();
            var nlp = Pipeline.For(Language.English);
            var trainDocs = nlp.Process(train).ToArray();
            var testDocs = nlp.Process(test).ToArray();
            using (var lda = new LDA(Language.English, 0, "reuters-lda"))
            {
                lda.Data.NumberOfTopics = 20; //Arbitrary number of topics
                lda.Train(trainDocs, Environment.ProcessorCount);
                await lda.StoreAsync();
            }

            using (var lda = await LDA.FromStoreAsync(Language.English, 0, "reuters-lda"))
            {
                foreach (var doc in testDocs)
                {
                    if (lda.TryPredict(doc, out var topics))
                    {
                        var docTopics = string.Join("\n", topics.Select(t => lda.TryDescribeTopic(t.TopicID, out var td) ? $"[{t.Score:n3}] => {td.ToString()}" : ""));

                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine(doc.Value);
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine(docTopics);
                        Console.WriteLine("------------------------------------------\n\n");
                    }
                }
            }
        }
    }
}
