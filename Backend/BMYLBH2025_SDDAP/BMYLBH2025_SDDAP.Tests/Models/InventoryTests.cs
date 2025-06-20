using Microsoft.VisualStudio.TestTools.UnitTesting;
using BMYLBH2025_SDDAP.Models;
using FluentAssertions;
using System;

namespace BMYLBH2025_SDDAP.Tests.Models
{
    [TestClass]
    public class InventoryTests : TestBase
    {
        #region Business Logic Tests

        [TestMethod]
        public void IsLowStock_WithQuantityBelowMinimum_ShouldReturnTrue()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory(quantity: 5);
            inventory.MinimumStockLevel = 10;

            // Act
            var result = inventory.IsLowStock();

            // Assert
            result.Should().BeTrue("Quantity (5) is below minimum stock level (10)");
        }

        [TestMethod]
        public void IsLowStock_WithQuantityAtMinimum_ShouldReturnFalse()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory(quantity: 10);
            inventory.MinimumStockLevel = 10;

            // Act
            var result = inventory.IsLowStock();

            // Assert
            result.Should().BeFalse("Quantity equals minimum stock level");
        }

        [TestMethod]
        public void IsLowStock_WithQuantityAboveMinimum_ShouldReturnFalse()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory(quantity: 15);
            inventory.MinimumStockLevel = 10;

            // Act
            var result = inventory.IsLowStock();

            // Assert
            result.Should().BeFalse("Quantity (15) is above minimum stock level (10)");
        }

        [TestMethod]
        public void CalculateStockValue_WithValidData_ShouldReturnCorrectValue()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory(quantity: 10);
            inventory.Price = 25.50m;

            // Act
            var result = inventory.CalculateStockValue();

            // Assert
            var expectedValue = 10 * 25.50m;
            result.Should().Be(expectedValue, "Stock value should be quantity × price");
        }

        [TestMethod]
        public void CalculateStockValue_WithZeroQuantity_ShouldReturnZero()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory(quantity: 0);
            inventory.Price = 25.50m;

            // Act
            var result = inventory.CalculateStockValue();

            // Assert
            result.Should().Be(0, "Zero quantity should result in zero value");
        }

        [TestMethod]
        public void CalculateStockValue_WithZeroPrice_ShouldReturnZero()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory(quantity: 10);
            inventory.Price = 0;

            // Act
            var result = inventory.CalculateStockValue();

            // Assert
            result.Should().Be(0, "Zero price should result in zero value");
        }

        [TestMethod]
        public void GetStockStatus_WithLowStock_ShouldReturnLowStock()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory(quantity: 2);
            inventory.MinimumStockLevel = 10;

            // Act
            var result = inventory.GetStockStatus();

            // Assert
            result.Should().Be("Low Stock", "Should indicate low stock when below minimum");
        }

        [TestMethod]
        public void GetStockStatus_WithNormalStock_ShouldReturnInStock()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory(quantity: 15);
            inventory.MinimumStockLevel = 10;

            // Act
            var result = inventory.GetStockStatus();

            // Assert
            result.Should().Be("In Stock", "Should indicate in stock when above minimum");
        }

        [TestMethod]
        public void GetStockStatus_WithZeroQuantity_ShouldReturnOutOfStock()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory(quantity: 0);
            inventory.MinimumStockLevel = 10;

            // Act
            var result = inventory.GetStockStatus();

            // Assert
            result.Should().Be("Out of Stock", "Should indicate out of stock when quantity is zero");
        }

        #endregion

        #region Property Validation Tests

        [TestMethod]
        public void Quantity_ShouldNotBeNegative()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory();

            // Act & Assert
            Action setNegativeQuantity = () => inventory.Quantity = -5;
            
            // Note: This test assumes validation is implemented in the setter
            // If validation is not in the model, this test documents expected behavior
            if (inventory.Quantity >= 0)
            {
                // Current implementation allows negative quantities
                // This test documents that we should add validation
                inventory.Quantity = -5;
                inventory.Quantity.Should().BeGreaterOrEqualTo(0, 
                    "Quantity should be validated to prevent negative values");
            }
        }

        [TestMethod]
        public void LastUpdated_ShouldBeSetWhenQuantityChanges()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory();
            var originalLastUpdated = inventory.LastUpdated;
            
            // Wait a small amount to ensure time difference
            System.Threading.Thread.Sleep(10);

            // Act
            inventory.UpdateQuantity(inventory.Quantity + 5);

            // Assert
            inventory.LastUpdated.Should().BeAfter(originalLastUpdated, 
                "LastUpdated should be updated when quantity changes");
        }

        [TestMethod]
        public void UpdateQuantity_WithValidQuantity_ShouldUpdateBothQuantityAndTimestamp()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory(quantity: 10);
            var originalLastUpdated = inventory.LastUpdated;
            var newQuantity = 15;
            
            System.Threading.Thread.Sleep(10);

            // Act
            inventory.UpdateQuantity(newQuantity);

            // Assert
            inventory.Quantity.Should().Be(newQuantity, "Quantity should be updated");
            inventory.LastUpdated.Should().BeAfter(originalLastUpdated, 
                "LastUpdated timestamp should be updated");
        }

        #endregion

        #region Navigation Properties Tests

        [TestMethod]
        public void ProductName_ShouldBePopulatedFromNavigationProperty()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory();
            inventory.ProductName = "Test Product Name";

            // Act & Assert
            inventory.ProductName.Should().NotBeNullOrEmpty("ProductName should be populated");
            inventory.ProductName.Should().Be("Test Product Name");
        }

        [TestMethod]
        public void CategoryName_ShouldBePopulatedFromNavigationProperty()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory();
            inventory.CategoryName = "Test Category";

            // Act & Assert
            inventory.CategoryName.Should().NotBeNullOrEmpty("CategoryName should be populated");
            inventory.CategoryName.Should().Be("Test Category");
        }

        [TestMethod]
        public void Price_ShouldBePopulatedFromNavigationProperty()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory();
            inventory.Price = 99.99m;

            // Act & Assert
            inventory.Price.Should().BeGreaterThan(0, "Price should be positive");
            inventory.Price.Should().Be(99.99m);
        }

        #endregion

        #region Edge Cases Tests

        [TestMethod]
        public void CalculateStockValue_WithLargeNumbers_ShouldHandleCorrectly()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory(quantity: 999999);
            inventory.Price = 999.99m;

            // Act
            var result = inventory.CalculateStockValue();

            // Assert
            var expectedValue = 999999 * 999.99m;
            result.Should().Be(expectedValue, "Should handle large calculations correctly");
            result.Should().BeGreaterThan(0, "Result should be positive");
        }

        [TestMethod]
        public void IsLowStock_WithZeroMinimumStockLevel_ShouldHandleGracefully()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory(quantity: 5);
            inventory.MinimumStockLevel = 0;

            // Act
            var result = inventory.IsLowStock();

            // Assert
            result.Should().BeFalse("Any positive quantity should not be low stock when minimum is 0");
        }

        [TestMethod]
        public void GetStockStatus_WithEqualQuantityAndMinimum_ShouldReturnInStock()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory(quantity: 10);
            inventory.MinimumStockLevel = 10;

            // Act
            var result = inventory.GetStockStatus();

            // Assert
            result.Should().Be("In Stock", "Equal quantity and minimum should be considered in stock");
        }

        #endregion

        #region Business Rules Tests

        [TestMethod]
        public void StockLevel_ShouldFollowBusinessRules()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory();

            // Act & Assert
            // Business Rule: Minimum stock level should be positive
            inventory.MinimumStockLevel.Should().BeGreaterThan(0, 
                "Minimum stock level should be positive for business operations");

            // Business Rule: Product ID should be valid
            inventory.ProductID.Should().BeGreaterThan(0, 
                "Product ID should be a valid positive integer");

            // Business Rule: Inventory ID should be unique and positive
            inventory.InventoryID.Should().BeGreaterOrEqualTo(0, 
                "Inventory ID should be non-negative");
        }

        [TestMethod]
        public void TimestampValidation_ShouldFollowBusinessRules()
        {
            // Arrange
            var inventory = MockData.CreateTestInventory();

            // Act & Assert
            // Business Rule: LastUpdated should not be in the future
            inventory.LastUpdated.Should().BeBefore(DateTime.Now.AddMinutes(1), 
                "LastUpdated should not be in the future");

            // Business Rule: LastUpdated should be reasonable (not too old)
            inventory.LastUpdated.Should().BeAfter(DateTime.Now.AddYears(-1), 
                "LastUpdated should be within reasonable timeframe");
        }

        #endregion
    }
} 