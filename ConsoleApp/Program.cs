using System;
using System.IO;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //int i = 1;
            //string connectrionStrPar = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            //Console.WriteLine(File.Process(File.FetchData(connectrionStrPar, i))); 
            _ = LanguageDetection.Detect();
            //EntityRecognition.Spotter();

        }

    }
}
