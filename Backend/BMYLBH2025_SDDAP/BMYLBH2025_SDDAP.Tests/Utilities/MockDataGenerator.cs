using BMYLBH2025_SDDAP.Models;
using System;
using System.Collections.Generic;

namespace BMYLBH2025_SDDAP.Tests.Utilities
{
    /// <summary>
    /// Utility class for generating mock data objects for testing
    /// </summary>
    public class MockDataGenerator
    {
        private static readonly Random _random = new Random();

        #region Product Mock Data

        public Product CreateTestProduct(int? categoryId = null)
        {
            return new Product
            {
                ProductID = _random.Next(1000, 9999),
                Name = $"Test Product {_random.Next(1, 100)}",
                Description = "Test product description for unit testing",
                Price = Math.Round((decimal)(_random.NextDouble() * 1000), 2),
                MinimumStockLevel = _random.Next(5, 50),
                CategoryID = categoryId ?? _random.Next(1, 4),
                CreatedAt = DateTime.Now.AddDays(-_random.Next(1, 30)),
                UpdatedAt = DateTime.Now
            };
        }

        public List<Product> CreateTestProducts(int count = 5)
        {
            var products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                products.Add(CreateTestProduct());
            }
            return products;
        }

        #endregion

        #region Inventory Mock Data

        public Inventory CreateTestInventory(int? productId = null, int? quantity = null)
        {
            return new Inventory
            {
                InventoryID = _random.Next(1000, 9999),
                ProductID = productId ?? _random.Next(1, 100),
                Quantity = quantity ?? _random.Next(0, 100),
                LastUpdated = DateTime.Now.AddHours(-_random.Next(1, 24))
            };
        }

        public List<Inventory> CreateTestInventoryItems(int count = 5)
        {
            var inventoryItems = new List<Inventory>();
            for (int i = 0; i < count; i++)
            {
                inventoryItems.Add(CreateTestInventory(i + 1));
            }
            return inventoryItems;
        }

        public Inventory CreateLowStockInventory(int productId, int lowQuantity = 2)
        {
            return new Inventory
            {
                InventoryID = _random.Next(1000, 9999),
                ProductID = productId,
                Quantity = lowQuantity,
                LastUpdated = DateTime.Now
            };
        }

        #endregion

        #region Category Mock Data

        public Category CreateTestCategory()
        {
            var categories = new[] { "Electronics", "Office Supplies", "Furniture", "Books", "Tools" };
            var categoryName = categories[_random.Next(categories.Length)];
            
            return new Category
            {
                CategoryID = _random.Next(1000, 9999),
                Name = $"{categoryName} {_random.Next(1, 100)}",
                Description = $"Test category for {categoryName.ToLower()}",
                CreatedAt = DateTime.Now.AddDays(-_random.Next(1, 30)),
                UpdatedAt = DateTime.Now
            };
        }

        public List<Category> CreateTestCategories(int count = 3)
        {
            var categories = new List<Category>();
            for (int i = 0; i < count; i++)
            {
                categories.Add(CreateTestCategory());
            }
            return categories;
        }

        #endregion

        #region User Mock Data

        public User CreateTestUser()
        {
            var userNumber = _random.Next(1, 1000);
            return new User
            {
                UserID = _random.Next(1000, 9999),
                Username = $"testuser{userNumber}",
                PasswordHash = "$2a$11$7Q.gWaI8eKQvJ5xQg5qjLek1yoKdC3F3YqVx8eqIy5Z2Q6GxJ8cK6", // BCrypt hash for "password123"
                Email = $"testuser{userNumber}@test.com",
                FullName = $"Test User {userNumber}",
                Role = "User",
                IsActive = true,
                IsEmailVerified = true,
                CreatedAt = DateTime.Now.AddDays(-_random.Next(1, 30)),
                UpdatedAt = DateTime.Now
            };
        }

        public User CreateTestAdmin()
        {
            var adminNumber = _random.Next(1, 100);
            return new User
            {
                UserID = _random.Next(1000, 9999),
                Username = $"admin{adminNumber}",
                PasswordHash = "$2a$11$7Q.gWaI8eKQvJ5xQg5qjLek1yoKdC3F3YqVx8eqIy5Z2Q6GxJ8cK6",
                Email = $"admin{adminNumber}@test.com",
                FullName = $"Test Administrator {adminNumber}",
                Role = "Admin",
                IsActive = true,
                IsEmailVerified = true,
                CreatedAt = DateTime.Now.AddDays(-_random.Next(1, 30)),
                UpdatedAt = DateTime.Now
            };
        }

        #endregion

        #region Notification Mock Data

        public Notification CreateTestNotification(int? userId = null)
        {
            var notificationTypes = new[] { NotificationType.LowStock, NotificationType.NewOrder, NotificationType.OrderStatusUpdate, NotificationType.SystemAlert };
            var priorities = new[] { NotificationPriority.Low, NotificationPriority.Medium, NotificationPriority.High, NotificationPriority.Critical };
            
            return new Notification
            {
                NotificationID = _random.Next(1000, 9999),
                UserID = userId ?? _random.Next(1, 10),
                Message = $"Test notification message {_random.Next(1, 100)}",
                Type = notificationTypes[_random.Next(notificationTypes.Length)],
                Priority = priorities[_random.Next(priorities.Length)],
                Date = DateTime.Now.AddHours(-_random.Next(1, 72)),
                IsRead = _random.Next(0, 2) == 1
            };
        }

        public List<Notification> CreateTestNotifications(int count = 5, int? userId = null)
        {
            var notifications = new List<Notification>();
            for (int i = 0; i < count; i++)
            {
                notifications.Add(CreateTestNotification(userId));
            }
            return notifications;
        }

        #endregion

        #region Supplier Mock Data

        public Supplier CreateTestSupplier()
        {
            var supplierNumber = _random.Next(1, 1000);
            return new Supplier
            {
                SupplierID = _random.Next(1000, 9999),
                Name = $"Test Supplier {supplierNumber} Ltd",
                ContactPerson = $"Contact Person {supplierNumber}",
                Email = $"contact{supplierNumber}@supplier.com",
                Phone = $"+1-555-{_random.Next(1000, 9999)}",
                Address = $"{_random.Next(100, 999)} Test Street, Test City, TC"
            };
        }

        #endregion

        #region Utility Methods

        public string GenerateRandomString(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[_random.Next(chars.Length)];
            }
            return new string(result);
        }

        public decimal GenerateRandomPrice(decimal min = 1.00m, decimal max = 1000.00m)
        {
            var range = max - min;
            var randomDecimal = (decimal)_random.NextDouble();
            return Math.Round(min + (range * randomDecimal), 2);
        }

        public DateTime GenerateRandomDate(int daysBack = 30)
        {
            return DateTime.Now.AddDays(-_random.Next(1, daysBack + 1));
        }

        #endregion
    }
} 