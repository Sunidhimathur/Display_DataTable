using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DatabaseToCsvUsingSRPAndISP
{
    public class MySql
    {
        public const string getMySqlQuery = "select * from StudentTable";
        public const string mySqlFilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DatabaseToCsvUsingSRPAndISP\MySqlExportedData.csv";
        private readonly static string mySqlDbConnectionString = "MySQLDbConnection";
        private static readonly string mySqlconnectionString = ConfigurationManager.ConnectionStrings[mySqlDbConnectionString].ConnectionString;

        public static void GetMySqlDb()
        {
            var connection = new MySqlConnection(mySqlconnectionString);
            var connectionManager = new ConnectionManager(connection);
            var executor = new SqlCommandExecutor(connectionManager.GetConnection());
            var dataReader = executor.ExecuteSqlCommand(getMySqlQuery);
            var data = new DataReaderConverter().Convert(dataReader);
            var csvWriter = new CsvWriter();
            csvWriter.WriteToCsvFile(data, mySqlFilePath);

            Console.WriteLine("Data exported to CSV file successfully.");
        }

        public class MySqlToCSV
        {
            public int MySqlValue { get; set; }

            public MySqlToCSV()
            {
                MySqlValue = 3;
            }
        }
    }
}
