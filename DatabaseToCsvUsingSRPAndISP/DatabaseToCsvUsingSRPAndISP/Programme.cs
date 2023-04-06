using DatabaseToCsvUsingSRPAndISP;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.SqlClient;

namespace CodeFirstApproach
{
    [Table("Course")]
    class Programme
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; } //PK

        [Required]
        [Column("CourseName", TypeName = "varchar")]
        [MaxLength(50)]
        public string Title { get; set; }

        public int Duration { get; set; }
        public float Fees { get; set; }

        //navigation property
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }

        public const string getSqlQuery = "select * from dbo.Course";
        public const string sqlFilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DatabaseToCsvUsingSRPAndISP\CourseExportedData.csv";
        public const string sqlDbConnectionString = "SQLServerDbConnectionEntityFramework";
        public static string sqlConnectionString = ConfigurationManager.ConnectionStrings[sqlDbConnectionString].ConnectionString;

        public static void GetCourseTable()
        {
            var connection = new SqlConnection(sqlConnectionString);
            var connectionManager = new ConnectionManager(connection);
            var executor = new SqlCommandExecutor(connectionManager.GetConnection());
            var dataReader = executor.ExecuteSqlCommand(getSqlQuery);
            var data = new DataReaderConverter().Convert(dataReader);
            var csvWriter = new CsvWriter();
            csvWriter.WriteToCsvFile(data, sqlFilePath);

            Console.WriteLine("Course Data exported to CSV file successfully.");
        }
    }
}
