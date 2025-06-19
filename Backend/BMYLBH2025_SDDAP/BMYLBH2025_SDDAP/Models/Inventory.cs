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
        
        // Navigation Properties
        public virtual Product Product { get; set; }
        
        // Business Methods
        public bool IsLowStock()
        {
            return Product != null && Quantity <= Product.MinimumStockLevel;
        }
        
        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity < 0)
                throw new ArgumentException("Quantity cannot be negative");
                
            Quantity = newQuantity;
            LastUpdated = DateTime.Now;
        }
        
        public decimal CalculateStockValue()
        {
            return Product != null ? Quantity * Product.Price : 0;
        }
    }
    
    public interface IInventoryRepository : IBaseRepository<Inventory>
    {
        // Business-specific methods
        Inventory GetByProductId(int productId);
        IEnumerable<Inventory> GetLowStockItems();
        IEnumerable<Inventory> GetByCategory(int categoryId);
        void UpdateStock(int productId, int quantity);
        decimal GetTotalInventoryValue();
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
                const string sql = @"
                    SELECT i.*, p.Name, p.Description, p.Price, p.MinimumStockLevel, p.CategoryID
                    FROM Inventory i
                    INNER JOIN Products p ON i.ProductID = p.ProductID
                    ORDER BY i.LastUpdated DESC";
                    
                return con.Query<Inventory, Product, Inventory>(sql, 
                    (inventory, product) => 
                    {
                        inventory.Product = product;
                        return inventory;
                    }, 
                    splitOn: "Name").ToList();
            }
        }
        
        public Inventory GetById(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT i.*, p.Name, p.Description, p.Price, p.MinimumStockLevel, p.CategoryID
                    FROM Inventory i
                    INNER JOIN Products p ON i.ProductID = p.ProductID
                    WHERE i.InventoryID = @Id";
                    
                return con.Query<Inventory, Product, Inventory>(sql, 
                    (inventory, product) => 
                    {
                        inventory.Product = product;
                        return inventory;
                    }, 
                    new { Id = id },
                    splitOn: "Name").FirstOrDefault();
            }
        }
        
        public void Add(Inventory entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    INSERT INTO Inventory (ProductID, Quantity, LastUpdated) 
                    VALUES (@ProductID, @Quantity, @LastUpdated)";
                    
                entity.LastUpdated = DateTime.Now;
                con.Execute(sql, entity);
            }
        }
        
        public void Update(Inventory entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    UPDATE Inventory 
                    SET ProductID = @ProductID, 
                        Quantity = @Quantity, 
                        LastUpdated = @LastUpdated 
                    WHERE InventoryID = @InventoryID";
                    
                entity.LastUpdated = DateTime.Now;
                con.Execute(sql, entity);
            }
        }
        
        public void Delete(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "DELETE FROM Inventory WHERE InventoryID = @Id";
                con.Execute(sql, new { Id = id });
            }
        }
        
        public Inventory GetByProductId(int productId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT i.*, p.Name, p.Description, p.Price, p.MinimumStockLevel, p.CategoryID
                    FROM Inventory i
                    INNER JOIN Products p ON i.ProductID = p.ProductID
                    WHERE i.ProductID = @ProductId";
                    
                return con.Query<Inventory, Product, Inventory>(sql, 
                    (inventory, product) => 
                    {
                        inventory.Product = product;
                        return inventory;
                    }, 
                    new { ProductId = productId },
                    splitOn: "Name").FirstOrDefault();
            }
        }
        
        public IEnumerable<Inventory> GetLowStockItems()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT i.*, p.Name, p.Description, p.Price, p.MinimumStockLevel, p.CategoryID
                    FROM Inventory i
                    INNER JOIN Products p ON i.ProductID = p.ProductID
                    WHERE i.Quantity <= p.MinimumStockLevel
                    ORDER BY i.Quantity ASC";
                    
                return con.Query<Inventory, Product, Inventory>(sql, 
                    (inventory, product) => 
                    {
                        inventory.Product = product;
                        return inventory;
                    }, 
                    splitOn: "Name").ToList();
            }
        }
        
        public IEnumerable<Inventory> GetByCategory(int categoryId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT i.*, p.Name, p.Description, p.Price, p.MinimumStockLevel, p.CategoryID
                    FROM Inventory i
                    INNER JOIN Products p ON i.ProductID = p.ProductID
                    WHERE p.CategoryID = @CategoryId
                    ORDER BY p.Name";
                    
                return con.Query<Inventory, Product, Inventory>(sql, 
                    (inventory, product) => 
                    {
                        inventory.Product = product;
                        return inventory;
                    }, 
                    new { CategoryId = categoryId },
                    splitOn: "Name").ToList();
            }
        }
        
        public void UpdateStock(int productId, int quantity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    UPDATE Inventory 
                    SET Quantity = @Quantity, LastUpdated = @LastUpdated 
                    WHERE ProductID = @ProductId";
                    
                con.Execute(sql, new { 
                    ProductId = productId, 
                    Quantity = quantity, 
                    LastUpdated = DateTime.Now 
                });
            }
        }
        
        public decimal GetTotalInventoryValue()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT SUM(i.Quantity * p.Price) as TotalValue
                    FROM Inventory i
                    INNER JOIN Products p ON i.ProductID = p.ProductID";
                    
                return con.QuerySingleOrDefault<decimal>(sql);
            }
        }
    }
}