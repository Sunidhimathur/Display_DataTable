using MultiDbToCSV;
using System;
using System.Configuration;

namespace SQLServerDbToCsv
{
    public class Sql
    {
        public const string GetSqlQuery = "select * from employee";
        public const string SqlFilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DataFromSQLMySQLAzureToCSV\SqlExportedData.csv";

        private static readonly string SqlDbConnectionString = "SQLServerDbConnection";
        private static readonly string SqlconnectionString = ConfigurationManager.ConnectionStrings[SqlDbConnectionString].ConnectionString;

        public static void GetSqlDb(string[] args)
        {
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.ExecuteSqlCommand<string>(SqlconnectionString, SqlFilePath, GetSqlQuery);

            Console.WriteLine("Data exported to CSV file successfully.");
        }
        public class SqlToCSV
        {
            public static int SqlValue { get; set; }

            public SqlToCSV()
            {
                SqlValue = 2;
            }
        }
    }
}