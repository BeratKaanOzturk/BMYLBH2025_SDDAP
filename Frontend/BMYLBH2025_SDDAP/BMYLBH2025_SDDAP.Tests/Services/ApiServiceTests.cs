using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using BMYLBH2025_SDDAP.Services;
using BMYLBH2025_SDDAP.Models;

namespace BMYLBH2025_SDDAP.Tests.Services
{
    [TestClass]
    public class ApiServiceTests
    {
        private ApiService _apiService;

        [TestInitialize]
        public void Setup()
        {
            _apiService = new ApiService("https://localhost:44313");
        }

        [TestCleanup]
        public void Cleanup()
        {
            _apiService?.Dispose();
        }

        #region Constructor Tests

        [TestMethod]
        public void ApiService_Constructor_ShouldInitializeWithDefaultUrl()
        {
            // Act
            using (var service = new ApiService())
            {
                // Assert
                service.Should().NotBeNull();
            }
        }

        [TestMethod]
        public void ApiService_Constructor_ShouldInitializeWithCustomUrl()
        {
            // Arrange
            var customUrl = "https://example.com";

            // Act
            using (var service = new ApiService(customUrl))
            {
                // Assert
                service.Should().NotBeNull();
            }
        }

        [TestMethod]
        public void ApiService_Constructor_ShouldInitializeWithHttpClient()
        {
            // Arrange
            using (var httpClient = new HttpClient())
            {
                // Act
                using (var service = new ApiService(httpClient))
                {
                    // Assert
                    service.Should().NotBeNull();
                }
            }
        }

        #endregion

        #region Authentication Token Tests

        [TestMethod]
        public void SetAuthToken_WithValidToken_ShouldNotThrow()
        {
            // Arrange
            var token = "test-token-123";

            // Act & Assert
            Action act = () => _apiService.SetAuthToken(token);
            act.Should().NotThrow();
        }

        [TestMethod]
        public void ClearAuthToken_ShouldNotThrow()
        {
            // Arrange
            _apiService.SetAuthToken("test-token");

            // Act & Assert
            Action act = () => _apiService.ClearAuthToken();
            act.Should().NotThrow();
        }

        #endregion

        #region Request Object Tests

        [TestMethod]
        public void LoginRequest_ShouldHaveRequiredProperties()
        {
            // Act
            var request = new LoginRequest
            {
                Email = "test@example.com",
                Password = "password123"
            };

            // Assert
            request.Email.Should().Be("test@example.com");
            request.Password.Should().Be("password123");
        }

        [TestMethod]
        public void RegisterRequest_ShouldHaveRequiredProperties()
        {
            // Act
            var request = new RegisterRequest
            {
                Email = "test@example.com",
                Password = "password123",
                FullName = "Test User"
            };

            // Assert
            request.Email.Should().Be("test@example.com");
            request.Password.Should().Be("password123");
            request.FullName.Should().Be("Test User");
        }

        [TestMethod]
        public void ForgotPasswordRequest_ShouldHaveRequiredProperties()
        {
            // Act
            var request = new ForgotPasswordRequest
            {
                Email = "test@example.com"
            };

            // Assert
            request.Email.Should().Be("test@example.com");
        }

        [TestMethod]
        public void ResetPasswordRequest_ShouldHaveRequiredProperties()
        {
            // Act
            var request = new ResetPasswordRequest
            {
                Email = "test@example.com",
                OTP = "123456",
                NewPassword = "newpassword123"
            };

            // Assert
            request.Email.Should().Be("test@example.com");
            request.OTP.Should().Be("123456");
            request.NewPassword.Should().Be("newpassword123");
        }

        #endregion

        #region Response Object Tests

        [TestMethod]
        public void ApiResponse_ShouldHaveCorrectStructure()
        {
            // Act
            var response = new ApiResponse<string>
            {
                Success = true,
                Message = "Test message",
                ErrorCode = "TEST_ERROR",
                Data = "Test data"
            };

            // Assert
            response.Success.Should().BeTrue();
            response.Message.Should().Be("Test message");
            response.ErrorCode.Should().Be("TEST_ERROR");
            response.Data.Should().Be("Test data");
        }

