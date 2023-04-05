using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using static SQLServerDbToCsv.Sql;
using static MicrosoftAzureDatabaseToCSV.MsAzure;

namespace DatabaseToCsvUsingInterfaceAndUnitTesting
{
    public class DatabaseManager
    {
        public interface ISqlCommandExecutor : IDisposable
        {
            IDbCommand CreateSqlCommand(string query);
            IDataReader ExecuteSqlCommand(IDbCommand dbCommand);
            IEnumerable<List<string>> ReadSqlDataReader(IDataReader dataReader);
            void WriteToCsvFile(IEnumerable<List<string>> data, string filePath);
        }

        public class SqlCommandExecutor : ISqlCommandExecutor
        {
            public IDbConnection dbConnection;

            public SqlCommandExecutor(IDbConnection connection)
            {
                dbConnection = connection ?? throw new ArgumentNullException("Connection was null");
                dbConnection.Open();
            }

            public IDbCommand CreateSqlCommand(string query)
            {
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                }

                return new SqlCommand(query, dbConnection as SqlConnection);
            }

            public IDataReader ExecuteSqlCommand(IDbCommand dbCommand)
            {
                dbCommand.Connection = dbConnection;
                return dbCommand.ExecuteReader();
            }

            public IEnumerable<List<string>> ReadSqlDataReader(IDataReader dataReader)
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);

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

            public void WriteToCsvFile(IEnumerable<List<string>> data, string filePath)
            {
                using (var streamWriter = new StreamWriter(filePath))
                {
                    foreach (var row in data)
                    {
                        var line = string.Join(",", row);
                        streamWriter.WriteLine(line);
                    }
                }
            }

            public void Dispose()
            {
                dbConnection?.Close();
                dbConnection?.Dispose();
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