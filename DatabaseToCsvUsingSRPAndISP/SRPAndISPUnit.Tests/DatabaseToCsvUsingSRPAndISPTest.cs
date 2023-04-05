using DatabaseToCsvUsingSRPAndISP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;

namespace SRPAndISPUnit.Tests
{
    [TestClass]
    public class DatabaseToCsvUsingSRPAndISPTest
    {
        // <-- Unit Testing: -->

        private Mock<IDbConnection> mockConnection;
        private ConnectionManager connectionManager;
        private Mock<IDataReader> mockDataReader;
        private DataReaderConverter dataReaderConverter;
       
        [TestInitialize]
        public void Initialize()
        {
            // Initialize the mock IDbConnection object
            mockConnection = new Mock<IDbConnection>();

            // Set up the mock object to return an open connection
            mockConnection.Setup(c => c.State).Returns(ConnectionState.Open);

            connectionManager = new ConnectionManager(mockConnection.Object);

            mockDataReader = new Mock<IDataReader>();
            dataReaderConverter = new DataReaderConverter();
        }

        [TestMethod]
        public void GetConnection_StateIsOpen_ReturnsConnection()
        {
            // Arrange
            // Nothing to arrange since the mock object is already set up in Initialize()

            // Act
            IDbConnection connection = connectionManager.GetConnection();

            // Assert
            Assert.AreEqual(mockConnection.Object, connection);
            mockConnection.Verify(c => c.Open(), Times.Never);
        }

        [TestMethod]
        public void ExecuteSqlCommand_WithQuery_ReturnsDataReader()
        {
            // Arrange
            var expectedReader = new Mock<IDataReader>();
            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.Setup(c => c.CreateCommand()).Returns(new Mock<IDbCommand>().Object);
            var sqlCommandExecutor = new SqlCommandExecutor(dbConnectionMock.Object);

            // Mock the ExecuteReader method of the IDbCommand object returned by IDbConnection.CreateCommand()
            var dbCommandMock = new Mock<IDbCommand>();
            dbCommandMock.Setup(c => c.ExecuteReader()).Returns(expectedReader.Object);
            dbConnectionMock.Setup(c => c.CreateCommand()).Returns(dbCommandMock.Object);

            // Act
            var actualReader = sqlCommandExecutor.ExecuteSqlCommand("SELECT * FROM Products");

            // Assert
            Assert.AreEqual(expectedReader.Object, actualReader);
        }

        [TestMethod]
        public void Convert_ReturnsCorrectData()
        {
            // Arrange
            mockDataReader.Setup(m => m.Read());

            // Act
            var result = dataReaderConverter.Convert(mockDataReader.Object);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}