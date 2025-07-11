﻿using System;
using System.Data;
using System.Data.SQLite;

namespace BMYLBH2025_SDDAP.Models
{
    /// <summary>
    /// SQLite implementation of IDbConnectionFactory
    /// </summary>
    public class SqliteConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqliteConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        /// <summary>
        /// Creates a new SQLite database connection
        /// </summary>
        /// <returns>A new SQLite connection instance</returns>
        public IDbConnection CreateConnection()
        {
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            
            // Enable foreign key constraints for this connection
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "PRAGMA foreign_keys = ON;";
                cmd.ExecuteNonQuery();
            }
            
            connection.Close();
            return connection;
        }
    }
}