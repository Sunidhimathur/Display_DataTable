using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DatabaseToCsvUsingSRPAndISP;
using System.Configuration;
using System.Data.SqlClient;
using System;

namespace CodeFirstApproach
{
    class StudentAddress
    {
        [Key, ForeignKey("Student")]
        public int StudentId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [NotMapped]
        public string Pincode { get; set; }

        //navigation property
        public virtual Student Student { get; set; }

        public const string getSqlQuery = "select * from dbo.StudentAddresses";
        public const string sqlFilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DatabaseToCsvUsingSRPAndISP\StudentAddressesExportedData.csv";
        public const string sqlDbConnectionString = "SQLServerDbConnectionEntityFramework";
        public static string sqlConnectionString = ConfigurationManager.ConnectionStrings[sqlDbConnectionString].ConnectionString;

        public static void GetStudentAddressesTable()
        {
            var connection = new SqlConnection(sqlConnectionString);
            var connectionManager = new ConnectionManager(connection);
            var executor = new SqlCommandExecutor(connectionManager.GetConnection());
            var dataReader = executor.ExecuteSqlCommand(getSqlQuery);
            var data = new DataReaderConverter().Convert(dataReader);
            var csvWriter = new CsvWriter();
            csvWriter.WriteToCsvFile(data, sqlFilePath);

            Console.WriteLine("Student Address Data exported to CSV file successfully.");
        }
    }
}
