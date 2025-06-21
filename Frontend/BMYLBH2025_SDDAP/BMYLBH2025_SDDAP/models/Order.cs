using System;
using System.Collections.Generic;
using System.Linq;

namespace BMYLBH2025_SDDAP.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int SupplierID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Supplier Supplier { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetail>();
            OrderDate = DateTime.Now;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Status = "Pending";
        }

        // Business methods
        public void AddItem(int productId, int quantity, decimal unitPrice)
        {
            var existingDetail = OrderDetails.FirstOrDefault(od => od.ProductID == productId);
            if (existingDetail != null)
            {
                existingDetail.Quantity += quantity;
            }
            else
            {
                OrderDetails.Add(new OrderDetail
                {
                    OrderID = this.OrderID,
                    ProductID = productId,
                    Quantity = quantity,
                    UnitPrice = unitPrice
                });
            }
            RecalculateTotal();
        }

        public void RemoveItem(int orderDetailId)
        {
            var detail = OrderDetails.FirstOrDefault(od => od.OrderDetailID == orderDetailId);
            if (detail != null)
            {
                OrderDetails.Remove(detail);
                RecalculateTotal();
            }
        }

        public void UpdateItemQuantity(int productId, int newQuantity)
        {
            var detail = OrderDetails.FirstOrDefault(od => od.ProductID == productId);
            if (detail != null)
            {
                if (newQuantity <= 0)
                {
                    OrderDetails.Remove(detail);
                }
                else
                {
                    detail.Quantity = newQuantity;
                }
                RecalculateTotal();
            }
        }

        public void RecalculateTotal()
        {
            TotalAmount = OrderDetails.Sum(od => od.CalculateSubtotal());
        }

        public bool PlaceOrder()
        {
            if (OrderDetails.Any() && Status == "Pending")
            {
                Status = "Confirmed";
                UpdatedAt = DateTime.Now;
                return true;
            }
            return false;
        }

        public bool CancelOrder()
        {
            if (Status == "Pending" || Status == "Confirmed")
            {
                Status = "Cancelled";
                UpdatedAt = DateTime.Now;
                return true;
            }
            return false;
        }

        public bool CompleteOrder()
        {
            if (Status == "Confirmed" || Status == "Processing")
            {
                Status = "Completed";
                UpdatedAt = DateTime.Now;
                return true;
            }
            return false;
        }

        public int GetTotalItems()
        {
            return OrderDetails.Sum(od => od.Quantity);
        }

        public string GetStatusColor()
        {
            return Status?.ToLower() switch
            {
                "pending" => "#FFC107",     // Yellow
                "confirmed" => "#17A2B8",   // Blue
                "processing" => "#007BFF",  // Primary Blue
                "completed" => "#28A745",   // Green
                "cancelled" => "#DC3545",   // Red
                _ => "#6C757D"              // Gray
            };
        }

        public bool CanEdit()
        {
            return Status == "Pending";
        }

        public bool CanCancel()
        {
            return Status == "Pending" || Status == "Confirmed";
        }

        public bool CanComplete()
        {
            return Status == "Confirmed" || Status == "Processing";
        }
    }
}