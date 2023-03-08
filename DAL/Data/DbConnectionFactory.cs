#nullable disable

using System.Data;
using Npgsql;

namespace DAL.Data
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        
        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Open()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
