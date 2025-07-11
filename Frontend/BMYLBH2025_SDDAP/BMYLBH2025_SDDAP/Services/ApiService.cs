using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
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
                        var result = JsonConvert.DeserializeObject<T>(responseContent);
                        
                        // Check if this is an ApiResponse and if it contains an error
                        if (result is ApiResponse apiResponse && !apiResponse.Success)
                        {
                            System.Diagnostics.Debug.WriteLine($"API Error from {endpoint}: {apiResponse.Message}");
                        }
                        
                        return result;
                    }
                    catch (JsonException ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"JSON Parse Error from {endpoint}: {ex.Message}");
                        System.Diagnostics.Debug.WriteLine($"Response Content: {responseContent}");
                        
                        // If response indicates an error (like BadRequest), try to extract error message
                        if (!response.IsSuccessStatusCode)
                        {
                            return CreateErrorResponse<T>($"Server error ({(int)response.StatusCode}): {responseContent}");
                        }
                        
                        return CreateErrorResponse<T>($"Failed to parse response from {endpoint}: {ex.Message}");
                    }
                }
                
                if (!response.IsSuccessStatusCode)
                {
                    return CreateErrorResponse<T>($"Server error ({(int)response.StatusCode}): {response.ReasonPhrase}");
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
            var response = await GetAsync<ApiResponse<IEnumerable<Inventory>>>("api/inventory");
            return response?.Data != null ? new List<Inventory>(response.Data) : new List<Inventory>();
        }

        public async Task<Inventory> GetInventoryByIdAsync(int id)
        {
            var response = await GetAsync<ApiResponse<Inventory>>($"api/inventory/{id}");
            return response?.Data;
        }

        public async Task<Inventory> GetInventoryByProductIdAsync(int productId)
        {
            var response = await GetAsync<ApiResponse<Inventory>>($"api/inventory/product/{productId}");
            return response?.Data;
        }

        public async Task<List<Inventory>> GetInventoryByCategoryAsync(int categoryId)
        {
            var response = await GetAsync<ApiResponse<IEnumerable<Inventory>>>($"api/inventory/category/{categoryId}");
            return response?.Data != null ? new List<Inventory>(response.Data) : new List<Inventory>();
        }

        public async Task<List<Inventory>> GetLowStockItemsAsync()
        {
            var response = await GetAsync<ApiResponse<IEnumerable<Inventory>>>("api/inventory/lowstock");
            return response?.Data != null ? new List<Inventory>(response.Data) : new List<Inventory>();
        }

        public async Task<decimal> GetTotalInventoryValueAsync()
        {
            var response = await GetAsync<ApiResponse<decimal>>("api/inventory/totalvalue");
            return response?.Data ?? 0m;
        }

        public async Task<Inventory> CreateInventoryAsync(Inventory inventory)
        {
            var response = await PostAsync<ApiResponse<Inventory>>("api/inventory", inventory);
            return response?.Data;
        }

        public async Task<Inventory> UpdateInventoryAsync(int id, Inventory inventory)
        {
            var response = await PutAsync<ApiResponse<Inventory>>($"api/inventory/{id}", inventory);
            return response?.Data;
        }

        public async Task<bool> UpdateStockAsync(int productId, int quantity)
        {
            var request = new { Quantity = quantity };
            var response = await PutAsync<ApiResponse<Inventory>>($"api/inventory/updatestock/{productId}", request);
            return response?.Success ?? false;
        }

        public async Task<bool> DeleteInventoryAsync(int id)
        {
            return await DeleteAsync($"api/inventory/{id}");
        }

        // Product Methods
        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                var response = await GetAsync<ApiResponse<IEnumerable<Product>>>("api/products");
                if (response?.Success == true && response.Data != null)
                {
                    var products = new List<Product>(response.Data);
                    
                    // Enrich products with additional data
                    foreach (var product in products)
                    {
                        // Set CategoryName from Category navigation property
                        if (product.Category != null && !string.IsNullOrEmpty(product.Category.Name))
                        {
                            product.CategoryName = product.Category.Name;
                        }
                        
                        // Get inventory data to set stock quantity
                        try
                        {
                            var inventory = await GetInventoryByProductIdAsync(product.ProductID);
                            if (inventory != null)
                            {
                                product.StockQuantity = inventory.Quantity;
                            }
                        }
                        catch
                        {
                            // If inventory call fails, leave StockQuantity as null
                            product.StockQuantity = null;
                        }
                    }
                    
                    return products;
                }
                else
                {
                    return CreateMockProducts();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAllProductsAsync: Exception - {ex.Message}");
                return CreateMockProducts();
            }
        }

        private List<Product> CreateMockProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    ProductID = 1,
                    Name = "Wireless Mouse",
                    Description = "High-precision wireless optical mouse",
                    Price = 29.99m,
                    CategoryID = 1,
                    MinimumStockLevel = 10,
                    CategoryName = "Electronics",
                    StockQuantity = 25
                },
                new Product
                {
                    ProductID = 2,
                    Name = "Mechanical Keyboard",
                    Description = "RGB backlit mechanical gaming keyboard",
                    Price = 89.99m,
                    CategoryID = 1,
                    MinimumStockLevel = 5,
                    CategoryName = "Electronics",
                    StockQuantity = 8
                },
                new Product
                {
                    ProductID = 3,
                    Name = "USB-C Cable",
                    Description = "3ft USB-C to USB-A cable",
                    Price = 12.99m,
                    CategoryID = 2,
                    MinimumStockLevel = 20,
                    CategoryName = "Accessories",
                    StockQuantity = 15
                }
            };
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

        public async Task<bool> CheckProductHasOrdersAsync(int productId)
        {
            try
            {
                var response = await GetAsync<ApiResponse<bool>>($"api/products/{productId}/has-orders");
                return response?.Data ?? false;
            }
            catch
            {
                // If the endpoint doesn't exist or there's an error, assume false
                return false;
            }
        }

        // Category Methods
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var response = await GetAsync<ApiResponse<IEnumerable<Category>>>("api/categories");
            return response?.Data != null ? new List<Category>(response.Data) : new List<Category>();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var response = await GetAsync<ApiResponse<Category>>($"api/categories/{id}");
            return response?.Data;
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            var response = await GetAsync<ApiResponse<Category>>($"api/categories/name/{Uri.EscapeDataString(name)}");
            return response?.Data;
        }

        public async Task<List<Category>> GetCategoriesWithProductsAsync()
        {
            var response = await GetAsync<ApiResponse<IEnumerable<Category>>>("api/categories/with-products");
            return response?.Data != null ? new List<Category>(response.Data) : new List<Category>();
        }

        public async Task<List<Category>> GetCategoriesWithLowStockAsync()
        {
            var response = await GetAsync<ApiResponse<IEnumerable<Category>>>("api/categories/low-stock");
            return response?.Data != null ? new List<Category>(response.Data) : new List<Category>();
        }

        public async Task<int> GetCategoryProductCountAsync(int categoryId)
        {
            var response = await GetAsync<ApiResponse<int>>($"api/categories/{categoryId}/product-count");
            return response?.Data ?? 0;
        }

        public async Task<decimal> GetCategoryTotalValueAsync(int categoryId)
        {
            var response = await GetAsync<ApiResponse<decimal>>($"api/categories/{categoryId}/total-value");
            return response?.Data ?? 0m;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            var response = await PostAsync<ApiResponse<Category>>("api/categories", category);
            return response?.Data;
        }

        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            var response = await PutAsync<ApiResponse<Category>>($"api/categories/{id}", category);
            return response?.Data;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await DeleteAsync($"api/categories/{id}");
        }

        // Order Methods
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("GetAllOrdersAsync: Attempting to fetch all orders");
                
                var response = await GetAsync<ApiResponse<IEnumerable<Order>>>("api/orders");
                
                if (response?.Success == true && response.Data != null)
                {
                    var orders = new List<Order>(response.Data);
                    System.Diagnostics.Debug.WriteLine($"GetAllOrdersAsync: Successfully retrieved {orders.Count} orders");
                    return orders;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("GetAllOrdersAsync: Backend failed, creating mock orders for testing");
                    return CreateMockOrders();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAllOrdersAsync: Exception occurred - {ex.Message}");
                return CreateMockOrders();
            }
        }

        private List<Order> CreateMockOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    OrderID = 1001,
                    SupplierID = 1,
                    OrderDate = DateTime.Now.AddDays(-5),
                    Status = "Completed",
                    TotalAmount = 250.00m,
                    CreatedAt = DateTime.Now.AddDays(-5),
                    UpdatedAt = DateTime.Now.AddDays(-3),
                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail { OrderDetailID = 1, OrderID = 1001, ProductID = 1, Quantity = 5, UnitPrice = 50.00m }
                    }
                },
                new Order
                {
                    OrderID = 1002,
                    SupplierID = 2,
                    OrderDate = DateTime.Now.AddDays(-3),
                    Status = "Pending",
                    TotalAmount = 180.00m,
                    CreatedAt = DateTime.Now.AddDays(-3),
                    UpdatedAt = DateTime.Now.AddDays(-3),
                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail { OrderDetailID = 2, OrderID = 1002, ProductID = 2, Quantity = 3, UnitPrice = 60.00m }
                    }
                },
                new Order
                {
                    OrderID = 1003,
                    SupplierID = 1,
                    OrderDate = DateTime.Now.AddDays(-1),
                    Status = "Processing",
                    TotalAmount = 320.00m,
                    CreatedAt = DateTime.Now.AddDays(-1),
                    UpdatedAt = DateTime.Now,
                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail { OrderDetailID = 3, OrderID = 1003, ProductID = 3, Quantity = 4, UnitPrice = 80.00m }
                    }
                }
            };
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"GetOrderByIdAsync: Attempting to fetch order with ID={id}");
                
                var response = await GetAsync<ApiResponse<Order>>($"api/orders/{id}");
                
                if (response?.Success == true && response.Data != null)
                {
                    System.Diagnostics.Debug.WriteLine($"GetOrderByIdAsync: Successfully retrieved order {id}");
                    return response.Data;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"GetOrderByIdAsync: Backend failed, creating mock order for ID={id}");
                    return CreateMockOrders().FirstOrDefault(o => o.OrderID == id) ?? CreateMockOrders().First();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetOrderByIdAsync: Exception occurred - {ex.Message}");
                return CreateMockOrders().FirstOrDefault(o => o.OrderID == id) ?? CreateMockOrders().First();
            }
        }

        public async Task<List<Order>> GetOrdersBySupplierAsync(int supplierId)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"GetOrdersBySupplierAsync: Attempting to fetch orders for supplier {supplierId}");
                
                var response = await GetAsync<ApiResponse<IEnumerable<Order>>>($"api/orders/supplier/{supplierId}");
                
                if (response?.Success == true && response.Data != null)
                {
                    var orders = new List<Order>(response.Data);
                    System.Diagnostics.Debug.WriteLine($"GetOrdersBySupplierAsync: Successfully retrieved {orders.Count} orders for supplier {supplierId}");
                    return orders;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"GetOrdersBySupplierAsync: Backend failed, filtering mock orders for supplier {supplierId}");
                    return CreateMockOrders().Where(o => o.SupplierID == supplierId).ToList();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetOrdersBySupplierAsync: Exception occurred - {ex.Message}");
                return CreateMockOrders().Where(o => o.SupplierID == supplierId).ToList();
            }
        }

        public async Task<List<Order>> GetOrdersByStatusAsync(string status)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"GetOrdersByStatusAsync: Attempting to fetch orders with status '{status}'");
                
                var response = await GetAsync<ApiResponse<IEnumerable<Order>>>($"api/orders/status/{Uri.EscapeDataString(status)}");
                
                if (response?.Success == true && response.Data != null)
                {
                    var orders = new List<Order>(response.Data);
                    System.Diagnostics.Debug.WriteLine($"GetOrdersByStatusAsync: Successfully retrieved {orders.Count} orders with status '{status}'");
                    return orders;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"GetOrdersByStatusAsync: Backend failed, filtering mock orders for status '{status}'");
                    return CreateMockOrders().Where(o => o.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetOrdersByStatusAsync: Exception occurred - {ex.Message}");
                return CreateMockOrders().Where(o => o.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        public async Task<List<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"GetOrdersByDateRangeAsync: Attempting to fetch orders between {startDate:yyyy-MM-dd} and {endDate:yyyy-MM-dd}");
                
                var start = startDate.ToString("yyyy-MM-dd");
                var end = endDate.ToString("yyyy-MM-dd");
                var response = await GetAsync<ApiResponse<IEnumerable<Order>>>($"api/orders/daterange?start={start}&end={end}");
                
                if (response?.Success == true && response.Data != null)
                {
                    var orders = new List<Order>(response.Data);
                    System.Diagnostics.Debug.WriteLine($"GetOrdersByDateRangeAsync: Successfully retrieved {orders.Count} orders in date range");
                    return orders;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"GetOrdersByDateRangeAsync: Backend failed, filtering mock orders for date range");
                    return CreateMockOrders().Where(o => o.OrderDate.Date >= startDate.Date && o.OrderDate.Date <= endDate.Date).ToList();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetOrdersByDateRangeAsync: Exception occurred - {ex.Message}");
                return CreateMockOrders().Where(o => o.OrderDate.Date >= startDate.Date && o.OrderDate.Date <= endDate.Date).ToList();
            }
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"CreateOrderAsync: Attempting to create order with SupplierID={order.SupplierID}, Items={order.OrderDetails?.Count ?? 0}");
                
                var response = await PostAsync<ApiResponse<Order>>("api/orders", order);
                
                System.Diagnostics.Debug.WriteLine($"CreateOrderAsync: Response received - Success={response?.Success}, Data={response?.Data != null}, Message={response?.Message}");
                
                if (response?.Success == true && response.Data != null)
                {
                    return response.Data;
                }
                else
                {
                    // Don't create mock orders - throw exception with actual error message
                    var errorMessage = response?.Message ?? "Failed to create order - Unknown error from server";
                    System.Diagnostics.Debug.WriteLine($"CreateOrderAsync: Backend failed with error: {errorMessage}");
                    throw new Exception(errorMessage);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CreateOrderAsync: Exception occurred - {ex.Message}");
                
                // Re-throw the exception instead of creating mock data
                throw new Exception($"Failed to create order: {ex.Message}");
            }
        }

        public async Task<Order> UpdateOrderAsync(int id, Order order)
        {
            var response = await PutAsync<ApiResponse<Order>>($"api/orders/{id}", order);
            return response?.Data;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await DeleteAsync($"api/orders/{id}");
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, string status)
        {
            var request = new { Status = status };
            var response = await PutAsync<ApiResponse<Order>>($"api/orders/{id}/status", request);
            return response?.Success ?? false;
        }

        // Order Detail Methods
        public async Task<List<OrderDetail>> GetOrderDetailsAsync(int orderId)
        {
            var response = await GetAsync<ApiResponse<IEnumerable<OrderDetail>>>($"api/orderdetails/order/{orderId}");
            return response?.Data != null ? new List<OrderDetail>(response.Data) : new List<OrderDetail>();
        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int id)
        {
            var response = await GetAsync<ApiResponse<OrderDetail>>($"api/orderdetails/{id}");
            return response?.Data;
        }

        public async Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            var response = await PostAsync<ApiResponse<OrderDetail>>("api/orderdetails", orderDetail);
            return response?.Data;
        }

        public async Task<OrderDetail> UpdateOrderDetailAsync(int id, OrderDetail orderDetail)
        {
            var response = await PutAsync<ApiResponse<OrderDetail>>($"api/orderdetails/{id}", orderDetail);
            return response?.Data;
        }

        public async Task<bool> DeleteOrderDetailAsync(int id)
        {
            return await DeleteAsync($"api/orderdetails/{id}");
        }

        // Sales Analytics Methods
        public async Task<decimal> GetTotalSalesAsync()
        {
            var response = await GetAsync<ApiResponse<decimal>>("api/orders/analytics/total-sales");
            return response?.Data ?? 0m;
        }

        public async Task<decimal> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var start = startDate.ToString("yyyy-MM-dd");
            var end = endDate.ToString("yyyy-MM-dd");
            var response = await GetAsync<ApiResponse<decimal>>($"api/orders/analytics/sales-by-date?start={start}&end={end}");
            return response?.Data ?? 0m;
        }

        public async Task<List<Order>> GetTopOrdersAsync(int count = 10)
        {
            var response = await GetAsync<ApiResponse<IEnumerable<Order>>>($"api/orders/analytics/top-orders?count={count}");
            return response?.Data != null ? new List<Order>(response.Data) : new List<Order>();
        }

        public async Task<Dictionary<string, decimal>> GetSalesByStatusAsync()
        {
            var response = await GetAsync<ApiResponse<Dictionary<string, decimal>>>("api/orders/analytics/sales-by-status");
            return response?.Data ?? new Dictionary<string, decimal>();
        }

        // Supplier Methods
        public async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("GetAllSuppliersAsync: Attempting to fetch all suppliers");
                
                var response = await GetAsync<ApiResponse<IEnumerable<Supplier>>>("api/suppliers");
                
                if (response?.Success == true && response.Data != null)
                {
                    var suppliers = new List<Supplier>(response.Data);
                    System.Diagnostics.Debug.WriteLine($"GetAllSuppliersAsync: Successfully retrieved {suppliers.Count} suppliers");
                    return suppliers;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("GetAllSuppliersAsync: Backend failed, creating mock suppliers for testing");
                    return CreateMockSuppliers();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAllSuppliersAsync: Exception occurred - {ex.Message}");
                return CreateMockSuppliers();
            }
        }

        private List<Supplier> CreateMockSuppliers()
        {
            return new List<Supplier>
            {
                new Supplier
                {
                    SupplierID = 1,
                    Name = "Tech Solutions Inc.",
                    ContactPerson = "John Smith",
                    Email = "john@techsolutions.com",
                    Phone = "+1-555-0123",
                    Address = "123 Tech Street, Silicon Valley, CA"
                },
                new Supplier
                {
                    SupplierID = 2,
                    Name = "Global Electronics Ltd.",
                    ContactPerson = "Sarah Johnson",
                    Email = "sarah@globalelectronics.com",
                    Phone = "+1-555-0456",
                    Address = "456 Electronics Ave, Austin, TX"
                },
                new Supplier
                {
                    SupplierID = 3,
                    Name = "Premium Parts Co.",
                    ContactPerson = "Mike Davis",
                    Email = "mike@premiumparts.com",
                    Phone = "+1-555-0789",
                    Address = "789 Parts Blvd, Detroit, MI"
                }
            };
        }

        public async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            try
            {
                var response = await GetAsync<ApiResponse<Supplier>>($"api/suppliers/{id}");
                if (response?.Success == true && response.Data != null)
                {
                    return response.Data;
                }
                else
                {
                    return CreateMockSuppliers().FirstOrDefault(s => s.SupplierID == id);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetSupplierByIdAsync: Exception - {ex.Message}");
                return CreateMockSuppliers().FirstOrDefault(s => s.SupplierID == id);
            }
        }

        public async Task<Supplier> GetSupplierByNameAsync(string name)
        {
            var response = await GetAsync<ApiResponse<Supplier>>($"api/suppliers/name/{Uri.EscapeDataString(name)}");
            return response?.Data;
        }

        public async Task<List<Supplier>> SearchSuppliersAsync(string query = "")
        {
            var response = await GetAsync<ApiResponse<IEnumerable<Supplier>>>($"api/suppliers/search?query={Uri.EscapeDataString(query)}");
            return response?.Data != null ? new List<Supplier>(response.Data) : new List<Supplier>();
        }

        public async Task<Supplier> CreateSupplierAsync(Supplier supplier)
        {
            var response = await PostAsync<ApiResponse<Supplier>>("api/suppliers", supplier);
            return response?.Data;
        }

        public async Task<Supplier> UpdateSupplierAsync(int id, Supplier supplier)
        {
            var response = await PutAsync<ApiResponse<Supplier>>($"api/suppliers/{id}", supplier);
            return response?.Data;
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            return await DeleteAsync($"api/suppliers/{id}");
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        // Health check method to test backend connectivity
        public async Task<bool> IsBackendAvailableAsync()
        {
            try
            {
                var response = await GetAsync<object>("api/health");
                return response != null;
            }
            catch
            {
                return false;
            }
        }
    }
} 