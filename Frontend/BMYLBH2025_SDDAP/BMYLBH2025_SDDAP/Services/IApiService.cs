using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BMYLBH2025_SDDAP.Models;

namespace BMYLBH2025_SDDAP.Services
{
    public interface IApiService : IDisposable
    {
        // Authentication methods
        Task<ApiResponse<LoginResponseData>> LoginAsync(LoginRequest request);
        Task<ApiResponse<RegisterResponseData>> RegisterAsync(RegisterRequest request);
        Task<ApiResponse<EmailResponseData>> ResendVerificationEmailAsync(ResendVerificationRequest request);
        Task<ApiResponse<EmailResponseData>> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<ApiResponse<EmailResponseData>> VerifyEmailAsync(string token);
        Task<ApiResponse<EmailResponseData>> VerifyResetOTPAsync(VerifyOTPRequest request);
        Task<ApiResponse<EmailResponseData>> ResetPasswordAsync(ResetPasswordRequest request);

        // Token management
        void SetAuthToken(string token);
        void ClearAuthToken();

        // Inventory methods
        Task<List<Inventory>> GetAllInventoryAsync();
        Task<Inventory> GetInventoryByIdAsync(int id);
        Task<Inventory> GetInventoryByProductIdAsync(int productId);
        Task<List<Inventory>> GetInventoryByCategoryAsync(int categoryId);
        Task<List<Inventory>> GetLowStockItemsAsync();
        Task<decimal> GetTotalInventoryValueAsync();
        Task<Inventory> CreateInventoryAsync(Inventory inventory);
        Task<Inventory> UpdateInventoryAsync(int id, Inventory inventory);
        Task<bool> UpdateStockAsync(int productId, int quantity);
        Task<bool> DeleteInventoryAsync(int id);

        // Product methods
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> SearchProductsAsync(string name = "", decimal? minPrice = null, decimal? maxPrice = null);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(int id, Product product);
        Task<bool> DeleteProductAsync(int id);

        // Category methods
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> GetCategoryByNameAsync(string name);
        Task<List<Category>> GetCategoriesWithProductsAsync();
        Task<List<Category>> GetCategoriesWithLowStockAsync();
        Task<int> GetCategoryProductCountAsync(int categoryId);
        Task<decimal> GetCategoryTotalValueAsync(int categoryId);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(int id, Category category);
        Task<bool> DeleteCategoryAsync(int id);

        // Order methods
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<List<Order>> GetOrdersBySupplierAsync(int supplierId);
        Task<List<Order>> GetOrdersByStatusAsync(string status);
        Task<List<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(int id, Order order);
        Task<bool> DeleteOrderAsync(int id);
        Task<bool> UpdateOrderStatusAsync(int id, string status);

        // Order Detail methods
        Task<List<OrderDetail>> GetOrderDetailsAsync(int orderId);
        Task<OrderDetail> GetOrderDetailByIdAsync(int id);
        Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail);
        Task<OrderDetail> UpdateOrderDetailAsync(int id, OrderDetail orderDetail);
        Task<bool> DeleteOrderDetailAsync(int id);

        // Sales analytics methods
        Task<decimal> GetTotalSalesAsync();
        Task<decimal> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<Order>> GetTopOrdersAsync(int count = 10);
        Task<Dictionary<string, decimal>> GetSalesByStatusAsync();

        // Supplier methods
        Task<List<Supplier>> GetAllSuppliersAsync();
        Task<Supplier> GetSupplierByIdAsync(int id);
        Task<Supplier> GetSupplierByNameAsync(string name);
        Task<List<Supplier>> SearchSuppliersAsync(string query = "");
        Task<Supplier> CreateSupplierAsync(Supplier supplier);
        Task<Supplier> UpdateSupplierAsync(int id, Supplier supplier);
        Task<bool> DeleteSupplierAsync(int id);
    }
} 