using System.Configuration;
using System.Data.SQLite;
using System;

namespace BMYLBH2025_SDDAP.Models
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase()
        {
            var connStr = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                
                // Enable foreign key constraints
                var pragmaCmd = conn.CreateCommand();
                pragmaCmd.CommandText = "PRAGMA foreign_keys = ON;";
                pragmaCmd.ExecuteNonQuery();
                
                // Create all tables
                CreateUsersTable(conn);
                CreateCategoriesTable(conn);
                CreateSuppliersTable(conn);
                CreateProductsTable(conn);
                CreateInventoryTable(conn);
                CreateOrdersTable(conn);
                CreateOrderDetailsTable(conn);
                CreateNotificationsTable(conn);
                
                // Insert sample data
                InsertSampleData(conn);
            }
        }


        
        private static void CreateUsersTable(SQLiteConnection conn)
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
        
        private static void CreateCategoriesTable(SQLiteConnection conn)
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
        
        private static void CreateSuppliersTable(SQLiteConnection conn)
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
        
        private static void CreateProductsTable(SQLiteConnection conn)
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
        
        private static void CreateInventoryTable(SQLiteConnection conn)
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
        
        private static void CreateOrdersTable(SQLiteConnection conn)
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
        
        private static void CreateOrderDetailsTable(SQLiteConnection conn)
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
                    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE,
                    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE RESTRICT
                );";
            cmd.ExecuteNonQuery();
        }
        
        private static void CreateNotificationsTable(SQLiteConnection conn)
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
                    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                    IsRead BOOLEAN NOT NULL DEFAULT 0,
                    FOREIGN KEY (UserID) REFERENCES Users(UserID)
                );";
            cmd.ExecuteNonQuery();
        }
        
        private static void InsertSampleData(SQLiteConnection conn)
        {
            // Check if sample data already exists to avoid duplicate inserts
            if (HasSampleData(conn))
            {
                return; // Sample data already exists, skip insertion
            }
            
            var cmd = conn.CreateCommand();
            
            // Insert sample categories
            cmd.CommandText = @"
                INSERT OR IGNORE INTO Categories (Name, Description) VALUES 
                ('Electronics', 'Electronic devices and components'),
                ('Office Supplies', 'General office equipment and supplies'),
                ('Furniture', 'Office and warehouse furniture');";
            cmd.ExecuteNonQuery();
            
            // Insert sample suppliers
            cmd.CommandText = @"
                INSERT OR IGNORE INTO Suppliers (Name, ContactPerson, Email, Phone, Address) VALUES 
                ('TechCorp Ltd', 'John Smith', 'john@techcorp.com', '+1-555-0123', '123 Tech Street, Silicon Valley, CA'),
                ('Office Solutions Inc', 'Sarah Johnson', 'sarah@officesolutions.com', '+1-555-0456', '456 Business Ave, New York, NY'),
                ('Furniture World', 'Mike Brown', 'mike@furnitureworld.com', '+1-555-0789', '789 Furniture St, Chicago, IL');";
            cmd.ExecuteNonQuery();
            
            // Insert admin user with proper hashed password
            cmd.CommandText = @"
                INSERT OR IGNORE INTO Users (Username, PasswordHash, Email, FullName, Role, IsEmailVerified, CreatedAt, UpdatedAt) VALUES 
                ('admin', 'YWRtaW4xMjNzYWx0', 'admin@inventory.com', 'System Administrator', 'Admin', 1, datetime('now'), datetime('now'));";
            cmd.ExecuteNonQuery();
            
            // Insert sample products
            cmd.CommandText = @"
                INSERT OR IGNORE INTO Products (Name, Description, Price, MinimumStockLevel, CategoryID) VALUES 
                ('Laptop Computer', 'High-performance business laptop', 1299.99, 5, 1),
                ('Wireless Mouse', 'Ergonomic wireless optical mouse', 29.99, 20, 1),
                ('USB Flash Drive', '64GB USB 3.0 flash drive', 19.99, 50, 1),
                ('Office Chair', 'Ergonomic office chair with lumbar support', 299.99, 3, 3),
                ('Desk Lamp', 'LED desk lamp with adjustable brightness', 49.99, 10, 3),
                ('Printer Paper', 'A4 white copy paper - 500 sheets', 12.99, 100, 2),
                ('Blue Pens', 'Pack of 10 blue ballpoint pens', 8.99, 30, 2),
                ('Notebook', 'Spiral-bound notebook - 200 pages', 5.99, 25, 2);";
            cmd.ExecuteNonQuery();
            
            // Insert sample inventory data
            cmd.CommandText = @"
                INSERT OR IGNORE INTO Inventory (ProductID, Quantity, LastUpdated) VALUES 
                (1, 12, datetime('now')),  -- Laptops: 12 units (above min stock of 5)
                (2, 45, datetime('now')),  -- Wireless Mouse: 45 units (above min stock of 20)
                (3, 25, datetime('now')),  -- USB Flash Drive: 25 units (below min stock of 50) - LOW STOCK
                (4, 2, datetime('now')),   -- Office Chair: 2 units (below min stock of 3) - LOW STOCK
                (5, 15, datetime('now')),  -- Desk Lamp: 15 units (above min stock of 10)
                (6, 150, datetime('now')), -- Printer Paper: 150 units (above min stock of 100)
                (7, 8, datetime('now')),   -- Blue Pens: 8 units (below min stock of 30) - LOW STOCK
                (8, 35, datetime('now'));  -- Notebook: 35 units (above min stock of 25)";
            cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Checks if sample data already exists in the database
        /// </summary>
        /// <param name="conn">SQLite database connection</param>
        /// <returns>True if sample data exists, false otherwise</returns>
        private static bool HasSampleData(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();
            
            // Check if admin user exists (primary indicator of sample data)
            cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE Username = 'admin'";
            var userCount = Convert.ToInt32(cmd.ExecuteScalar());
            
            // Check if sample categories exist
            cmd.CommandText = "SELECT COUNT(*) FROM Categories WHERE Name IN ('Electronics', 'Office Supplies', 'Furniture')";
            var categoryCount = Convert.ToInt32(cmd.ExecuteScalar());
            
            // Check if sample products exist
            cmd.CommandText = "SELECT COUNT(*) FROM Products WHERE Name IN ('Laptop Computer', 'Wireless Mouse', 'USB Flash Drive')";
            var productCount = Convert.ToInt32(cmd.ExecuteScalar());
            
            // If any of the key sample data exists, assume all sample data has been inserted
            return userCount > 0 || categoryCount > 0 || productCount > 0;
        }
    }
} 