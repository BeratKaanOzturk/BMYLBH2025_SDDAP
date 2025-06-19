using BMYLBH2025_SDDAP.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BMYLBH2025_SDDAP.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ProductsController(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // GET api/products
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllProducts()
        {
            try
            {
                using (var con = _connectionFactory.CreateConnection())
                {
                    const string sql = @"
                        SELECT p.*, c.Name as CategoryName, c.Description as CategoryDescription
                        FROM Products p
                        LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                        ORDER BY p.Name";

                    var products = con.Query<Product, Category, Product>(sql,
                        (product, category) =>
                        {
                            product.Category = category;
                            return product;
                        },
                        splitOn: "CategoryName").ToList();

                    return Ok(products);
                }
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

                using (var con = _connectionFactory.CreateConnection())
                {
                    const string sql = @"
                        SELECT p.*, c.Name as CategoryName, c.Description as CategoryDescription
                        FROM Products p
                        LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                        WHERE p.ProductID = @Id";

                    var product = con.Query<Product, Category, Product>(sql,
                        (product, category) =>
                        {
                            product.Category = category;
                            return product;
                        },
                        new { Id = id },
                        splitOn: "CategoryName").FirstOrDefault();

                    if (product == null)
                        return NotFound();

                    return Ok(product);
                }
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

                using (var con = _connectionFactory.CreateConnection())
                {
                    const string sql = @"
                        SELECT p.*, c.Name as CategoryName, c.Description as CategoryDescription
                        FROM Products p
                        LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                        WHERE p.CategoryID = @CategoryId
                        ORDER BY p.Name";

                    var products = con.Query<Product, Category, Product>(sql,
                        (product, category) =>
                        {
                            product.Category = category;
                            return product;
                        },
                        new { CategoryId = categoryId },
                        splitOn: "CategoryName").ToList();

                    return Ok(products);
                }
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
                using (var con = _connectionFactory.CreateConnection())
                {
                    var sql = @"
                        SELECT p.*, c.Name as CategoryName, c.Description as CategoryDescription
                        FROM Products p
                        LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                        WHERE 1=1";

                    var parameters = new DynamicParameters();

                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        sql += " AND p.Name LIKE @Name";
                        parameters.Add("Name", $"%{name}%");
                    }

                    if (minPrice.HasValue)
                    {
                        sql += " AND p.Price >= @MinPrice";
                        parameters.Add("MinPrice", minPrice.Value);
                    }

                    if (maxPrice.HasValue)
                    {
                        sql += " AND p.Price <= @MaxPrice";
                        parameters.Add("MaxPrice", maxPrice.Value);
                    }

                    sql += " ORDER BY p.Name";

                    var products = con.Query<Product, Category, Product>(sql,
                        (product, category) =>
                        {
                            product.Category = category;
                            return product;
                        },
                        parameters,
                        splitOn: "CategoryName").ToList();

                    return Ok(products);
                }
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

                using (var con = _connectionFactory.CreateConnection())
                {
                    const string sql = @"
                        INSERT INTO Products (Name, Description, Price, MinimumStockLevel, CategoryID)
                        VALUES (@Name, @Description, @Price, @MinimumStockLevel, @CategoryID);
                        SELECT last_insert_rowid();";

                    var productId = con.QuerySingle<int>(sql, product);
                    product.ProductID = productId;

                    return Created($"api/products/{product.ProductID}", product);
                }
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

                using (var con = _connectionFactory.CreateConnection())
                {
                    // Check if product exists
                    var existingProduct = con.QueryFirstOrDefault<Product>(
                        "SELECT * FROM Products WHERE ProductID = @Id", new { Id = id });

                    if (existingProduct == null)
                        return NotFound();

                    // Update product
                    const string sql = @"
                        UPDATE Products 
                        SET Name = @Name, 
                            Description = @Description, 
                            Price = @Price, 
                            MinimumStockLevel = @MinimumStockLevel, 
                            CategoryID = @CategoryID
                        WHERE ProductID = @ProductID";

                    product.ProductID = id;
                    con.Execute(sql, product);

                    return Ok(product);
                }
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

                using (var con = _connectionFactory.CreateConnection())
                {
                    // Check if product exists
                    var existingProduct = con.QueryFirstOrDefault<Product>(
                        "SELECT * FROM Products WHERE ProductID = @Id", new { Id = id });

                    if (existingProduct == null)
                        return NotFound();

                    // Check if product has inventory records
                    var hasInventory = con.QueryFirstOrDefault<bool>(
                        "SELECT COUNT(*) FROM Inventory WHERE ProductID = @Id", new { Id = id });

                    if (hasInventory)
                        return BadRequest("Cannot delete product with existing inventory records");

                    // Delete product
                    con.Execute("DELETE FROM Products WHERE ProductID = @Id", new { Id = id });

                    return Ok(new { Message = "Product deleted successfully" });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
} 