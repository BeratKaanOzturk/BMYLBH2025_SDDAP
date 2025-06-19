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
                    Password TEXT NOT NULL,
                    Email TEXT NOT NULL UNIQUE,
                    Role TEXT NOT NULL DEFAULT 'User',
                    IsActive BOOLEAN NOT NULL DEFAULT 1,
                    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
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
                    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
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
                    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
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
                    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
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
                    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
                    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
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
                    IsRead BOOLEAN NOT NULL DEFAULT 0,
                    FOREIGN KEY (UserID) REFERENCES Users(UserID)
                );";
            cmd.ExecuteNonQuery();
        }
        
        private static void InsertSampleData(SQLiteConnection conn)
        {
            // Insert sample categories
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT OR IGNORE INTO Categories (Name, Description) VALUES 
                ('Electronics', 'Electronic devices and components'),
                ('Office Supplies', 'General office equipment and supplies'),
                ('Furniture', 'Office and warehouse furniture');";
            cmd.ExecuteNonQuery();
            
            // Insert sample supplier
            cmd.CommandText = @"
                INSERT OR IGNORE INTO Suppliers (Name, ContactPerson, Email, Phone, Address) VALUES 
                ('TechCorp Ltd', 'John Smith', 'john@techcorp.com', '+1-555-0123', '123 Tech Street, Silicon Valley, CA');";
            cmd.ExecuteNonQuery();
            
            // Insert admin user (with plain password for now - should be hashed in production)
            cmd.CommandText = @"
                INSERT OR IGNORE INTO Users (Username, Password, Email, Role) VALUES 
                ('admin', 'admin123', 'admin@inventory.com', 'Admin');";
            cmd.ExecuteNonQuery();
        }
    }
} 