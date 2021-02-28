using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        //static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            string connectionStr = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            SqlConnection connection = new SqlConnection(connectionStr);
            string sqlQuery = "SELECT * FROM TestApplication.dbo.Files";
            try
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    NLPFile fl = new NLPFile((IDataRecord)reader);
                    Console.WriteLine(fl.id);
                    //await EntityRecognition.PatternSpotter(fl.FetchData()); // 04:12:53.89
                    //EntityRecognition.Spotter(fl.FetchData());  // 00:00:53.84
                    //_ = LanguageDetection.Detect(fl.FetchData()); // 00:11:09.16
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            connection.Close();

            //await LatentDirichletAllocation.LDAllocation();// 00:02:51.14

            await TextClassification.TxtClassification();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
            Console.WriteLine("\nRunTime " + elapsedTime);






        }
    }
}

