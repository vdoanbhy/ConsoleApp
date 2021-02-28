using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ConsoleApp
{
    class NLPFile
    {
        public int id;
        public string fileName;
        public string status;
        public string score;

        public NLPFile(IDataRecord record)
        {

            id = (int)record[0];
            fileName = (string)record[1];
            status = record[2].ToString();
            score = record[2].ToString();

        }

        public string FetchData()
        {
            return NLPFile.Process(fileName);
        }


        public static string Process(string FileName)
        {

            string text = "";
            //bool Success = true;
            try
            {
                text = System.IO.File.ReadAllText(FileName);
                //System.Console.WriteLine("contents of writetext.txt = {0}", text);
            }
            catch (Exception e)
            {
                //Success = false;
                Console.WriteLine(e.Message);
            }
            return text;
        }
    }
}
