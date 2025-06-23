using BMYLBH2025_SDDAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BMYLBH2025_SDDAP.Controllers
{
    [RoutePrefix("api/suppliers")]
    public class SuppliersController : ApiController
    {
        private readonly ISupplierRepository _supplierRepository;

        public SuppliersController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        // GET api/suppliers
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllSuppliers()
        {
            try
            {
                var suppliers = _supplierRepository.GetAll();
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
                    return BadRequest("Supplier name cannot be empty");

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
                var suppliers = _supplierRepository.SearchByName(query);
                return Ok(ApiResponse<IEnumerable<Supplier>>.CreateSuccess(suppliers, "Suppliers search completed successfully"));
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

                if (supplier.Name.Trim().Length < 2)
                    return BadRequest("Supplier name must be at least 2 characters long");

                if (supplier.Name.Trim().Length > 200)
                    return BadRequest("Supplier name cannot exceed 200 characters");

                // Validate email format if provided
                if (!string.IsNullOrWhiteSpace(supplier.Email) && !IsValidEmail(supplier.Email))
                    return BadRequest("Invalid email format");

                // Check if supplier with same name already exists
                var existingSupplier = _supplierRepository.GetByName(supplier.Name.Trim());
                if (existingSupplier != null)
                    return BadRequest("Supplier with this name already exists");

                // Clean up data
                supplier.Name = supplier.Name.Trim();
                supplier.ContactPerson = supplier.ContactPerson?.Trim();
                supplier.Email = supplier.Email?.Trim();
                supplier.Phone = supplier.Phone?.Trim();
                supplier.Address = supplier.Address?.Trim();

                _supplierRepository.Add(supplier);

                // Retrieve the created supplier with its assigned ID
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

                if (supplier.Name.Trim().Length < 2)
                    return BadRequest("Supplier name must be at least 2 characters long");

                if (supplier.Name.Trim().Length > 200)
                    return BadRequest("Supplier name cannot exceed 200 characters");

                // Validate email format if provided
                if (!string.IsNullOrWhiteSpace(supplier.Email) && !IsValidEmail(supplier.Email))
                    return BadRequest("Invalid email format");

                var existingSupplier = _supplierRepository.GetById(id);
                if (existingSupplier == null)
                    return NotFound();

                // Check if another supplier with same name already exists
                var duplicateSupplier = _supplierRepository.GetByName(supplier.Name.Trim());
                if (duplicateSupplier != null && duplicateSupplier.SupplierID != id)
                    return BadRequest("Another supplier with this name already exists");

                // Clean up data
                supplier.Name = supplier.Name.Trim();
                supplier.ContactPerson = supplier.ContactPerson?.Trim();
                supplier.Email = supplier.Email?.Trim();
                supplier.Phone = supplier.Phone?.Trim();
                supplier.Address = supplier.Address?.Trim();

                supplier.SupplierID = id;
                _supplierRepository.Update(supplier);

                // Retrieve the updated supplier
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

                // Check if supplier has orders
                if (_supplierRepository.HasOrderReferences(id))
                    return BadRequest("Cannot delete supplier. It has associated orders. Please remove all orders related to this supplier first.");

                _supplierRepository.Delete(id);

                return Ok(ApiResponse.CreateSuccess("Supplier deleted successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #region Helper Methods

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
} 