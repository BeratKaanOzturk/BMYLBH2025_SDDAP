using System;
using System.Collections.Generic;
using System.Linq;

namespace BMYLBH2025_SDDAP.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation Properties
        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            if (product == null) return;
                
            if (Products == null)
                Products = new List<Product>();
                
            if (!Products.Contains(product))
            {
                Products.Add(product);
                product.CategoryID = this.CategoryID;
            }
        }

        public bool RemoveProduct(Product product)
        {
            if (product == null || Products == null) return false;
            
            return Products.Remove(product);
        }

        public void RemoveProduct(int productId)
        {
            if (Products == null) return;
            
            var product = Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                Products.Remove(product);
            }
        }
    }
}