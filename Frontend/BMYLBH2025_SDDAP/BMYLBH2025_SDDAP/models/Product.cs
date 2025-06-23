namespace BMYLBH2025_SDDAP.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int MinimumStockLevel { get; set; }
        public int CategoryID { get; set; }

        // Navigation property
        public Category Category { get; set; }
        
        // Additional properties for UI
        public string CategoryName { get; set; }
        public int? StockQuantity { get; set; }

        public Product()
        {
            // Constructor
        }

        public void UpdateInfo(string name, string description, decimal price)
        {
            // Update product information
            Name = name;
            Description = description;
            Price = price;
        }

        public void SetCategory(int categoryId)
        {
            // Assign category
            CategoryID = categoryId;
        }

        public bool CheckStockLevel()
        {
            // Check stock level
            return StockQuantity.HasValue && StockQuantity.Value > MinimumStockLevel;
        }
        
        public bool IsLowStock()
        {
            // Check if product is low on stock
            return StockQuantity.HasValue && StockQuantity.Value <= MinimumStockLevel;
        }
    }
}