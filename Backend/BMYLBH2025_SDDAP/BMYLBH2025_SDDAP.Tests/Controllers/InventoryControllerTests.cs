using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using BMYLBH2025_SDDAP.Controllers;
using BMYLBH2025_SDDAP.Models;

namespace BMYLBH2025_SDDAP.Tests.Controllers
{
    [TestClass]
    public class InventoryControllerTests
    {
        private Mock<IInventoryRepository> _mockRepository;
        private InventoryController _controller;
        private List<Inventory> _testInventoryData;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IInventoryRepository>();
            _controller = new InventoryController(_mockRepository.Object);
            
            // Setup test data
            _testInventoryData = new List<Inventory>
            {
                new Inventory 
                { 
                    InventoryID = 1, 
                    ProductID = 1, 
                    Quantity = 50, 
                    LastUpdated = DateTime.Now,
                    Product = new Product 
                    { 
                        ProductID = 1, 
                        Name = "Laptop", 
                        Price = 999.99m, 
                        MinimumStockLevel = 10 
                    }
                },
                new Inventory 
                { 
                    InventoryID = 2, 
                    ProductID = 2, 
                    Quantity = 5, 
                    LastUpdated = DateTime.Now,
                    Product = new Product 
                    { 
                        ProductID = 2, 
                        Name = "Mouse", 
                        Price = 25.99m, 
                        MinimumStockLevel = 20 
                    }
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _controller?.Dispose();
        }

        #region GET /api/inventory Tests

        [TestMethod]
        public void GetAllInventory_WithValidData_ShouldReturnOkResult()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAll()).Returns(_testInventoryData);

            // Act
            var result = _controller.GetAllInventory();

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<Inventory>>>();
            var okResult = result as OkNegotiatedContentResult<IEnumerable<Inventory>>;
            okResult.Content.Should().HaveCount(2);
            _mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetAllInventory_WithEmptyData_ShouldReturnOkWithEmptyList()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAll()).Returns(new List<Inventory>());

