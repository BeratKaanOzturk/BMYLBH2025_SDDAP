using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace BMYLBH2025_SDDAP.Models
{
    public class Product
    {
        // Primary Key
        public int ProductID { get; set; }
        
        // Basic Information
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int MinimumStockLevel { get; set; }
        
        // Foreign Key
        public int CategoryID { get; set; }
        
        // Navigation Properties (for relationships)
        public virtual Category Category { get; set; }
        public virtual Inventory Inventory { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        
        // Additional properties for test compatibility
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public Product()
        {
            OrderDetails = new List<OrderDetail>();
        }

        // Business Methods
        public void UpdateInfo(string name, string description, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty");
            
            if (price < 0)
                throw new ArgumentException("Product price cannot be negative");
                
            Name = name;
            Description = description;
            Price = price;
        }

        public void SetCategory(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Invalid category ID");
                
            CategoryID = categoryId;
        }

        public bool CheckStockLevel()
        {
            return Inventory != null && Inventory.Quantity > MinimumStockLevel;
        }
        
        public bool IsLowStock()
        {
            return Inventory != null && Inventory.Quantity <= MinimumStockLevel;
        }
        
        public decimal CalculateTotalValue()
        {
            return Inventory != null ? Inventory.Quantity * Price : 0;
        }
        
        // Additional business methods for testing
        public bool IsValidPrice()
        {
            return Price > 0;
        }
        
        public string GetFormattedPrice()
        {
            return Price.ToString("C");
        }
        
        public bool IsValidMinimumStockLevel()
        {
            return MinimumStockLevel > 0;
        }
        
        public bool ValidateProduct()
        {
            return !string.IsNullOrEmpty(Name) && IsValidPrice() && IsValidMinimumStockLevel();
        }
        
        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.Now;
        }
        
        public TimeSpan GetAge()
        {
            return DateTime.Now - CreatedAt;
        }
    }
    
    public interface IProductRepository : IBaseRepository<Product>
    {
        // Business-specific methods
        IEnumerable<Product> GetByCategory(int categoryId);
        IEnumerable<Product> SearchByName(string name);
        IEnumerable<Product> GetLowStockProducts();
        Product GetByName(string name);
        bool UpdatePrice(int productId, decimal newPrice);
        bool UpdateMinimumStockLevel(int productId, int newLevel);
        decimal GetTotalProductValue();
    }
    
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        
        public ProductRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        
        public IEnumerable<Product> GetAll()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                con.Open();
                const string sql = @"
                    SELECT p.ProductID, p.Name AS ProductName, p.Description AS ProductDescription, p.Price, p.MinimumStockLevel, p.CategoryID AS ProductCategoryID,
                           c.CategoryID AS CategoryID, c.Name AS CategoryName, c.Description AS CategoryDescription
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                    ORDER BY p.Name";
                    
                var result = con.Query(sql);
                var products = new List<Product>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var product = new Product
                        {
                            ProductID = row.ProductID == null ? 0 : Convert.ToInt32(row.ProductID),
                            Name = Convert.ToString(row.ProductName) ?? string.Empty,
                            Description = Convert.ToString(row.ProductDescription) ?? string.Empty,
                            Price = row.Price == null ? 0m : Convert.ToDecimal(row.Price),
                            MinimumStockLevel = row.MinimumStockLevel == null ? 0 : Convert.ToInt32(row.MinimumStockLevel),
                            CategoryID = row.ProductCategoryID == null ? 0 : Convert.ToInt32(row.ProductCategoryID)
                        };
                        
                        // Check if category data exists
                        if (row.CategoryID != null && row.CategoryName != null)
                        {
                            product.Category = new Category
                            {
                                CategoryID = Convert.ToInt32(row.CategoryID),
                                Name = Convert.ToString(row.CategoryName) ?? string.Empty,
                                Description = Convert.ToString(row.CategoryDescription) ?? string.Empty
                            };
                            product.CategoryName = Convert.ToString(row.CategoryName) ?? string.Empty;
                        }
                        
                        products.Add(product);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return products;
            }
        }
        
        public Product GetById(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT p.ProductID, p.Name AS ProductName, p.Description AS ProductDescription, p.Price, p.MinimumStockLevel, p.CategoryID AS ProductCategoryID,
                           c.CategoryID AS CategoryID, c.Name AS CategoryName, c.Description AS CategoryDescription
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                    WHERE p.ProductID = @Id";
                    
                var row = con.QueryFirstOrDefault(sql, new { Id = id });
                if (row == null) return null;
                
                try
                {
                    var product = new Product
                    {
                        ProductID = row.ProductID == null ? 0 : Convert.ToInt32(row.ProductID),
                        Name = Convert.ToString(row.ProductName) ?? string.Empty,
                        Description = Convert.ToString(row.ProductDescription) ?? string.Empty,
                        Price = row.Price == null ? 0m : Convert.ToDecimal(row.Price),
                        MinimumStockLevel = row.MinimumStockLevel == null ? 0 : Convert.ToInt32(row.MinimumStockLevel),
                        CategoryID = row.ProductCategoryID == null ? 0 : Convert.ToInt32(row.ProductCategoryID)
                    };
                    
                    // Check if category data exists
                    if (row.CategoryID != null && row.CategoryName != null)
                    {
                        product.Category = new Category
                        {
                            CategoryID = Convert.ToInt32(row.CategoryID),
                            Name = Convert.ToString(row.CategoryName) ?? string.Empty,
                            Description = Convert.ToString(row.CategoryDescription) ?? string.Empty
                        };
                        product.CategoryName = Convert.ToString(row.CategoryName) ?? string.Empty;
                    }
                    
                    return product;
                }
                catch (Exception ex)
                {
                    // Log the error or handle as needed
                    // Return null if conversion fails
                    return null;
                }
            }
        }
        
        public void Add(Product entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    INSERT INTO Products (Name, Description, Price, MinimumStockLevel, CategoryID) 
                    VALUES (@Name, @Description, @Price, @MinimumStockLevel, @CategoryID)";
                    
                con.Execute(sql, entity);
            }
        }
        
        public void Update(Product entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    UPDATE Products 
                    SET Name = @Name, 
                        Description = @Description, 
                        Price = @Price, 
                        MinimumStockLevel = @MinimumStockLevel, 
                        CategoryID = @CategoryID 
                    WHERE ProductID = @ProductID";
                    
                con.Execute(sql, entity);
            }
        }
        
        public void Delete(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "DELETE FROM Products WHERE ProductID = @Id";
                con.Execute(sql, new { Id = id });
            }
        }
        
        public IEnumerable<Product> GetByCategory(int categoryId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT p.ProductID, p.Name AS ProductName, p.Description AS ProductDescription, p.Price, p.MinimumStockLevel, p.CategoryID AS ProductCategoryID,
                           c.CategoryID AS CategoryID, c.Name AS CategoryName, c.Description AS CategoryDescription
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                    WHERE p.CategoryID = @CategoryId
                    ORDER BY p.Name";
                    
                var result = con.Query(sql, new { CategoryId = categoryId });
                var products = new List<Product>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var product = new Product
                        {
                            ProductID = row.ProductID == null ? 0 : Convert.ToInt32(row.ProductID),
                            Name = Convert.ToString(row.ProductName) ?? string.Empty,
                            Description = Convert.ToString(row.ProductDescription) ?? string.Empty,
                            Price = row.Price == null ? 0m : Convert.ToDecimal(row.Price),
                            MinimumStockLevel = row.MinimumStockLevel == null ? 0 : Convert.ToInt32(row.MinimumStockLevel),
                            CategoryID = row.ProductCategoryID == null ? 0 : Convert.ToInt32(row.ProductCategoryID)
                        };
                        
                        // Check if category data exists
                        if (row.CategoryID != null && row.CategoryName != null)
                        {
                            product.Category = new Category
                            {
                                CategoryID = Convert.ToInt32(row.CategoryID),
                                Name = Convert.ToString(row.CategoryName) ?? string.Empty,
                                Description = Convert.ToString(row.CategoryDescription) ?? string.Empty
                            };
                            product.CategoryName = Convert.ToString(row.CategoryName) ?? string.Empty;
                        }
                        
                        products.Add(product);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return products;
            }
        }
        
        public IEnumerable<Product> SearchByName(string name)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT p.ProductID, p.Name AS ProductName, p.Description AS ProductDescription, p.Price, p.MinimumStockLevel, p.CategoryID AS ProductCategoryID,
                           c.CategoryID AS CategoryID, c.Name AS CategoryName, c.Description AS CategoryDescription
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                    WHERE p.Name LIKE @Name
                    ORDER BY p.Name";
                    
                var result = con.Query(sql, new { Name = $"%{name}%" });
                var products = new List<Product>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var product = new Product
                        {
                            ProductID = row.ProductID == null ? 0 : Convert.ToInt32(row.ProductID),
                            Name = Convert.ToString(row.ProductName) ?? string.Empty,
                            Description = Convert.ToString(row.ProductDescription) ?? string.Empty,
                            Price = row.Price == null ? 0m : Convert.ToDecimal(row.Price),
                            MinimumStockLevel = row.MinimumStockLevel == null ? 0 : Convert.ToInt32(row.MinimumStockLevel),
                            CategoryID = row.ProductCategoryID == null ? 0 : Convert.ToInt32(row.ProductCategoryID)
                        };
                        
                        // Check if category data exists
                        if (row.CategoryID != null && row.CategoryName != null)
                        {
                            product.Category = new Category
                            {
                                CategoryID = Convert.ToInt32(row.CategoryID),
                                Name = Convert.ToString(row.CategoryName) ?? string.Empty,
                                Description = Convert.ToString(row.CategoryDescription) ?? string.Empty
                            };
                            product.CategoryName = Convert.ToString(row.CategoryName) ?? string.Empty;
                        }
                        
                        products.Add(product);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return products;
            }
        }
        
        public IEnumerable<Product> GetLowStockProducts()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT p.ProductID, p.Name AS ProductName, p.Description AS ProductDescription, p.Price, p.MinimumStockLevel, p.CategoryID AS ProductCategoryID,
                           c.CategoryID AS CategoryID, c.Name AS CategoryName, c.Description AS CategoryDescription,
                           i.Quantity
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                    LEFT JOIN Inventory i ON p.ProductID = i.ProductID
                    WHERE i.Quantity <= p.MinimumStockLevel
                    ORDER BY i.Quantity ASC";
                    
                var result = con.Query(sql);
                var products = new List<Product>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var product = new Product
                        {
                            ProductID = row.ProductID == null ? 0 : Convert.ToInt32(row.ProductID),
                            Name = Convert.ToString(row.ProductName) ?? string.Empty,
                            Description = Convert.ToString(row.ProductDescription) ?? string.Empty,
                            Price = row.Price == null ? 0m : Convert.ToDecimal(row.Price),
                            MinimumStockLevel = row.MinimumStockLevel == null ? 0 : Convert.ToInt32(row.MinimumStockLevel),
                            CategoryID = row.ProductCategoryID == null ? 0 : Convert.ToInt32(row.ProductCategoryID)
                        };
                        
                        // Add inventory information if available
                        if (row.Quantity != null)
                        {
                            product.Inventory = new Inventory
                            {
                                ProductID = product.ProductID,
                                Quantity = Convert.ToInt32(row.Quantity)
                            };
                        }
                        
                        // Check if category data exists
                        if (row.CategoryID != null && row.CategoryName != null)
                        {
                            product.Category = new Category
                            {
                                CategoryID = Convert.ToInt32(row.CategoryID),
                                Name = Convert.ToString(row.CategoryName) ?? string.Empty,
                                Description = Convert.ToString(row.CategoryDescription) ?? string.Empty
                            };
                            product.CategoryName = Convert.ToString(row.CategoryName) ?? string.Empty;
                        }
                        
                        products.Add(product);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return products;
            }
        }
        
        public Product GetByName(string name)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT p.ProductID, p.Name AS ProductName, p.Description AS ProductDescription, p.Price, p.MinimumStockLevel, p.CategoryID AS ProductCategoryID,
                           c.CategoryID AS CategoryID, c.Name AS CategoryName, c.Description AS CategoryDescription
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                    WHERE p.Name = @Name";
                    
                var row = con.QueryFirstOrDefault(sql, new { Name = name });
                if (row == null) return null;
                
                try
                {
                    var product = new Product
                    {
                        ProductID = row.ProductID == null ? 0 : Convert.ToInt32(row.ProductID),
                        Name = Convert.ToString(row.ProductName) ?? string.Empty,
                        Description = Convert.ToString(row.ProductDescription) ?? string.Empty,
                        Price = row.Price == null ? 0m : Convert.ToDecimal(row.Price),
                        MinimumStockLevel = row.MinimumStockLevel == null ? 0 : Convert.ToInt32(row.MinimumStockLevel),
                        CategoryID = row.ProductCategoryID == null ? 0 : Convert.ToInt32(row.ProductCategoryID)
                    };
                    
                    // Check if category data exists
                    if (row.CategoryID != null && row.CategoryName != null)
                    {
                        product.Category = new Category
                        {
                            CategoryID = Convert.ToInt32(row.CategoryID),
                            Name = Convert.ToString(row.CategoryName) ?? string.Empty,
                            Description = Convert.ToString(row.CategoryDescription) ?? string.Empty
                        };
                        product.CategoryName = Convert.ToString(row.CategoryName) ?? string.Empty;
                    }
                    
                    return product;
                }
                catch (Exception ex)
                {
                    // Log the error or handle as needed
                    // Return null if conversion fails
                    return null;
                }
            }
        }
        
        public bool UpdatePrice(int productId, decimal newPrice)
        {
            try
            {
                using (var con = _connectionFactory.CreateConnection())
                {
                    const string sql = "UPDATE Products SET Price = @Price WHERE ProductID = @ProductId";
                    var result = con.Execute(sql, new { Price = newPrice, ProductId = productId });
                    return result > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public bool UpdateMinimumStockLevel(int productId, int newLevel)
        {
            try
            {
                using (var con = _connectionFactory.CreateConnection())
                {
                    const string sql = "UPDATE Products SET MinimumStockLevel = @Level WHERE ProductID = @ProductId";
                    var result = con.Execute(sql, new { Level = newLevel, ProductId = productId });
                    return result > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public decimal GetTotalProductValue()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT COALESCE(SUM(p.Price * i.Quantity), 0) 
                    FROM Products p
                    INNER JOIN Inventory i ON p.ProductID = i.ProductID";
                    
                return con.QuerySingle<decimal>(sql);
            }
        }
    }
}