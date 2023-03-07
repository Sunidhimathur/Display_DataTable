using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Text;
using System.IO;

namespace DataFromDatabase
{
    class SelectStatement
    {
        public static void Main()
        {
            Read();
            Console.ReadKey();
        }
        static void Read()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("***********************");
            Console.WriteLine("* Student Data Table: *");
            Console.WriteLine("***********************");
            Console.ForegroundColor = ConsoleColor.Green;
            string constr = "server=localhost;uid=root;pwd=Nidhi@2204;database=Student";
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = constr;
            con.Open();
            string sql, output = "";
            sql = "select * from StudentTable";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataReader reader = cmd.ExecuteReader();

            ConsoleDisplayFormatter.PrintSeperatorLine();
            ConsoleDisplayFormatter.PrintRow("ID", "Name", "Address", "MobileNumber");
            ConsoleDisplayFormatter.PrintSeperatorLine();

            while (reader.Read())
            {
                ConsoleDisplayFormatter.PrintRow(output + reader.GetValue(0) + "                " + reader.GetValue(1) + "                " + reader.GetValue(2) + "            " + reader.GetValue(3) + "            " + "\n");
            }
            ConsoleDisplayFormatter.PrintSeperatorLine();
            Console.Write(output);
            reader.Close();
            cmd.Dispose();
            con.Close();
            Console.ForegroundColor = ConsoleColor.Blue;
        }
    }
}