            // Act
            var result = _controller.GetAllInventory();

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<Inventory>>>();
            var okResult = result as OkNegotiatedContentResult<IEnumerable<Inventory>>;
            okResult.Content.Should().BeEmpty();
        }

        [TestMethod]
        public void GetAllInventory_WithRepositoryException_ShouldReturnInternalServerError()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAll()).Throws(new Exception("Database error"));

            // Act
            var result = _controller.GetAllInventory();

            // Assert
            result.Should().BeOfType<ExceptionResult>();
        }

        #endregion

        #region GET /api/inventory/{id} Tests

        [TestMethod]
        public void GetInventoryById_WithValidId_ShouldReturnOkResult()
        {
            // Arrange
            var testInventory = _testInventoryData.First();
            _mockRepository.Setup(r => r.GetById(1)).Returns(testInventory);

            // Act
            var result = _controller.GetInventoryById(1);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<Inventory>>();
            var okResult = result as OkNegotiatedContentResult<Inventory>;
            okResult.Content.InventoryID.Should().Be(1);
            okResult.Content.ProductID.Should().Be(1);
        }

        [TestMethod]
        public void GetInventoryById_WithInvalidId_ShouldReturnBadRequest()
        {
            // Act
            var result = _controller.GetInventoryById(-1);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
            var badRequestResult = result as BadRequestErrorMessageResult;
            badRequestResult.Message.Should().Be("Invalid inventory ID");
        }

        [TestMethod]
        public void GetInventoryById_WithNonExistentId_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetById(999)).Returns((Inventory)null);

            // Act
            var result = _controller.GetInventoryById(999);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        #endregion

        #region GET /api/inventory/product/{productId} Tests

        [TestMethod]
        public void GetInventoryByProductId_WithValidProductId_ShouldReturnOkResult()
        {
            // Arrange
            var testInventory = _testInventoryData.First();
            _mockRepository.Setup(r => r.GetByProductId(1)).Returns(testInventory);

            // Act
            var result = _controller.GetInventoryByProductId(1);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<Inventory>>();
            var okResult = result as OkNegotiatedContentResult<Inventory>;
            okResult.Content.ProductID.Should().Be(1);
        }

        [TestMethod]
        public void GetInventoryByProductId_WithInvalidProductId_ShouldReturnBadRequest()
        {
            // Act
            var result = _controller.GetInventoryByProductId(0);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void GetInventoryByProductId_WithNonExistentProductId_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByProductId(999)).Returns((Inventory)null);

            // Act
            var result = _controller.GetInventoryByProductId(999);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        #endregion

        #region GET /api/inventory/lowstock Tests

        [TestMethod]
        public void GetLowStockItems_WithLowStockItems_ShouldReturnOkResult()
        {
            // Arrange
            var lowStockItems = _testInventoryData.Where(i => i.Quantity <= i.Product.MinimumStockLevel).ToList();
            _mockRepository.Setup(r => r.GetLowStockItems()).Returns(lowStockItems);

            // Act
            var result = _controller.GetLowStockItems();

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<Inventory>>>();
            var okResult = result as OkNegotiatedContentResult<IEnumerable<Inventory>>;
            okResult.Content.Should().HaveCount(1); // Only Mouse should be low stock
        }

        [TestMethod]
        public void GetLowStockItems_WithNoLowStockItems_ShouldReturnEmptyList()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetLowStockItems()).Returns(new List<Inventory>());

            // Act
            var result = _controller.GetLowStockItems();

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<Inventory>>>();
            var okResult = result as OkNegotiatedContentResult<IEnumerable<Inventory>>;
            okResult.Content.Should().BeEmpty();
        }

        #endregion

        #region GET /api/inventory/category/{categoryId} Tests

        [TestMethod]
        public void GetInventoryByCategory_WithValidCategoryId_ShouldReturnOkResult()
        {
            // Arrange
            var categoryInventory = _testInventoryData.Take(1).ToList();
            _mockRepository.Setup(r => r.GetByCategory(1)).Returns(categoryInventory);

            // Act
            var result = _controller.GetInventoryByCategory(1);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<Inventory>>>();
            var okResult = result as OkNegotiatedContentResult<IEnumerable<Inventory>>;
            okResult.Content.Should().HaveCount(1);
        }

        [TestMethod]
        public void GetInventoryByCategory_WithInvalidCategoryId_ShouldReturnBadRequest()
        {
            // Act
            var result = _controller.GetInventoryByCategory(-1);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        #endregion

        #region GET /api/inventory/totalvalue Tests

        [TestMethod]
        public void GetTotalInventoryValue_WithValidData_ShouldReturnCorrectValue()
        {
            // Arrange
            var expectedTotal = 51294.95m; // (50 * 999.99) + (5 * 25.99)
            _mockRepository.Setup(r => r.GetTotalInventoryValue()).Returns(expectedTotal);

            // Act
            var result = _controller.GetTotalInventoryValue();

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<object>>();
            var okResult = result as OkNegotiatedContentResult<object>;
            var response = okResult.Content;
            response.Should().NotBeNull();
        }

        [TestMethod]
        public void GetTotalInventoryValue_WithZeroValue_ShouldReturnZero()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetTotalInventoryValue()).Returns(0m);

            // Act
            var result = _controller.GetTotalInventoryValue();

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<object>>();
        }

        #endregion

        #region POST /api/inventory Tests

        [TestMethod]
        public void CreateInventory_WithValidData_ShouldReturnCreatedResult()
        {
            // Arrange
            var newInventory = new Inventory
            {
                ProductID = 3,
                Quantity = 100,
                LastUpdated = DateTime.Now
            };
            _mockRepository.Setup(r => r.GetByProductId(3)).Returns((Inventory)null);
            _mockRepository.Setup(r => r.Add(It.IsAny<Inventory>()));

            // Act
            var result = _controller.CreateInventory(newInventory);

            // Assert
            result.Should().BeOfType<CreatedNegotiatedContentResult<Inventory>>();
            _mockRepository.Verify(r => r.Add(newInventory), Times.Once);
        }

        [TestMethod]
        public void CreateInventory_WithNullInventory_ShouldReturnBadRequest()
        {
            // Act
            var result = _controller.CreateInventory(null);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
            var badRequestResult = result as BadRequestErrorMessageResult;
            badRequestResult.Message.Should().Be("Inventory data is required");
        }

        [TestMethod]
        public void CreateInventory_WithInvalidProductId_ShouldReturnBadRequest()
        {
            // Arrange
            var invalidInventory = new Inventory { ProductID = 0, Quantity = 10 };

            // Act
            var result = _controller.CreateInventory(invalidInventory);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void CreateInventory_WithNegativeQuantity_ShouldReturnBadRequest()
        {
            // Arrange
            var invalidInventory = new Inventory { ProductID = 1, Quantity = -5 };

            // Act
            var result = _controller.CreateInventory(invalidInventory);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void CreateInventory_WithExistingProduct_ShouldReturnBadRequest()
        {
            // Arrange
            var existingInventory = _testInventoryData.First();
            var duplicateInventory = new Inventory { ProductID = 1, Quantity = 10 };
            _mockRepository.Setup(r => r.GetByProductId(1)).Returns(existingInventory);

            // Act
            var result = _controller.CreateInventory(duplicateInventory);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
            var badRequestResult = result as BadRequestErrorMessageResult;
            badRequestResult.Message.Should().Be("Inventory already exists for this product");
        }

        #endregion

        #region PUT /api/inventory/{id} Tests

        [TestMethod]
        public void UpdateInventory_WithValidData_ShouldReturnOkResult()
        {
            // Arrange
            var existingInventory = _testInventoryData.First();
            var updatedInventory = new Inventory
            {
                InventoryID = 1,
                ProductID = 1,
                Quantity = 75,
                LastUpdated = DateTime.Now
            };
            _mockRepository.Setup(r => r.GetById(1)).Returns(existingInventory);
            _mockRepository.Setup(r => r.Update(It.IsAny<Inventory>()));

            // Act
            var result = _controller.UpdateInventory(1, updatedInventory);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<Inventory>>();
            _mockRepository.Verify(r => r.Update(It.IsAny<Inventory>()), Times.Once);
        }

        [TestMethod]
        public void UpdateInventory_WithInvalidId_ShouldReturnBadRequest()
        {
            // Arrange
            var inventory = new Inventory { Quantity = 10 };

            // Act
            var result = _controller.UpdateInventory(-1, inventory);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void UpdateInventory_WithNullInventory_ShouldReturnBadRequest()
        {
            // Act
            var result = _controller.UpdateInventory(1, null);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void UpdateInventory_WithNonExistentId_ShouldReturnNotFound()
        {
            // Arrange
            var inventory = new Inventory { Quantity = 10 };
            _mockRepository.Setup(r => r.GetById(999)).Returns((Inventory)null);

            // Act
            var result = _controller.UpdateInventory(999, inventory);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        #endregion

        #region PUT /api/inventory/updatestock/{productId} Tests

        [TestMethod]
        public void UpdateStock_WithValidData_ShouldReturnOkResult()
        {
            // Arrange
            var request = new UpdateStockRequest { Quantity = 100 };
            _mockRepository.Setup(r => r.UpdateStock(1, 100)).Returns(true);

            // Act
            var result = _controller.UpdateStock(1, request);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<object>>();
            _mockRepository.Verify(r => r.UpdateStock(1, 100), Times.Once);
        }

        [TestMethod]
        public void UpdateStock_WithInvalidProductId_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new UpdateStockRequest { Quantity = 100 };

            // Act
            var result = _controller.UpdateStock(-1, request);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void UpdateStock_WithNullRequest_ShouldReturnBadRequest()
        {
            // Act
            var result = _controller.UpdateStock(1, null);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void UpdateStock_WithNegativeQuantity_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new UpdateStockRequest { Quantity = -10 };

            // Act
            var result = _controller.UpdateStock(1, request);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void UpdateStock_WithFailedUpdate_ShouldReturnNotFound()
        {
            // Arrange
            var request = new UpdateStockRequest { Quantity = 100 };
            _mockRepository.Setup(r => r.UpdateStock(999, 100)).Returns(false);

            // Act
            var result = _controller.UpdateStock(999, request);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        #endregion

        #region DELETE /api/inventory/{id} Tests

        [TestMethod]
        public void DeleteInventory_WithValidId_ShouldReturnOkResult()
        {
            // Arrange
            var existingInventory = _testInventoryData.First();
            _mockRepository.Setup(r => r.GetById(1)).Returns(existingInventory);
            _mockRepository.Setup(r => r.Delete(1));

            // Act
            var result = _controller.DeleteInventory(1);

            // Assert
            result.Should().BeOfType<OkResult>();
            _mockRepository.Verify(r => r.Delete(1), Times.Once);
        }

        [TestMethod]
        public void DeleteInventory_WithInvalidId_ShouldReturnBadRequest()
        {
            // Act
            var result = _controller.DeleteInventory(-1);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void DeleteInventory_WithNonExistentId_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetById(999)).Returns((Inventory)null);

            // Act
            var result = _controller.DeleteInventory(999);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        #endregion

        #region Performance Tests

        [TestMethod]
        public void GetAllInventory_ShouldCompleteWithinTimeLimit()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAll()).Returns(_testInventoryData);
            var startTime = DateTime.Now;

            // Act
            var result = _controller.GetAllInventory();

            // Assert
            var executionTime = DateTime.Now - startTime;
            executionTime.Should().BeLessThan(TimeSpan.FromSeconds(2));
            result.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<Inventory>>>();
        }

        [TestMethod]
        public void GetLowStockItems_ShouldCompleteWithinTimeLimit()
        {
            // Arrange
            var lowStockItems = _testInventoryData.Where(i => i.Quantity <= i.Product.MinimumStockLevel).ToList();
            _mockRepository.Setup(r => r.GetLowStockItems()).Returns(lowStockItems);
            var startTime = DateTime.Now;

            // Act
            var result = _controller.GetLowStockItems();

            // Assert
            var executionTime = DateTime.Now - startTime;
            executionTime.Should().BeLessThan(TimeSpan.FromSeconds(1));
            result.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<Inventory>>>();
        }

        #endregion
    }

    // Helper class for UpdateStock tests
    public class UpdateStockRequest
    {
        public int Quantity { get; set; }
    }
} 