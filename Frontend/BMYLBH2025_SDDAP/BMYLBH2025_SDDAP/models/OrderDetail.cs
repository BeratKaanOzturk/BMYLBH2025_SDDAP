namespace BMYLBH2025_SDDAP.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public OrderDetail()
        {
            // Constructor
        }

        public decimal CalculateSubtotal()
        {
            // Calculate subtotal
            return Quantity * UnitPrice;
        }
    }
}