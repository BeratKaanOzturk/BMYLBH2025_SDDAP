using BMYLBH2025_SDDAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BMYLBH2025_SDDAP.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrdersController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        // GET api/orders
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllOrders()
        {
            try
            {
                var orders = _orderRepository.GetOrdersWithDetails().ToList();
                return Ok(ApiResponse<IEnumerable<Order>>.CreateSuccess(orders, "Orders retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/orders/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetOrderById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid order ID");

                var order = _orderRepository.GetOrderWithDetails(id);
                if (order == null)
                    return NotFound();

                return Ok(ApiResponse<Order>.CreateSuccess(order, "Order retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/orders
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateOrder([FromBody] Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest("Order data is required");
                }

                // Set defaults
                order.OrderDate = order.OrderDate == default(DateTime) ? DateTime.Now : order.OrderDate;
                order.Status = string.IsNullOrWhiteSpace(order.Status) ? "Pending" : order.Status;
                
                // Set CreatedBy to a default user (admin) if not provided
                if (order.CreatedBy == null || order.CreatedBy <= 0)
                {
                    // Try to get the admin user ID, default to 1 if not found
                    order.CreatedBy = 1; // Assuming admin user has ID 1
                }

                _orderRepository.Add(order);
                
                // Add order details if provided
                if (order.OrderDetails != null && order.OrderDetails.Any())
                {
                    foreach (var detail in order.OrderDetails)
                    {
                        detail.OrderID = order.OrderID;
                        _orderDetailRepository.Add(detail);
                    }
                }

                // Retrieve the complete order with details
                var createdOrder = _orderRepository.GetOrderWithDetails(order.OrderID);
                return Ok(ApiResponse<Order>.CreateSuccess(createdOrder, "Order created successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/orders/{id}
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateOrder(int id, [FromBody] Order order)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid order ID");

                if (order == null)
                    return BadRequest("Order data is required");

                var existingOrder = _orderRepository.GetById(id);
                if (existingOrder == null)
                    return NotFound();

                order.OrderID = id;
                _orderRepository.Update(order);

                // Handle OrderDetails update
                if (order.OrderDetails != null)
                {
                    // Get existing order details
                    var existingOrderDetails = _orderDetailRepository.GetByOrderId(id).ToList();
                    
                    // Delete existing order details that are not in the new list
                    var newDetailIds = order.OrderDetails.Where(od => od.OrderDetailID > 0).Select(od => od.OrderDetailID).ToList();
                    var detailsToDelete = existingOrderDetails.Where(od => !newDetailIds.Contains(od.OrderDetailID)).ToList();
                    
                    foreach (var detailToDelete in detailsToDelete)
                    {
                        _orderDetailRepository.Delete(detailToDelete.OrderDetailID);
                    }
                    
                    // Update or add order details
                    foreach (var detail in order.OrderDetails)
                    {
                        detail.OrderID = id;
                        
                        // Validate order detail
                        if (detail.ProductID <= 0)
                            return BadRequest("Valid Product ID is required for order detail");
                        
                        if (detail.Quantity <= 0)
                            return BadRequest("Quantity must be greater than 0 for order detail");
                        
                        if (detail.UnitPrice < 0)
                            return BadRequest("Unit price cannot be negative for order detail");
                        
                        if (detail.OrderDetailID > 0)
                        {
                            // Update existing detail
                            _orderDetailRepository.Update(detail);
                        }
                        else
                        {
                            // Add new detail
                            _orderDetailRepository.Add(detail);
                        }
                    }
                    
                    // Recalculate order total after updating details
                    var updatedOrderForRecalc = _orderRepository.GetOrderWithDetails(id);
                    if (updatedOrderForRecalc != null)
                    {
                        updatedOrderForRecalc.RecalculateTotal();
                        _orderRepository.Update(updatedOrderForRecalc);
                    }
                }

                var updatedOrder = _orderRepository.GetOrderWithDetails(id);
                return Ok(ApiResponse<Order>.CreateSuccess(updatedOrder, "Order updated successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/orders/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteOrder(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid order ID");

                var existingOrder = _orderRepository.GetById(id);
                if (existingOrder == null)
                    return NotFound();

                _orderRepository.Delete(id);
                return Ok(ApiResponse<object>.CreateSuccess(null, "Order deleted successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/orders/supplier/{supplierId}
        [HttpGet]
        [Route("supplier/{supplierId:int}")]
        public IHttpActionResult GetOrdersBySupplier(int supplierId)
        {
            try
            {
                if (supplierId <= 0)
                    return BadRequest("Invalid supplier ID");

                var orders = _orderRepository.GetOrdersBySupplier(supplierId).ToList();
                return Ok(ApiResponse<IEnumerable<Order>>.CreateSuccess(orders, "Orders retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/orders/status/{status}
        [HttpGet]
        [Route("status/{status}")]
        public IHttpActionResult GetOrdersByStatus(string status)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(status))
                    return BadRequest("Status cannot be empty");

                var orders = _orderRepository.GetOrdersByStatus(status).ToList();
                return Ok(ApiResponse<IEnumerable<Order>>.CreateSuccess(orders, "Orders retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/orders/analytics/sales
        [HttpGet]
        [Route("analytics/sales")]
        public IHttpActionResult GetSalesAnalytics()
        {
            try
            {
                var analytics = _orderRepository.GetSalesAnalytics();
                return Ok(ApiResponse<object>.CreateSuccess(analytics, "Sales analytics retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
} 