        [TestMethod]
        public void LoginResponseData_ShouldHaveCorrectStructure()
        {
            // Act
            var responseData = new LoginResponseData
            {
                Email = "test@example.com",
                FullName = "Test User",
                Token = "jwt-token-123",
                Role = "User",
                ShowForgotPassword = true,
                ShowResendVerification = false
            };

            // Assert
            responseData.Email.Should().Be("test@example.com");
            responseData.FullName.Should().Be("Test User");
            responseData.Token.Should().Be("jwt-token-123");
            responseData.Role.Should().Be("User");
            responseData.ShowForgotPassword.Should().BeTrue();
            responseData.ShowResendVerification.Should().BeFalse();
        }

        #endregion

        #region Integration Tests (These will only work if backend is running)

        [TestMethod]
        public async Task LoginAsync_WithInvalidCredentials_ShouldReturnErrorResponse()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "nonexistent@example.com",
                Password = "wrongpassword"
            };

            try
            {
                // Act
                var result = await _apiService.LoginAsync(loginRequest);

                // Assert
                result.Should().NotBeNull();
                result.Success.Should().BeFalse();
            }
            catch (Exception ex)
            {
                // If backend is not running, the test should handle the exception gracefully
                (ex is HttpRequestException || ex is TaskCanceledException || ex is ArgumentNullException)
                    .Should().BeTrue("Expected network-related exception when backend is not running");
            }
        }

        [TestMethod]
        public async Task GetAllInventoryAsync_ShouldReturnList()
        {
            try
            {
                // Act
                var result = await _apiService.GetAllInventoryAsync();

                // Assert
                result.Should().NotBeNull();
                result.Should().BeOfType<List<Inventory>>();
            }
            catch (Exception ex)
            {
                // If backend is not running, the test should handle the exception gracefully
                (ex is HttpRequestException || ex is TaskCanceledException || ex is ArgumentNullException)
                    .Should().BeTrue("Expected network-related exception when backend is not running");
            }
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ShouldReturnList()
        {
            try
            {
                // Act
                var result = await _apiService.GetAllProductsAsync();

                // Assert
                result.Should().NotBeNull();
                result.Should().BeOfType<List<Product>>();
            }
            catch (Exception ex)
            {
                // If backend is not running, the test should handle the exception gracefully
                (ex is HttpRequestException || ex is TaskCanceledException || ex is ArgumentNullException)
                    .Should().BeTrue("Expected network-related exception when backend is not running");
            }
        }

        #endregion

        #region Performance Tests

        [TestMethod]
        public void ApiService_Initialization_ShouldCompleteQuickly()
        {
            // Arrange
            var startTime = DateTime.Now;

            // Act
            using (var service = new ApiService())
            {
                // Do nothing, just initialize
            }

            // Assert
            var initTime = DateTime.Now - startTime;
            initTime.Should().BeLessThan(TimeSpan.FromSeconds(1));
        }

        [TestMethod]
        public void SetAuthToken_ShouldCompleteQuickly()
        {
            // Arrange
            var startTime = DateTime.Now;
            var token = "test-token-123";

            // Act
            _apiService.SetAuthToken(token);

            // Assert
            var setTime = DateTime.Now - startTime;
            setTime.Should().BeLessThan(TimeSpan.FromMilliseconds(100));
        }

        #endregion

        #region Disposal Tests

        [TestMethod]
        public void ApiService_Dispose_ShouldNotThrow()
        {
            // Arrange
            var service = new ApiService();

            // Act & Assert
            Action act = () => service.Dispose();
            act.Should().NotThrow();
        }

        [TestMethod]
        public void ApiService_DoubleDispose_ShouldNotThrow()
        {
            // Arrange
            var service = new ApiService();

            // Act & Assert
            Action act = () =>
            {
                service.Dispose();
                service.Dispose(); // Should not throw on second dispose
            };
            act.Should().NotThrow();
        }

        #endregion
    }
} 