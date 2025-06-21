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
        public int SupplierID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public int? CreatedBy { get; set; }
        
        // Navigation Properties
        public virtual Supplier Supplier { get; set; }
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
            return SupplierID > 0 && TotalAmount > 0 && OrderDetails?.Any() == true;
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
        IEnumerable<Order> GetBySupplierId(int supplierId);
        IEnumerable<Order> GetByCreatedBy(int userId);
        IEnumerable<Order> GetByStatus(string status);
        IEnumerable<Order> GetByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<Order> GetRecentOrders(int count = 10);
        decimal GetTotalOrderValue(int orderId);
        bool UpdateOrderStatus(int orderId, string status);
        IEnumerable<Order> GetOrdersWithDetails();
        
        // Additional methods needed by controller
        Order GetOrderWithDetails(int id);
        IEnumerable<Order> GetOrdersBySupplier(int supplierId);
        IEnumerable<Order> GetOrdersByStatus(string status);
        dynamic GetSalesAnalytics();
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
                const string sql = @"
                    SELECT o.OrderID, o.SupplierID, o.OrderDate, o.Status, o.TotalAmount, o.CreatedBy,
                           s.SupplierID AS Supplier_SupplierID, s.Name AS Supplier_Name, s.ContactPerson AS Supplier_ContactPerson, 
                           s.Email AS Supplier_Email, s.Phone AS Supplier_Phone, s.Address AS Supplier_Address
                    FROM Orders o
                    LEFT JOIN Suppliers s ON o.SupplierID = s.SupplierID
                    ORDER BY o.OrderDate DESC";
                var result = con.Query(sql);
                var orders = new List<Order>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var order = new Order
                        {
                            OrderID = row.OrderID == null ? 0 : Convert.ToInt32(row.OrderID),
                            SupplierID = row.SupplierID == null ? 0 : Convert.ToInt32(row.SupplierID),
                            OrderDate = row.OrderDate == null ? DateTime.Now : Convert.ToDateTime(row.OrderDate),
                            Status = Convert.ToString(row.Status) ?? string.Empty,
                            TotalAmount = row.TotalAmount == null ? 0m : Convert.ToDecimal(row.TotalAmount),
                            CreatedBy = row.CreatedBy == null ? (int?)null : Convert.ToInt32(row.CreatedBy),
                            Supplier = row.Supplier_SupplierID != null ? new Supplier
                            {
                                SupplierID = Convert.ToInt32(row.Supplier_SupplierID),
                                Name = Convert.ToString(row.Supplier_Name) ?? string.Empty,
                                ContactPerson = Convert.ToString(row.Supplier_ContactPerson) ?? string.Empty,
                                Email = Convert.ToString(row.Supplier_Email) ?? string.Empty,
                                Phone = Convert.ToString(row.Supplier_Phone) ?? string.Empty,
                                Address = Convert.ToString(row.Supplier_Address) ?? string.Empty
                            } : null
                        };
                        
                        orders.Add(order);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return orders;
            }
        }
        
        public Order GetById(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Orders WHERE OrderID = @Id";
                var row = con.QueryFirstOrDefault(sql, new { Id = id });
                if (row == null) return null;
                
                try
                {
                    var order = new Order
                    {
                        OrderID = row.OrderID == null ? 0 : Convert.ToInt32(row.OrderID),
                        SupplierID = row.SupplierID == null ? 0 : Convert.ToInt32(row.SupplierID),
                        OrderDate = row.OrderDate == null ? DateTime.Now : Convert.ToDateTime(row.OrderDate),
                        Status = Convert.ToString(row.Status) ?? string.Empty,
                        TotalAmount = row.TotalAmount == null ? 0m : Convert.ToDecimal(row.TotalAmount),
                        CreatedBy = row.CreatedBy == null ? (int?)null : Convert.ToInt32(row.CreatedBy)
                    };
                    
                    return order;
                }
                catch (Exception ex)
                {
                    // Log the error or handle as needed
                    // Return null if conversion fails
                    return null;
                }
            }
        }
        
        public void Add(Order entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    INSERT INTO Orders (SupplierID, OrderDate, Status, TotalAmount, CreatedBy) 
                    VALUES (@SupplierID, @OrderDate, @Status, @TotalAmount, @CreatedBy);
                    SELECT last_insert_rowid();";
                    
                var newId = con.QuerySingle<int>(sql, entity);
                entity.OrderID = newId;
            }
        }
        
        public void Update(Order entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    UPDATE Orders 
                    SET SupplierID = @SupplierID, 
                        OrderDate = @OrderDate, 
                        Status = @Status, 
                        TotalAmount = @TotalAmount,
                        CreatedBy = @CreatedBy
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
        
        public IEnumerable<Order> GetBySupplierId(int supplierId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Orders WHERE SupplierID = @SupplierId ORDER BY OrderDate DESC";
                var result = con.Query(sql, new { SupplierId = supplierId });
                var orders = new List<Order>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var order = new Order
                        {
                            OrderID = row.OrderID == null ? 0 : Convert.ToInt32(row.OrderID),
                            SupplierID = row.SupplierID == null ? 0 : Convert.ToInt32(row.SupplierID),
                            OrderDate = row.OrderDate == null ? DateTime.Now : Convert.ToDateTime(row.OrderDate),
                            Status = Convert.ToString(row.Status) ?? string.Empty,
                            TotalAmount = row.TotalAmount == null ? 0m : Convert.ToDecimal(row.TotalAmount),
                            CreatedBy = row.CreatedBy == null ? (int?)null : Convert.ToInt32(row.CreatedBy)
                        };
                        
                        orders.Add(order);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return orders;
            }
        }
        
        public IEnumerable<Order> GetByCreatedBy(int userId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Orders WHERE CreatedBy = @UserId ORDER BY OrderDate DESC";
                var result = con.Query(sql, new { UserId = userId });
                var orders = new List<Order>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var order = new Order
                        {
                            OrderID = row.OrderID == null ? 0 : Convert.ToInt32(row.OrderID),
                            SupplierID = row.SupplierID == null ? 0 : Convert.ToInt32(row.SupplierID),
                            OrderDate = row.OrderDate == null ? DateTime.Now : Convert.ToDateTime(row.OrderDate),
                            Status = Convert.ToString(row.Status) ?? string.Empty,
                            TotalAmount = row.TotalAmount == null ? 0m : Convert.ToDecimal(row.TotalAmount),
                            CreatedBy = row.CreatedBy == null ? (int?)null : Convert.ToInt32(row.CreatedBy)
                        };
                        
                        orders.Add(order);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return orders;
            }
        }
        
        public IEnumerable<Order> GetByStatus(string status)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Orders WHERE Status = @Status ORDER BY OrderDate DESC";
                var result = con.Query(sql, new { Status = status });
                var orders = new List<Order>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var order = new Order
                        {
                            OrderID = row.OrderID == null ? 0 : Convert.ToInt32(row.OrderID),
                            SupplierID = row.SupplierID == null ? 0 : Convert.ToInt32(row.SupplierID),
                            OrderDate = row.OrderDate == null ? DateTime.Now : Convert.ToDateTime(row.OrderDate),
                            Status = Convert.ToString(row.Status) ?? string.Empty,
                            TotalAmount = row.TotalAmount == null ? 0m : Convert.ToDecimal(row.TotalAmount),
                            CreatedBy = row.CreatedBy == null ? (int?)null : Convert.ToInt32(row.CreatedBy)
                        };
                        
                        orders.Add(order);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return orders;
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
                    
                var result = con.Query(sql, new { StartDate = startDate, EndDate = endDate });
                var orders = new List<Order>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var order = new Order
                        {
                            OrderID = row.OrderID == null ? 0 : Convert.ToInt32(row.OrderID),
                            SupplierID = row.SupplierID == null ? 0 : Convert.ToInt32(row.SupplierID),
                            OrderDate = row.OrderDate == null ? DateTime.Now : Convert.ToDateTime(row.OrderDate),
                            Status = Convert.ToString(row.Status) ?? string.Empty,
                            TotalAmount = row.TotalAmount == null ? 0m : Convert.ToDecimal(row.TotalAmount),
                            CreatedBy = row.CreatedBy == null ? (int?)null : Convert.ToInt32(row.CreatedBy)
                        };
                        
                        orders.Add(order);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return orders;
            }
        }
        
        public IEnumerable<Order> GetRecentOrders(int count = 10)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Orders ORDER BY OrderDate DESC LIMIT @Count";
                var result = con.Query(sql, new { Count = count });
                var orders = new List<Order>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var order = new Order
                        {
                            OrderID = row.OrderID == null ? 0 : Convert.ToInt32(row.OrderID),
                            SupplierID = row.SupplierID == null ? 0 : Convert.ToInt32(row.SupplierID),
                            OrderDate = row.OrderDate == null ? DateTime.Now : Convert.ToDateTime(row.OrderDate),
                            Status = Convert.ToString(row.Status) ?? string.Empty,
                            TotalAmount = row.TotalAmount == null ? 0m : Convert.ToDecimal(row.TotalAmount),
                            CreatedBy = row.CreatedBy == null ? (int?)null : Convert.ToInt32(row.CreatedBy)
                        };
                        
                        orders.Add(order);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return orders;
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
                    SELECT o.OrderID, o.SupplierID, o.OrderDate, o.Status, o.TotalAmount, o.CreatedBy,
                           s.SupplierID AS Supplier_SupplierID, s.Name AS Supplier_Name, s.ContactPerson AS Supplier_ContactPerson, 
                           s.Email AS Supplier_Email, s.Phone AS Supplier_Phone, s.Address AS Supplier_Address,
                           od.OrderDetailID, od.OrderID AS DetailOrderID, od.ProductID, od.Quantity, od.UnitPrice,
                           p.ProductID AS Product_ProductID, p.Name AS Product_Name, p.Description AS Product_Description, 
                           p.Price AS Product_Price, p.CategoryID AS Product_CategoryID
                    FROM Orders o
                    LEFT JOIN Suppliers s ON o.SupplierID = s.SupplierID
                    LEFT JOIN OrderDetails od ON o.OrderID = od.OrderID
                    LEFT JOIN Products p ON od.ProductID = p.ProductID
                    ORDER BY o.OrderDate DESC";
                    
                var result = con.Query(sql);
                var orderDict = new Dictionary<int, Order>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        int orderId = Convert.ToInt32(row.OrderID);
                        
                        if (!orderDict.ContainsKey(orderId))
                        {
                            orderDict[orderId] = new Order
                            {
                                OrderID = orderId,
                                SupplierID = row.SupplierID == null ? 0 : Convert.ToInt32(row.SupplierID),
                                OrderDate = row.OrderDate == null ? DateTime.Now : Convert.ToDateTime(row.OrderDate),
                                Status = Convert.ToString(row.Status) ?? string.Empty,
                                TotalAmount = row.TotalAmount == null ? 0m : Convert.ToDecimal(row.TotalAmount),
                                CreatedBy = row.CreatedBy == null ? (int?)null : Convert.ToInt32(row.CreatedBy),
                                OrderDetails = new List<OrderDetail>(),
                                Supplier = row.Supplier_SupplierID != null ? new Supplier
                                {
                                    SupplierID = Convert.ToInt32(row.Supplier_SupplierID),
                                    Name = Convert.ToString(row.Supplier_Name) ?? string.Empty,
                                    ContactPerson = Convert.ToString(row.Supplier_ContactPerson) ?? string.Empty,
                                    Email = Convert.ToString(row.Supplier_Email) ?? string.Empty,
                                    Phone = Convert.ToString(row.Supplier_Phone) ?? string.Empty,
                                    Address = Convert.ToString(row.Supplier_Address) ?? string.Empty
                                } : null
                            };
                        }
                        
                        // Add order detail if it exists
                        if (row.OrderDetailID != null)
                        {
                            var orderDetail = new OrderDetail
                            {
                                OrderDetailID = Convert.ToInt32(row.OrderDetailID),
                                OrderID = orderId,
                                ProductID = row.ProductID == null ? 0 : Convert.ToInt32(row.ProductID),
                                Quantity = row.Quantity == null ? 0 : Convert.ToInt32(row.Quantity),
                                UnitPrice = row.UnitPrice == null ? 0m : Convert.ToDecimal(row.UnitPrice),
                                Product = row.Product_ProductID != null ? new Product
                                {
                                    ProductID = Convert.ToInt32(row.Product_ProductID),
                                    Name = Convert.ToString(row.Product_Name) ?? "Unknown Product",
                                    Description = Convert.ToString(row.Product_Description) ?? string.Empty,
                                    Price = row.Product_Price == null ? 0m : Convert.ToDecimal(row.Product_Price),
                                    CategoryID = row.Product_CategoryID == null ? 0 : Convert.ToInt32(row.Product_CategoryID)
                                } : null
                            };
                            
                            orderDict[orderId].OrderDetails.Add(orderDetail);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return orderDict.Values.ToList();
            }
        }
        
        // Additional method implementations needed by controller
        public Order GetOrderWithDetails(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT o.OrderID, o.SupplierID, o.OrderDate, o.Status, o.TotalAmount, o.CreatedBy,
                           s.SupplierID AS Supplier_SupplierID, s.Name AS Supplier_Name, s.ContactPerson AS Supplier_ContactPerson, 
                           s.Email AS Supplier_Email, s.Phone AS Supplier_Phone, s.Address AS Supplier_Address,
                           od.OrderDetailID, od.OrderID AS DetailOrderID, od.ProductID, od.Quantity, od.UnitPrice,
                           p.ProductID AS Product_ProductID, p.Name AS Product_Name, p.Description AS Product_Description, 
                           p.Price AS Product_Price, p.CategoryID AS Product_CategoryID
                    FROM Orders o
                    LEFT JOIN Suppliers s ON o.SupplierID = s.SupplierID
                    LEFT JOIN OrderDetails od ON o.OrderID = od.OrderID
                    LEFT JOIN Products p ON od.ProductID = p.ProductID
                    WHERE o.OrderID = @OrderId
                    ORDER BY od.OrderDetailID";
                    
                var result = con.Query(sql, new { OrderId = id });
                Order order = null;
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        if (order == null)
                        {
                            order = new Order
                            {
                                OrderID = Convert.ToInt32(row.OrderID),
                                SupplierID = row.SupplierID == null ? 0 : Convert.ToInt32(row.SupplierID),
                                OrderDate = row.OrderDate == null ? DateTime.Now : Convert.ToDateTime(row.OrderDate),
                                Status = Convert.ToString(row.Status) ?? string.Empty,
                                TotalAmount = row.TotalAmount == null ? 0m : Convert.ToDecimal(row.TotalAmount),
                                CreatedBy = row.CreatedBy == null ? (int?)null : Convert.ToInt32(row.CreatedBy),
                                OrderDetails = new List<OrderDetail>(),
                                Supplier = row.Supplier_SupplierID != null ? new Supplier
                                {
                                    SupplierID = Convert.ToInt32(row.Supplier_SupplierID),
                                    Name = Convert.ToString(row.Supplier_Name) ?? string.Empty,
                                    ContactPerson = Convert.ToString(row.Supplier_ContactPerson) ?? string.Empty,
                                    Email = Convert.ToString(row.Supplier_Email) ?? string.Empty,
                                    Phone = Convert.ToString(row.Supplier_Phone) ?? string.Empty,
                                    Address = Convert.ToString(row.Supplier_Address) ?? string.Empty
                                } : null
                            };
                        }
                        
                        // Add order detail if it exists
                        if (row.OrderDetailID != null)
                        {
                            var orderDetail = new OrderDetail
                            {
                                OrderDetailID = Convert.ToInt32(row.OrderDetailID),
                                OrderID = Convert.ToInt32(row.OrderID),
                                ProductID = row.ProductID == null ? 0 : Convert.ToInt32(row.ProductID),
                                Quantity = row.Quantity == null ? 0 : Convert.ToInt32(row.Quantity),
                                UnitPrice = row.UnitPrice == null ? 0m : Convert.ToDecimal(row.UnitPrice),
                                Product = row.Product_ProductID != null ? new Product
                                {
                                    ProductID = Convert.ToInt32(row.Product_ProductID),
                                    Name = Convert.ToString(row.Product_Name) ?? "Unknown Product",
                                    Description = Convert.ToString(row.Product_Description) ?? string.Empty,
                                    Price = row.Product_Price == null ? 0m : Convert.ToDecimal(row.Product_Price),
                                    CategoryID = row.Product_CategoryID == null ? 0 : Convert.ToInt32(row.Product_CategoryID)
                                } : null
                            };
                            
                            order.OrderDetails.Add(orderDetail);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        continue;
                    }
                }
                
                return order;
            }
        }
        
        public IEnumerable<Order> GetOrdersBySupplier(int supplierId)
        {
            // This is the same as GetBySupplierId, just alias for controller compatibility
            return GetBySupplierId(supplierId);
        }
        
        public IEnumerable<Order> GetOrdersByStatus(string status)
        {
            // This is the same as GetByStatus, just alias for controller compatibility
            return GetByStatus(status);
        }
        
        public dynamic GetSalesAnalytics()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT 
                        COUNT(*) as TotalOrders,
                        COALESCE(SUM(TotalAmount), 0) as TotalSales,
                        COALESCE(AVG(TotalAmount), 0) as AverageOrderValue,
                        COUNT(CASE WHEN Status = 'Completed' THEN 1 END) as CompletedOrders,
                        COUNT(CASE WHEN Status = 'Pending' THEN 1 END) as PendingOrders,
                        COUNT(CASE WHEN Status = 'Cancelled' THEN 1 END) as CancelledOrders
                    FROM Orders";
                    
                var result = con.QueryFirstOrDefault(sql);
                
                return new
                {
                    TotalOrders = result?.TotalOrders ?? 0,
                    TotalSales = result?.TotalSales ?? 0m,
                    AverageOrderValue = result?.AverageOrderValue ?? 0m,
                    CompletedOrders = result?.CompletedOrders ?? 0,
                    PendingOrders = result?.PendingOrders ?? 0,
                    CancelledOrders = result?.CancelledOrders ?? 0
                };
            }
        }
    }
}