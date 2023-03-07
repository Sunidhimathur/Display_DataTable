using MultiDbToCSV;
using System;
using System.Configuration;

namespace MicrosoftAzureDatabaseToCSV
{
    public class MsAzure
    {
        public const string GetMsAzureQuery = "SELECT * FROM dbo.Customers";
        public const string MsAzureFilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DataFromSQLMySQLAzureToCSV\MsAzureExportedData.csv";

        private static readonly string MsAzureDbConnectionString = "AzureDataBaseToCSV";
        private static readonly string MsAzureconnectionString = ConfigurationManager.ConnectionStrings[MsAzureDbConnectionString].ConnectionString;

        public static void GetMsAzureDb(string[] args)
        {
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.ExecuteSqlCommand<string>(MsAzureconnectionString, MsAzureFilePath, GetMsAzureQuery);

            Console.WriteLine("Data exported to CSV file successfully.");
        }
        public class MsAzureToCSV
        {
            public static int MsAzureValue { get; set; }

            public MsAzureToCSV()
            {
                MsAzureValue = 3;
            }
        }
    }
}