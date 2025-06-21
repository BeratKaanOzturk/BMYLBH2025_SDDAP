using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BMYLBH2025_SDDAP.Models;

namespace BMYLBH2025_SDDAP.Controllers
{
    [RoutePrefix("api/suppliers")]
    public class SuppliersController : ApiController
    {
        private readonly ISupplierRepository _supplierRepository;

        public SuppliersController()
        {
            var connectionFactory = new SqliteConnectionFactory();
            _supplierRepository = new SupplierRepository(connectionFactory);
        }

        // GET api/suppliers
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllSuppliers()
        {
            try
            {
                var suppliers = _supplierRepository.GetAll().ToList();
                return Ok(ApiResponse<IEnumerable<Supplier>>.CreateSuccess(suppliers, "Suppliers retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/suppliers/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetSupplierById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid supplier ID");

                var supplier = _supplierRepository.GetById(id);
                if (supplier == null)
                    return NotFound();

                return Ok(ApiResponse<Supplier>.CreateSuccess(supplier, "Supplier retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/suppliers/name/{name}
        [HttpGet]
        [Route("name/{name}")]
        public IHttpActionResult GetSupplierByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return BadRequest("Supplier name is required");

                var supplier = _supplierRepository.GetByName(name);
                if (supplier == null)
                    return NotFound();

                return Ok(ApiResponse<Supplier>.CreateSuccess(supplier, "Supplier retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/suppliers/search?query=tech
        [HttpGet]
        [Route("search")]
        public IHttpActionResult SearchSuppliers(string query = "")
        {
            try
            {
                var suppliers = _supplierRepository.SearchByName(query).ToList();
                return Ok(ApiResponse<IEnumerable<Supplier>>.CreateSuccess(suppliers, "Suppliers search completed"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/suppliers
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateSupplier([FromBody] Supplier supplier)
        {
            try
            {
                if (supplier == null)
                    return BadRequest("Supplier data is required");

                if (string.IsNullOrWhiteSpace(supplier.Name))
                    return BadRequest("Supplier name is required");

                // Check if supplier with same name already exists
                var existingSupplier = _supplierRepository.GetByName(supplier.Name);
                if (existingSupplier != null)
                    return BadRequest("Supplier with this name already exists");

                _supplierRepository.Add(supplier);

                // Get the created supplier
                var createdSupplier = _supplierRepository.GetByName(supplier.Name);
                return Ok(ApiResponse<Supplier>.CreateSuccess(createdSupplier, "Supplier created successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/suppliers/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateSupplier(int id, [FromBody] Supplier supplier)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid supplier ID");

                if (supplier == null)
                    return BadRequest("Supplier data is required");

                if (string.IsNullOrWhiteSpace(supplier.Name))
                    return BadRequest("Supplier name is required");

                var existingSupplier = _supplierRepository.GetById(id);
                if (existingSupplier == null)
                    return NotFound();

                // Check if another supplier with same name already exists
                var duplicateSupplier = _supplierRepository.GetByName(supplier.Name);
                if (duplicateSupplier != null && duplicateSupplier.SupplierID != id)
                    return BadRequest("Another supplier with this name already exists");

                supplier.SupplierID = id;
                _supplierRepository.Update(supplier);

                var updatedSupplier = _supplierRepository.GetById(id);
                return Ok(ApiResponse<Supplier>.CreateSuccess(updatedSupplier, "Supplier updated successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/suppliers/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteSupplier(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid supplier ID");

                var existingSupplier = _supplierRepository.GetById(id);
                if (existingSupplier == null)
                    return NotFound();

                _supplierRepository.Delete(id);
                return Ok(ApiResponse.CreateSuccess("Supplier deleted successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
} 