using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMYLBH2025_SDDAP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    }
}

namespace BMYLBH2025_SDDAP.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }

        public User()
        {
            // Constructor
        }

        public bool Login(string username, string password)
        {
            // Login process
            return false;
        }

        public void Logout()
        {
            // Logout process
        }

        public bool ResetPassword(string oldPassword, string newPassword)
        {
            // Reset password
            return false;
        }

        public void UpdateProfile(string email, string username)
        {
            // Update profile
        }
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int MinimumStockLevel { get; set; }
        public int CategoryID { get; set; }

        public Product()
        {
            // Constructor
        }

        public void UpdateInfo(string name, string description, decimal price)
        {
            // Update product information
        }

        public void SetCategory(int categoryId)
        {
            // Assign category
        }

        public bool CheckStockLevel()
        {
            // Check stock level
            return false;
        }
    }

    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Category()
        {
            // Constructor
        }

        public void AddProduct(Product product)
        {
            // Add product
        }

        public void RemoveProduct(int productId)
        {
            // Remove product
        }
    }

    public class Supplier
    {
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Supplier()
        {
            // Constructor
        }

        public void UpdateInfo(string name, string contactPerson, string email, string phone, string address)
        {
            // Update supplier information
        }

        public List<Order> GetOrderHistory()
        {
            // Get order history
            return new List<Order>();
        }
    }

    public class Inventory
    {
        public int InventoryID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }

        public Inventory()
        {
            // Constructor
        }

        public void AddStock(int amount)
        {
            // Add stock
        }

        public bool RemoveStock(int amount)
        {
            // Remove stock
            return false;
        }

        public bool CheckLowStock()
        {
            // Check low stock
            return false;
        }
    }

    public class Order
    {
        public int OrderID { get; set; }
        public int SupplierID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }

        public Order()
        {
            // Constructor
        }

        public void AddItem(int productId, int quantity, decimal unitPrice)
        {
            // Add order item
        }

        public void RemoveItem(int orderDetailId)
        {
            // Remove order item
        }

        public bool PlaceOrder()
        {
            // Complete order
            return false;
        }

        public bool CancelOrder()
        {
            // Cancel order
            return false;
        }

        public List<OrderDetail> GetOrderDetails()
        {
            // Get order details
            return new List<OrderDetail>();
        }
    }

    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

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

    public class Notification
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }

        public Notification()
        {
            // Constructor
        }

        public void MarkAsRead()
        {
            // Mark as read
        }

        public bool Send()
        {
            // Send notification
            return false;
        }
    }
}
