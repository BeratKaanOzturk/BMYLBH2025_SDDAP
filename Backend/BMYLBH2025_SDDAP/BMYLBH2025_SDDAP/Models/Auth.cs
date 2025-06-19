using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BMYLBH2025_SDDAP.Models
{
    public class Auth
    {
    }
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public static class TokenStore
    {
        public static Dictionary<string, int> Tokens { get; } = new Dictionary<string, int>();
    }
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            // Headers contains Authorization header with Bearer scheme
            var headers = actionContext.Request.Headers;
            if (headers.Authorization != null && headers.Authorization.Scheme == "Bearer")
            {
                string token = headers.Authorization.Parameter;
                // Check if the token exists in the TokenStore
                return TokenStore.Tokens.ContainsKey(token);
            }
            return false;
        }
    }
}