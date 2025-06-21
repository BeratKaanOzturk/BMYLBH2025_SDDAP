using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BMYLBH2025_SDDAP.Models;

namespace BMYLBH2025_SDDAP.Controllers
{
    [RoutePrefix("api/orderdetails")]
    public class OrderDetailsController : ApiController
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderDetailsController(IOrderDetailRepository orderDetailRepository, 
            IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        // GET api/orderdetails
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllOrderDetails()
        {
            try
            {
                var orderDetails = _orderDetailRepository.GetAll().ToList();
                return Ok(ApiResponse<IEnumerable<OrderDetail>>.CreateSuccess(orderDetails, "Order details retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/orderdetails/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetOrderDetailById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid order detail ID");

                var orderDetail = _orderDetailRepository.GetById(id);
                if (orderDetail == null)
                    return NotFound();

                return Ok(ApiResponse<OrderDetail>.CreateSuccess(orderDetail, "Order detail retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/orderdetails/order/5
        [HttpGet]
        [Route("order/{orderId:int}")]
        public IHttpActionResult GetOrderDetailsByOrderId(int orderId)
        {
            try
            {
                if (orderId <= 0)
                    return BadRequest("Invalid order ID");

                var orderDetails = _orderDetailRepository.GetByOrderId(orderId).ToList();
                return Ok(ApiResponse<IEnumerable<OrderDetail>>.CreateSuccess(orderDetails, "Order details retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/orderdetails/product/5
        [HttpGet]
        [Route("product/{productId:int}")]
        public IHttpActionResult GetOrderDetailsByProductId(int productId)
        {
            try
            {
                if (productId <= 0)
                    return BadRequest("Invalid product ID");

                var orderDetails = _orderDetailRepository.GetByProductId(productId).ToList();
                return Ok(ApiResponse<IEnumerable<OrderDetail>>.CreateSuccess(orderDetails, "Order details retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/orderdetails/order/5/total
        [HttpGet]
        [Route("order/{orderId:int}/total")]
        public IHttpActionResult GetOrderTotal(int orderId)
        {
            try
            {
                if (orderId <= 0)
                    return BadRequest("Invalid order ID");

                var total = _orderDetailRepository.GetTotalByOrderId(orderId);
                return Ok(ApiResponse<decimal>.CreateSuccess(total, "Order total retrieved successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/orderdetails
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateOrderDetail([FromBody] OrderDetail orderDetail)
        {
            try
            {
                if (orderDetail == null)
                    return BadRequest("Order detail data is required");

                if (orderDetail.OrderID <= 0)
                    return BadRequest("Valid Order ID is required");

                if (orderDetail.ProductID <= 0)
                    return BadRequest("Valid Product ID is required");

                if (orderDetail.Quantity <= 0)
                    return BadRequest("Quantity must be greater than 0");

                if (orderDetail.UnitPrice < 0)
                    return BadRequest("Unit price cannot be negative");

                // Verify order exists
                var order = _orderRepository.GetById(orderDetail.OrderID);
                if (order == null)
                    return BadRequest("Order not found");

                // Verify product exists
                var product = _productRepository.GetById(orderDetail.ProductID);
                if (product == null)
                    return BadRequest("Product not found");

                _orderDetailRepository.Add(orderDetail);

                // Get the created order detail
                var createdOrderDetail = _orderDetailRepository.GetByOrderId(orderDetail.OrderID)
                    .OrderByDescending(od => od.OrderDetailID)
                    .FirstOrDefault();

                return Ok(ApiResponse<OrderDetail>.CreateSuccess(createdOrderDetail, "Order detail created successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/orderdetails/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateOrderDetail(int id, [FromBody] OrderDetail orderDetail)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid order detail ID");

                if (orderDetail == null)
                    return BadRequest("Order detail data is required");

                var existingOrderDetail = _orderDetailRepository.GetById(id);
                if (existingOrderDetail == null)
                    return NotFound();

                if (orderDetail.Quantity <= 0)
                    return BadRequest("Quantity must be greater than 0");

                if (orderDetail.UnitPrice < 0)
                    return BadRequest("Unit price cannot be negative");

                orderDetail.OrderDetailID = id;
                _orderDetailRepository.Update(orderDetail);

                var updatedOrderDetail = _orderDetailRepository.GetById(id);
                return Ok(ApiResponse<OrderDetail>.CreateSuccess(updatedOrderDetail, "Order detail updated successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/orderdetails/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteOrderDetail(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid order detail ID");

                var existingOrderDetail = _orderDetailRepository.GetById(id);
                if (existingOrderDetail == null)
                    return NotFound();

                _orderDetailRepository.Delete(id);
                return Ok(ApiResponse.CreateSuccess("Order detail deleted successfully"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
} 