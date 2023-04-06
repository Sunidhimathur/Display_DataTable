using System;
using static DatabaseToCsvUsingSRPAndISP.MsAzure;
using static DatabaseToCsvUsingSRPAndISP.Sql;
using static DatabaseToCsvUsingSRPAndISP.MySql;
using CodeFirstApproach;
using static CodeFirstApproach.Programme;
using static CodeFirstApproach.Subject;
using static CodeFirstApproach.Student;
using static CodeFirstApproach.StudentAddress;

namespace DatabaseToCsvUsingSRPAndISP
{
    public class UserInterface
    {
        public static void Main()
        {
            try
            {
                SqlToCSV sqlToCSV = new SqlToCSV();
                MsAzureToCSV msAzureToCSV = new MsAzureToCSV();
                MySqlToCSV mySqlToCSV = new MySqlToCSV();

                string value;
                int result;

                Console.WriteLine("Following are the Multiple Databases:");
                Console.WriteLine("1: SQL Database\n" + "2: Azure Database\n" + "3: MySQL Database");
                Console.WriteLine("Enter a number to extract the data from specified database: ");
                value = Console.ReadLine();

                // convert to integer
                result = Convert.ToInt32(value);
                switch (result)
                {
                    case 1:
                        GetSqlDb();
                        break;
                    case 2:
                        GetMsAzureDb();
                        break;
                    case 3:
                        GetMySqlDb();
                        break;
                    default:
                        Console.WriteLine("Wrong Value is entered. Hence, No Data found!");
                        break;
                }

                Console.WriteLine("Please press enter to implement Code First Approach for Entity Framework to create the database!");
                Console.ReadLine();

                using (MyDbContext db = new MyDbContext())
                {
                    Programme p = new Programme
                    {
                        Id = 100,
                        Title = "B.Tech",
                        Duration = 4,
                        Fees = 500000
                    };
                    db.Programmes.Add(p);
                    db.SaveChanges();
                    Console.WriteLine("Database created!\n");

                    Console.WriteLine("Following are the Tables in Database:");
                    Console.WriteLine("1: Course\n" + "2: StudentAddresses\n" + "3: Students\n" + "4: Subjects\n");
                    Console.WriteLine("Press enter to convert data from Tables into CSV file: ");
                    Console.ReadLine();

                    GetCourseTable();
                    GetStudentAddressesTable();
                    GetStudentsTable();
                    GetSubjectsTable();
                    db.Database.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Press enter to close...");
                Console.ReadLine();
            }
        }
    }
}
