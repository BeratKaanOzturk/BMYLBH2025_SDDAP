using BMYLBH2025_SDDAP.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace BMYLBH2025_SDDAP.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET api/categories
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllCategories()
        {
            try
            {
                var categories = _categoryRepository.GetAll();
                return Ok(ApiResponse<IEnumerable<Category>>.CreateSuccess(categories, "Categories retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/categories/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetCategoryById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid category ID");

                var category = _categoryRepository.GetById(id);
                if (category == null)
                    return NotFound();

                return Ok(ApiResponse<Category>.CreateSuccess(category, "Category retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/categories/name/{name}
        [HttpGet]
        [Route("name/{name}")]
        public IHttpActionResult GetCategoryByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return BadRequest("Category name cannot be empty");

                var category = _categoryRepository.GetByName(name);
                if (category == null)
                    return NotFound();

                return Ok(ApiResponse<Category>.CreateSuccess(category, "Category retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/categories/with-products
        [HttpGet]
        [Route("with-products")]
        public IHttpActionResult GetCategoriesWithProducts()
        {
            try
            {
                var categories = _categoryRepository.GetCategoriesWithProducts();
                return Ok(ApiResponse<IEnumerable<Category>>.CreateSuccess(categories, "Categories with products retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/categories/low-stock
        [HttpGet]
        [Route("low-stock")]
        public IHttpActionResult GetCategoriesWithLowStock()
        {
            try
            {
                var categories = _categoryRepository.GetCategoriesWithLowStock();
                return Ok(ApiResponse<IEnumerable<Category>>.CreateSuccess(categories, "Categories with low stock retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/categories/5/product-count
        [HttpGet]
        [Route("{id:int}/product-count")]
        public IHttpActionResult GetProductCount(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid category ID");

                var count = _categoryRepository.GetProductCount(id);
                return Ok(ApiResponse<int>.CreateSuccess(count, "Product count retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/categories/5/total-value
        [HttpGet]
        [Route("{id:int}/total-value")]
        public IHttpActionResult GetTotalCategoryValue(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid category ID");

                var totalValue = _categoryRepository.GetTotalCategoryValue(id);
                return Ok(ApiResponse<decimal>.CreateSuccess(totalValue, "Category total value retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/categories
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateCategory([FromBody] Category category)
        {
            try
            {
                if (category == null)
                    return BadRequest("Category data is required");

                if (string.IsNullOrWhiteSpace(category.Name))
                    return BadRequest("Category name is required");

                _categoryRepository.Add(category);
                return Ok(ApiResponse.CreateSuccess("Category created successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/categories/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid category ID");

                if (category == null)
                    return BadRequest("Category data is required");

                category.CategoryID = id;
                _categoryRepository.Update(category);
                return Ok(ApiResponse.CreateSuccess("Category updated successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/categories/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteCategory(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid category ID");

                _categoryRepository.Delete(id);
                return Ok(ApiResponse.CreateSuccess("Category deleted successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
} 