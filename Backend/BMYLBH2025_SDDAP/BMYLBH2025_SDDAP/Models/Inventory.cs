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
        
        // Additional properties for test compatibility
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int MinimumStockLevel { get; set; }
        
        // Business Methods
        public bool IsLowStock()
        {
            if (Product != null)
                return Quantity < Product.MinimumStockLevel;
            else
                return Quantity < MinimumStockLevel;
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
            if (Product != null)
                return Quantity * Product.Price;
            else
                return Quantity * Price;
        }
        
        public string GetStockStatus()
        {
            if (Quantity == 0)
                return "Out of Stock";
            else if (IsLowStock())
                return "Low Stock";
            else
                return "In Stock";
        }
    }
    
    public interface IInventoryRepository : IBaseRepository<Inventory>
    {
        // Business-specific methods
        Inventory GetByProductId(int productId);
        IEnumerable<Inventory> GetLowStockItems();
        IEnumerable<Inventory> GetByCategory(int categoryId);
        bool UpdateStock(int productId, int quantity);
        decimal GetTotalInventoryValue();
        bool Create(Inventory inventory);
        bool UpdateInventory(Inventory inventory);
        bool DeleteInventory(int id);
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
                con.Open();
                const string sql = @"
                    SELECT i.InventoryID, i.ProductID, i.Quantity, i.LastUpdated,
                           p.ProductID, p.Name, p.Description, p.Price, p.MinimumStockLevel, p.CategoryID,
                           c.CategoryID, c.Name as CategoryName, c.Description as CategoryDescription
                    FROM Inventory i
                    INNER JOIN Products p ON i.ProductID = p.ProductID
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                    ORDER BY i.LastUpdated DESC";
                    
                return con.Query<Inventory, Product, Category, Inventory>(sql, 
                    (inventory, product, category) => 
                    {
                        product.Category = category;
                        inventory.Product = product;
                        // Populate additional navigation properties for testing
                        inventory.ProductName = product.Name;
                        inventory.CategoryName = category?.Name;
                        inventory.Price = product.Price;
                        inventory.MinimumStockLevel = product.MinimumStockLevel;
                        return inventory;
                    }, 
                    splitOn: "ProductID,CategoryID").ToList();
            }
        }
        
        public Inventory GetById(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT i.InventoryID, i.ProductID, i.Quantity, i.LastUpdated,
                           p.ProductID, p.Name, p.Description, p.Price, p.MinimumStockLevel, p.CategoryID,
                           c.CategoryID, c.Name as CategoryName, c.Description as CategoryDescription
                    FROM Inventory i
                    INNER JOIN Products p ON i.ProductID = p.ProductID
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                    WHERE i.InventoryID = @Id";
                    
                return con.Query<Inventory, Product, Category, Inventory>(sql, 
                    (inventory, product, category) => 
                    {
                        product.Category = category;
                        inventory.Product = product;
                        return inventory;
                    }, 
                    new { Id = id },
                    splitOn: "ProductID,CategoryID").FirstOrDefault();
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
        
        public bool Create(Inventory entity)
        {
            try
            {
                using (var con = _connectionFactory.CreateConnection())
                {
                    con.Open();
                    const string sql = @"
                        INSERT INTO Inventory (ProductID, Quantity, LastUpdated) 
                        VALUES (@ProductID, @Quantity, @LastUpdated)";
                        
                    entity.LastUpdated = DateTime.Now;
                    var result = con.Execute(sql, entity);
                    return result > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public bool UpdateInventory(Inventory entity)
        {
            try
            {
                using (var con = _connectionFactory.CreateConnection())
                {
                    con.Open();
                    const string sql = @"
                        UPDATE Inventory 
                        SET ProductID = @ProductID, 
                            Quantity = @Quantity, 
                            LastUpdated = @LastUpdated 
                        WHERE InventoryID = @InventoryID";
                        
                    entity.LastUpdated = DateTime.Now;
                    var result = con.Execute(sql, entity);
                    return result > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public bool DeleteInventory(int id)
        {
            try
            {
                using (var con = _connectionFactory.CreateConnection())
                {
                    con.Open();
                    const string sql = "DELETE FROM Inventory WHERE InventoryID = @Id";
                    var result = con.Execute(sql, new { Id = id });
                    return result > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public Inventory GetByProductId(int productId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT i.InventoryID, i.ProductID, i.Quantity, i.LastUpdated,
                           p.ProductID, p.Name, p.Description, p.Price, p.MinimumStockLevel, p.CategoryID,
                           c.CategoryID, c.Name as CategoryName, c.Description as CategoryDescription
                    FROM Inventory i
                    INNER JOIN Products p ON i.ProductID = p.ProductID
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                    WHERE i.ProductID = @ProductId";
                    
                return con.Query<Inventory, Product, Category, Inventory>(sql, 
                    (inventory, product, category) => 
                    {
                        product.Category = category;
                        inventory.Product = product;
                        return inventory;
                    }, 
                    new { ProductId = productId },
                    splitOn: "ProductID,CategoryID").FirstOrDefault();
            }
        }
        
        public IEnumerable<Inventory> GetLowStockItems()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT i.InventoryID, i.ProductID, i.Quantity, i.LastUpdated,
                           p.ProductID, p.Name, p.Description, p.Price, p.MinimumStockLevel, p.CategoryID,
                           c.CategoryID, c.Name as CategoryName, c.Description as CategoryDescription
                    FROM Inventory i
                    INNER JOIN Products p ON i.ProductID = p.ProductID
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                    WHERE i.Quantity <= p.MinimumStockLevel
                    ORDER BY i.Quantity ASC";
                    
                return con.Query<Inventory, Product, Category, Inventory>(sql, 
                    (inventory, product, category) => 
                    {
                        product.Category = category;
                        inventory.Product = product;
                        return inventory;
                    }, 
                    splitOn: "ProductID,CategoryID").ToList();
            }
        }
        
        public IEnumerable<Inventory> GetByCategory(int categoryId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT i.InventoryID, i.ProductID, i.Quantity, i.LastUpdated,
                           p.ProductID, p.Name, p.Description, p.Price, p.MinimumStockLevel, p.CategoryID,
                           c.CategoryID, c.Name as CategoryName, c.Description as CategoryDescription
                    FROM Inventory i
                    INNER JOIN Products p ON i.ProductID = p.ProductID
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                    WHERE p.CategoryID = @CategoryId
                    ORDER BY p.Name";
                    
                return con.Query<Inventory, Product, Category, Inventory>(sql, 
                    (inventory, product, category) => 
                    {
                        product.Category = category;
                        inventory.Product = product;
                        return inventory;
                    }, 
                    new { CategoryId = categoryId },
                    splitOn: "ProductID,CategoryID").ToList();
            }
        }
        
        public bool UpdateStock(int productId, int quantity)
        {
            try
            {
                using (var con = _connectionFactory.CreateConnection())
                {
                    con.Open();
                    const string sql = @"
                        UPDATE Inventory 
                        SET Quantity = Quantity + @Quantity, 
                            LastUpdated = @LastUpdated 
                        WHERE ProductID = @ProductId";
                        
                    var result = con.Execute(sql, new { 
                        ProductId = productId, 
                        Quantity = quantity, 
                        LastUpdated = DateTime.Now 
                    });
                    return result > 0;
                }
            }
            catch
            {
                return false;
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
        
        public void Update(Inventory entity)
        {
            UpdateInventory(entity);
        }
        
        public void Delete(int id)
        {
            DeleteInventory(id);
        }
    }
}