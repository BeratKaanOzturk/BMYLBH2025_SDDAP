using Microsoft.VisualStudio.TestTools.UnitTesting;
using BMYLBH2025_SDDAP.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMYLBH2025_SDDAP.Tests.Repositories
{
    [TestClass]
    public class InventoryRepositoryTests : TestBase
    {
        private InventoryRepository _inventoryRepository;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _inventoryRepository = new InventoryRepository(ConnectionFactory);
        }

        #region CRUD Operations Tests

        [TestMethod]
        public void GetAll_ShouldReturnAllInventoryItems()
        {
            // Act
            var result = _inventoryRepository.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(4); // Based on test data seeding
            result.All(i => i.ProductID > 0).Should().BeTrue();
            result.All(i => i.Quantity >= 0).Should().BeTrue();
            
            // Verify navigation properties are loaded
            result.All(i => !string.IsNullOrEmpty(i.ProductName)).Should().BeTrue();
            result.All(i => !string.IsNullOrEmpty(i.CategoryName)).Should().BeTrue();
        }

        [TestMethod]
        public void GetById_WithValidId_ShouldReturnInventoryItem()
        {
            // Arrange
            var inventoryId = 1;

            // Act
            var result = _inventoryRepository.GetById(inventoryId);

            // Assert
            result.Should().NotBeNull();
            result.InventoryID.Should().Be(inventoryId);
            result.ProductID.Should().BeGreaterThan(0);
            result.Quantity.Should().BeGreaterOrEqualTo(0);
            result.LastUpdated.Should().BeBefore(DateTime.Now);
            
            // Verify navigation properties
            result.ProductName.Should().NotBeNullOrEmpty();
            result.CategoryName.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void GetById_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var invalidId = 99999;

            // Act
            var result = _inventoryRepository.GetById(invalidId);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void GetByProductId_WithValidProductId_ShouldReturnInventoryItem()
        {
            // Arrange
            var productId = 1;

            // Act
            var result = _inventoryRepository.GetByProductId(productId);

            // Assert
            result.Should().NotBeNull();
            result.ProductID.Should().Be(productId);
            result.Quantity.Should().BeGreaterOrEqualTo(0);
            result.ProductName.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void GetByProductId_WithInvalidProductId_ShouldReturnNull()
        {
            // Arrange
            var invalidProductId = 99999;

            // Act
            var result = _inventoryRepository.GetByProductId(invalidProductId);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Create_WithValidInventory_ShouldCreateSuccessfully()
        {
            // Arrange
            var newInventory = MockData.CreateTestInventory(productId: 5, quantity: 25);

            // Act
            var result = _inventoryRepository.Create(newInventory);

            // Assert
            result.Should().BeTrue();
            
            // Verify the item was created
            var createdItem = _inventoryRepository.GetByProductId(5);
            createdItem.Should().NotBeNull();
            createdItem.ProductID.Should().Be(5);
            createdItem.Quantity.Should().Be(25);
        }

        [TestMethod]
        public void Create_WithNullInventory_ShouldThrowException()
        {
            // Act & Assert
            AssertThrows<ArgumentNullException>(() => _inventoryRepository.Create(null));
        }

        [TestMethod]
        public void Create_WithDuplicateProductId_ShouldReturnFalse()
        {
            // Arrange - Product ID 1 already exists in test data
            var duplicateInventory = MockData.CreateTestInventory(productId: 1, quantity: 50);

            // Act
            var result = _inventoryRepository.Create(duplicateInventory);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void Update_WithValidInventory_ShouldUpdateSuccessfully()
        {
            // Arrange
            var existingInventory = _inventoryRepository.GetById(1);
            existingInventory.Should().NotBeNull();
            
            var originalQuantity = existingInventory.Quantity;
            var newQuantity = originalQuantity + 10;
            existingInventory.Quantity = newQuantity;

            // Act
            var result = _inventoryRepository.UpdateInventory(existingInventory);

            // Assert
            result.Should().BeTrue();
            
            // Verify the update
            var updatedInventory = _inventoryRepository.GetById(1);
            updatedInventory.Quantity.Should().Be(newQuantity);
            updatedInventory.LastUpdated.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMinutes(1));
        }

        [TestMethod]
        public void Update_WithNullInventory_ShouldThrowException()
        {
            // Act & Assert
            AssertThrows<ArgumentNullException>(() => _inventoryRepository.UpdateInventory(null));
        }

        [TestMethod]
        public void Delete_WithValidId_ShouldDeleteSuccessfully()
        {
            // Arrange
            var inventoryId = 1;
            var existingInventory = _inventoryRepository.GetById(inventoryId);
            existingInventory.Should().NotBeNull();

            // Act
            var result = _inventoryRepository.DeleteInventory(inventoryId);

            // Assert
            result.Should().BeTrue();
            
            // Verify deletion
            var deletedInventory = _inventoryRepository.GetById(inventoryId);
            deletedInventory.Should().BeNull();
        }

        [TestMethod]
        public void Delete_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            var invalidId = 99999;

            // Act
            var result = _inventoryRepository.DeleteInventory(invalidId);

            // Assert
            result.Should().BeFalse();
        }

        #endregion

        #region Business Logic Tests

        [TestMethod]
        public void GetLowStockItems_ShouldReturnItemsBelowMinimumLevel()
        {
            // Act
            var lowStockItems = _inventoryRepository.GetLowStockItems();

            // Assert
            lowStockItems.Should().NotBeNull();
            lowStockItems.Should().HaveCountGreaterThan(0);
            
            // Verify each item is actually low stock
            foreach (var item in lowStockItems)
            {
                item.Quantity.Should().BeLessThan(item.MinimumStockLevel, 
                    $"Item {item.ProductName} should be below minimum stock level");
            }
        }

        [TestMethod]
        public void GetByCategory_WithValidCategoryId_ShouldReturnCategoryItems()
        {
            // Arrange
            var categoryId = 1; // Electronics category

            // Act
            var categoryItems = _inventoryRepository.GetByCategory(categoryId);

            // Assert
            categoryItems.Should().NotBeNull();
            categoryItems.Should().HaveCountGreaterThan(0);
            categoryItems.All(i => i.CategoryName == "Electronics").Should().BeTrue();
        }

        [TestMethod]
        public void GetByCategory_WithInvalidCategoryId_ShouldReturnEmptyList()
        {
            // Arrange
            var invalidCategoryId = 99999;

            // Act
            var result = _inventoryRepository.GetByCategory(invalidCategoryId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void UpdateStock_WithValidData_ShouldUpdateQuantity()
        {
            // Arrange
            var productId = 1;
            var originalInventory = _inventoryRepository.GetByProductId(productId);
            var originalQuantity = originalInventory.Quantity;
            var quantityChange = 5;

            // Act
            var result = _inventoryRepository.UpdateStock(productId, quantityChange);

            // Assert
            result.Should().BeTrue();
            
            // Verify the stock update
            var updatedInventory = _inventoryRepository.GetByProductId(productId);
            updatedInventory.Quantity.Should().Be(originalQuantity + quantityChange);
            updatedInventory.LastUpdated.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMinutes(1));
        }

        [TestMethod]
        public void UpdateStock_WithNegativeChange_ShouldDecreaseQuantity()
        {
            // Arrange
            var productId = 2; // Has 25 items in test data
            var originalInventory = _inventoryRepository.GetByProductId(productId);
            var originalQuantity = originalInventory.Quantity;
            var quantityChange = -10;

            // Act
            var result = _inventoryRepository.UpdateStock(productId, quantityChange);

            // Assert
            result.Should().BeTrue();
            
            // Verify the stock decrease
            var updatedInventory = _inventoryRepository.GetByProductId(productId);
            updatedInventory.Quantity.Should().Be(originalQuantity + quantityChange);
        }

        [TestMethod]
        public void UpdateStock_WithInvalidProductId_ShouldReturnFalse()
        {
            // Arrange
            var invalidProductId = 99999;
            var quantityChange = 10;

            // Act
            var result = _inventoryRepository.UpdateStock(invalidProductId, quantityChange);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void GetTotalInventoryValue_ShouldReturnCorrectSum()
        {
            // Act
            var totalValue = _inventoryRepository.GetTotalInventoryValue();

            // Assert
            totalValue.Should().BeGreaterThan(0);
            
            // Verify calculation by getting all items and calculating manually
            var allItems = _inventoryRepository.GetAll();
            var expectedTotal = allItems.Sum(i => i.Quantity * i.Price);
            
            totalValue.Should().Be(expectedTotal);
        }

        #endregion

        #region Edge Cases and Error Handling

        [TestMethod]
        public void Create_WithNegativeQuantity_ShouldCreateWithZero()
        {
            // Arrange
            var inventoryWithNegativeQuantity = MockData.CreateTestInventory(productId: 6, quantity: -5);

            // Act
            var result = _inventoryRepository.Create(inventoryWithNegativeQuantity);

            // Assert
            result.Should().BeTrue();
            
            // Verify quantity is handled properly (should be 0 or rejected)
            var createdItem = _inventoryRepository.GetByProductId(6);
            createdItem.Should().NotBeNull();
            createdItem.Quantity.Should().BeGreaterOrEqualTo(0);
        }

        [TestMethod]
        public void UpdateStock_ThatWouldMakeQuantityNegative_ShouldHandleGracefully()
        {
            // Arrange
            var productId = 4; // Has only 2 items in test data
            var originalInventory = _inventoryRepository.GetByProductId(productId);
            var originalQuantity = originalInventory.Quantity;
            var largeNegativeChange = -100; // Would make quantity negative

            // Act
            var result = _inventoryRepository.UpdateStock(productId, largeNegativeChange);

            // Assert - Behavior depends on business rules
            // Either it should fail (return false) or set to 0
            if (result)
            {
                var updatedInventory = _inventoryRepository.GetByProductId(productId);
                updatedInventory.Quantity.Should().BeGreaterOrEqualTo(0);
            }
            else
            {
                // Stock update was rejected due to business rules
                var unchangedInventory = _inventoryRepository.GetByProductId(productId);
                unchangedInventory.Quantity.Should().Be(originalQuantity);
            }
        }

        [TestMethod]
        public void GetAll_WithEmptyDatabase_ShouldReturnEmptyList()
        {
            // Arrange - Clear all test data
            DbHelper.ClearTestData();

            // Act
            var result = _inventoryRepository.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        #endregion

        #region Performance Tests

        [TestMethod]
        public void GetAll_ShouldCompleteWithinTimeLimit()
        {
            // Arrange
            var startTime = DateTime.Now;

            // Act
            var result = _inventoryRepository.GetAll();

            // Assert
            var endTime = DateTime.Now;
            var executionTime = endTime - startTime;
            
            result.Should().NotBeNull();
            executionTime.Should().BeLessThan(TimeSpan.FromSeconds(2), 
                "GetAll operation should complete within 2 seconds");
        }

        [TestMethod]
        public void GetLowStockItems_ShouldCompleteWithinTimeLimit()
        {
            // Arrange
            var startTime = DateTime.Now;

            // Act
            var result = _inventoryRepository.GetLowStockItems();

            // Assert
            var endTime = DateTime.Now;
            var executionTime = endTime - startTime;
            
            result.Should().NotBeNull();
            executionTime.Should().BeLessThan(TimeSpan.FromSeconds(1), 
                "GetLowStockItems operation should complete within 1 second");
        }

        #endregion
    }
} 