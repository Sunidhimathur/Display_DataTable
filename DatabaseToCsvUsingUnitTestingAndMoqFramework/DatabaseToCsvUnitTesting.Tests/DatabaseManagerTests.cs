using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data;
using System.Data.SqlClient;
using static DatabaseToCsvUsingInterfaceAndUnitTesting.DatabaseManager;

namespace DatabaseToCsvUsingInterfaceAndUnitTesting.Tests
{
    [TestClass]
    public class DatabaseManagerTests
    {
        // <-- Unit Testing: -->

        private Mock<IDbConnection> mockDbConnection;
        private Mock<IDbCommand> mockDbCommand;
        private Mock<IDataReader> mockDataReader;

        private ISqlCommandExecutor sqlCommandExecutor;

        [TestInitialize]
        public void Setup()
        {
            mockDbConnection = new Mock<IDbConnection>();
            mockDbCommand = new Mock<IDbCommand>();
            mockDataReader = new Mock<IDataReader>();

            sqlCommandExecutor = new SqlCommandExecutor(mockDbConnection.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullConnection_ThrowsException()
        {
            // Arrange
            IDbConnection connection = null;

            // Act
            var executor = new SqlCommandExecutor(connection);
        }

        [TestMethod]
        public void CreateSqlCommand_Should_Return_Command()
        {
            // Arrange
            string query = "SELECT * FROM products";
            var expectedSqlCommand = new SqlCommand(query, mockDbConnection.Object as SqlConnection);

            // Act
            var result = sqlCommandExecutor.CreateSqlCommand(query);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedSqlCommand.CommandText, result.CommandText);
            Assert.IsInstanceOfType(result, typeof(SqlCommand));
        }

        [TestMethod]
        public void ExecuteSqlCommand_ReturnsSqlDataReader()
        {
            // Arrange
            mockDbCommand.Setup(c => c.ExecuteReader()).Returns(mockDataReader.Object);

            // Act
            var result = sqlCommandExecutor.ExecuteSqlCommand(mockDbCommand.Object);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IDataReader));
        }

        [TestCleanup]
        public void Cleanup()
        {
            sqlCommandExecutor.Dispose();
        }
    }
}
