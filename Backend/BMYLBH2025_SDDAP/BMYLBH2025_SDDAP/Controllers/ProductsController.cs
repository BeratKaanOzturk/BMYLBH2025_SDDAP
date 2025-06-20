using BMYLBH2025_SDDAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BMYLBH2025_SDDAP.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET api/products
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllProducts()
        {
            try
            {
                var products = _productRepository.GetAll();
                return Ok(ApiResponse<IEnumerable<Product>>.CreateSuccess(products, "Products retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/products/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetProductById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid product ID");

                var product = _productRepository.GetById(id);
                if (product == null)
                    return NotFound();

                return Ok(ApiResponse<Product>.CreateSuccess(product, "Product retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/products/category/5
        [HttpGet]
        [Route("category/{categoryId:int}")]
        public IHttpActionResult GetProductsByCategory(int categoryId)
        {
            try
            {
                if (categoryId <= 0)
                    return BadRequest("Invalid category ID");

                var products = _productRepository.GetByCategory(categoryId);
                return Ok(ApiResponse<IEnumerable<Product>>.CreateSuccess(products, "Products retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/products/search?name=laptop
        [HttpGet]
        [Route("search")]
        public IHttpActionResult SearchProducts(string name = "", decimal? minPrice = null, decimal? maxPrice = null)
        {
            try
            {
                IEnumerable<Product> products;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    products = _productRepository.SearchByName(name);
                }
                else
                {
                    products = _productRepository.GetAll();
                }

                // Apply price filters if specified (simple filtering for now)
                if (minPrice.HasValue)
                    products = products.Where(p => p.Price >= minPrice.Value);

                if (maxPrice.HasValue)
                    products = products.Where(p => p.Price <= maxPrice.Value);

                return Ok(ApiResponse<IEnumerable<Product>>.CreateSuccess(products, "Products retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/products/low-stock
        [HttpGet]
        [Route("low-stock")]
        public IHttpActionResult GetLowStockProducts()
        {
            try
            {
                var products = _productRepository.GetLowStockProducts();
                return Ok(ApiResponse<IEnumerable<Product>>.CreateSuccess(products, "Low stock products retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/products/name/{name}
        [HttpGet]
        [Route("name/{name}")]
        public IHttpActionResult GetProductByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return BadRequest("Product name cannot be empty");

                var product = _productRepository.GetByName(name);
                if (product == null)
                    return NotFound();

                return Ok(ApiResponse<Product>.CreateSuccess(product, "Product retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/products/total-value
        [HttpGet]
        [Route("total-value")]
        public IHttpActionResult GetTotalProductValue()
        {
            try
            {
                var totalValue = _productRepository.GetTotalProductValue();
                return Ok(ApiResponse<decimal>.CreateSuccess(totalValue, "Total product value retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/products
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Product data is required");

                if (string.IsNullOrWhiteSpace(product.Name))
                    return BadRequest("Product name is required");

                if (product.Price < 0)
                    return BadRequest("Product price cannot be negative");

                if (product.MinimumStockLevel < 0)
                    return BadRequest("Minimum stock level cannot be negative");

                _productRepository.Add(product);
                return Ok(ApiResponse<Product>.CreateSuccess(product, "Product created successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/products/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid product ID");

                if (product == null)
                    return BadRequest("Product data is required");

                if (string.IsNullOrWhiteSpace(product.Name))
                    return BadRequest("Product name is required");

                if (product.Price < 0)
                    return BadRequest("Product price cannot be negative");

                var existingProduct = _productRepository.GetById(id);
                if (existingProduct == null)
                    return NotFound();

                product.ProductID = id;
                _productRepository.Update(product);
                
                return Ok(ApiResponse<Product>.CreateSuccess(product, "Product updated successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/products/5/price
        [HttpPut]
        [Route("{id:int}/price")]
        public IHttpActionResult UpdateProductPrice(int id, [FromBody] UpdatePriceRequest request)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid product ID");

                if (request == null || request.Price < 0)
                    return BadRequest("Valid price is required");

                var success = _productRepository.UpdatePrice(id, request.Price);
                if (!success)
                    return NotFound();

                return Ok(ApiResponse.CreateSuccess("Product price updated successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/products/5/minimum-stock
        [HttpPut]
        [Route("{id:int}/minimum-stock")]
        public IHttpActionResult UpdateMinimumStockLevel(int id, [FromBody] UpdateMinimumStockRequest request)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid product ID");

                if (request == null || request.MinimumStockLevel < 0)
                    return BadRequest("Valid minimum stock level is required");

                var success = _productRepository.UpdateMinimumStockLevel(id, request.MinimumStockLevel);
                if (!success)
                    return NotFound();

                return Ok(ApiResponse.CreateSuccess("Minimum stock level updated successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/products/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid product ID");

                var existingProduct = _productRepository.GetById(id);
                if (existingProduct == null)
                    return NotFound();

                _productRepository.Delete(id);
                return Ok(ApiResponse.CreateSuccess("Product deleted successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

    // Request DTOs
    public class UpdatePriceRequest
    {
        public decimal Price { get; set; }
    }

    public class UpdateMinimumStockRequest
    {
        public int MinimumStockLevel { get; set; }
    }
} 