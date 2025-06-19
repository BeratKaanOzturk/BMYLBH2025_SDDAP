using System;
using System.Collections.Generic;

namespace BMYLBH2025_SDDAP.Models
{
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
}