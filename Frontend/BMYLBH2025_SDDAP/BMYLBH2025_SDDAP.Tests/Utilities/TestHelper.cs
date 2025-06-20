using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Reflection;
using BMYLBH2025_SDDAP.Models;

namespace BMYLBH2025_SDDAP.Tests.Utilities
{
    /// <summary>
    /// Utility class providing common testing methods and mock data generation
    /// </summary>
    public static class TestHelper
    {
        #region Mock Data Generation

        /// <summary>
        /// Generates a list of mock inventory items for testing
        /// </summary>
        public static List<Inventory> GenerateMockInventory(int count = 5)
        {
            var inventoryList = new List<Inventory>();
            var random = new Random();

            for (int i = 1; i <= count; i++)
            {
                inventoryList.Add(new Inventory
                {
                    InventoryID = i,
                    ProductID = i,
                    Quantity = random.Next(0, 100),
                    LastUpdated = DateTime.Now.AddDays(-random.Next(0, 30)),
                    Product = GenerateMockProduct(i)
                });
            }

            return inventoryList;
        }

        /// <summary>
        /// Generates a mock product for testing
        /// </summary>
        public static Product GenerateMockProduct(int id)
        {
            var products = new[]
            {
                new { Name = "Laptop Computer", Description = "High-performance laptop", Price = 999.99m, MinStock = 10, CategoryId = 1 },
                new { Name = "Wireless Mouse", Description = "Ergonomic wireless mouse", Price = 25.99m, MinStock = 20, CategoryId = 1 },
                new { Name = "Mechanical Keyboard", Description = "RGB mechanical keyboard", Price = 75.50m, MinStock = 15, CategoryId = 1 },
                new { Name = "Office Chair", Description = "Comfortable office chair", Price = 199.99m, MinStock = 5, CategoryId = 2 },
                new { Name = "Desk Lamp", Description = "LED desk lamp", Price = 45.00m, MinStock = 12, CategoryId = 2 }
            };

            var productData = products[(id - 1) % products.Length];

            return new Product
            {
                ProductID = id,
                Name = productData.Name,
                Description = productData.Description,
                Price = productData.Price,
                MinimumStockLevel = productData.MinStock,
                CategoryID = productData.CategoryId,
                Category = GenerateMockCategory(productData.CategoryId)
            };
        }

        /// <summary>
        /// Generates a mock category for testing
        /// </summary>
        public static Category GenerateMockCategory(int id)
        {
            var categories = new[]
            {
                new { Name = "Electronics", Description = "Electronic devices and accessories" },
                new { Name = "Furniture", Description = "Office and home furniture" },
                new { Name = "Supplies", Description = "Office supplies and materials" }
            };

            var categoryData = categories[(id - 1) % categories.Length];

            return new Category
            {
                CategoryID = id,
                Name = categoryData.Name,
                Description = categoryData.Description
            };
        }

        /// <summary>
        /// Generates a mock user for testing
        /// </summary>
        public static User GenerateMockUser(int id, string role = "User")
        {
            return new User
            {
                UserID = id,
                Username = $"testuser{id}",
                Email = $"testuser{id}@example.com",
                Password = "password123",
                Role = role,
                IsActive = true
            };
        }

        /// <summary>
        /// Generates mock login response data
        /// </summary>
        public static LoginResponseData GenerateMockLoginResponse(string email = "test@example.com", string role = "User")
        {
            return new LoginResponseData
            {
                Email = email,
                FullName = "Test User",
                Token = GenerateMockJwtToken(),
                Role = role,
                UserID = 1
            };
        }

