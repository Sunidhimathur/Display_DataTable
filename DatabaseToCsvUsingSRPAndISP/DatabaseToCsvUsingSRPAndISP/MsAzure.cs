using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DatabaseToCsvUsingSRPAndISP
{
    public class MsAzure
    {
        public const string getMsAzureQuery = "SELECT * FROM dbo.Customers";
        public const string msAzureFilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DatabaseToCsvUsingSRPAndISP\MsAzureExportedData.csv";
        public const string msAzureDbConnectionString = "AzureDataBaseToCSV";
        public static string msAzureConnectionString = ConfigurationManager.ConnectionStrings[msAzureDbConnectionString].ConnectionString;

        public static void GetMsAzureDb()
        {
            var connection = new SqlConnection(msAzureConnectionString);
            var connectionManager = new ConnectionManager(connection);
            var executor = new SqlCommandExecutor(connectionManager.GetConnection());
            var dataReader = executor.ExecuteSqlCommand(getMsAzureQuery);
            var data = new DataReaderConverter().Convert(dataReader);
            var csvWriter = new CsvWriter();
            csvWriter.WriteToCsvFile(data, msAzureFilePath);

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
