using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static DatabaseToCsvUsingInterfaceAndUnitTesting.DatabaseManager;

namespace DatabaseToCsvUnitTesting.Tests
{
    [TestClass]
    public class DatabaseManagerTests
    {
        const string query = "SELECT * FROM employee";

        [TestMethod]
        public void SqlCommandExecutor_ValidConnectionString()
        {
            // Arrange
            string connectionString = "Integrated Security=SSPI;Initial Catalog=master;Data Source=desktop-jbvptsp";

            // Act
            using (var executor = new SqlCommandExecutor(connectionString))
            {
                // Assert
                Assert.IsNotNull(executor);
            }
        }

        [TestMethod]
        public void ExecuteSqlCommand_ValidQuery_ReturnData()
        {
            // Arrange
            string connectionString = "Integrated Security=SSPI;Initial Catalog=master;Data Source=desktop-jbvptsp";
            var expectedData = new List<List<string>>()
            {
                new List<string> { "employee_ID", "name", "location" , "designation" },
                new List<string> { "1", "John", "Germany" , "Project Manager" },
                new List<string> { "2", "Jane", "America", "Assistant Developer" },
                new List<string> { "3", "Mary", "India", "Software Tester"}
            };

            // Act
            using (var executor = new SqlCommandExecutor(connectionString))
            {
                var actualData = executor.ExecuteSqlCommand(query).ToList();

                // Assert
                Assert.AreEqual(expectedData.Count, actualData.Count);
                for (int i = 0; i < expectedData.Count; i++)
                {
                    Assert.AreEqual(expectedData[i].Count, actualData[i].Count);

                    for (int j = 0; j < expectedData.Count; j++)
                    {
                        Assert.AreEqual(expectedData[i].Count, actualData[i].Count);
                    }
                }
            }
        }

        [TestMethod]
        public void WriteToCsvFile_WritesFileSuccessfully()
        {
            // Arrange
            string connectionString = "Integrated Security=SSPI;Initial Catalog=master;Data Source=desktop-jbvptsp";
            var data = new List<List<string>>
            {
                new List<string> { "employee_ID", "name", "location" , "designation" },
                new List<string> { "1", "John", "Germany" , "Project Manager" },
                new List<string> { "2", "Jane", "America", "Assistant Developer" },
                new List<string> { "3", "Mary", "India", "Software Tester"}
            };
            var filePath = @"C:\Users\670285104\Desktop\Practice\Display DataTable\DatabaseToCsvUsingInterfaceAndUnitTesting\SqlExportedDataTest.csv";

            // Act
            using (var executor = new SqlCommandExecutor(connectionString))
            {
                executor.WriteToCsvFile(data, filePath);
            }

            // Assert
            Assert.IsTrue(File.Exists(filePath));
        }
        
        [TestMethod]
        public void CreateSqlCommand_Should_Return_Command()
        {
            // Arrange
            string connectionString = "Integrated Security=SSPI;Initial Catalog=master;Data Source=desktop-jbvptsp";
            string query = "SELECT * FROM employee";
            var executor = new SqlCommandExecutor(connectionString);
            var sqlCommandExecutor = new SqlCommandExecutor(connectionString);

            // Act
            var result = executor.CreateSqlCommand(query);
            var sqlCommand = sqlCommandExecutor.CreateSqlCommand(query);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(query, result.CommandText);
            Assert.AreEqual(connectionString, result.Connection.ConnectionString);
            Assert.IsInstanceOfType(sqlCommand, typeof(SqlCommand));
        }

        [TestMethod]
        public void ExecuteSqlCommand_ReturnsSqlDataReader()
        {
            // Arrange
            string connectionString = "Integrated Security=SSPI;Initial Catalog=master;Data Source=desktop-jbvptsp";
            string query = "SELECT * FROM employee";
            var sqlCommandExecutor = new SqlCommandExecutor(connectionString);
            var sqlCommand = sqlCommandExecutor.CreateSqlCommand(query);

            // Act
            var sqlDataReader = sqlCommandExecutor.ExecuteSqlCommand(sqlCommand);
            var result = sqlCommandExecutor.ReadSqlDataReader(sqlDataReader);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<List<string>>));
        }
    }
}
