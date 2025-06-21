using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace BMYLBH2025_SDDAP.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        
        // Navigation Properties
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        public OrderDetail()
        {
            // Constructor
        }

        public decimal CalculateSubtotal()
        {
            // Calculate subtotal
            return Quantity * UnitPrice;
        }
    }
    
    public interface IOrderDetailRepository : IBaseRepository<OrderDetail>
    {
        // Business-specific methods
        IEnumerable<OrderDetail> GetByOrderId(int orderId);
        IEnumerable<OrderDetail> GetByProductId(int productId);
        decimal GetTotalByOrderId(int orderId);
        bool UpdateQuantity(int orderDetailId, int newQuantity);
        bool UpdateUnitPrice(int orderDetailId, decimal newUnitPrice);
    }
    
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        
        public OrderDetailRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        
        public IEnumerable<OrderDetail> GetAll()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM OrderDetails ORDER BY OrderDetailID";
                var result = con.Query(sql);
                var orderDetails = new List<OrderDetail>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderDetailID = row.OrderDetailID == null ? 0 : Convert.ToInt32(row.OrderDetailID),
                            OrderID = row.OrderID == null ? 0 : Convert.ToInt32(row.OrderID),
                            ProductID = row.ProductID == null ? 0 : Convert.ToInt32(row.ProductID),
                            Quantity = row.Quantity == null ? 0 : Convert.ToInt32(row.Quantity),
                            UnitPrice = row.UnitPrice == null ? 0m : Convert.ToDecimal(row.UnitPrice)
                        };
                        
                        orderDetails.Add(orderDetail);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return orderDetails;
            }
        }
        
        public OrderDetail GetById(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM OrderDetails WHERE OrderDetailID = @Id";
                var row = con.QueryFirstOrDefault(sql, new { Id = id });
                if (row == null) return null;
                
                try
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderDetailID = row.OrderDetailID == null ? 0 : Convert.ToInt32(row.OrderDetailID),
                        OrderID = row.OrderID == null ? 0 : Convert.ToInt32(row.OrderID),
                        ProductID = row.ProductID == null ? 0 : Convert.ToInt32(row.ProductID),
                        Quantity = row.Quantity == null ? 0 : Convert.ToInt32(row.Quantity),
                        UnitPrice = row.UnitPrice == null ? 0m : Convert.ToDecimal(row.UnitPrice)
                    };
                    
                    return orderDetail;
                }
                catch (Exception ex)
                {
                    // Log the error or handle as needed
                    // Return null if conversion fails
                    return null;
                }
            }
        }
        
        public void Add(OrderDetail entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                // Calculate LineTotal
                var lineTotal = entity.Quantity * entity.UnitPrice;
                
                const string sql = @"
                    INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice, LineTotal) 
                    VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice, @LineTotal);
                    SELECT last_insert_rowid();";
                    
                var newId = con.QuerySingle<int>(sql, new 
                {
                    entity.OrderID,
                    entity.ProductID,
                    entity.Quantity,
                    entity.UnitPrice,
                    LineTotal = lineTotal
                });
                entity.OrderDetailID = newId;
            }
        }
        
        public void Update(OrderDetail entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                // Calculate LineTotal
                var lineTotal = entity.Quantity * entity.UnitPrice;
                
                const string sql = @"
                    UPDATE OrderDetails 
                    SET OrderID = @OrderID, 
                        ProductID = @ProductID, 
                        Quantity = @Quantity, 
                        UnitPrice = @UnitPrice,
                        LineTotal = @LineTotal
                    WHERE OrderDetailID = @OrderDetailID";
                    
                con.Execute(sql, new 
                {
                    entity.OrderID,
                    entity.ProductID,
                    entity.Quantity,
                    entity.UnitPrice,
                    entity.OrderDetailID,
                    LineTotal = lineTotal
                });
            }
        }
        
        public void Delete(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "DELETE FROM OrderDetails WHERE OrderDetailID = @Id";
                con.Execute(sql, new { Id = id });
            }
        }
        
        public IEnumerable<OrderDetail> GetByOrderId(int orderId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM OrderDetails WHERE OrderID = @OrderId ORDER BY OrderDetailID";
                var result = con.Query(sql, new { OrderId = orderId });
                var orderDetails = new List<OrderDetail>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderDetailID = row.OrderDetailID == null ? 0 : Convert.ToInt32(row.OrderDetailID),
                            OrderID = row.OrderID == null ? 0 : Convert.ToInt32(row.OrderID),
                            ProductID = row.ProductID == null ? 0 : Convert.ToInt32(row.ProductID),
                            Quantity = row.Quantity == null ? 0 : Convert.ToInt32(row.Quantity),
                            UnitPrice = row.UnitPrice == null ? 0m : Convert.ToDecimal(row.UnitPrice)
                        };
                        
                        orderDetails.Add(orderDetail);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return orderDetails;
            }
        }
        
        public IEnumerable<OrderDetail> GetByProductId(int productId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM OrderDetails WHERE ProductID = @ProductId ORDER BY OrderDetailID";
                var result = con.Query(sql, new { ProductId = productId });
                var orderDetails = new List<OrderDetail>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderDetailID = row.OrderDetailID == null ? 0 : Convert.ToInt32(row.OrderDetailID),
                            OrderID = row.OrderID == null ? 0 : Convert.ToInt32(row.OrderID),
                            ProductID = row.ProductID == null ? 0 : Convert.ToInt32(row.ProductID),
                            Quantity = row.Quantity == null ? 0 : Convert.ToInt32(row.Quantity),
                            UnitPrice = row.UnitPrice == null ? 0m : Convert.ToDecimal(row.UnitPrice)
                        };
                        
                        orderDetails.Add(orderDetail);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return orderDetails;
            }
        }
        
        public decimal GetTotalByOrderId(int orderId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT COALESCE(SUM(Quantity * UnitPrice), 0) 
                    FROM OrderDetails 
                    WHERE OrderID = @OrderId";
                    
                return con.QuerySingle<decimal>(sql, new { OrderId = orderId });
            }
        }
        
        public bool UpdateQuantity(int orderDetailId, int newQuantity)
        {
            try
            {
                using (var con = _connectionFactory.CreateConnection())
                {
                    const string sql = "UPDATE OrderDetails SET Quantity = @Quantity WHERE OrderDetailID = @OrderDetailId";
                    var result = con.Execute(sql, new { Quantity = newQuantity, OrderDetailId = orderDetailId });
                    return result > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public bool UpdateUnitPrice(int orderDetailId, decimal newUnitPrice)
        {
            try
            {
                using (var con = _connectionFactory.CreateConnection())
                {
                    const string sql = "UPDATE OrderDetails SET UnitPrice = @UnitPrice WHERE OrderDetailID = @OrderDetailId";
                    var result = con.Execute(sql, new { UnitPrice = newUnitPrice, OrderDetailId = orderDetailId });
                    return result > 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}