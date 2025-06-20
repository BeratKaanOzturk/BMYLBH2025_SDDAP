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
    }
} 