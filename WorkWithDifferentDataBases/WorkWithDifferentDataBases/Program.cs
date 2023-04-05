using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Database_Operation
{
    class SelectStatement
    {
        static void Main()
        {
            Read();
            Console.ReadKey();
        }

        static void Read()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Connection with MySql Database:\n");
            string constr1;
            MySqlConnection conn1;
            constr1 = @"server=localhost;uid=root;pwd=Nidhi@2204;database=Student";

            conn1 = new MySqlConnection(constr1);
            conn1.Open();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Connection Open!\n");

            MySqlCommand cmd1;
            MySqlDataReader dreader1;

            string sql1, output1 = "";

            sql1 = "select * from StudentTable";

            cmd1 = new MySqlCommand(sql1, conn1);

            dreader1 = cmd1.ExecuteReader();

            while (dreader1.Read())
            {
                output1 = output1 + dreader1.GetValue(0) + " - " +
                                    dreader1.GetValue(1) + " , " + dreader1.GetValue(2) + " , " + dreader1.GetValue(3) + "\n";
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(output1);

            Console.ForegroundColor = ConsoleColor.Blue;
            dreader1.Close();
            cmd1.Dispose();
            conn1.Close();



            Console.WriteLine("\n");



            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Connection with Sql Database:\n");
            string constr2 = @"Integrated Security=SSPI;Initial Catalog=master;Data Source=desktop-jbvptsp";
            using(SqlConnection conn2 = new SqlConnection(constr2))
            {
                string sql2 = "select * from employee";
                conn2.Open();
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Connection Open!\n");

                    
                    string output2 = "";

                    using (SqlCommand cmd2 = new SqlCommand(sql2, conn2))
                    {
                        SqlDataReader reader = cmd2.ExecuteReader();

                        while (reader.Read())
                        {
                            output2 = output2 + reader.GetValue(0) + " - " +
                                                reader.GetValue(1) + " , " + reader.GetValue(2) + " , " + reader.GetValue(3) + "\n";
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(output2);

                        reader.Close();
                        cmd2.Dispose();
                        conn2.Close();
                    }
                }
                 catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.ToString());
                }

                Console.ForegroundColor = ConsoleColor.Blue;

            }
        }
    }
}