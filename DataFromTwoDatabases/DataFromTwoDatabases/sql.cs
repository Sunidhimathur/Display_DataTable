using System.Data;
using System.Data.SqlClient;

namespace DataFromTwoDatabases
{
    public class sql
    {
        /*public static void Main()
        {
            GetCSV2();
        }*/

        public static string GetCSV2()
        {
            using (SqlConnection con2 = new SqlConnection(GetConnectionString2()))
            {
                con2.Open();
                return CreateCSV2(new SqlCommand("select * from employee", con2).ExecuteReader());
            }
        }

        private static string CreateCSV2(IDataReader reader)
        {
            string file2 = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DataFromTwoDatabases\ExportedData2.csv";
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

        private static string GetConnectionString2()
        {
            return @"Integrated Security=SSPI;Initial Catalog=master;Data Source=desktop-jbvptsp";
        }

        public class Class2
        {
            public int value { get; set; }

            public Class2()
            {
                value = 2;
            }
        }
    }
}
