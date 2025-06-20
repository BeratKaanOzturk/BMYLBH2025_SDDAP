using System.Data;

namespace BMYLBH2025_SDDAP.Models
{
    /// <summary>
    /// Interface for creating database connections
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Creates a new database connection
        /// </summary>
        /// <returns>A new database connection instance</returns>
        IDbConnection CreateConnection();
    }
} 