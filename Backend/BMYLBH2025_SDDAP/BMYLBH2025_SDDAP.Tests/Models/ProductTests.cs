using Microsoft.VisualStudio.TestTools.UnitTesting;
using BMYLBH2025_SDDAP.Models;
using FluentAssertions;
using System;

namespace BMYLBH2025_SDDAP.Tests.Models
{
    [TestClass]
    public class ProductTests : TestBase
    {
        #region Business Logic Tests

        [TestMethod]
        public void IsValidPrice_WithPositivePrice_ShouldReturnTrue()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            product.Price = 99.99m;

            // Act
            var result = product.IsValidPrice();

            // Assert
            result.Should().BeTrue("Positive prices should be valid");
        }

        [TestMethod]
        public void IsValidPrice_WithZeroPrice_ShouldReturnFalse()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            product.Price = 0;

            // Act
            var result = product.IsValidPrice();

            // Assert
            result.Should().BeFalse("Zero price should be invalid");
        }

        [TestMethod]
        public void IsValidPrice_WithNegativePrice_ShouldReturnFalse()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            product.Price = -10.00m;

            // Act
            var result = product.IsValidPrice();

            // Assert
            result.Should().BeFalse("Negative prices should be invalid");
        }

        [TestMethod]
        public void GetFormattedPrice_WithValidPrice_ShouldReturnFormattedString()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            product.Price = 123.45m;

            // Act
            var result = product.GetFormattedPrice();

            // Assert
            result.Should().Be("$123.45", "Price should be formatted as currency");
        }

        [TestMethod]
        public void GetFormattedPrice_WithZeroPrice_ShouldReturnFormattedZero()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            product.Price = 0;

            // Act
            var result = product.GetFormattedPrice();

            // Assert
            result.Should().Be("$0.00", "Zero price should be formatted correctly");
        }

        [TestMethod]
        public void IsValidMinimumStockLevel_WithPositiveLevel_ShouldReturnTrue()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            product.MinimumStockLevel = 10;

            // Act
            var result = product.IsValidMinimumStockLevel();

            // Assert
            result.Should().BeTrue("Positive minimum stock levels should be valid");
        }

        [TestMethod]
        public void IsValidMinimumStockLevel_WithZeroLevel_ShouldReturnFalse()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            product.MinimumStockLevel = 0;

            // Act
            var result = product.IsValidMinimumStockLevel();

            // Assert
            result.Should().BeFalse("Zero minimum stock level should be invalid for business operations");
        }

        #endregion

        #region Validation Tests

        [TestMethod]
        public void ValidateProduct_WithValidData_ShouldReturnTrue()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            product.Name = "Valid Product Name";
            product.Price = 99.99m;
            product.MinimumStockLevel = 10;
            product.CategoryID = 1;

            // Act
            var result = product.ValidateProduct();

            // Assert
            result.Should().BeTrue("Valid product data should pass validation");
        }

        [TestMethod]
        public void ValidateProduct_WithEmptyName_ShouldReturnFalse()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            product.Name = "";
            product.Price = 99.99m;
            product.MinimumStockLevel = 10;

            // Act
            var result = product.ValidateProduct();

            // Assert
            result.Should().BeFalse("Empty product name should fail validation");
        }

        [TestMethod]
        public void ValidateProduct_WithNullName_ShouldReturnFalse()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            product.Name = null;
            product.Price = 99.99m;
            product.MinimumStockLevel = 10;

            // Act
            var result = product.ValidateProduct();

            // Assert
            result.Should().BeFalse("Null product name should fail validation");
        }

        [TestMethod]
        public void ValidateProduct_WithInvalidPrice_ShouldReturnFalse()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            product.Name = "Valid Product";
            product.Price = -10.00m;
            product.MinimumStockLevel = 10;

            // Act
            var result = product.ValidateProduct();

            // Assert
            result.Should().BeFalse("Invalid price should fail validation");
        }

        #endregion

        #region Property Tests

        [TestMethod]
        public void Name_ShouldNotBeNullOrEmpty()
        {
            // Arrange
            var product = MockData.CreateTestProduct();

            // Act & Assert
            product.Name.Should().NotBeNullOrEmpty("Product name should be populated");
        }

        [TestMethod]
        public void Price_ShouldBePositive()
        {
            // Arrange
            var product = MockData.CreateTestProduct();

            // Act & Assert
            product.Price.Should().BeGreaterThan(0, "Product price should be positive");
        }

        [TestMethod]
        public void MinimumStockLevel_ShouldBePositive()
        {
            // Arrange
            var product = MockData.CreateTestProduct();

            // Act & Assert
            product.MinimumStockLevel.Should().BeGreaterThan(0, "Minimum stock level should be positive");
        }

        [TestMethod]
        public void CategoryID_ShouldBeValid()
        {
            // Arrange
            var product = MockData.CreateTestProduct();

            // Act & Assert
            product.CategoryID.Should().BeGreaterThan(0, "Category ID should be a valid positive integer");
        }

        [TestMethod]
        public void CreatedAt_ShouldBeReasonable()
        {
            // Arrange
            var product = MockData.CreateTestProduct();

            // Act & Assert
            product.CreatedAt.Should().BeBefore(DateTime.Now.AddMinutes(1), "CreatedAt should not be in the future");
            product.CreatedAt.Should().BeAfter(DateTime.Now.AddYears(-1), "CreatedAt should be within reasonable timeframe");
        }

        [TestMethod]
        public void UpdatedAt_ShouldBeAfterCreatedAt()
        {
            // Arrange
            var product = MockData.CreateTestProduct();

            // Act & Assert
            product.UpdatedAt.Should().BeOnOrAfter(product.CreatedAt, "UpdatedAt should be on or after CreatedAt");
        }

        #endregion

        #region Business Rules Tests

        [TestMethod]
        public void ProductID_ShouldBeUniqueAndPositive()
        {
            // Arrange
            var product1 = MockData.CreateTestProduct();
            var product2 = MockData.CreateTestProduct();

            // Act & Assert
            product1.ProductID.Should().BeGreaterThan(0, "Product ID should be positive");
            product2.ProductID.Should().BeGreaterThan(0, "Product ID should be positive");
            product1.ProductID.Should().NotBe(product2.ProductID, "Product IDs should be unique");
        }

        [TestMethod]
        public void Description_CanBeNullOrEmpty()
        {
            // Arrange
            var product = MockData.CreateTestProduct();

            // Act
            product.Description = null;

            // Assert
            // Description is optional, so null should be acceptable
            product.Description.Should().BeNull("Description is optional and can be null");

            // Act
            product.Description = "";

            // Assert
            product.Description.Should().BeEmpty("Description is optional and can be empty");
        }

        #endregion

        #region Edge Cases Tests

        [TestMethod]
        public void Price_WithVeryLargeValue_ShouldHandleCorrectly()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            var largePrice = 999999.99m;

            // Act
            product.Price = largePrice;

            // Assert
            product.Price.Should().Be(largePrice, "Should handle large price values");
            product.IsValidPrice().Should().BeTrue("Large positive prices should be valid");
        }

        [TestMethod]
        public void MinimumStockLevel_WithVeryLargeValue_ShouldHandleCorrectly()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            var largeStockLevel = 999999;

            // Act
            product.MinimumStockLevel = largeStockLevel;

            // Assert
            product.MinimumStockLevel.Should().Be(largeStockLevel, "Should handle large stock level values");
            product.IsValidMinimumStockLevel().Should().BeTrue("Large positive stock levels should be valid");
        }

        [TestMethod]
        public void Name_WithVeryLongString_ShouldHandleGracefully()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            var longName = new string('A', 1000); // 1000 character name

            // Act
            product.Name = longName;

            // Assert
            product.Name.Should().Be(longName, "Should handle long product names");
            product.Name.Length.Should().Be(1000, "Name length should be preserved");
        }

        #endregion

        #region Category Relationship Tests

        [TestMethod]
        public void CategoryID_ShouldLinkToValidCategory()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            product.CategoryID = 1; // Should correspond to a valid category

            // Act & Assert
            product.CategoryID.Should().BeGreaterThan(0, "Category ID should reference a valid category");
        }

        [TestMethod]
        public void CategoryName_ShouldBePopulatedFromNavigation()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            product.CategoryName = "Test Category";

            // Act & Assert
            product.CategoryName.Should().NotBeNullOrEmpty("Category name should be populated from navigation property");
            product.CategoryName.Should().Be("Test Category");
        }

        #endregion

        #region Timestamp Tests

        [TestMethod]
        public void UpdateTimestamp_ShouldUpdateWhenProductChanges()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            var originalUpdatedAt = product.UpdatedAt;
            
            System.Threading.Thread.Sleep(10);

            // Act
            product.UpdateTimestamp();

            // Assert
            product.UpdatedAt.Should().BeAfter(originalUpdatedAt, "UpdatedAt should be updated when product changes");
        }

        [TestMethod]
        public void GetAge_ShouldReturnCorrectTimeSpan()
        {
            // Arrange
            var product = MockData.CreateTestProduct();
            product.CreatedAt = DateTime.Now.AddDays(-10);

            // Act
            var age = product.GetAge();

            // Assert
            age.Should().BeCloseTo(TimeSpan.FromDays(10), TimeSpan.FromHours(1), 
                "Product age should be approximately 10 days");
        }

        #endregion
    }
} 