        /// <summary>
        /// Generates a mock JWT token for testing
        /// </summary>
        public static string GenerateMockJwtToken()
        {
            return "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IlRlc3QgVXNlciIsImlhdCI6MTUxNjIzOTAyMn0.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        }

        #endregion

        #region API Response Helpers

        /// <summary>
        /// Creates a successful API response for testing
        /// </summary>
        public static ApiResponse<T> CreateSuccessResponse<T>(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Creates an error API response for testing
        /// </summary>
        public static ApiResponse<T> CreateErrorResponse<T>(string errorCode, string message, T data = default(T))
        {
            return new ApiResponse<T>
            {
                Success = false,
                ErrorCode = errorCode,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Creates a validation error response
        /// </summary>
        public static ApiResponse<T> CreateValidationErrorResponse<T>(string message = "Validation failed")
        {
            return CreateErrorResponse<T>("ValidationError", message);
        }

        /// <summary>
        /// Creates a network error response
        /// </summary>
        public static ApiResponse<T> CreateNetworkErrorResponse<T>(string message = "Network error occurred")
        {
            return CreateErrorResponse<T>("NetworkError", message);
        }

        #endregion

        #region Windows Forms Testing Helpers

        /// <summary>
        /// Invokes a private method on a target object using reflection
        /// </summary>
        public static T InvokePrivateMethod<T>(object target, string methodName, params object[] parameters)
        {
            var method = target.GetType().GetMethod(methodName,
                BindingFlags.NonPublic | BindingFlags.Instance);
            
            if (method == null)
                throw new ArgumentException($"Method '{methodName}' not found on type '{target.GetType().Name}'");

            return (T)method.Invoke(target, parameters);
        }

        /// <summary>
        /// Invokes a private method without return value
        /// </summary>
        public static void InvokePrivateMethod(object target, string methodName, params object[] parameters)
        {
            var method = target.GetType().GetMethod(methodName,
                BindingFlags.NonPublic | BindingFlags.Instance);
            
            if (method == null)
                throw new ArgumentException($"Method '{methodName}' not found on type '{target.GetType().Name}'");

            method.Invoke(target, parameters);
        }

        /// <summary>
        /// Invokes a private async method
        /// </summary>
        public static async Task<T> InvokePrivateMethodAsync<T>(object target, string methodName, params object[] parameters)
        {
            var method = target.GetType().GetMethod(methodName,
                BindingFlags.NonPublic | BindingFlags.Instance);
            
            if (method == null)
                throw new ArgumentException($"Method '{methodName}' not found on type '{target.GetType().Name}'");

            var task = (Task<T>)method.Invoke(target, parameters);
            return await task;
        }

        /// <summary>
        /// Simulates a button click event
        /// </summary>
        public static void SimulateButtonClick(Button button)
        {
            var clickMethod = typeof(Button).GetMethod("OnClick",
                BindingFlags.NonPublic | BindingFlags.Instance);
            clickMethod?.Invoke(button, new object[] { EventArgs.Empty });
        }

        /// <summary>
        /// Simulates a key press event on a control
        /// </summary>
        public static void SimulateKeyPress(Control control, Keys key)
        {
            var keyPressEventArgs = new KeyPressEventArgs((char)key);
            var keyPressMethod = control.GetType().GetMethod("OnKeyPress",
                BindingFlags.NonPublic | BindingFlags.Instance);
            keyPressMethod?.Invoke(control, new object[] { keyPressEventArgs });
        }

        /// <summary>
        /// Simulates text input in a TextBox
        /// </summary>
        public static void SimulateTextInput(TextBox textBox, string text)
        {
            textBox.Text = text;
            textBox.SelectionStart = text.Length;
        }

        /// <summary>
        /// Finds a control by name in a form's control hierarchy
        /// </summary>
        public static T FindControl<T>(Control parent, string controlName) where T : Control
        {
            foreach (Control control in parent.Controls)
            {
                if (control.Name == controlName && control is T)
                    return (T)control;

                var found = FindControl<T>(control, controlName);
                if (found != null)
                    return found;
            }
            return null;
        }

        /// <summary>
        /// Gets the private field value from an object
        /// </summary>
        public static T GetPrivateField<T>(object target, string fieldName)
        {
            var field = target.GetType().GetField(fieldName,
                BindingFlags.NonPublic | BindingFlags.Instance);
            
            if (field == null)
                throw new ArgumentException($"Field '{fieldName}' not found on type '{target.GetType().Name}'");

            return (T)field.GetValue(target);
        }

        /// <summary>
        /// Sets the private field value on an object
        /// </summary>
        public static void SetPrivateField(object target, string fieldName, object value)
        {
            var field = target.GetType().GetField(fieldName,
                BindingFlags.NonPublic | BindingFlags.Instance);
            
            if (field == null)
                throw new ArgumentException($"Field '{fieldName}' not found on type '{target.GetType().Name}'");

            field.SetValue(target, value);
        }

        #endregion

        #region Test Data Validation

        /// <summary>
        /// Validates that an email address is in correct format
        /// </summary>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validates that a password meets minimum requirements
        /// </summary>
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            return password.Length >= 6; // Minimum 6 characters
        }

        /// <summary>
        /// Validates that a JWT token has the correct format
        /// </summary>
        public static bool IsValidJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return false;

            var parts = token.Split('.');
            return parts.Length == 3; // JWT has 3 parts separated by dots
        }

        #endregion

        #region Performance Testing Helpers

        /// <summary>
        /// Measures the execution time of an action
        /// </summary>
        public static TimeSpan MeasureExecutionTime(Action action)
        {
            var startTime = DateTime.Now;
            action();
            return DateTime.Now - startTime;
        }

        /// <summary>
        /// Measures the execution time of an async action
        /// </summary>
        public static async Task<TimeSpan> MeasureExecutionTimeAsync(Func<Task> action)
        {
            var startTime = DateTime.Now;
            await action();
            return DateTime.Now - startTime;
        }

        /// <summary>
        /// Asserts that an action completes within the specified time limit
        /// </summary>
        public static void AssertExecutionTime(Action action, TimeSpan maxTime, string operationName = "Operation")
        {
            var executionTime = MeasureExecutionTime(action);
            if (executionTime > maxTime)
            {
                throw new AssertionException($"{operationName} took {executionTime.TotalSeconds:F2} seconds, which exceeds the limit of {maxTime.TotalSeconds:F2} seconds.");
            }
        }

        /// <summary>
        /// Asserts that an async action completes within the specified time limit
        /// </summary>
        public static async Task AssertExecutionTimeAsync(Func<Task> action, TimeSpan maxTime, string operationName = "Operation")
        {
            var executionTime = await MeasureExecutionTimeAsync(action);
            if (executionTime > maxTime)
            {
                throw new AssertionException($"{operationName} took {executionTime.TotalSeconds:F2} seconds, which exceeds the limit of {maxTime.TotalSeconds:F2} seconds.");
            }
        }

        #endregion

        #region Assertion Helpers

        /// <summary>
        /// Custom assertion exception for test failures
        /// </summary>
        public class AssertionException : Exception
        {
            public AssertionException(string message) : base(message) { }
        }

        /// <summary>
        /// Asserts that a control exists and is of the expected type
        /// </summary>
        public static void AssertControlExists<T>(Control parent, string controlName) where T : Control
        {
            var control = FindControl<T>(parent, controlName);
            if (control == null)
            {
                throw new AssertionException($"Control '{controlName}' of type '{typeof(T).Name}' not found");
            }
        }

        /// <summary>
        /// Asserts that a form has the expected title
        /// </summary>
        public static void AssertFormTitle(Form form, string expectedTitle)
        {
            if (form.Text != expectedTitle)
            {
                throw new AssertionException($"Expected form title '{expectedTitle}', but got '{form.Text}'");
            }
        }

        #endregion

        #region Cleanup Helpers

        /// <summary>
        /// Safely disposes of a form and its resources
        /// </summary>
        public static void SafeDisposeForm(Form form)
        {
            try
            {
                if (form != null && !form.IsDisposed)
                {
                    if (form.InvokeRequired)
                    {
                        form.Invoke(new Action(() => form.Dispose()));
                    }
                    else
                    {
                        form.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception but don't throw to avoid masking test failures
                System.Diagnostics.Debug.WriteLine($"Error disposing form: {ex.Message}");
            }
        }

        /// <summary>
        /// Processes pending Windows messages (useful for UI testing)
        /// </summary>
        public static void ProcessPendingMessages()
        {
            Application.DoEvents();
        }

        /// <summary>
        /// Waits for a condition to be true with timeout
        /// </summary>
        public static async Task<bool> WaitForCondition(Func<bool> condition, TimeSpan timeout)
        {
            var startTime = DateTime.Now;
            while (DateTime.Now - startTime < timeout)
            {
                if (condition())
                    return true;
                
                await Task.Delay(50); // Check every 50ms
            }
            return false;
        }

        #endregion
    }
} 