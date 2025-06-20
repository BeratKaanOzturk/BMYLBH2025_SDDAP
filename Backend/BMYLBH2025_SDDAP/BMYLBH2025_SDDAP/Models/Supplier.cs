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
                return con.Query<Supplier>(sql).ToList();
            }
        }
        
        public Supplier GetById(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Suppliers WHERE SupplierID = @Id";
                return con.QuerySingleOrDefault<Supplier>(sql, new { Id = id });
            }
        }
        
        public void Add(Supplier entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    INSERT INTO Suppliers (Name, ContactPerson, Email, Phone, Address) 
                    VALUES (@Name, @ContactPerson, @Email, @Phone, @Address)";
                    
                con.Execute(sql, entity);
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
                return con.QuerySingleOrDefault<Supplier>(sql, new { Name = name });
            }
        }
        
        public Supplier GetByEmail(string email)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Suppliers WHERE Email = @Email";
                return con.QuerySingleOrDefault<Supplier>(sql, new { Email = email });
            }
        }
        
        public IEnumerable<Supplier> SearchByName(string name)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Suppliers WHERE Name LIKE @Name ORDER BY Name";
                return con.Query<Supplier>(sql, new { Name = $"%{name}%" }).ToList();
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
                    
                return con.Query<Supplier>(sql).ToList();
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
    }
}