using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using BMYLBH2025_SDDAP.Models;

namespace BMYLBH2025_SDDAP.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private string _authToken;

        public ApiService() : this("https://localhost:44313")
        {
        }

        public ApiService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = httpClient.BaseAddress?.ToString() ?? "https://localhost:44313";
        }

        public void SetAuthToken(string token)
        {
            _authToken = token;
            _httpClient.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public void ClearAuthToken()
        {
            _authToken = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        // Generic HTTP Methods
        private async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
                
                return default(T);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calling GET {endpoint}: {ex.Message}");
                return default(T);
            }
        }

        private async Task<T> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync(endpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                if (!string.IsNullOrEmpty(responseContent))
                {
                    try
                    {
                        return JsonConvert.DeserializeObject<T>(responseContent);
                    }
                    catch (JsonException)
                    {
                        // Return error response for failed deserialization
                        return CreateErrorResponse<T>($"Failed to parse response from {endpoint}");
                    }
                }
                
                return CreateErrorResponse<T>($"No response from {endpoint}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calling POST {endpoint}: {ex.Message}");
                return CreateErrorResponse<T>($"Network error: {ex.Message}");
            }
        }

        private async Task<T> PutAsync<T>(string endpoint, object data)
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
                
                return default(T);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calling PUT {endpoint}: {ex.Message}");
                return default(T);
            }
        }

        private async Task<bool> DeleteAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(endpoint);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calling DELETE {endpoint}: {ex.Message}");
                return false;
            }
        }

        private T CreateErrorResponse<T>(string message)
        {
            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(ApiResponse<>))
            {
                var responseType = typeof(T);
                var response = Activator.CreateInstance(responseType);
                
                var successProperty = responseType.GetProperty("Success");
                var messageProperty = responseType.GetProperty("Message");
                
                successProperty?.SetValue(response, false);
                messageProperty?.SetValue(response, message);
                
                return (T)response;
            }
            
            return default(T);
        }

        // Authentication Methods
        public async Task<ApiResponse<LoginResponseData>> LoginAsync(LoginRequest request)
        {
            return await PostAsync<ApiResponse<LoginResponseData>>("api/auth/login", request);
        }

        public async Task<ApiResponse<RegisterResponseData>> RegisterAsync(RegisterRequest request)
        {
            return await PostAsync<ApiResponse<RegisterResponseData>>("api/auth/register", request);
        }

        public async Task<ApiResponse<EmailResponseData>> ResendVerificationEmailAsync(ResendVerificationRequest request)
        {
            return await PostAsync<ApiResponse<EmailResponseData>>("api/auth/resend-verification", request);
        }

        public async Task<ApiResponse<EmailResponseData>> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            return await PostAsync<ApiResponse<EmailResponseData>>("api/auth/forgot-password", request);
        }

        public async Task<ApiResponse<EmailResponseData>> VerifyEmailAsync(string token)
        {
            return await GetAsync<ApiResponse<EmailResponseData>>($"api/auth/verify-email?token={token}");
        }

        public async Task<ApiResponse<EmailResponseData>> VerifyResetOTPAsync(VerifyOTPRequest request)
        {
            return await PostAsync<ApiResponse<EmailResponseData>>("api/auth/verify-reset-otp", request);
        }

        public async Task<ApiResponse<EmailResponseData>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return await PostAsync<ApiResponse<EmailResponseData>>("api/auth/reset-password", request);
        }

        // Inventory Methods
        public async Task<List<Inventory>> GetAllInventoryAsync()
        {
            return await GetAsync<List<Inventory>>("api/inventory") ?? new List<Inventory>();
        }

        public async Task<Inventory> GetInventoryByIdAsync(int id)
        {
            return await GetAsync<Inventory>($"api/inventory/{id}");
        }

        public async Task<List<Inventory>> GetLowStockItemsAsync()
        {
            return await GetAsync<List<Inventory>>("api/inventory/lowstock") ?? new List<Inventory>();
        }

        public async Task<decimal> GetTotalInventoryValueAsync()
        {
            try
            {
                var response = await GetAsync<dynamic>("api/inventory/totalvalue");
                return response?.TotalValue ?? 0m;
            }
            catch
            {
                return 0m;
            }
        }

        public async Task<Inventory> CreateInventoryAsync(Inventory inventory)
        {
            return await PostAsync<Inventory>("api/inventory", inventory);
        }

        public async Task<Inventory> UpdateInventoryAsync(int id, Inventory inventory)
        {
            return await PutAsync<Inventory>($"api/inventory/{id}", inventory);
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
            var response = await GetAsync<ApiResponse<IEnumerable<Product>>>("api/products");
            return response?.Data != null ? new List<Product>(response.Data) : new List<Product>();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var response = await GetAsync<ApiResponse<Product>>($"api/products/{id}");
            return response?.Data;
        }

        public async Task<List<Product>> SearchProductsAsync(string name = "", decimal? minPrice = null, decimal? maxPrice = null)
        {
            var queryParams = new List<string>();
            
            if (!string.IsNullOrEmpty(name))
                queryParams.Add($"name={Uri.EscapeDataString(name)}");
            
            if (minPrice.HasValue)
                queryParams.Add($"minPrice={minPrice.Value}");
            
            if (maxPrice.HasValue)
                queryParams.Add($"maxPrice={maxPrice.Value}");
            
            var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
            var response = await GetAsync<ApiResponse<IEnumerable<Product>>>($"api/products/search{queryString}");
            
            return response?.Data != null ? new List<Product>(response.Data) : new List<Product>();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var response = await PostAsync<ApiResponse<Product>>("api/products", product);
            return response?.Data;
        }

        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            var response = await PutAsync<ApiResponse<Product>>($"api/products/{id}", product);
            return response?.Data;
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
} 