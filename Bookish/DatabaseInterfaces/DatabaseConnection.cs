using System.Data;
using Npgsql;

namespace Bookish.DatabaseInterfaces
{
    public class DatabaseConnection
    {
        private const string ConnectionString = "Server=akita.zoo.lan;Port=5432;Database=bookish;Username=bookish;Password=pw";

        public IDbConnection db
        {
            get
            {
                using IDbConnection connection = new NpgsqlConnection(ConnectionString);
            
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                return connection;
            }
        }
    }
}