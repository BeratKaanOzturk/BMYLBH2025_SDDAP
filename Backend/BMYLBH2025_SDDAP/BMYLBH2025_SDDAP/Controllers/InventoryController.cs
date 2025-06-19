using BMYLBH2025_SDDAP.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace BMYLBH2025_SDDAP.Controllers
{
    [RoutePrefix("api/inventory")]
    public class InventoryController : ApiController
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        // GET api/inventory
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllInventory()
        {
            try
            {
                var inventory = _inventoryRepository.GetAll();
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/inventory/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetInventoryById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid inventory ID");

                var inventory = _inventoryRepository.GetById(id);
                if (inventory == null)
                    return NotFound();

                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/inventory/product/5
        [HttpGet]
        [Route("product/{productId:int}")]
        public IHttpActionResult GetInventoryByProductId(int productId)
        {
            try
            {
                if (productId <= 0)
                    return BadRequest("Invalid product ID");

                var inventory = _inventoryRepository.GetByProductId(productId);
                if (inventory == null)
                    return NotFound();

                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/inventory/lowstock
        [HttpGet]
        [Route("lowstock")]
        public IHttpActionResult GetLowStockItems()
        {
            try
            {
                var lowStockItems = _inventoryRepository.GetLowStockItems();
                return Ok(lowStockItems);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/inventory/category/5
        [HttpGet]
        [Route("category/{categoryId:int}")]
        public IHttpActionResult GetInventoryByCategory(int categoryId)
        {
            try
            {
                if (categoryId <= 0)
                    return BadRequest("Invalid category ID");

                var inventory = _inventoryRepository.GetByCategory(categoryId);
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/inventory/totalvalue
        [HttpGet]
        [Route("totalvalue")]
        public IHttpActionResult GetTotalInventoryValue()
        {
            try
            {
                var totalValue = _inventoryRepository.GetTotalInventoryValue();
                return Ok(new { TotalValue = totalValue });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/inventory
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateInventory([FromBody] Inventory inventory)
        {
            try
            {
                if (inventory == null)
                    return BadRequest("Inventory data is required");

                if (inventory.ProductID <= 0)
                    return BadRequest("Valid Product ID is required");

                if (inventory.Quantity < 0)
                    return BadRequest("Quantity cannot be negative");

                // Check if inventory already exists for this product
                var existingInventory = _inventoryRepository.GetByProductId(inventory.ProductID);
                if (existingInventory != null)
                    return BadRequest("Inventory already exists for this product");

                _inventoryRepository.Add(inventory);
                return Created($"api/inventory/{inventory.InventoryID}", inventory);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/inventory/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateInventory(int id, [FromBody] Inventory inventory)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid inventory ID");

                if (inventory == null)
                    return BadRequest("Inventory data is required");

                if (inventory.Quantity < 0)
                    return BadRequest("Quantity cannot be negative");

                var existingInventory = _inventoryRepository.GetById(id);
                if (existingInventory == null)
                    return NotFound();

                inventory.InventoryID = id;
                _inventoryRepository.Update(inventory);
                
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/inventory/updatestock/5
        [HttpPut]
        [Route("updatestock/{productId:int}")]
        public IHttpActionResult UpdateStock(int productId, [FromBody] UpdateStockRequest request)
        {
            try
            {
                if (productId <= 0)
                    return BadRequest("Invalid product ID");

                if (request == null || request.Quantity < 0)
                    return BadRequest("Valid quantity is required");

                _inventoryRepository.UpdateStock(productId, request.Quantity);
                
                return Ok(new { Message = "Stock updated successfully" });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/inventory/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteInventory(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid inventory ID");

                var inventory = _inventoryRepository.GetById(id);
                if (inventory == null)
                    return NotFound();

                _inventoryRepository.Delete(id);
                
                return Ok(new { Message = "Inventory deleted successfully" });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

    // DTOs for API requests
    public class UpdateStockRequest
    {
        public int Quantity { get; set; }
    }
} 