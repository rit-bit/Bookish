using System;
using System.Data;
using System.IO;
using Npgsql;

namespace Bookish.DatabaseInterfaces
{
    public static class DatabaseConnection
    {
        private static readonly string ConnectionString = File.ReadAllText(Path.Combine("res", "ConnectionString.txt"));

        public static NpgsqlConnection GetConnection()
        {
            var connection = new NpgsqlConnection(ConnectionString);
        
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            return connection;
        }
    }
}