namespace BMYLBH2025_SDDAP.Models
{
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
}