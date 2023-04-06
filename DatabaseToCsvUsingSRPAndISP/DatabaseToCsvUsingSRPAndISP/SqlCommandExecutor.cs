using System;
using System.Data;

namespace DatabaseToCsvUsingSRPAndISP
{
    public class SqlCommandExecutor : IDbCommandExecutor
    {
        private readonly IDbConnection dbConnection;

        public SqlCommandExecutor(IDbConnection connection)
        {
            dbConnection = connection ?? throw new ArgumentNullException("Connection was null");
        }

        public IDataReader ExecuteSqlCommand(string query)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = query;
                return command.ExecuteReader();
            }
        }
    }
}