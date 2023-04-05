using MySqlConnector;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using static MicrosoftAzureDatabaseToCSV.MsAzure;
using static MySQLDbToCsv.MySql;
using static SQLServerDbToCsv.Sql;

namespace MultiDbToCSV
{
    public class DatabaseManager
    {
        public const string GetMySqlQuery = "select * from StudentTable";
        public const string MySqlFilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DataFromSQLMySQLAzureToCSV\MySqlExportedData.csv";

        // Read the connection string from the app.config file
        private static readonly string MySqlDbConnectionString = "MySQLDbConnection";
        private readonly string MySqlconnectionString = ConfigurationManager.ConnectionStrings[MySqlDbConnectionString].ConnectionString;

        public void ExecuteMySqlCommand()
        {
            try
            {
                // Open a connection to the SQL Server database
                using (MySqlConnection connection = new MySqlConnection(MySqlconnectionString))
                {
                    connection.Open();

                    // Execute a SQL query to retrieve data from the database
                    using (MySqlCommand command = new MySqlCommand(GetMySqlQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Write the data to a CSV file
                            using (StreamWriter writer = new StreamWriter(MySqlFilePath))
                            {
                                // Write the data to the file uisng a StringBuilder
                                StringBuilder sb = new StringBuilder();

                                // Write the column names to the file
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    writer.Write(reader.GetName(i));
                                    if (i < reader.FieldCount - 1)
                                    {
                                        writer.Write(",");
                                    }
                                }
                                writer.WriteLine();

                                // Write the data to the file
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        writer.Write("\"" + reader[i].ToString() + "\"");
                                        if (i < reader.FieldCount - 1)
                                        {
                                            writer.Write(",");
                                        }
                                    }
                                    writer.WriteLine();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }


        public void ExecuteSqlCommand<Execute>(string connectionString, string path, string query)
        {
            if (connectionString == null || connectionString == string.Empty)
            {
                throw new ArgumentNullException("ConnectionString", "Connection string was empty");
            }
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(sqlReader);

                            // Write the data to the file
                            using (StreamWriter sqlWriter = new StreamWriter(path))
                            {
                                WriteDataTableToCsv(dataTable, sqlWriter);
                            }
                            sqlReader.Close();
                            sqlConnection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            List<string> WriteDataTableToCsv(DataTable dataTable, StreamWriter writer)
            {
                var csvList = new List<string>();

                var columnNames = dataTable.Columns.Cast<DataColumn>()
                    .Select(column => column.ColumnName)
                    .ToArray();

                csvList.Add(string.Join(",", columnNames));
                writer.WriteLine(string.Join(",", columnNames));

                //var rows = dataTable.AsEnumerable()
                var rows = dataTable.Rows.Cast<DataRow>()
                    .Select((row) =>
                        string.Join(",", row.ItemArray.Select(field => $"\"{field}\"")));

                foreach (var row in rows)
                {
                    csvList.Add(row);
                    writer.WriteLine(row);
                }
                return csvList;
            }
        }
    }

            // <-- If not returning list -->

            /*void WriteDataTableToCsv(DataTable dataTable, StreamWriter writer)
            {
                var columnNames = dataTable.Columns.Cast<DataColumn>()
                    .Select(column => column.ColumnName)
                    .ToArray();

                writer.WriteLine(string.Join(",", columnNames));

                var rows = dataTable.AsEnumerable()
                    .Select(row => string.Join(",", row.ItemArray.Select(field => $"\"{field}\"")));

                foreach (var row in rows)
                {
                    writer.WriteLine(row);
                }
            }*/


    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                MySqlToCSV mySqlToCSV = new MySqlToCSV();
                SqlToCSV sqlToCSV = new SqlToCSV();
                MsAzureToCSV msAzureToCSV = new MsAzureToCSV();

                string value;
                int result;

                Console.WriteLine("Write a number 1, 2 or 3: ");
                value = Console.ReadLine();

                // convert to integer
                result = Convert.ToInt32(value);
                switch (result)
                {
                    case 1:
                        GetMySqlDb(args);
                        break;
                    case 2:
                        GetSqlDb(args);
                        break;
                    case 3:
                        GetMsAzureDb(args);
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
