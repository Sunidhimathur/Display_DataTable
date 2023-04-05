using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DatabaseToCsvUsingSRPAndISP
{
    public class Sql
    {
        public const string getSqlQuery = "SELECT * FROM employee";
        public const string sqlFilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DatabaseToCsvUsingSRPAndISP\SqlExportedData.csv";
        public const string sqlDbConnectionString = "SQLServerDbConnection";
        public static string sqlConnectionString = ConfigurationManager.ConnectionStrings[sqlDbConnectionString].ConnectionString;

        public static void GetSqlDb()
        {
            var connection = new SqlConnection(sqlConnectionString);
            var connectionManager = new ConnectionManager(connection);
            var executor = new SqlCommandExecutor(connectionManager.GetConnection());
            var dataReader = executor.ExecuteSqlCommand(getSqlQuery);
            var data = new DataReaderConverter().Convert(dataReader);
            var csvWriter = new CsvWriter();
            csvWriter.WriteToCsvFile(data, sqlFilePath);

            Console.WriteLine("Data exported to CSV file successfully.");
        }

        public class SqlToCSV
        {
            public static int SqlValue { get; set; }

            public SqlToCSV()
            {
                SqlValue = 1;
            }
        }
    }
}
