using MySqlConnector;
using System.Data;

namespace DataFromTwoDatabases
{
    public class mysql
    {
        /*public static void Main()
        {
            GetCSV1();
        }*/

        public static string GetCSV1()
        {
            using (MySqlConnection con1 = new MySqlConnection(GetConnectionString1()))
            {
                con1.Open();
                return CreateCSV1(new MySqlCommand("select * from StudentTable", con1).ExecuteReader());
            }
        }

        private static string CreateCSV1(IDataReader reader)
        {
            string file1 = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DataFromTwoDatabases\ExportedData1.csv";
            List<string> lines1 = new List<string>();

            string headerLine1 = ""; //string.empty()
            if (reader.Read())
            {
                string[] columns = new string[reader.FieldCount];
                int columnsCount = columns.Length;
                for (int i = 0; i < columnsCount; i++)
                {
                    columns[i] = reader.GetName(i);
                }
                headerLine1 = string.Join(",", columns);
                lines1.Add(headerLine1);

                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                lines1.Add(string.Join(",", values));
            }
            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                lines1.Add(string.Join(",", values));
            }

            File.WriteAllLines(file1, lines1);
            //using(StreamWriter sw = new StreamWriter(file1)) { } , StreamBuilder
            return file1;
        }

        private static string GetConnectionString1()
        {
            return @"server=localhost;uid=root;pwd=Nidhi@2204;database=Student";
        }

        public class Class1
        {
            public int value { get; set; }

            public Class1()
            {
                value = 1;
            }
        }
    }
}