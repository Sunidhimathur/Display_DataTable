using DatabaseToCsvUsingSRPAndISP;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.SqlClient;

namespace CodeFirstApproach
{
    class Student
    {
        [Key]
        public int Rollno { get; set; }

        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsBonafied { get; set; }
        public int ContactNo { get; set; }

        //[ForeignKey("Programme")]
        public int ProgrammeId { get; set; }

        //navigation property
        [ForeignKey("ProgrammeId")]
        public virtual Programme Programme { get; set; }
        public virtual StudentAddress StudentAddress { get; set; }

        public const string getSqlQuery = "select * from dbo.Students";
        public const string sqlFilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DatabaseToCsvUsingSRPAndISP\StudentsExportedData.csv";
        public const string sqlDbConnectionString = "SQLServerDbConnectionEntityFramework";
        public static string sqlConnectionString = ConfigurationManager.ConnectionStrings[sqlDbConnectionString].ConnectionString;

        public static void GetStudentsTable()
        {
            var connection = new SqlConnection(sqlConnectionString);
            var connectionManager = new ConnectionManager(connection);
            var executor = new SqlCommandExecutor(connectionManager.GetConnection());
            var dataReader = executor.ExecuteSqlCommand(getSqlQuery);
            var data = new DataReaderConverter().Convert(dataReader);
            var csvWriter = new CsvWriter();
            csvWriter.WriteToCsvFile(data, sqlFilePath);

            Console.WriteLine("Student Data exported to CSV file successfully.");
        }
    }
}

