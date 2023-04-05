using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static MicrosoftAzureDatabaseToCSV.MsAzure;
using static SQLServerDbToCsv.Sql;

namespace DatabaseToCsvUsingInterface
{
    public class DatabaseManager
    {
        public class SqlCommandExecutor : IDisposable
        {
            private readonly SqlConnection sqlConnection;

            public SqlCommandExecutor(string connectionString)
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ArgumentNullException(nameof(connectionString), "Connection string was empty");
                }

                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
            }

            public IEnumerable<List<string>> ExecuteSqlCommand(string query)
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(sqlReader);

                        var data = new List<List<string>>();
                        var columnNames = dataTable.Columns.Cast<DataColumn>()
                            .Select(column => column.ColumnName)
                            .ToList();
                        data.Add(columnNames);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            var rowValues = row.ItemArray.Select(field => $"\"{field}\"").ToList();
                            data.Add(rowValues);
                        }

                        return data;
                    }
                }
            }

            public void WriteToCsvFile(IEnumerable<List<string>> data, string filePath)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var row in data)
                    {
                        var rowValues = row.Select(field => $"{field}");
                        var rowCsv = string.Join(",", rowValues);
                        writer.WriteLine(rowCsv);
                    }
                }
            }

            public void Dispose()
            {
                sqlConnection.Dispose();
            }
        }

        public class Program
        {
            public static void Main()
            {
                try
                {
                    SqlToCSV sqlToCSV = new SqlToCSV();
                    MsAzureToCSV msAzureToCSV = new MsAzureToCSV();

                    string value;
                    int result;

                    Console.WriteLine("Write a number 1 or 2: ");
                    value = Console.ReadLine();

                    // convert to integer
                    result = Convert.ToInt32(value);
                    switch (result)
                    {
                        case 1:
                            GetSqlDb();
                            break;
                        case 2:
                            GetMsAzureDb();
                            break;
                        default:
                            Console.WriteLine("Wrong Value is entered. Hence, No Data found!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    Console.WriteLine("Press enter to close...");
                    Console.ReadLine();
                }
            }
        }
    }
}
