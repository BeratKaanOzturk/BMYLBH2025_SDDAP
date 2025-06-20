using Microsoft.VisualStudio.TestTools.UnitTesting;
using BMYLBH2025_SDDAP.Models;
using FluentAssertions;
using System;
using System.Data.SQLite;

namespace BMYLBH2025_SDDAP.Tests.Repositories
{
    [TestClass]
    public class BaseRepositoryTests : TestBase
    {
        #region Database Connection Tests

        [TestMethod]
        public void ConnectionFactory_ShouldCreateValidConnection()
        {
            // Act
            var connection = ConnectionFactory.CreateConnection();

            // Assert
            connection.Should().NotBeNull();
            connection.Should().BeOfType<SQLiteConnection>();
        }

        [TestMethod]
        public void ConnectionFactory_ShouldCreateOpenableConnection()
        {
            // Act & Assert
            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Should().NotBeNull();
                
                // Should be able to open connection without exception
                Action openAction = () => connection.Open();
                openAction.Should().NotThrow();
                
                connection.State.Should().Be(System.Data.ConnectionState.Open);
            }
        }

        [TestMethod]
        public void TestDatabase_ShouldHaveCorrectSchema()
        {
            // Arrange
            var expectedTables = new[]
            {
                "Users", "Categories", "Suppliers", "Products", 
                "Inventory", "Orders", "OrderDetails", "Notifications"
            };

            // Act & Assert
            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();
                
                foreach (var tableName in expectedTables)
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';";
                    var result = cmd.ExecuteScalar();
                    
                    result.Should().NotBeNull($"Table {tableName} should exist");
                }
            }
        }

        [TestMethod]
        public void TestDatabase_ShouldHaveTestData()
        {
            // Act & Assert
            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();
                
                // Check Categories
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Categories;";
                var categoryCount = Convert.ToInt32(cmd.ExecuteScalar());
                categoryCount.Should().BeGreaterThan(0, "Categories table should have test data");
                
                // Check Products
                cmd.CommandText = "SELECT COUNT(*) FROM Products;";
                var productCount = Convert.ToInt32(cmd.ExecuteScalar());
                productCount.Should().BeGreaterThan(0, "Products table should have test data");
                
                // Check Inventory
                cmd.CommandText = "SELECT COUNT(*) FROM Inventory;";
                var inventoryCount = Convert.ToInt32(cmd.ExecuteScalar());
                inventoryCount.Should().BeGreaterThan(0, "Inventory table should have test data");
            }
        }

        #endregion

        #region Foreign Key Constraints Tests

        [TestMethod]
        public void Products_ShouldHaveForeignKeyToCategories()
        {
            // Act & Assert
            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();
                
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"
                    SELECT COUNT(*) 
                    FROM Products p 
                    INNER JOIN Categories c ON p.CategoryID = c.CategoryID;";
                
                var joinCount = Convert.ToInt32(cmd.ExecuteScalar());
                joinCount.Should().BeGreaterThan(0, "Products should be properly linked to Categories");
            }
        }

        [TestMethod]
        public void Inventory_ShouldHaveForeignKeyToProducts()
        {
            // Act & Assert
            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();
                
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"
                    SELECT COUNT(*) 
                    FROM Inventory i 
                    INNER JOIN Products p ON i.ProductID = p.ProductID;";
                
                var joinCount = Convert.ToInt32(cmd.ExecuteScalar());
                joinCount.Should().BeGreaterThan(0, "Inventory should be properly linked to Products");
            }
        }

        #endregion

        #region Database Transaction Tests

        [TestMethod]
        public void DatabaseTransaction_ShouldRollbackOnError()
        {
            // Arrange
            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();
                
                // Get initial count
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Categories;";
                var initialCount = Convert.ToInt32(cmd.ExecuteScalar());
                
                // Act - Try to insert invalid data in a transaction
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // This should succeed
                        cmd = connection.CreateCommand();
                        cmd.Transaction = transaction;
                        cmd.CommandText = "INSERT INTO Categories (Name, Description) VALUES ('Test Category', 'Test Description');";
                        cmd.ExecuteNonQuery();
                        
                        // This should fail due to duplicate name constraint
                        cmd.CommandText = "INSERT INTO Categories (Name, Description) VALUES ('Test Category', 'Duplicate');";
                        cmd.ExecuteNonQuery();
                        
                        transaction.Commit();
                        Assert.Fail("Transaction should have failed due to constraint violation");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
                
                // Assert - Count should be unchanged
                cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Categories;";
                var finalCount = Convert.ToInt32(cmd.ExecuteScalar());
                
                finalCount.Should().Be(initialCount, "Transaction rollback should restore original state");
            }
        }

        #endregion

        #region Data Integrity Tests

        [TestMethod]
        public void Categories_ShouldHaveUniqueNames()
        {
            // Act & Assert
            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();
                
                var cmd = connection.CreateCommand();
                
                // Try to insert duplicate category name
                Action duplicateInsert = () =>
                {
                    cmd.CommandText = "INSERT INTO Categories (Name, Description) VALUES ('Electronics', 'Duplicate Electronics');";
                    cmd.ExecuteNonQuery();
                };
                
                duplicateInsert.Should().Throw<SQLiteException>("Category names should be unique");
            }
        }

        [TestMethod]
        public void Users_ShouldHaveUniqueUsernames()
        {
            // Act & Assert
            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();
                
                var cmd = connection.CreateCommand();
                
                // Try to insert duplicate username
                Action duplicateInsert = () =>
                {
                    cmd.CommandText = @"
                        INSERT INTO Users (Username, PasswordHash, Email, FullName, Role) 
                        VALUES ('admin', 'hash', 'duplicate@test.com', 'Duplicate Admin', 'Admin');";
                    cmd.ExecuteNonQuery();
                };
                
                duplicateInsert.Should().Throw<SQLiteException>("Usernames should be unique");
            }
        }

        [TestMethod]
        public void Users_ShouldHaveUniqueEmails()
        {
            // Act & Assert
            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();
                
                var cmd = connection.CreateCommand();
                
                // Try to insert duplicate email
                Action duplicateInsert = () =>
                {
                    cmd.CommandText = @"
                        INSERT INTO Users (Username, PasswordHash, Email, FullName, Role) 
                        VALUES ('newuser', 'hash', 'admin@test.com', 'New User', 'User');";
                    cmd.ExecuteNonQuery();
                };
                
                duplicateInsert.Should().Throw<SQLiteException>("Email addresses should be unique");
            }
        }

        [TestMethod]
        public void Inventory_ShouldHaveUniqueProductIds()
        {
            // Act & Assert
            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();
                
                var cmd = connection.CreateCommand();
                
                // Try to insert duplicate inventory for same product
                Action duplicateInsert = () =>
                {
                    cmd.CommandText = "INSERT INTO Inventory (ProductID, Quantity) VALUES (1, 50);";
                    cmd.ExecuteNonQuery();
                };
                
                duplicateInsert.Should().Throw<SQLiteException>("Each product should have only one inventory record");
            }
        }

        #endregion

        #region Database Performance Tests

        [TestMethod]
        public void DatabaseQuery_ShouldCompleteWithinTimeLimit()
        {
            // Arrange
            var startTime = DateTime.Now;
            
            // Act
            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();
                
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"
                    SELECT i.*, p.Name as ProductName, c.Name as CategoryName, p.Price
                    FROM Inventory i
                    INNER JOIN Products p ON i.ProductID = p.ProductID
                    INNER JOIN Categories c ON p.CategoryID = c.CategoryID;";
                
                var reader = cmd.ExecuteReader();
                var recordCount = 0;
                
                while (reader.Read())
                {
                    recordCount++;
                }
                
                reader.Close();
            }
            
            // Assert
            var endTime = DateTime.Now;
            var executionTime = endTime - startTime;
            
            executionTime.Should().BeLessThan(TimeSpan.FromSeconds(1), 
                "Complex join query should complete within 1 second");
        }

        #endregion

        #region Test Data Validation

        [TestMethod]
        public void TestData_ShouldHaveValidRelationships()
        {
            // Act & Assert
            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();
                
                // Check that all products have valid categories
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"
                    SELECT COUNT(*) 
                    FROM Products p 
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID 
                    WHERE c.CategoryID IS NULL;";
                
                var orphanedProducts = Convert.ToInt32(cmd.ExecuteScalar());
                orphanedProducts.Should().Be(0, "All products should have valid categories");
                
                // Check that all inventory has valid products
                cmd.CommandText = @"
                    SELECT COUNT(*) 
                    FROM Inventory i 
                    LEFT JOIN Products p ON i.ProductID = p.ProductID 
                    WHERE p.ProductID IS NULL;";
                
                var orphanedInventory = Convert.ToInt32(cmd.ExecuteScalar());
                orphanedInventory.Should().Be(0, "All inventory should have valid products");
            }
        }

        [TestMethod]
        public void TestData_ShouldHaveReasonableValues()
        {
            // Act & Assert
            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();
                
                var cmd = connection.CreateCommand();
                
                // Check that prices are positive
                cmd.CommandText = "SELECT COUNT(*) FROM Products WHERE Price <= 0;";
                var negativeOrZeroPrices = Convert.ToInt32(cmd.ExecuteScalar());
                negativeOrZeroPrices.Should().Be(0, "All product prices should be positive");
                
                // Check that quantities are non-negative
                cmd.CommandText = "SELECT COUNT(*) FROM Inventory WHERE Quantity < 0;";
                var negativeQuantities = Convert.ToInt32(cmd.ExecuteScalar());
                negativeQuantities.Should().Be(0, "All inventory quantities should be non-negative");
                
                // Check that minimum stock levels are positive
                cmd.CommandText = "SELECT COUNT(*) FROM Products WHERE MinimumStockLevel <= 0;";
                var invalidMinStockLevels = Convert.ToInt32(cmd.ExecuteScalar());
                invalidMinStockLevels.Should().Be(0, "All minimum stock levels should be positive");
            }
        }

        #endregion
    }
} 