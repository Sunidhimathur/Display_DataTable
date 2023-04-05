using System.Configuration;
using System.Data.Entity;

namespace CodeFirstApproach
{
    class MyDbContext : DbContext
    {
        public const string sqlDbConnectionString = "SQLServerDbConnectionEntityFramework";
        public static string sqlConnectionString = ConfigurationManager.ConnectionStrings[sqlDbConnectionString].ConnectionString;
        public MyDbContext() : base(sqlConnectionString) { }
        public virtual DbSet<Programme> Programmes { get; set; }
        public virtual DbSet<StudentAddress> StudentAddresses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
    }
}
