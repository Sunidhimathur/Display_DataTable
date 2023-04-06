using DatabaseToCsvUsingSRPAndISP;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace CodeFirstApproach
{
    class Subject
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public int Marks { get; set; }

        //navigation property
        public virtual ICollection<Programme> Programmes { get; set; }

        public const string getSqlQuery = "select * from dbo.Subjects";
        public const string sqlFilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DatabaseToCsvUsingSRPAndISP\SubjectsExportedData.csv";
        public const string sqlDbConnectionString = "SQLServerDbConnectionEntityFramework";
        public static string sqlConnectionString = ConfigurationManager.ConnectionStrings[sqlDbConnectionString].ConnectionString;

        public static void GetSubjectsTable()
        {
            var connection = new SqlConnection(sqlConnectionString);
            var connectionManager = new ConnectionManager(connection);
            var executor = new SqlCommandExecutor(connectionManager.GetConnection());
            var dataReader = executor.ExecuteSqlCommand(getSqlQuery);
            var data = new DataReaderConverter().Convert(dataReader);
            var csvWriter = new CsvWriter();
            csvWriter.WriteToCsvFile(data, sqlFilePath);

            Console.WriteLine("Subject Data exported to CSV file successfully.\n");
        }
    }
}