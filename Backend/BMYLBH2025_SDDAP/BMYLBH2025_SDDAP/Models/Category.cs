using System;
using System.Collections.Generic;
using System.Linq;

namespace BMYLBH2025_SDDAP.Models
{
    public class Category
    {
        // Primary Key
        public int CategoryID { get; set; }
        
        // Basic Information
        public string Name { get; set; }
        public string Description { get; set; }
        
        // Navigation Properties
        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>();
        }

        // Business Methods
        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
                
            if (!Products.Contains(product))
            {
                Products.Add(product);
                product.CategoryID = this.CategoryID;
            }
        }

        public void RemoveProduct(int productId)
        {
            var product = Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                Products.Remove(product);
                product.CategoryID = 0; // Reset category
            }
        }
        
        public List<Product> GetProducts()
        {
            return Products.ToList();
        }
        
        public int GetProductCount()
        {
            return Products.Count;
        }
        
        public decimal GetTotalCategoryValue()
        {
            return Products.Sum(p => p.CalculateTotalValue());
        }
        
        public List<Product> GetLowStockProducts()
        {
            return Products.Where(p => p.IsLowStock()).ToList();
        }
    }
}