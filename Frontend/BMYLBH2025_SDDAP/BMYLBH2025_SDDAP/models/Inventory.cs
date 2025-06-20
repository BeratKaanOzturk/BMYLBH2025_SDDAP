using System;

namespace BMYLBH2025_SDDAP.Models
{
    public class Inventory
    {
        public int InventoryID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }

        // Navigation property
        public Product Product { get; set; }

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
}