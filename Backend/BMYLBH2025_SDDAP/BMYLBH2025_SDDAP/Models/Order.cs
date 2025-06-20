using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace BMYLBH2025_SDDAP.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        
        // Navigation Properties
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
            OrderDate = DateTime.Now;
            Status = "Pending";
        }
        
        // Business Methods
        public void AddOrderDetail(OrderDetail detail)
        {
            if (detail == null) return;
            
            if (OrderDetails == null)
                OrderDetails = new List<OrderDetail>();
                
            OrderDetails.Add(detail);
            detail.OrderID = this.OrderID;
            RecalculateTotal();
        }
        
        public void RemoveOrderDetail(OrderDetail detail)
        {
            if (detail == null || OrderDetails == null) return;
            
            OrderDetails.Remove(detail);
            RecalculateTotal();
        }
        
        public void RecalculateTotal()
        {
            TotalAmount = OrderDetails?.Sum(od => od.Quantity * od.UnitPrice) ?? 0;
        }
        
        public bool IsValidOrder()
        {
            return UserID > 0 && TotalAmount > 0 && OrderDetails?.Any() == true;
        }
        
        public void UpdateStatus(string newStatus)
        {
            if (!string.IsNullOrWhiteSpace(newStatus))
                Status = newStatus;
        }
    }
    
    public interface IOrderRepository : IBaseRepository<Order>
    {
        // Business-specific methods
        IEnumerable<Order> GetByUserId(int userId);
        IEnumerable<Order> GetByStatus(string status);
        IEnumerable<Order> GetByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<Order> GetRecentOrders(int count = 10);
        decimal GetTotalOrderValue(int orderId);
        bool UpdateOrderStatus(int orderId, string status);
        IEnumerable<Order> GetOrdersWithDetails();
    }
    
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        
        public OrderRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        
        public IEnumerable<Order> GetAll()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Orders ORDER BY OrderDate DESC";
                return con.Query<Order>(sql).ToList();
            }
        }
        
        public Order GetById(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Orders WHERE OrderID = @Id";
                return con.QuerySingleOrDefault<Order>(sql, new { Id = id });
            }
        }
        
        public void Add(Order entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    INSERT INTO Orders (UserID, OrderDate, Status, TotalAmount) 
                    VALUES (@UserID, @OrderDate, @Status, @TotalAmount)";
                    
                con.Execute(sql, entity);
            }
        }
        
        public void Update(Order entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    UPDATE Orders 
                    SET UserID = @UserID, 
                        OrderDate = @OrderDate, 
                        Status = @Status, 
                        TotalAmount = @TotalAmount 
                    WHERE OrderID = @OrderID";
                    
                con.Execute(sql, entity);
            }
        }
        
        public void Delete(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "DELETE FROM Orders WHERE OrderID = @Id";
                con.Execute(sql, new { Id = id });
            }
        }
        
        public IEnumerable<Order> GetByUserId(int userId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Orders WHERE UserID = @UserId ORDER BY OrderDate DESC";
                return con.Query<Order>(sql, new { UserId = userId }).ToList();
            }
        }
        
        public IEnumerable<Order> GetByStatus(string status)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Orders WHERE Status = @Status ORDER BY OrderDate DESC";
                return con.Query<Order>(sql, new { Status = status }).ToList();
            }
        }
        
        public IEnumerable<Order> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT * FROM Orders 
                    WHERE OrderDate >= @StartDate AND OrderDate <= @EndDate 
                    ORDER BY OrderDate DESC";
                    
                return con.Query<Order>(sql, new { StartDate = startDate, EndDate = endDate }).ToList();
            }
        }
        
        public IEnumerable<Order> GetRecentOrders(int count = 10)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Orders ORDER BY OrderDate DESC LIMIT @Count";
                return con.Query<Order>(sql, new { Count = count }).ToList();
            }
        }
        
        public decimal GetTotalOrderValue(int orderId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT COALESCE(SUM(od.Quantity * od.UnitPrice), 0) 
                    FROM OrderDetails od 
                    WHERE od.OrderID = @OrderId";
                    
                return con.QuerySingle<decimal>(sql, new { OrderId = orderId });
            }
        }
        
        public bool UpdateOrderStatus(int orderId, string status)
        {
            try
            {
                using (var con = _connectionFactory.CreateConnection())
                {
                    const string sql = "UPDATE Orders SET Status = @Status WHERE OrderID = @OrderId";
                    var result = con.Execute(sql, new { Status = status, OrderId = orderId });
                    return result > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public IEnumerable<Order> GetOrdersWithDetails()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT o.*, od.*, p.Name as ProductName
                    FROM Orders o
                    LEFT JOIN OrderDetails od ON o.OrderID = od.OrderID
                    LEFT JOIN Products p ON od.ProductID = p.ProductID
                    ORDER BY o.OrderDate DESC";
                    
                var orderDict = new Dictionary<int, Order>();
                
                return con.Query<Order, OrderDetail, Order>(sql,
                    (order, orderDetail) =>
                    {
                        if (!orderDict.TryGetValue(order.OrderID, out Order orderEntry))
                        {
                            orderEntry = order;
                            orderEntry.OrderDetails = new List<OrderDetail>();
                            orderDict.Add(order.OrderID, orderEntry);
                        }
                        
                        if (orderDetail != null)
                        {
                            orderEntry.OrderDetails.Add(orderDetail);
                        }
                        
                        return orderEntry;
                    },
                    splitOn: "OrderDetailID").Distinct().ToList();
            }
        }
    }
}