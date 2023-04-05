using MultiDbToCSV;
using System;

namespace MySQLDbToCsv
{
    public class MySql
    {
        public static void GetMySqlDb(string[] args)
        {
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.ExecuteMySqlCommand();

            Console.WriteLine("Data exported to CSV file successfully.");
        }

        public class MySqlToCSV
        {
            public int MySqlValue { get; set; }

            public MySqlToCSV()
            {
                MySqlValue = 1;
            }
        }
    }
}
