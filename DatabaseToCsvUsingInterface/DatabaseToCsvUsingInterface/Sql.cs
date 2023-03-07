using System;
using System.Configuration;
using static DatabaseToCsvUsingInterface.DatabaseManager;

namespace SQLServerDbToCsv
{
    public class Sql
    {
        public const string getSqlQuery = "SELECT * FROM Employee";
        public const string sqlFilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DatabaseToCsvUsingInterface\SqlExportedData.csv";
        public const string sqlDbConnectionString = "SQLServerDbConnection";
        public static string sqlConnectionString = ConfigurationManager.ConnectionStrings[sqlDbConnectionString].ConnectionString;

        public static void GetSqlDb()
        {
            using (SqlCommandExecutor executer = new SqlCommandExecutor(sqlConnectionString))
            {
                var data = executer.ExecuteSqlCommand(getSqlQuery);
                executer.WriteToCsvFile(data, sqlFilePath);
            }
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