using System.Data;
using Npgsql;

namespace Bookish.DatabaseInterfaces
{
    public static class DatabaseConnection
    {
        // TODO Create config file for connection string
        private const string ConnectionString = "Server=localhost;Port=5432;Database=bookish;Username=bookish;Password=pw";

        public static NpgsqlConnection GetConnection() {
            var connection = new NpgsqlConnection(ConnectionString);
        
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            return connection;
        }
    }
}