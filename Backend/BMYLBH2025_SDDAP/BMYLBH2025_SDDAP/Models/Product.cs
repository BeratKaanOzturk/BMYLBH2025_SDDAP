using System;
using System.Collections.Generic;

namespace BMYLBH2025_SDDAP.Models
{
    public class Product
    {
        // Primary Key
        public int ProductID { get; set; }
        
        // Basic Information
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int MinimumStockLevel { get; set; }
        
        // Foreign Key
        public int CategoryID { get; set; }
        
        // Navigation Properties (for relationships)
        public virtual Category Category { get; set; }
        public virtual Inventory Inventory { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Product()
        {
            OrderDetails = new List<OrderDetail>();
        }

        // Business Methods
        public void UpdateInfo(string name, string description, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty");
            
            if (price < 0)
                throw new ArgumentException("Product price cannot be negative");
                
            Name = name;
            Description = description;
            Price = price;
        }

        public void SetCategory(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Invalid category ID");
                
            CategoryID = categoryId;
        }

        public bool CheckStockLevel()
        {
            return Inventory != null && Inventory.Quantity > MinimumStockLevel;
        }
        
        public bool IsLowStock()
        {
            return Inventory != null && Inventory.Quantity <= MinimumStockLevel;
        }
        
        public decimal CalculateTotalValue()
        {
            return Inventory != null ? Inventory.Quantity * Price : 0;
        }
    }
}