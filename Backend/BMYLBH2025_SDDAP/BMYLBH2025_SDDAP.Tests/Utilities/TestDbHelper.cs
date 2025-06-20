using BMYLBH2025_SDDAP.Models;
using System;
using System.Data.SQLite;

namespace BMYLBH2025_SDDAP.Tests.Utilities
{
    /// <summary>
    /// Helper class for managing test database setup and data operations
    /// </summary>
    public class TestDbHelper
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public TestDbHelper(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        /// <summary>
        /// Creates the complete database schema for testing
        /// </summary>
        public void CreateTestSchema()
        {
            using (var conn = _connectionFactory.CreateConnection())
            {
                conn.Open();
                
                CreateUsersTable((SQLiteConnection)conn);
                CreateCategoriesTable((SQLiteConnection)conn);
                CreateSuppliersTable((SQLiteConnection)conn);
                CreateProductsTable((SQLiteConnection)conn);
                CreateInventoryTable((SQLiteConnection)conn);
                CreateOrdersTable((SQLiteConnection)conn);
                CreateOrderDetailsTable((SQLiteConnection)conn);
                CreateNotificationsTable((SQLiteConnection)conn);
            }
        }

        /// <summary>
        /// Seeds the test database with sample data
        /// </summary>
        public void SeedTestData()
        {
            using (var conn = _connectionFactory.CreateConnection())
            {
                conn.Open();
                
                SeedCategories((SQLiteConnection)conn);
                SeedSuppliers((SQLiteConnection)conn);
                SeedUsers((SQLiteConnection)conn);
                SeedProducts((SQLiteConnection)conn);
                SeedInventory((SQLiteConnection)conn);
            }
        }

        /// <summary>
        /// Clears all data from test database
        /// </summary>
        public void ClearTestData()
        {
            using (var conn = _connectionFactory.CreateConnection())
            {
                conn.Open();
                
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    DELETE FROM Notifications;
                    DELETE FROM OrderDetails;
                    DELETE FROM Orders;
                    DELETE FROM Inventory;
                    DELETE FROM Products;
                    DELETE FROM Suppliers;
                    DELETE FROM Categories;
                    DELETE FROM Users;";
                cmd.ExecuteNonQuery();
            }
        }

        #region Schema Creation Methods

        private void CreateUsersTable(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users (
                    UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    Password TEXT,
                    PasswordHash TEXT NOT NULL,
                    Email TEXT NOT NULL UNIQUE,
                    FullName TEXT NOT NULL,
                    Role TEXT NOT NULL DEFAULT 'User',
                    IsActive BOOLEAN NOT NULL DEFAULT 1,
                    IsEmailVerified BOOLEAN NOT NULL DEFAULT 0,
                    EmailVerificationToken TEXT,
                    PasswordResetToken TEXT,
                    PasswordResetExpiry DATETIME,
                    PasswordResetOTP TEXT,
                    OTPExpiry DATETIME,
                    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                );";
            cmd.ExecuteNonQuery();
        }

        private void CreateCategoriesTable(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Categories (
                    CategoryID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL UNIQUE,
                    Description TEXT,
                    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                );";
            cmd.ExecuteNonQuery();
        }

        private void CreateSuppliersTable(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Suppliers (
                    SupplierID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    ContactPerson TEXT,
                    Email TEXT,
                    Phone TEXT,
                    Address TEXT,
                    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                );";
            cmd.ExecuteNonQuery();
        }

        private void CreateProductsTable(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Products (
                    ProductID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    Price DECIMAL(10,2) NOT NULL,
                    MinimumStockLevel INTEGER NOT NULL DEFAULT 10,
                    CategoryID INTEGER,
                    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
                );";
            cmd.ExecuteNonQuery();
        }

        private void CreateInventoryTable(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Inventory (
                    InventoryID INTEGER PRIMARY KEY AUTOINCREMENT,
                    ProductID INTEGER NOT NULL,
                    Quantity INTEGER NOT NULL DEFAULT 0,
                    LastUpdated DATETIME DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
                    UNIQUE(ProductID)
                );";
            cmd.ExecuteNonQuery();
        }

        private void CreateOrdersTable(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Orders (
                    OrderID INTEGER PRIMARY KEY AUTOINCREMENT,
                    SupplierID INTEGER NOT NULL,
                    OrderDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    Status TEXT NOT NULL DEFAULT 'Pending',
                    TotalAmount DECIMAL(10,2) NOT NULL DEFAULT 0,
                    CreatedBy INTEGER,
                    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID),
                    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
                );";
            cmd.ExecuteNonQuery();
        }

