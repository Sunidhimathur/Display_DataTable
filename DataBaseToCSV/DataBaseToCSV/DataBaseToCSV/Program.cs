using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace DataBaseToCSV
{
    public class program
    {
        public static void Main()
        {
            GetCSV1();
            GetCSV2();
        }

        public static string GetCSV1()
        {
            using (MySqlConnection con1 = new MySqlConnection(GetConnectionString1()))
            {
                con1.Open();
                return CreateCSV1(new MySqlCommand("select * from StudentTable", con1).ExecuteReader());
            }
        }

        public static string GetCSV2()
        {
            using (SqlConnection con2 = new SqlConnection(GetConnectionString2()))
            {
                con2.Open();
                return CreateCSV2(new SqlCommand("select * from employee", con2).ExecuteReader());
            }
        }

        private static string CreateCSV1(IDataReader reader)
        {
            string file1 = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DataBaseToCSV\ExportedData1.csv";
            List<string> lines1 = new List<string>();

            string headerLine1 = "";
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
            return file1;
        }

        private static string CreateCSV2(IDataReader reader)
        {
            string file2 = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DataBaseToCSV\ExportedData2.csv";
            List<string> lines2 = new List<string>();

            string headerLine2 = "";
            if (reader.Read())
            {
                string[] columns = new string[reader.FieldCount];
                int columnsCount = columns.Length;
                for (int i = 0; i < columnsCount; i++)
                {
                    columns[i] = reader.GetName(i);
                }
                headerLine2 = string.Join(",", columns);
                lines2.Add(headerLine2);

                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                lines2.Add(string.Join(",", values));
            }
            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                lines2.Add(string.Join(",", values));
            }

            File.WriteAllLines(file2, lines2);
            return file2;
        }

        private static string GetConnectionString1()
        {
            return @"server=localhost;uid=root;pwd=Nidhi@2204;database=Student";
        }
        private static string GetConnectionString2()
        {
            return @"Integrated Security=SSPI;Initial Catalog=master;Data Source=desktop-jbvptsp";
        }
    }
}
