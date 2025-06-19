using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BMYLBH2025_SDDAP.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private string _authToken;

        public ApiService(string baseUrl = "https://localhost:44313")
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public void SetAuthToken(string token)
        {
            _authToken = token;
            _httpClient.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        // Generic HTTP Methods
        public async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
                
                throw new HttpRequestException($"API call failed: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calling API: {ex.Message}", ex);
            }
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync(endpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                // Always try to deserialize the response content, regardless of status code
                // This allows us to handle structured error responses from the backend
                if (!string.IsNullOrEmpty(responseContent))
                {
                    try
                    {
                        return JsonConvert.DeserializeObject<T>(responseContent);
                    }
                    catch (JsonException)
                    {
                        // If deserialization fails, throw the original error
                        throw new HttpRequestException($"API call failed: {response.StatusCode} - {responseContent}");
                    }
                }
                
                throw new HttpRequestException($"API call failed: {response.StatusCode} - No response content");
            }
            catch (HttpRequestException)
            {
                throw; // Re-throw HTTP exceptions as-is
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calling API: {ex.Message}", ex);
            }
        }

        public async Task<T> PutAsync<T>(string endpoint, object data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync(endpoint, content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
                
                throw new HttpRequestException($"API call failed: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calling API: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(endpoint);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calling API: {ex.Message}", ex);
            }
        }

        // Authentication Methods
        public async Task<ApiResponse<LoginResponseData>> LoginAsync(string email, string password)
        {
            var loginRequest = new { Email = email, Password = password };
            return await PostAsync<ApiResponse<LoginResponseData>>("api/auth/login", loginRequest);
        }

        public async Task<ApiResponse<RegisterResponseData>> RegisterAsync(string email, string password, string fullName)
        {
            var registerRequest = new { Email = email, Password = password, FullName = fullName };
            return await PostAsync<ApiResponse<RegisterResponseData>>("api/auth/register", registerRequest);
        }

        public async Task<ApiResponse<EmailResponseData>> ResendVerificationEmailAsync(string email)
        {
            var request = new { Email = email };
            return await PostAsync<ApiResponse<EmailResponseData>>("api/auth/resend-verification", request);
        }

        public async Task<ApiResponse<EmailResponseData>> ForgotPasswordAsync(string email)
        {
            var request = new { Email = email };
            return await PostAsync<ApiResponse<EmailResponseData>>("api/auth/forgot-password", request);
        }

        public async Task<ApiResponse<OTPResponseData>> VerifyResetOTPAsync(string email, string otp)
        {
            var request = new { Email = email, OTP = otp };
            return await PostAsync<ApiResponse<OTPResponseData>>("api/auth/verify-reset-otp", request);
        }

        public async Task<ApiResponse<PasswordResetResponseData>> ResetPasswordAsync(string email, string otp, string newPassword)
        {
            var request = new { Email = email, OTP = otp, NewPassword = newPassword };
            return await PostAsync<ApiResponse<PasswordResetResponseData>>("api/auth/reset-password", request);
        }

        // Inventory Methods
        public async Task<List<InventoryItem>> GetAllInventoryAsync()
        {
            return await GetAsync<List<InventoryItem>>("api/inventory");
        }

        public async Task<List<InventoryItem>> GetLowStockItemsAsync()
        {
            return await GetAsync<List<InventoryItem>>("api/inventory/lowstock");
        }

        public async Task<decimal> GetTotalInventoryValueAsync()
        {
            var response = await GetAsync<dynamic>("api/inventory/totalvalue");
            return response.TotalValue;
        }

        public async Task<InventoryItem> CreateInventoryAsync(InventoryItem inventory)
        {
            return await PostAsync<InventoryItem>("api/inventory", inventory);
        }

        public async Task<InventoryItem> UpdateInventoryAsync(int id, InventoryItem inventory)
        {
            return await PutAsync<InventoryItem>($"api/inventory/{id}", inventory);
        }

        public async Task<bool> UpdateStockAsync(int productId, int quantity)
        {
            var request = new { Quantity = quantity };
            var response = await PutAsync<dynamic>($"api/inventory/updatestock/{productId}", request);
            return response != null;
        }

        public async Task<bool> DeleteInventoryAsync(int id)
        {
            return await DeleteAsync($"api/inventory/{id}");
        }

        // Product Methods
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await GetAsync<List<Product>>("api/products");
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await GetAsync<Product>($"api/products/{id}");
        }

        public async Task<List<Product>> SearchProductsAsync(string name = "", decimal? minPrice = null, decimal? maxPrice = null)
        {
            var queryParams = new List<string>();
            
            if (!string.IsNullOrEmpty(name))
                queryParams.Add($"name={Uri.EscapeDataString(name)}");
                
            if (minPrice.HasValue)
                queryParams.Add($"minPrice={minPrice}");
                
            if (maxPrice.HasValue)
                queryParams.Add($"maxPrice={maxPrice}");

            var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
            return await GetAsync<List<Product>>($"api/products/search{queryString}");
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            return await PostAsync<Product>("api/products", product);
        }

        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            return await PutAsync<Product>($"api/products/{id}", product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await DeleteAsync($"api/products/{id}");
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }

    // Generic API Response wrapper
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string ErrorType { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class ApiResponse : ApiResponse<object>
    {
    }

    // DTOs matching backend models
    public class LoginResponseData
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public bool ShowResendVerification { get; set; }
        public bool ShowForgotPassword { get; set; }
    }

    public class RegisterResponseData
    {
        public int UserId { get; set; }
        public string Email { get; set; }
    }

    public class EmailResponseData
    {
        public string Email { get; set; }
        public string Action { get; set; }
    }

    public class OTPResponseData
    {
        public string Email { get; set; }
        public bool IsValid { get; set; }
    }

    public class PasswordResetResponseData
    {
        public string Email { get; set; }
        public bool Success { get; set; }
    }

    public class InventoryItem
    {
        public int InventoryID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
        public Product Product { get; set; }
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int MinimumStockLevel { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }

    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
} 