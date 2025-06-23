using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace BMYLBH2025_SDDAP.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        
        // Navigation Properties
        public virtual ICollection<Product> Products { get; set; }
        
        public Supplier()
        {
            Products = new List<Product>();
        }
        
        // Business Methods
        public bool IsValidSupplier()
        {
            return !string.IsNullOrWhiteSpace(Name) && 
                   !string.IsNullOrWhiteSpace(Email) && 
                   !string.IsNullOrWhiteSpace(Phone);
        }
        
        public void UpdateContactInfo(string contactPerson, string email, string phone)
        {
            if (!string.IsNullOrWhiteSpace(contactPerson))
                ContactPerson = contactPerson;
            if (!string.IsNullOrWhiteSpace(email))
                Email = email;
            if (!string.IsNullOrWhiteSpace(phone))
                Phone = phone;
        }

        public void UpdateInfo(string name, string contactPerson, string email, string phone, string address)
        {
            // Update supplier information
        }

        public List<Order> GetOrderHistory()
        {
            // Get order history
            return new List<Order>();
        }
    }
    
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        // Business-specific methods
        Supplier GetByName(string name);
        Supplier GetByEmail(string email);
        IEnumerable<Supplier> SearchByName(string name);
        IEnumerable<Supplier> GetSuppliersWithProducts();
        bool HasProducts(int supplierId);
        int GetProductCount(int supplierId);
        bool HasOrderReferences(int supplierId);
    }
    
    public class SupplierRepository : ISupplierRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        
        public SupplierRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        
        public IEnumerable<Supplier> GetAll()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Suppliers ORDER BY Name";
                var result = con.Query(sql);
                var suppliers = new List<Supplier>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var supplier = new Supplier
                        {
                            SupplierID = row.SupplierID == null ? 0 : Convert.ToInt32(row.SupplierID),
                            Name = Convert.ToString(row.Name) ?? string.Empty,
                            ContactPerson = Convert.ToString(row.ContactPerson) ?? string.Empty,
                            Email = Convert.ToString(row.Email) ?? string.Empty,
                            Phone = Convert.ToString(row.Phone) ?? string.Empty,
                            Address = Convert.ToString(row.Address) ?? string.Empty
                        };
                        
                        suppliers.Add(supplier);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return suppliers;
            }
        }
        
        public Supplier GetById(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Suppliers WHERE SupplierID = @Id";
                var row = con.QueryFirstOrDefault(sql, new { Id = id });
                if (row == null) return null;
                
                try
                {
                    var supplier = new Supplier
                    {
                        SupplierID = row.SupplierID == null ? 0 : Convert.ToInt32(row.SupplierID),
                        Name = Convert.ToString(row.Name) ?? string.Empty,
                        ContactPerson = Convert.ToString(row.ContactPerson) ?? string.Empty,
                        Email = Convert.ToString(row.Email) ?? string.Empty,
                        Phone = Convert.ToString(row.Phone) ?? string.Empty,
                        Address = Convert.ToString(row.Address) ?? string.Empty
                    };
                    
                    return supplier;
                }
                catch (Exception ex)
                {
                    // Log the error or handle as needed
                    // Return null if conversion fails
                    return null;
                }
            }
        }
        
        public void Add(Supplier entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    INSERT INTO Suppliers (Name, ContactPerson, Email, Phone, Address) 
                    VALUES (@Name, @ContactPerson, @Email, @Phone, @Address);
                    SELECT CAST(last_insert_rowid() AS INTEGER);";
                    
                var insertedId = con.QuerySingle<int>(sql, entity);
                entity.SupplierID = insertedId;
            }
        }
        
        public void Update(Supplier entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    UPDATE Suppliers 
                    SET Name = @Name, 
                        ContactPerson = @ContactPerson, 
                        Email = @Email, 
                        Phone = @Phone, 
                        Address = @Address 
                    WHERE SupplierID = @SupplierID";
                    
                con.Execute(sql, entity);
            }
        }
        
        public void Delete(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "DELETE FROM Suppliers WHERE SupplierID = @Id";
                con.Execute(sql, new { Id = id });
            }
        }
        
        public Supplier GetByName(string name)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Suppliers WHERE Name = @Name";
                var row = con.QueryFirstOrDefault(sql, new { Name = name });
                if (row == null) return null;
                
                try
                {
                    var supplier = new Supplier
                    {
                        SupplierID = row.SupplierID == null ? 0 : Convert.ToInt32(row.SupplierID),
                        Name = Convert.ToString(row.Name) ?? string.Empty,
                        ContactPerson = Convert.ToString(row.ContactPerson) ?? string.Empty,
                        Email = Convert.ToString(row.Email) ?? string.Empty,
                        Phone = Convert.ToString(row.Phone) ?? string.Empty,
                        Address = Convert.ToString(row.Address) ?? string.Empty
                    };
                    
                    return supplier;
                }
                catch (Exception ex)
                {
                    // Log the error or handle as needed
                    // Return null if conversion fails
                    return null;
                }
            }
        }
        
        public Supplier GetByEmail(string email)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Suppliers WHERE Email = @Email";
                var row = con.QueryFirstOrDefault(sql, new { Email = email });
                if (row == null) return null;
                
                try
                {
                    var supplier = new Supplier
                    {
                        SupplierID = row.SupplierID == null ? 0 : Convert.ToInt32(row.SupplierID),
                        Name = Convert.ToString(row.Name) ?? string.Empty,
                        ContactPerson = Convert.ToString(row.ContactPerson) ?? string.Empty,
                        Email = Convert.ToString(row.Email) ?? string.Empty,
                        Phone = Convert.ToString(row.Phone) ?? string.Empty,
                        Address = Convert.ToString(row.Address) ?? string.Empty
                    };
                    
                    return supplier;
                }
                catch (Exception ex)
                {
                    // Log the error or handle as needed
                    // Return null if conversion fails
                    return null;
                }
            }
        }
        
        public IEnumerable<Supplier> SearchByName(string name)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Suppliers WHERE Name LIKE @Name ORDER BY Name";
                var result = con.Query(sql, new { Name = $"%{name}%" });
                var suppliers = new List<Supplier>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var supplier = new Supplier
                        {
                            SupplierID = row.SupplierID == null ? 0 : Convert.ToInt32(row.SupplierID),
                            Name = Convert.ToString(row.Name) ?? string.Empty,
                            ContactPerson = Convert.ToString(row.ContactPerson) ?? string.Empty,
                            Email = Convert.ToString(row.Email) ?? string.Empty,
                            Phone = Convert.ToString(row.Phone) ?? string.Empty,
                            Address = Convert.ToString(row.Address) ?? string.Empty
                        };
                        
                        suppliers.Add(supplier);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return suppliers;
            }
        }
        
        public IEnumerable<Supplier> GetSuppliersWithProducts()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT DISTINCT s.* 
                    FROM Suppliers s
                    INNER JOIN Products p ON s.SupplierID = p.SupplierID
                    ORDER BY s.Name";
                    
                var result = con.Query(sql);
                var suppliers = new List<Supplier>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var supplier = new Supplier
                        {
                            SupplierID = row.SupplierID == null ? 0 : Convert.ToInt32(row.SupplierID),
                            Name = Convert.ToString(row.Name) ?? string.Empty,
                            ContactPerson = Convert.ToString(row.ContactPerson) ?? string.Empty,
                            Email = Convert.ToString(row.Email) ?? string.Empty,
                            Phone = Convert.ToString(row.Phone) ?? string.Empty,
                            Address = Convert.ToString(row.Address) ?? string.Empty
                        };
                        
                        suppliers.Add(supplier);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return suppliers;
            }
        }
        
        public bool HasProducts(int supplierId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT COUNT(*) FROM Products WHERE SupplierID = @SupplierId";
                var count = con.QuerySingle<int>(sql, new { SupplierId = supplierId });
                return count > 0;
            }
        }
        
        public int GetProductCount(int supplierId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT COUNT(*) FROM Products WHERE SupplierID = @SupplierId";
                return con.QuerySingle<int>(sql, new { SupplierId = supplierId });
            }
        }

        public bool HasOrderReferences(int supplierId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT COUNT(*) 
                    FROM Orders o
                    INNER JOIN OrderItems oi ON o.OrderID = oi.OrderID
                    INNER JOIN Products p ON oi.ProductID = p.ProductID
                    WHERE p.SupplierID = @SupplierId";
                    
                var count = con.QuerySingle<int>(sql, new { SupplierId = supplierId });
                return count > 0;
            }
        }
    }
}