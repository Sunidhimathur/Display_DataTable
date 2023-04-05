using System;
using System.Configuration;
using static DatabaseToCsvUsingInterfaceAndUnitTesting.DatabaseManager;

namespace SQLServerDbToCsv
{
    public class Sql
    {
        public const string getSqlQuery = "SELECT * FROM employee";
        public const string sqlFilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DatabaseToCsvUsingInterfaceAndUnitTesting\SqlExportedData.csv";
        public const string sqlDbConnectionString = "SQLServerDbConnection";
        public static string sqlConnectionString = ConfigurationManager.ConnectionStrings[sqlDbConnectionString].ConnectionString;

        public static void GetSqlDb()
        {
            using (SqlCommandExecutor executer = new SqlCommandExecutor(sqlConnectionString))
            {
                var sqlCommand = executer.CreateSqlCommand(getSqlQuery);
                var data = executer.ReadSqlDataReader(executer.ExecuteSqlCommand(sqlCommand));
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
