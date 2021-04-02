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
            await LatentDirichletAllocation.LDAllocation();// 00:02:51.14
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ////await LatentDirichletAllocation.LDAllocation();
            //string connectionStr = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            //SqlConnection connection = null;
            //SqlCommand command = null;
            //string sqlQuery = "SELECT * FROM TestApplication.dbo.Files Where id = 1";
            //try
            //{

            //    //EntityRecognition entityRecognition = new EntityRecognition();
            //    //entityRecognition.Initializer();
            //    connection = new SqlConnection(connectionStr);
            //    command = new SqlCommand(sqlQuery, connection);
            //    connection.Open();
            //    SqlDataReader reader = command.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        NLPFile fl = new NLPFile((IDataRecord)reader);
            //        Console.WriteLine(fl.id);
            //        MSTextRecognizer.MSRecognizer(fl.FetchData());
            //        //await EntityRecognition.PatternSpotter(fl.FetchData()); // 05:12:53.89 - 00:00:02.27 for the 1st file
            //        //entityRecognition.Spotter(fl.FetchData());  // 00:00:53.84 - 00:00:01.91 for the first file - estimate 4hour
            //        //await LanguageDetection.Detect(fl.FetchData()); // 00:10:50.14
            //    }
            //    reader.Close();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Error: " + e.Message);
            //}
            //finally
            //{
            //    if (command != null)
            //    {
            //        command.Dispose();
            //    }
            //    if (connection != null)
            //    {
            //        connection.Close();
            //        connection.Dispose();
            //    }
            //}

            //await TextClassification.TxtClassification();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
            Console.WriteLine("\nRunTime " + elapsedTime);






        }
    }
}

