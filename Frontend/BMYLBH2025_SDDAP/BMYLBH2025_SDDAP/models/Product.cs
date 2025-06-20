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
}