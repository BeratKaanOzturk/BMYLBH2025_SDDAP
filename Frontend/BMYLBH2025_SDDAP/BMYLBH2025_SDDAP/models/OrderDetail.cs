namespace BMYLBH2025_SDDAP.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public Product Product { get; set; }

        public OrderDetail()
        {
            Quantity = 1;
            UnitPrice = 0m;
        }

        public decimal CalculateSubtotal()
        {
            return Quantity * UnitPrice;
        }

        public decimal GetTotalDiscount()
        {
            // For future discount functionality
            return 0m;
        }

        public decimal GetFinalAmount()
        {
            return CalculateSubtotal() - GetTotalDiscount();
        }

        public string GetFormattedSubtotal()
        {
            return CalculateSubtotal().ToString("C");
        }

        public string GetFormattedUnitPrice()
        {
            return UnitPrice.ToString("C");
        }

        public bool IsValidQuantity()
        {
            return Quantity > 0;
        }

        public bool IsValidPrice()
        {
            return UnitPrice >= 0;
        }

        public bool IsValid()
        {
            return IsValidQuantity() && IsValidPrice() && ProductID > 0;
        }
    }
}