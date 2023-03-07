using System;
using System.Configuration;
using static DatabaseToCsvUsingInterface.DatabaseManager;

namespace MicrosoftAzureDatabaseToCSV
{
    public class MsAzure
    {
        public const string getMsAzureQuery = "SELECT * FROM dbo.Customers";
        public const string msAzureFilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DatabaseToCsvUsingInterface\MsAzureExportedData.csv";
        public const string msAzureDbConnectionString = "AzureDataBaseToCSV";
        public static string msAzureconnectionString = ConfigurationManager.ConnectionStrings[msAzureDbConnectionString].ConnectionString;

        public static void GetMsAzureDb()
        {
            using (SqlCommandExecutor executer = new SqlCommandExecutor(msAzureconnectionString))
            {
                var data = executer.ExecuteSqlCommand(getMsAzureQuery);
                executer.WriteToCsvFile(data, msAzureFilePath);
            }
            Console.WriteLine("Data exported to CSV file successfully.");
        }

        public class MsAzureToCSV
        {
            public static int MsAzureValue { get; set; }

            public MsAzureToCSV()
            {
                MsAzureValue = 2;
            }
        }
    }
}