        private void CreateOrderDetailsTable(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS OrderDetails (
                    OrderDetailID INTEGER PRIMARY KEY AUTOINCREMENT,
                    OrderID INTEGER NOT NULL,
                    ProductID INTEGER NOT NULL,
                    Quantity INTEGER NOT NULL,
                    UnitPrice DECIMAL(10,2) NOT NULL,
                    LineTotal DECIMAL(10,2) NOT NULL,
                    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
                    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
                );";
            cmd.ExecuteNonQuery();
        }

        private void CreateNotificationsTable(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Notifications (
                    NotificationID INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserID INTEGER NOT NULL,
                    Message TEXT NOT NULL,
                    Type TEXT NOT NULL DEFAULT 'General',
                    Priority TEXT NOT NULL DEFAULT 'Medium',
                    Date DATETIME DEFAULT CURRENT_TIMESTAMP,
                    IsRead BOOLEAN NOT NULL DEFAULT 0,
                    FOREIGN KEY (UserID) REFERENCES Users(UserID)
                );";
            cmd.ExecuteNonQuery();
        }

        #endregion

        #region Data Seeding Methods

        private void SeedCategories(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Categories (Name, Description) VALUES 
                ('Electronics', 'Electronic devices and components'),
                ('Office Supplies', 'General office equipment and supplies'),
                ('Furniture', 'Office and warehouse furniture');";
            cmd.ExecuteNonQuery();
        }

        private void SeedSuppliers(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Suppliers (Name, ContactPerson, Email, Phone, Address) VALUES 
                ('TechCorp Ltd', 'John Smith', 'john@techcorp.com', '+1-555-0123', '123 Tech Street, Silicon Valley, CA'),
                ('Office Plus', 'Jane Doe', 'jane@officeplus.com', '+1-555-0456', '456 Business Ave, New York, NY');";
            cmd.ExecuteNonQuery();
        }

        private void SeedUsers(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Users (Username, PasswordHash, Email, FullName, Role, IsEmailVerified) VALUES 
                ('admin', '$2a$11$7Q.gWaI8eKQvJ5xQg5qjLek1yoKdC3F3YqVx8eqIy5Z2Q6GxJ8cK6', 'admin@test.com', 'Test Administrator', 'Admin', 1),
                ('testuser', '$2a$11$7Q.gWaI8eKQvJ5xQg5qjLek1yoKdC3F3YqVx8eqIy5Z2Q6GxJ8cK6', 'user@test.com', 'Test User', 'User', 1);";
            cmd.ExecuteNonQuery();
        }

        private void SeedProducts(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Products (Name, Description, Price, MinimumStockLevel, CategoryID) VALUES 
                ('Test Laptop', 'Test laptop for unit testing', 999.99, 5, 1),
                ('Test Mouse', 'Test wireless mouse', 25.99, 20, 1),
                ('Test Paper', 'Test office paper pack', 10.99, 50, 2),
                ('Test Chair', 'Test office chair', 199.99, 3, 3);";
            cmd.ExecuteNonQuery();
        }

        private void SeedInventory(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Inventory (ProductID, Quantity, LastUpdated) VALUES 
                (1, 10, datetime('now')),
                (2, 25, datetime('now')),
                (3, 30, datetime('now')),
                (4, 2, datetime('now'));";
            cmd.ExecuteNonQuery();
        }

        #endregion
    }
} 