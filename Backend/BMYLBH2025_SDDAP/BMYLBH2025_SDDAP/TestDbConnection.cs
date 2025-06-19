using System;
using System.Configuration;
using System.Data.SQLite;
using BMYLBH2025_SDDAP.Models;

namespace BMYLBH2025_SDDAP
{
    public class TestDbConnection
    {
        public static void TestConnection()
        {
            try
            {
                // Initialize database
                DatabaseInitializer.InitializeDatabase();
                Console.WriteLine("✅ Database initialized successfully");

                // Test connection
                var connStr = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                using (var conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    Console.WriteLine("✅ Database connection successful");

                    // Test data
                    var cmd = conn.CreateCommand();
                    
                    // Check products count
                    cmd.CommandText = "SELECT COUNT(*) FROM Products";
                    var productCount = Convert.ToInt32(cmd.ExecuteScalar());
                    Console.WriteLine($"✅ Products in database: {productCount}");

                    // Check inventory count
                    cmd.CommandText = "SELECT COUNT(*) FROM Inventory";
                    var inventoryCount = Convert.ToInt32(cmd.ExecuteScalar());
                    Console.WriteLine($"✅ Inventory records: {inventoryCount}");

                    // Check categories count
                    cmd.CommandText = "SELECT COUNT(*) FROM Categories";
                    var categoryCount = Convert.ToInt32(cmd.ExecuteScalar());
                    Console.WriteLine($"✅ Categories in database: {categoryCount}");

                    // Test inventory with products join
                    cmd.CommandText = @"
                        SELECT i.InventoryID, i.Quantity, p.Name, p.Price 
                        FROM Inventory i 
                        INNER JOIN Products p ON i.ProductID = p.ProductID 
                        LIMIT 5";
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\n📦 Sample Inventory Data:");
                        while (reader.Read())
                        {
                            Console.WriteLine($"  ID: {reader["InventoryID"]}, Product: {reader["Name"]}, Qty: {reader["Quantity"]}, Price: ${reader["Price"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }
    }
} 