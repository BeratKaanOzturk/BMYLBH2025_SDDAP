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
    public class ProductsControllerTests
    {
        private Mock<IProductRepository> _mockRepository;
        private ProductsController _controller;
        private List<Product> _testProductData;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IProductRepository>();
            _controller = new ProductsController(_mockRepository.Object);
            
            // Setup test data
            _testProductData = new List<Product>
            {
                new Product 
                { 
                    ProductID = 1, 
                    Name = "Laptop Computer", 
                    Description = "High-performance laptop",
                    Price = 999.99m, 
                    MinimumStockLevel = 10,
                    CategoryID = 1,
                    Category = new Category { CategoryID = 1, Name = "Electronics" }
                },
                new Product 
                { 
                    ProductID = 2, 
                    Name = "Wireless Mouse", 
                    Description = "Ergonomic wireless mouse",
                    Price = 25.99m, 
                    MinimumStockLevel = 20,
                    CategoryID = 1,
                    Category = new Category { CategoryID = 1, Name = "Electronics" }
                },
                new Product 
                { 
                    ProductID = 3, 
                    Name = "Office Chair", 
                    Description = "Comfortable office chair",
                    Price = 199.99m, 
                    MinimumStockLevel = 5,
                    CategoryID = 2,
                    Category = new Category { CategoryID = 2, Name = "Furniture" }
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _controller?.Dispose();
        }

        #region GET /api/products Tests

        [TestMethod]
        public void GetAllProducts_WithValidData_ShouldReturnOkResult()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAll()).Returns(_testProductData);

            // Act
            var result = _controller.GetAllProducts();

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>;
            okResult.Content.Data.Should().HaveCount(3);
            okResult.Content.Success.Should().BeTrue();
            okResult.Content.Message.Should().Be("Products retrieved successfully");
            _mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetAllProducts_WithEmptyData_ShouldReturnOkWithEmptyList()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAll()).Returns(new List<Product>());

            // Act
            var result = _controller.GetAllProducts();

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>;
            okResult.Content.Data.Should().BeEmpty();
            okResult.Content.Success.Should().BeTrue();
        }

        [TestMethod]
        public void GetAllProducts_WithRepositoryException_ShouldReturnInternalServerError()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAll()).Throws(new Exception("Database error"));

            // Act
            var result = _controller.GetAllProducts();

            // Assert
            result.Should().BeOfType<ExceptionResult>();
        }

        #endregion

        #region GET /api/products/{id} Tests

        [TestMethod]
        public void GetProductById_WithValidId_ShouldReturnOkResult()
        {
            // Arrange
            var testProduct = _testProductData.First();
            _mockRepository.Setup(r => r.GetById(1)).Returns(testProduct);

            // Act
            var result = _controller.GetProductById(1);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<Product>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<Product>>;
            okResult.Content.Data.ProductID.Should().Be(1);
            okResult.Content.Data.Name.Should().Be("Laptop Computer");
            okResult.Content.Success.Should().BeTrue();
        }

        [TestMethod]
        public void GetProductById_WithInvalidId_ShouldReturnBadRequest()
        {
            // Act
            var result = _controller.GetProductById(-1);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
            var badRequestResult = result as BadRequestErrorMessageResult;
            badRequestResult.Message.Should().Be("Invalid product ID");
        }

        [TestMethod]
        public void GetProductById_WithNonExistentId_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetById(999)).Returns((Product)null);

            // Act
            var result = _controller.GetProductById(999);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        #endregion

        #region GET /api/products/category/{categoryId} Tests

        [TestMethod]
        public void GetProductsByCategory_WithValidCategoryId_ShouldReturnOkResult()
        {
            // Arrange
            var electronicsProducts = _testProductData.Where(p => p.CategoryID == 1).ToList();
            _mockRepository.Setup(r => r.GetByCategory(1)).Returns(electronicsProducts);

            // Act
            var result = _controller.GetProductsByCategory(1);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>;
            okResult.Content.Data.Should().HaveCount(2);
            okResult.Content.Success.Should().BeTrue();
        }

        [TestMethod]
        public void GetProductsByCategory_WithInvalidCategoryId_ShouldReturnBadRequest()
        {
            // Act
            var result = _controller.GetProductsByCategory(-1);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void GetProductsByCategory_WithNonExistentCategory_ShouldReturnEmptyList()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByCategory(999)).Returns(new List<Product>());

            // Act
            var result = _controller.GetProductsByCategory(999);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>;
            okResult.Content.Data.Should().BeEmpty();
        }

        #endregion

        #region GET /api/products/search Tests

        [TestMethod]
        public void SearchProducts_WithNameParameter_ShouldReturnMatchingProducts()
        {
            // Arrange
            var searchResults = _testProductData.Where(p => p.Name.Contains("Laptop")).ToList();
            _mockRepository.Setup(r => r.SearchByName("Laptop")).Returns(searchResults);

            // Act
            var result = _controller.SearchProducts("Laptop");

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>;
            okResult.Content.Data.Should().HaveCount(1);
            okResult.Content.Data.First().Name.Should().Contain("Laptop");
        }

        [TestMethod]
        public void SearchProducts_WithPriceRange_ShouldReturnFilteredProducts()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAll()).Returns(_testProductData);

            // Act
            var result = _controller.SearchProducts("", 20m, 30m);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>;
            okResult.Content.Data.Should().HaveCount(1);
            okResult.Content.Data.First().Price.Should().Be(25.99m);
        }

        [TestMethod]
        public void SearchProducts_WithMinPriceOnly_ShouldReturnProductsAboveMinPrice()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAll()).Returns(_testProductData);

            // Act
            var result = _controller.SearchProducts("", 100m);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>;
            okResult.Content.Data.Should().HaveCount(2); // Laptop and Chair
        }

        [TestMethod]
        public void SearchProducts_WithMaxPriceOnly_ShouldReturnProductsBelowMaxPrice()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAll()).Returns(_testProductData);

            // Act
            var result = _controller.SearchProducts("", null, 100m);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>;
            okResult.Content.Data.Should().HaveCount(1); // Only Mouse
        }

        [TestMethod]
        public void SearchProducts_WithNoParameters_ShouldReturnAllProducts()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAll()).Returns(_testProductData);

            // Act
            var result = _controller.SearchProducts();

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>;
            okResult.Content.Data.Should().HaveCount(3);
        }

        [TestMethod]
        public void SearchProducts_WithNoResults_ShouldReturnEmptyList()
        {
            // Arrange
            _mockRepository.Setup(r => r.SearchByName("NonExistent")).Returns(new List<Product>());

            // Act
            var result = _controller.SearchProducts("NonExistent");

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>;
            okResult.Content.Data.Should().BeEmpty();
        }

        #endregion

        #region GET /api/products/low-stock Tests

        [TestMethod]
        public void GetLowStockProducts_WithLowStockItems_ShouldReturnOkResult()
        {
            // Arrange
            var lowStockProducts = _testProductData.Take(1).ToList(); // Assuming first product is low stock
            _mockRepository.Setup(r => r.GetLowStockProducts()).Returns(lowStockProducts);

            // Act
            var result = _controller.GetLowStockProducts();

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>;
            okResult.Content.Data.Should().HaveCount(1);
            okResult.Content.Message.Should().Be("Low stock products retrieved successfully");
        }

        [TestMethod]
        public void GetLowStockProducts_WithNoLowStockItems_ShouldReturnEmptyList()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetLowStockProducts()).Returns(new List<Product>());

            // Act
            var result = _controller.GetLowStockProducts();

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>;
            okResult.Content.Data.Should().BeEmpty();
        }

        #endregion

        #region GET /api/products/name/{name} Tests

        [TestMethod]
        public void GetProductByName_WithValidName_ShouldReturnOkResult()
        {
            // Arrange
            var testProduct = _testProductData.First();
            _mockRepository.Setup(r => r.GetByName("Laptop Computer")).Returns(testProduct);

            // Act
            var result = _controller.GetProductByName("Laptop Computer");

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<Product>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<Product>>;
            okResult.Content.Data.Name.Should().Be("Laptop Computer");
        }

        [TestMethod]
        public void GetProductByName_WithEmptyName_ShouldReturnBadRequest()
        {
            // Act
            var result = _controller.GetProductByName("");

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void GetProductByName_WithNonExistentName_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByName("NonExistent")).Returns((Product)null);

            // Act
            var result = _controller.GetProductByName("NonExistent");

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        #endregion

        #region GET /api/products/total-value Tests

        [TestMethod]
        public void GetTotalProductValue_WithValidData_ShouldReturnCorrectValue()
        {
            // Arrange
            var expectedTotal = 15000.50m;
            _mockRepository.Setup(r => r.GetTotalProductValue()).Returns(expectedTotal);

            // Act
            var result = _controller.GetTotalProductValue();

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<decimal>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<decimal>>;
            okResult.Content.Data.Should().Be(expectedTotal);
            okResult.Content.Message.Should().Be("Total product value retrieved successfully");
        }

        [TestMethod]
        public void GetTotalProductValue_WithZeroValue_ShouldReturnZero()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetTotalProductValue()).Returns(0m);

            // Act
            var result = _controller.GetTotalProductValue();

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<decimal>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<decimal>>;
            okResult.Content.Data.Should().Be(0m);
        }

        #endregion

        #region POST /api/products Tests

        [TestMethod]
        public void CreateProduct_WithValidData_ShouldReturnOkResult()
        {
            // Arrange
            var newProduct = new Product
            {
                Name = "New Product",
                Description = "Test product",
                Price = 50.00m,
                MinimumStockLevel = 10,
                CategoryID = 1
            };
            _mockRepository.Setup(r => r.Add(It.IsAny<Product>()));

            // Act
            var result = _controller.CreateProduct(newProduct);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<Product>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<Product>>;
            okResult.Content.Success.Should().BeTrue();
            okResult.Content.Message.Should().Be("Product created successfully");
            _mockRepository.Verify(r => r.Add(newProduct), Times.Once);
        }

        [TestMethod]
        public void CreateProduct_WithNullProduct_ShouldReturnBadRequest()
        {
            // Act
            var result = _controller.CreateProduct(null);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
            var badRequestResult = result as BadRequestErrorMessageResult;
            badRequestResult.Message.Should().Be("Product data is required");
        }

        [TestMethod]
        public void CreateProduct_WithEmptyName_ShouldReturnBadRequest()
        {
            // Arrange
            var invalidProduct = new Product
            {
                Name = "",
                Price = 50.00m,
                MinimumStockLevel = 10
            };

            // Act
            var result = _controller.CreateProduct(invalidProduct);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
            var badRequestResult = result as BadRequestErrorMessageResult;
            badRequestResult.Message.Should().Be("Product name is required");
        }

        [TestMethod]
        public void CreateProduct_WithNegativePrice_ShouldReturnBadRequest()
        {
            // Arrange
            var invalidProduct = new Product
            {
                Name = "Test Product",
                Price = -10.00m,
                MinimumStockLevel = 10
            };

            // Act
            var result = _controller.CreateProduct(invalidProduct);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
            var badRequestResult = result as BadRequestErrorMessageResult;
            badRequestResult.Message.Should().Be("Product price cannot be negative");
        }

        [TestMethod]
        public void CreateProduct_WithNegativeMinimumStockLevel_ShouldReturnBadRequest()
        {
            // Arrange
            var invalidProduct = new Product
            {
                Name = "Test Product",
                Price = 50.00m,
                MinimumStockLevel = -5
            };

            // Act
            var result = _controller.CreateProduct(invalidProduct);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
            var badRequestResult = result as BadRequestErrorMessageResult;
            badRequestResult.Message.Should().Be("Minimum stock level cannot be negative");
        }

        #endregion

        #region PUT /api/products/{id} Tests

        [TestMethod]
        public void UpdateProduct_WithValidData_ShouldReturnOkResult()
        {
            // Arrange
            var existingProduct = _testProductData.First();
            var updatedProduct = new Product
            {
                ProductID = 1,
                Name = "Updated Laptop",
                Description = "Updated description",
                Price = 1099.99m,
                MinimumStockLevel = 15,
                CategoryID = 1
            };
            _mockRepository.Setup(r => r.GetById(1)).Returns(existingProduct);
            _mockRepository.Setup(r => r.Update(It.IsAny<Product>()));

            // Act
            var result = _controller.UpdateProduct(1, updatedProduct);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<Product>>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse<Product>>;
            okResult.Content.Success.Should().BeTrue();
            okResult.Content.Message.Should().Be("Product updated successfully");
            _mockRepository.Verify(r => r.Update(It.IsAny<Product>()), Times.Once);
        }

        [TestMethod]
        public void UpdateProduct_WithInvalidId_ShouldReturnBadRequest()
        {
            // Arrange
            var product = new Product { Name = "Test" };

            // Act
            var result = _controller.UpdateProduct(-1, product);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void UpdateProduct_WithNonExistentId_ShouldReturnNotFound()
        {
            // Arrange
            var product = new Product { Name = "Test", Price = 10.00m };
            _mockRepository.Setup(r => r.GetById(999)).Returns((Product)null);

            // Act
            var result = _controller.UpdateProduct(999, product);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        #endregion

        #region PUT /api/products/{id}/price Tests

        [TestMethod]
        public void UpdateProductPrice_WithValidData_ShouldReturnOkResult()
        {
            // Arrange
            var request = new UpdatePriceRequest { Price = 1199.99m };
            _mockRepository.Setup(r => r.UpdatePrice(1, 1199.99m)).Returns(true);

            // Act
            var result = _controller.UpdateProductPrice(1, request);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse>;
            okResult.Content.Success.Should().BeTrue();
            okResult.Content.Message.Should().Be("Product price updated successfully");
        }

        [TestMethod]
        public void UpdateProductPrice_WithInvalidId_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new UpdatePriceRequest { Price = 100.00m };

            // Act
            var result = _controller.UpdateProductPrice(-1, request);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void UpdateProductPrice_WithNegativePrice_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new UpdatePriceRequest { Price = -50.00m };

            // Act
            var result = _controller.UpdateProductPrice(1, request);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void UpdateProductPrice_WithFailedUpdate_ShouldReturnNotFound()
        {
            // Arrange
            var request = new UpdatePriceRequest { Price = 100.00m };
            _mockRepository.Setup(r => r.UpdatePrice(999, 100.00m)).Returns(false);

            // Act
            var result = _controller.UpdateProductPrice(999, request);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        #endregion

        #region PUT /api/products/{id}/minimum-stock Tests

        [TestMethod]
        public void UpdateMinimumStockLevel_WithValidData_ShouldReturnOkResult()
        {
            // Arrange
            var request = new UpdateMinimumStockRequest { MinimumStockLevel = 25 };
            _mockRepository.Setup(r => r.UpdateMinimumStockLevel(1, 25)).Returns(true);

            // Act
            var result = _controller.UpdateMinimumStockLevel(1, request);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse>;
            okResult.Content.Success.Should().BeTrue();
            okResult.Content.Message.Should().Be("Minimum stock level updated successfully");
        }

        [TestMethod]
        public void UpdateMinimumStockLevel_WithNegativeLevel_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new UpdateMinimumStockRequest { MinimumStockLevel = -5 };

            // Act
            var result = _controller.UpdateMinimumStockLevel(1, request);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        #endregion

        #region DELETE /api/products/{id} Tests

        [TestMethod]
        public void DeleteProduct_WithValidId_ShouldReturnOkResult()
        {
            // Arrange
            var existingProduct = _testProductData.First();
            _mockRepository.Setup(r => r.GetById(1)).Returns(existingProduct);
            _mockRepository.Setup(r => r.Delete(1));

            // Act
            var result = _controller.DeleteProduct(1);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse>>();
            var okResult = result as OkNegotiatedContentResult<ApiResponse>;
            okResult.Content.Success.Should().BeTrue();
            okResult.Content.Message.Should().Be("Product deleted successfully");
            _mockRepository.Verify(r => r.Delete(1), Times.Once);
        }

        [TestMethod]
        public void DeleteProduct_WithInvalidId_ShouldReturnBadRequest()
        {
            // Act
            var result = _controller.DeleteProduct(-1);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void DeleteProduct_WithNonExistentId_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetById(999)).Returns((Product)null);

            // Act
            var result = _controller.DeleteProduct(999);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        #endregion

        #region Performance Tests

        [TestMethod]
        public void GetAllProducts_ShouldCompleteWithinTimeLimit()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAll()).Returns(_testProductData);
            var startTime = DateTime.Now;

            // Act
            var result = _controller.GetAllProducts();

            // Assert
            var executionTime = DateTime.Now - startTime;
            executionTime.Should().BeLessThan(TimeSpan.FromSeconds(2));
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>>();
        }

        [TestMethod]
        public void SearchProducts_ShouldCompleteWithinTimeLimit()
        {
            // Arrange
            _mockRepository.Setup(r => r.SearchByName("Laptop")).Returns(_testProductData.Take(1).ToList());
            var startTime = DateTime.Now;

            // Act
            var result = _controller.SearchProducts("Laptop");

            // Assert
            var executionTime = DateTime.Now - startTime;
            executionTime.Should().BeLessThan(TimeSpan.FromSeconds(1));
            result.Should().BeOfType<OkNegotiatedContentResult<ApiResponse<IEnumerable<Product>>>>();
        }

        #endregion
    }

    // Helper classes for request DTOs
    public class UpdatePriceRequest
    {
        public decimal Price { get; set; }
    }

    public class UpdateMinimumStockRequest
    {
        public int MinimumStockLevel { get; set; }
    }
} 