using System;
using System.Data;
using System.Data.SqlClient;
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
            string connectionStr = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlCommand command1 = null;
            string sqlQuery = "SELECT * FROM TestApplication.dbo.Files where test = 0";
            string sqlQuery1 = "SELECT * FROM TestApplication.dbo.Files where test = 1";
            var nlp = Pipeline.For(Language.English);
            try
            {
                connection = new SqlConnection(connectionStr);
                command = new SqlCommand(sqlQuery, connection);
                command1 = new SqlCommand(sqlQuery1, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                System.Collections.Generic.List<Document> trainDocs = new System.Collections.Generic.List<Document>();
                
                System.Collections.Generic.List<Document> testDocs = new System.Collections.Generic.List<Document>();
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("/[^/]*/");
                while (reader.Read())
                {
                    NLPFile flTrain = new NLPFile((IDataRecord)reader);
                    //Console.WriteLine(flTrain.FetchData());
                    var doc = new Document(flTrain.FetchData(),Language.English);
                    string label = reg.Matches(((string)reader[1]))[2].Value;
                    label = label.Trim('/');
                    doc.Labels.Add(label) ;
                    trainDocs.Add(doc); 
                }
                reader.Close();

                using (var lda = new LDA(Language.English, 0, "reuters-lda"))
                {
                    lda.Data.NumberOfTopics = 20; //Arbitrary number of topics
                    lda.Train(trainDocs, Environment.ProcessorCount);
                    await lda.StoreAsync();
                }

                SqlDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    NLPFile flTest = new NLPFile((IDataRecord)reader1);
                    //Console.WriteLine(flTest.FetchData());
                    testDocs.Add(new Document(flTest.FetchData()));
                    
                }
                reader1.Close();

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
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                if (command != null || command1 != null)
                {
                    command.Dispose();
                    command1.Dispose();
                }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
    }
}
