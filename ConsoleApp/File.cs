using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ConsoleApp
{
    class File
    {
        public int id;
        public string fileName;
        public string status;
        public string score;

        public static string FetchData(string connectionStrPar, int count)
        {
            string readInFileName = "";
            string connectionStr = connectionStrPar;
            SqlConnection connection = new SqlConnection(connectionStr);
            string sqlQuery = "SELECT * FROM TestApplication.dbo.Files WHERE id = @count;";
            try{
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@count", count);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) {
                    readInFileName = ReadSingleRow((IDataRecord)reader);
                }
                reader.Close();
            }
            catch (Exception e){
                Console.WriteLine("Error: " + e.Message);
            }
            return readInFileName;
        }
        private static string ReadSingleRow(IDataRecord record)
        {
            var file = new File();
            file.id = (int)record[0];
            file.fileName = (string)record[1];
            file.status = record[2].ToString();
            file.score = record[2].ToString();
            return file.fileName;
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
