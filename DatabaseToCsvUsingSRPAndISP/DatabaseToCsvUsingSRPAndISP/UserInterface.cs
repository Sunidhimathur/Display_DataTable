using System;
using static DatabaseToCsvUsingSRPAndISP.MsAzure;
using static DatabaseToCsvUsingSRPAndISP.Sql;
using static DatabaseToCsvUsingSRPAndISP.MySql;
using CodeFirstApproach;

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

                Console.WriteLine("Following are the Multiple databases:");
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
                MyDbContext db = new MyDbContext();
                Programme p = new Programme();
                p.Id = 100;
                p.Title = "B.Tech";
                p.Duration = 4;
                p.Fees = 500000;
                db.Programmes.Add(p);
                db.SaveChanges();
                Console.WriteLine("Database created!");
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
