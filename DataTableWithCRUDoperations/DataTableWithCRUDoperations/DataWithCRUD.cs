using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace DataTableWithCRUDoperations
{
    public class DataWithCRUD
    {
        const string sqlDbConnectionString = "SQLServerDbConnection";
        readonly string connectionString = ConfigurationManager.ConnectionStrings[sqlDbConnectionString].ConnectionString;

        public interface IDataWithCRUD
        {
            void CreateTable();
            void ReadTable();
            void UpdateTable();
            void DeleteTable();
        }

        public void CreateTable()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string createTableQuery = "CREATE TABLE Users (Id INT PRIMARY KEY IDENTITY(1,1), Name VARCHAR(50), Age INT, Email VARCHAR(50))";

                    using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Table created successfully!");
                        Console.WriteLine("Table name is: Users");
                        Console.WriteLine("Please press enter to insert record...");
                        Console.ReadLine();
                    }

                    string insertQuery = "INSERT INTO Users (Name, Age, Email) VALUES ('John Doe', 30, 'johndoe@email.com')";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Data inserted successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Data insertion failed!");
                        }
                        Console.WriteLine("Please press enter to retrieve record...");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.ReadLine();
        }

        public void ReadTable()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM Users";
                    string FilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DataTableWithCRUDoperations\users.csv";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            using (StreamWriter writer = new StreamWriter(FilePath))
                            {
                                StringBuilder sb = new StringBuilder();

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
                            Console.WriteLine("Data written to users.csv");
                            Console.WriteLine("Please press enter to update record...");
                            Console.ReadLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.ReadLine();
        }

        public void UpdateTable()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = "UPDATE Users SET Age = 35 WHERE Name = 'John Doe'";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Data updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Data update failed!");
                        }
                        Console.WriteLine("Please press enter to retrieve updated record...");
                        Console.ReadLine();
                    }

                    string selectQuery = "SELECT * FROM Users";
                    string FilePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DataTableWithCRUDoperations\users.csv";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            using (StreamWriter writer = new StreamWriter(FilePath))
                            {
                                StringBuilder sb = new StringBuilder();

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
                            Console.WriteLine("Updated Data written to users.csv");
                            Console.WriteLine("Please press enter to delete record...");
                            Console.ReadLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.ReadLine();
        }

        public void DeleteTable()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM Users WHERE Name='John Doe'";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Data deleted successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Data deletion failed!");
                        }
                        Console.WriteLine("Please press enter to delete table...");
                        Console.ReadLine();
                    }

                    string dropTableQuery = "DROP TABLE Users";

                    using (SqlCommand command = new SqlCommand(dropTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Table deleted successfully!");
                        Console.WriteLine("Press enter to close...");
                    }

                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.ReadLine();
        }

        class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Email { get; set; }
        }  
    }
    class Program
    {
        static void Main()
        {
            DataWithCRUD dataWithCRUD = new DataWithCRUD();
            dataWithCRUD.CreateTable();
            dataWithCRUD.ReadTable();
            dataWithCRUD.UpdateTable();
            dataWithCRUD.DeleteTable();
        }
    }
}
