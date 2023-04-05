using System;
using System.Collections.Generic;
using System.Data;

namespace DatabaseToCsvUsingSRPAndISP
{
    public interface IDbConnector
    {
        IDbConnection GetConnection();
    }

    public interface IDbCommandExecutor
    {
        IDataReader ExecuteSqlCommand(string query);
    }

    public interface IDataReaderConverter
    {
        IEnumerable<List<string>> Convert(IDataReader dataReader);
    }

    public interface ICsvWriter
    {
        void WriteToCsvFile(IEnumerable<List<string>> data, string filePath);
    }

    public class ConnectionManager : IDbConnector
    {
        private readonly IDbConnection dbConnection;

        public ConnectionManager(IDbConnection connection)
        {
            dbConnection = connection ?? throw new ArgumentNullException("Connection was null");
        }

        public IDbConnection GetConnection()
        {
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }

            return dbConnection;
        }
    }
}
