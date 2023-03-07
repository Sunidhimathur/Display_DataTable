using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace MicrosoftAzureDatabaseToCSV
{
    public class Program
    {
        const string GetQuery = "SELECT * FROM dbo.Customers";
        static void Main(string[] args)
        {
            try
            {
                // Read the connection string from the app.config file
                string connectionString = ConfigurationManager.ConnectionStrings["AzureDataBaseToCSV"].ConnectionString;

                // Open a connection to the Azure database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Execute a SQL query to retrieve data from the database
                    using (SqlCommand command = new SqlCommand(GetQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Write the data to a CSV file

                            using (StreamWriter writer = new StreamWriter("C:\\Users\\670285104\\Desktop\\Practice\\Display DataTable\\MicrosoftAzureDatabaseToCSV\\ExportedData.csv"))
                            {
                                // Write the data to the file uisng a StringBuilder
                                //StringBuilder sb = new StringBuilder();
                                /*string str1 = "string1";
                                string str2 = "string2";
                                str1 = str1+ str2;
                                string str4 = str1.Concat(str2).ToString();
                                StringBuilder sb = new StringBuilder();
                                sb.Append(str1);
                                sb.AppendLine(str2);
                                sb.ToString();*/

                                // Write the column names to the file
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    writer.Write(reader.GetName(i));
                                    if (i < reader.FieldCount - 1)
                                    {
                                        writer.Write(",");
                                    }
                                }
                                writer.WriteLine();

                                // Write the data to the file
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        writer.Write("\"" + reader[i].ToString() + "\"");
                                        if (i < reader.FieldCount - 1)
                                        {
                                            writer.Write(",");
                                        }
                                    }
                                    writer.WriteLine();
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("Data exported to CSV file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred!" + ex.Message);
            }
            finally
            {
                Console.WriteLine("Press enter to close...");
                Console.ReadLine();
            }
        }
    }
}