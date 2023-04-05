using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using static DataTableWithCRUDoperations.DataWithCRUD;

namespace CrudOperations.Tests
{
    [TestClass]
    public class DataWithCRUDTests
    {
        private Mock<IDataWithCRUD> mockDataWithCRUD;

        [TestInitialize]
        public void TestInitialize()
        {
            mockDataWithCRUD = new Mock<IDataWithCRUD>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            mockDataWithCRUD.VerifyAll();
        }

        /*In each test method, the mockDataWithCRUD object is setup to expect a call 
        to the corresponding method, and then the method is called using 
        mockDataWithCRUD.Object. Finally, the Verify method is used to ensure that 
        the method was called exactly once.*/

        [TestMethod]
        public void CreateTable_ExecutesSuccessfully()
        {
            // Arrange
            mockDataWithCRUD.Setup(x => x.CreateTable());

            // Act
            mockDataWithCRUD.Object.CreateTable();

            // Assert
            mockDataWithCRUD.Verify(x => x.CreateTable(), Times.Once);
        }

        [TestMethod]
        public void ReadTable_ExecutesSuccessfully()
        {
            // Arrange
            mockDataWithCRUD.Setup(x => x.ReadTable());

            // Act
            mockDataWithCRUD.Object.ReadTable();

            // Assert
            mockDataWithCRUD.Verify(x => x.ReadTable(), Times.Once);
        }

        [TestMethod]
        public void UpdateTable_ExecutesSuccessfully()
        {
            // Arrange
            mockDataWithCRUD.Setup(x => x.UpdateTable());

            // Act
            mockDataWithCRUD.Object.UpdateTable();

            // Assert
            mockDataWithCRUD.Verify(x => x.UpdateTable(), Times.Once);
        }

        [TestMethod]
        public void DeleteTable_ExecutesSuccessfully()
        {
            // Arrange
            mockDataWithCRUD.Setup(x => x.DeleteTable());

            // Act
            mockDataWithCRUD.Object.DeleteTable();

            // Assert
            mockDataWithCRUD.Verify(x => x.DeleteTable(), Times.Once);
        }
    }
}
