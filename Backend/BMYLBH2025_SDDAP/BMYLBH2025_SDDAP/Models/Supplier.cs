using System.Collections.Generic;

namespace BMYLBH2025_SDDAP.Models
{
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
}