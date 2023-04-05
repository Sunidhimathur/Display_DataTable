using System;
using System.Configuration;
using System.Data.SqlClient;
using static DatabaseToCsvUsingInterfaceAndUnitTesting.DatabaseManager;

namespace MicrosoftAzureDatabaseToCSV
{
    public class MsAzure
    {
        public const string getMsAzureQuery = "SELECT * FROM dbo.Customers";
        public const string msAzureFilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DatabaseToCsvUsingUnitTestingAndMoqFramework\MsAzureExportedData.csv";
        public const string msAzureDbConnectionString = "AzureDataBaseToCSV";
        public static string msAzureConnectionString = ConfigurationManager.ConnectionStrings[msAzureDbConnectionString].ConnectionString;

        public static void GetMsAzureDb()
        {
            using (SqlCommandExecutor executer = new SqlCommandExecutor(new SqlConnection(msAzureConnectionString)))
            {
                var sqlCommand = executer.CreateSqlCommand(getMsAzureQuery);
                var data = executer.ReadSqlDataReader(executer.ExecuteSqlCommand(sqlCommand));
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