using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Dapper;

namespace BMYLBH2025_SDDAP.Models
{
    public class Category
    {
        // Primary Key
        public int CategoryID { get; set; }
        
        // Basic Information
        public string Name { get; set; }
        public string Description { get; set; }
        
        // Additional properties for test compatibility
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        // Navigation Properties
        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>();
        }

        // Business Methods
        public void AddProduct(Product product)
        {
            if (product == null) return;
                
            if (Products == null)
                Products = new List<Product>();
                
            if (!Products.Contains(product))
            {
                Products.Add(product);
                product.CategoryID = this.CategoryID;
            }
        }

        public bool RemoveProduct(Product product)
        {
            if (product == null || Products == null) return false;
            
            return Products.Remove(product);
        }

        public void RemoveProduct(int productId)
        {
            var product = Products?.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                Products.Remove(product);
                product.CategoryID = 0; // Reset category
            }
        }
        
        public List<Product> GetProducts()
        {
            return Products.ToList();
        }
        
        public int GetProductCount()
        {
            return Products?.Count ?? 0;
        }
        
        public decimal GetTotalCategoryValue()
        {
            return Products?.Sum(p => p.CalculateTotalValue()) ?? 0;
        }
        
        public List<Product> GetLowStockProducts()
        {
            return Products?.Where(p => p.IsLowStock()).ToList() ?? new List<Product>();
        }
        
        // Additional business methods for testing
        public bool IsValidCategory()
        {
            return !string.IsNullOrEmpty(Name);
        }
        
        public decimal GetTotalInventoryValue()
        {
            return Products?.Sum(p => p.Price) ?? 0;
        }
        
        public decimal GetAverageProductPrice()
        {
            if (Products == null || !Products.Any()) return 0;
            return Products.Average(p => p.Price);
        }
        
        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.Now;
        }
        
        public TimeSpan GetAge()
        {
            return DateTime.Now - CreatedAt;
        }
        
        public bool HasProductsWithName(string productName)
        {
            return Products?.Any(p => p.Name == productName) ?? false;
        }
    }
    
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        // Business-specific methods
        Category GetByName(string name);
        IEnumerable<Category> GetCategoriesWithProducts();
        IEnumerable<Category> GetCategoriesWithLowStock();
        bool HasProducts(int categoryId);
        int GetProductCount(int categoryId);
        decimal GetTotalCategoryValue(int categoryId);
    }
    
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        
        public CategoryRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        
        public IEnumerable<Category> GetAll()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Categories ORDER BY Name";
                var result = con.Query(sql);
                var categories = new List<Category>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var category = new Category
                        {
                            CategoryID = row.CategoryID == null ? 0 : Convert.ToInt32(row.CategoryID),
                            Name = Convert.ToString(row.Name) ?? string.Empty,
                            Description = Convert.ToString(row.Description) ?? string.Empty,
                            CreatedAt = row.CreatedAt == null ? DateTime.Now : Convert.ToDateTime(row.CreatedAt),
                            UpdatedAt = row.UpdatedAt == null ? DateTime.Now : Convert.ToDateTime(row.UpdatedAt)
                        };
                        
                        categories.Add(category);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return categories;
            }
        }
        
        public Category GetById(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Categories WHERE CategoryID = @Id";
                var row = con.QueryFirstOrDefault(sql, new { Id = id });
                if (row == null) return null;
                
                try
                {
                    var category = new Category
                    {
                        CategoryID = row.CategoryID == null ? 0 : Convert.ToInt32(row.CategoryID),
                        Name = Convert.ToString(row.Name) ?? string.Empty,
                        Description = Convert.ToString(row.Description) ?? string.Empty,
                        CreatedAt = row.CreatedAt == null ? DateTime.Now : Convert.ToDateTime(row.CreatedAt),
                        UpdatedAt = row.UpdatedAt == null ? DateTime.Now : Convert.ToDateTime(row.UpdatedAt)
                    };
                    
                    return category;
                }
                catch (Exception ex)
                {
                    // Log the error or handle as needed
                    // Return null if conversion fails
                    return null;
                }
            }
        }
        
        public void Add(Category entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    INSERT INTO Categories (Name, Description) 
                    VALUES (@Name, @Description);
                    SELECT CAST(last_insert_rowid() AS INTEGER);";
                    
                var insertedId = con.QuerySingle<int>(sql, entity);
                entity.CategoryID = insertedId;
            }
        }
        
        public void Update(Category entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    UPDATE Categories 
                    SET Name = @Name, Description = @Description 
                    WHERE CategoryID = @CategoryID";
                    
                con.Execute(sql, entity);
            }
        }
        
        public void Delete(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "DELETE FROM Categories WHERE CategoryID = @Id";
                con.Execute(sql, new { Id = id });
            }
        }
        
        public Category GetByName(string name)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Categories WHERE Name = @Name";
                var row = con.QueryFirstOrDefault(sql, new { Name = name });
                if (row == null) return null;
                
                try
                {
                    var category = new Category
                    {
                        CategoryID = row.CategoryID == null ? 0 : Convert.ToInt32(row.CategoryID),
                        Name = Convert.ToString(row.Name) ?? string.Empty,
                        Description = Convert.ToString(row.Description) ?? string.Empty,
                        CreatedAt = row.CreatedAt == null ? DateTime.Now : Convert.ToDateTime(row.CreatedAt),
                        UpdatedAt = row.UpdatedAt == null ? DateTime.Now : Convert.ToDateTime(row.UpdatedAt)
                    };
                    
                    return category;
                }
                catch (Exception ex)
                {
                    // Log the error or handle as needed
                    // Return null if conversion fails
                    return null;
                }
            }
        }
        
        public IEnumerable<Category> GetCategoriesWithProducts()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT DISTINCT c.* 
                    FROM Categories c
                    INNER JOIN Products p ON c.CategoryID = p.CategoryID
                    ORDER BY c.Name";
                    
                var result = con.Query(sql);
                var categories = new List<Category>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var category = new Category
                        {
                            CategoryID = row.CategoryID == null ? 0 : Convert.ToInt32(row.CategoryID),
                            Name = Convert.ToString(row.Name) ?? string.Empty,
                            Description = Convert.ToString(row.Description) ?? string.Empty,
                            CreatedAt = row.CreatedAt == null ? DateTime.Now : Convert.ToDateTime(row.CreatedAt),
                            UpdatedAt = row.UpdatedAt == null ? DateTime.Now : Convert.ToDateTime(row.UpdatedAt)
                        };
                        
                        categories.Add(category);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return categories;
            }
        }
        
        public IEnumerable<Category> GetCategoriesWithLowStock()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT DISTINCT c.* 
                    FROM Categories c
                    INNER JOIN Products p ON c.CategoryID = p.CategoryID
                    INNER JOIN Inventory i ON p.ProductID = i.ProductID
                    WHERE i.Quantity <= p.MinimumStockLevel
                    ORDER BY c.Name";
                    
                var result = con.Query(sql);
                var categories = new List<Category>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var category = new Category
                        {
                            CategoryID = row.CategoryID == null ? 0 : Convert.ToInt32(row.CategoryID),
                            Name = Convert.ToString(row.Name) ?? string.Empty,
                            Description = Convert.ToString(row.Description) ?? string.Empty,
                            CreatedAt = row.CreatedAt == null ? DateTime.Now : Convert.ToDateTime(row.CreatedAt),
                            UpdatedAt = row.UpdatedAt == null ? DateTime.Now : Convert.ToDateTime(row.UpdatedAt)
                        };
                        
                        categories.Add(category);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return categories;
            }
        }
        
        public bool HasProducts(int categoryId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT COUNT(*) FROM Products WHERE CategoryID = @CategoryId";
                var count = con.QuerySingle<int>(sql, new { CategoryId = categoryId });
                return count > 0;
            }
        }
        
        public int GetProductCount(int categoryId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT COUNT(*) FROM Products WHERE CategoryID = @CategoryId";
                return con.QuerySingle<int>(sql, new { CategoryId = categoryId });
            }
        }
        
        public decimal GetTotalCategoryValue(int categoryId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT COALESCE(SUM(p.Price * i.Quantity), 0) 
                    FROM Products p
                    INNER JOIN Inventory i ON p.ProductID = i.ProductID
                    WHERE p.CategoryID = @CategoryId";
                    
                return con.QuerySingle<decimal>(sql, new { CategoryId = categoryId });
            }
        }
    }
}