using Microsoft.VisualStudio.TestTools.UnitTesting;
using BMYLBH2025_SDDAP.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMYLBH2025_SDDAP.Tests.Models
{
    [TestClass]
    public class CategoryTests : TestBase
    {
        #region Basic Property Tests

        [TestMethod]
        public void Name_ShouldNotBeNullOrEmpty()
        {
            // Arrange
            var category = MockData.CreateTestCategory();

            // Act & Assert
            category.Name.Should().NotBeNullOrEmpty("Category name should be populated");
        }

        [TestMethod]
        public void CategoryID_ShouldBePositive()
        {
            // Arrange
            var category = MockData.CreateTestCategory();

            // Act & Assert
            category.CategoryID.Should().BeGreaterThan(0, "Category ID should be positive");
        }

        [TestMethod]
        public void CreatedAt_ShouldBeReasonable()
        {
            // Arrange
            var category = MockData.CreateTestCategory();

            // Act & Assert
            category.CreatedAt.Should().BeBefore(DateTime.Now.AddMinutes(1), "CreatedAt should not be in the future");
            category.CreatedAt.Should().BeAfter(DateTime.Now.AddYears(-1), "CreatedAt should be within reasonable timeframe");
        }

        [TestMethod]
        public void UpdatedAt_ShouldBeAfterCreatedAt()
        {
            // Arrange
            var category = MockData.CreateTestCategory();

            // Act & Assert
            category.UpdatedAt.Should().BeOnOrAfter(category.CreatedAt, "UpdatedAt should be on or after CreatedAt");
        }

        #endregion

        #region Business Logic Tests

        [TestMethod]
        public void IsValidCategory_WithValidData_ShouldReturnTrue()
        {
            // Arrange
            var category = MockData.CreateTestCategory();
            category.Name = "Valid Category";

            // Act
            var result = category.IsValidCategory();

            // Assert
            result.Should().BeTrue("Valid category data should pass validation");
        }

        [TestMethod]
        public void IsValidCategory_WithEmptyName_ShouldReturnFalse()
        {
            // Arrange
            var category = MockData.CreateTestCategory();
            category.Name = "";

            // Act
            var result = category.IsValidCategory();

            // Assert
            result.Should().BeFalse("Empty category name should fail validation");
        }

        [TestMethod]
        public void IsValidCategory_WithNullName_ShouldReturnFalse()
        {
            // Arrange
            var category = MockData.CreateTestCategory();
            category.Name = null;

            // Act
            var result = category.IsValidCategory();

            // Assert
            result.Should().BeFalse("Null category name should fail validation");
        }

        #endregion

        #region Product Collection Tests

        [TestMethod]
        public void GetProductCount_WithProducts_ShouldReturnCorrectCount()
        {
            // Arrange
            var category = MockData.CreateTestCategory();
            var products = MockData.CreateTestProducts(3);
            
            // Simulate products in this category
            category.Products = products;

            // Act
            var result = category.GetProductCount();

            // Assert
            result.Should().Be(3, "Should return the correct number of products in category");
        }

        [TestMethod]
        public void GetProductCount_WithNoProducts_ShouldReturnZero()
        {
            // Arrange
            var category = MockData.CreateTestCategory();
            category.Products = new List<Product>();

            // Act
            var result = category.GetProductCount();

            // Assert
            result.Should().Be(0, "Should return zero when no products in category");
        }

        [TestMethod]
        public void GetProductCount_WithNullProducts_ShouldReturnZero()
        {
            // Arrange
            var category = MockData.CreateTestCategory();
            category.Products = null;

            // Act
            var result = category.GetProductCount();

            // Assert
            result.Should().Be(0, "Should return zero when products collection is null");
        }

        #endregion
    }
}
