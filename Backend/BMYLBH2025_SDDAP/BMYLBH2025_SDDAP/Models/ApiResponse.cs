using System;

namespace BMYLBH2025_SDDAP.Models
{
    /// <summary>
    /// Generic API response wrapper for consistent response structure
    /// </summary>
    /// <typeparam name="T">Type of data payload</typeparam>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string ErrorType { get; set; }
        public DateTime Timestamp { get; set; }

        public ApiResponse()
        {
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// Create a successful response with data
        /// </summary>
        public static ApiResponse<T> CreateSuccess(T data, string message = "Operation completed successfully")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Create a successful response without data
        /// </summary>
        public static ApiResponse<T> CreateSuccess(string message = "Operation completed successfully")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = default(T)
            };
        }

        /// <summary>
        /// Create an error response
        /// </summary>
        public static ApiResponse<T> CreateError(string message, string errorType = null, T data = default(T))
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                ErrorType = errorType,
                Data = data
            };
        }
    }

    /// <summary>
    /// Non-generic API response for operations that don't return data
    /// </summary>
    public class ApiResponse : ApiResponse<object>
    {
        /// <summary>
        /// Create a successful response without data
        /// </summary>
        public static ApiResponse CreateSuccess(string message = "Operation completed successfully")
        {
            return new ApiResponse
            {
                Success = true,
                Message = message,
                Data = null
            };
        }

        /// <summary>
        /// Create an error response
        /// </summary>
        public static ApiResponse CreateError(string message, string errorType = null)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message,
                ErrorType = errorType,
                Data = null
            };
        }
    }
} 