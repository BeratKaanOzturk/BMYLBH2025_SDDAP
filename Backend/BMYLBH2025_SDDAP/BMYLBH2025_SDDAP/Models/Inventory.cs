using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMYLBH2025_SDDAP.Models
{
    public class Inventory
    {
        public int InventoryID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
    }
    public interface IInventoryRepository : IBaseRepository<Inventory>
    {
        // Additional methods specific to Inventory can be defined here
    }
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public InventoryRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public IEnumerable<Inventory> GetAll()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                return con.Query<Inventory>("SELECT * FROM Inventory").ToList();
            }
        }
        public Inventory GetById(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                return con.QueryFirstOrDefault<Inventory>($"SELECT * FROM Inventory WHERE id={id}");
            }
        }
        public void Add(Inventory entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                con.Execute("INSERT INTO Inventory () VALUES ()");
            }
        }
        public void Update(Inventory entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                con.Execute($"UPDATE Inventory SET   WHERE id = {entity.InventoryID}");
            }
        }
        public void Delete(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                con.Execute($"DELETE FROM Inventory WHERE id = {id}");
            }
        }
    }
}