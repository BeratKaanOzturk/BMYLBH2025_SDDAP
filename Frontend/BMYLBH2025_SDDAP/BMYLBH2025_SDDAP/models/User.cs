namespace BMYLBH2025_SDDAP.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }

        public User()
        {
            // Constructor
        }

        public bool Login(string username, string password)
        {
            // Login process
            return false;
        }

        public void Logout()
        {
            // Logout process
        }

        public bool ResetPassword(string oldPassword, string newPassword)
        {
            // Reset password
            return false;
        }

        public void UpdateProfile(string email, string username)
        {
            // Update profile
        }
    }
}