using System.Data;
using System.Data.SQLite;

namespace BMYLBH2025_SDDAP.Models
{
    public class SqliteConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        public SqliteConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection CreateConnection()
        {
            var conn = new SQLiteConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}