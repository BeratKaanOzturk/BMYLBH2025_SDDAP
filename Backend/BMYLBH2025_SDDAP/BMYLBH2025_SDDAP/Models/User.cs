using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMYLBH2025_SDDAP.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmailVerified { get; set; }
        public string EmailVerificationToken { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetExpiry { get; set; }
        public string PasswordResetOTP { get; set; }
        public DateTime? OTPExpiry { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public interface IUserRepository : IBaseRepository<User>
    {
        // Additional methods specific to User can be defined here
        User GetByUsername(string username);
        User GetByEmail(string email);
        User GetByVerificationToken(string token);
        User GetByPasswordResetOTP(string otp);
        void Create(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<User> GetAll()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                var result = con.Query("SELECT * FROM Users");
                var users = new List<User>();
                
                foreach (dynamic row in result)
                {
                    try
                    {
                        var user = new User
                        {
                            UserID = row.UserID == null ? 0 : Convert.ToInt32(row.UserID),
                            Username = Convert.ToString(row.Username) ?? string.Empty,
                            Password = Convert.ToString(row.Password) ?? string.Empty,
                            PasswordHash = Convert.ToString(row.PasswordHash) ?? string.Empty,
                            Email = Convert.ToString(row.Email) ?? string.Empty,
                            FullName = Convert.ToString(row.FullName) ?? string.Empty,
                            Role = Convert.ToString(row.Role) ?? string.Empty,
                            IsActive = row.IsActive == null ? false : Convert.ToBoolean(row.IsActive),
                            IsEmailVerified = row.IsEmailVerified == null ? false : Convert.ToBoolean(row.IsEmailVerified),
                            EmailVerificationToken = Convert.ToString(row.EmailVerificationToken) ?? string.Empty,
                            PasswordResetToken = Convert.ToString(row.PasswordResetToken) ?? string.Empty,
                            PasswordResetExpiry = row.PasswordResetExpiry == null ? (DateTime?)null : Convert.ToDateTime(row.PasswordResetExpiry),
                            PasswordResetOTP = Convert.ToString(row.PasswordResetOTP) ?? string.Empty,
                            OTPExpiry = row.OTPExpiry == null ? (DateTime?)null : Convert.ToDateTime(row.OTPExpiry),
                            CreatedAt = row.CreatedAt == null ? DateTime.Now : Convert.ToDateTime(row.CreatedAt),
                            UpdatedAt = row.UpdatedAt == null ? (DateTime?)null : Convert.ToDateTime(row.UpdatedAt)
                        };
                        
                        users.Add(user);
                    }
                    catch (Exception ex)
                    {
                        // Log the error or handle as needed
                        // For now, we'll skip this row if conversion fails
                        continue;
                    }
                }
                
                return users;
            }
        }

        public User GetById(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                var row = con.QueryFirstOrDefault($"SELECT * FROM Users WHERE UserID=@Id", new { Id = id });
                if (row == null) return null;
                
                try
                {
                    var user = new User
                    {
                        UserID = row.UserID == null ? 0 : Convert.ToInt32(row.UserID),
                        Username = Convert.ToString(row.Username) ?? string.Empty,
                        Password = Convert.ToString(row.Password) ?? string.Empty,
                        PasswordHash = Convert.ToString(row.PasswordHash) ?? string.Empty,
                        Email = Convert.ToString(row.Email) ?? string.Empty,
                        FullName = Convert.ToString(row.FullName) ?? string.Empty,
                        Role = Convert.ToString(row.Role) ?? string.Empty,
                        IsActive = row.IsActive == null ? false : Convert.ToBoolean(row.IsActive),
                        IsEmailVerified = row.IsEmailVerified == null ? false : Convert.ToBoolean(row.IsEmailVerified),
                        EmailVerificationToken = Convert.ToString(row.EmailVerificationToken) ?? string.Empty,
                        PasswordResetToken = Convert.ToString(row.PasswordResetToken) ?? string.Empty,
                        PasswordResetExpiry = row.PasswordResetExpiry == null ? (DateTime?)null : Convert.ToDateTime(row.PasswordResetExpiry),
                        PasswordResetOTP = Convert.ToString(row.PasswordResetOTP) ?? string.Empty,
                        OTPExpiry = row.OTPExpiry == null ? (DateTime?)null : Convert.ToDateTime(row.OTPExpiry),
                        CreatedAt = row.CreatedAt == null ? DateTime.Now : Convert.ToDateTime(row.CreatedAt),
                        UpdatedAt = row.UpdatedAt == null ? (DateTime?)null : Convert.ToDateTime(row.UpdatedAt)
                    };
                    
                    return user;
                }
                catch (Exception ex)
                {
                    // Log the error or handle as needed
                    // Return null if conversion fails
                    return null;
                }
            }
        }

        public void Add(User entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                con.Execute(
                    @"INSERT INTO Users (Username, Password, PasswordHash, Email, FullName, Role, IsActive, 
                      IsEmailVerified, EmailVerificationToken, PasswordResetToken, PasswordResetExpiry, 
                      PasswordResetOTP, OTPExpiry, CreatedAt, UpdatedAt) 
                      VALUES (@Username, @Password, @PasswordHash, @Email, @FullName, @Role, @IsActive, 
                      @IsEmailVerified, @EmailVerificationToken, @PasswordResetToken, @PasswordResetExpiry, 
                      @PasswordResetOTP, @OTPExpiry, @CreatedAt, @UpdatedAt)",
                    entity);
            }
        }

        public void Update(User entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                entity.UpdatedAt = DateTime.UtcNow;
                con.Execute(
                    @"UPDATE Users SET Username = @Username, Password = @Password, PasswordHash = @PasswordHash, 
                      Email = @Email, FullName = @FullName, Role = @Role, IsActive = @IsActive, 
                      IsEmailVerified = @IsEmailVerified, EmailVerificationToken = @EmailVerificationToken, 
                      PasswordResetToken = @PasswordResetToken, PasswordResetExpiry = @PasswordResetExpiry, 
                      PasswordResetOTP = @PasswordResetOTP, OTPExpiry = @OTPExpiry, UpdatedAt = @UpdatedAt 
                      WHERE UserID = @UserID",
                    entity);
            }
        }

        public void Delete(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                con.Execute("DELETE FROM Users WHERE UserID = @Id", new { Id = id });
            }
        }

        public User GetByUsername(string username)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                var row = con.QueryFirstOrDefault("SELECT * FROM Users WHERE Username = @Username",
                    new { Username = username });
                if (row == null) return null;
                
                try
                {
                    var user = new User
                    {
                        UserID = row.UserID == null ? 0 : Convert.ToInt32(row.UserID),
                        Username = Convert.ToString(row.Username) ?? string.Empty,
                        Password = Convert.ToString(row.Password) ?? string.Empty,
                        PasswordHash = Convert.ToString(row.PasswordHash) ?? string.Empty,
                        Email = Convert.ToString(row.Email) ?? string.Empty,
                        FullName = Convert.ToString(row.FullName) ?? string.Empty,
                        Role = Convert.ToString(row.Role) ?? string.Empty,
                        IsActive = row.IsActive == null ? false : Convert.ToBoolean(row.IsActive),
                        IsEmailVerified = row.IsEmailVerified == null ? false : Convert.ToBoolean(row.IsEmailVerified),
                        EmailVerificationToken = Convert.ToString(row.EmailVerificationToken) ?? string.Empty,
                        PasswordResetToken = Convert.ToString(row.PasswordResetToken) ?? string.Empty,
                        PasswordResetExpiry = row.PasswordResetExpiry == null ? (DateTime?)null : Convert.ToDateTime(row.PasswordResetExpiry),
                        PasswordResetOTP = Convert.ToString(row.PasswordResetOTP) ?? string.Empty,
                        OTPExpiry = row.OTPExpiry == null ? (DateTime?)null : Convert.ToDateTime(row.OTPExpiry),
                        CreatedAt = row.CreatedAt == null ? DateTime.Now : Convert.ToDateTime(row.CreatedAt),
                        UpdatedAt = row.UpdatedAt == null ? (DateTime?)null : Convert.ToDateTime(row.UpdatedAt)
                    };
                    
                    return user;
                }
                catch (Exception ex)
                {
                    // Log the error or handle as needed
                    // Return null if conversion fails
                    return null;
                }
            }
        }

        public User GetByEmail(string email)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                var row = con.QueryFirstOrDefault("SELECT * FROM Users WHERE Email = @Email",
                    new { Email = email });
                if (row == null) return null;
                
                try
                {
                    var user = new User
                    {
                        UserID = row.UserID == null ? 0 : Convert.ToInt32(row.UserID),
                        Username = Convert.ToString(row.Username) ?? string.Empty,
                        Password = Convert.ToString(row.Password) ?? string.Empty,
                        PasswordHash = Convert.ToString(row.PasswordHash) ?? string.Empty,
                        Email = Convert.ToString(row.Email) ?? string.Empty,
                        FullName = Convert.ToString(row.FullName) ?? string.Empty,
                        Role = Convert.ToString(row.Role) ?? string.Empty,
                        IsActive = row.IsActive == null ? false : Convert.ToBoolean(row.IsActive),
                        IsEmailVerified = row.IsEmailVerified == null ? false : Convert.ToBoolean(row.IsEmailVerified),
                        EmailVerificationToken = Convert.ToString(row.EmailVerificationToken) ?? string.Empty,
                        PasswordResetToken = Convert.ToString(row.PasswordResetToken) ?? string.Empty,
                        PasswordResetExpiry = row.PasswordResetExpiry == null ? (DateTime?)null : Convert.ToDateTime(row.PasswordResetExpiry),
                        PasswordResetOTP = Convert.ToString(row.PasswordResetOTP) ?? string.Empty,
                        OTPExpiry = row.OTPExpiry == null ? (DateTime?)null : Convert.ToDateTime(row.OTPExpiry),
                        CreatedAt = row.CreatedAt == null ? DateTime.Now : Convert.ToDateTime(row.CreatedAt),
                        UpdatedAt = row.UpdatedAt == null ? (DateTime?)null : Convert.ToDateTime(row.UpdatedAt)
                    };
                    
                    return user;
                }
                catch (Exception ex)
                {
                    // Log the error or handle as needed
                    // Return null if conversion fails
                    return null;
                }
            }
        }

        public User GetByVerificationToken(string token)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                var row = con.QueryFirstOrDefault("SELECT * FROM Users WHERE EmailVerificationToken = @Token",
                    new { Token = token });
                if (row == null) return null;
                
                try
                {
                    var user = new User
                    {
                        UserID = row.UserID == null ? 0 : Convert.ToInt32(row.UserID),
                        Username = Convert.ToString(row.Username) ?? string.Empty,
                        Password = Convert.ToString(row.Password) ?? string.Empty,
                        PasswordHash = Convert.ToString(row.PasswordHash) ?? string.Empty,
                        Email = Convert.ToString(row.Email) ?? string.Empty,
                        FullName = Convert.ToString(row.FullName) ?? string.Empty,
                        Role = Convert.ToString(row.Role) ?? string.Empty,
                        IsActive = row.IsActive == null ? false : Convert.ToBoolean(row.IsActive),
                        IsEmailVerified = row.IsEmailVerified == null ? false : Convert.ToBoolean(row.IsEmailVerified),
                        EmailVerificationToken = Convert.ToString(row.EmailVerificationToken) ?? string.Empty,
                        PasswordResetToken = Convert.ToString(row.PasswordResetToken) ?? string.Empty,
                        PasswordResetExpiry = row.PasswordResetExpiry == null ? (DateTime?)null : Convert.ToDateTime(row.PasswordResetExpiry),
                        PasswordResetOTP = Convert.ToString(row.PasswordResetOTP) ?? string.Empty,
                        OTPExpiry = row.OTPExpiry == null ? (DateTime?)null : Convert.ToDateTime(row.OTPExpiry),
                        CreatedAt = row.CreatedAt == null ? DateTime.Now : Convert.ToDateTime(row.CreatedAt),
                        UpdatedAt = row.UpdatedAt == null ? (DateTime?)null : Convert.ToDateTime(row.UpdatedAt)
                    };
                    
                    return user;
                }
                catch (Exception ex)
                {
                    // Log the error or handle as needed
                    // Return null if conversion fails
                    return null;
                }
            }
        }

        public User GetByPasswordResetOTP(string otp)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                var row = con.QueryFirstOrDefault(
                    "SELECT * FROM Users WHERE PasswordResetOTP = @OTP AND OTPExpiry > @Now",
                    new { OTP = otp, Now = DateTime.UtcNow });
                if (row == null) return null;
                
                try
                {
                    var user = new User
                    {
                        UserID = row.UserID == null ? 0 : Convert.ToInt32(row.UserID),
                        Username = Convert.ToString(row.Username) ?? string.Empty,
                        Password = Convert.ToString(row.Password) ?? string.Empty,
                        PasswordHash = Convert.ToString(row.PasswordHash) ?? string.Empty,
                        Email = Convert.ToString(row.Email) ?? string.Empty,
                        FullName = Convert.ToString(row.FullName) ?? string.Empty,
                        Role = Convert.ToString(row.Role) ?? string.Empty,
                        IsActive = row.IsActive == null ? false : Convert.ToBoolean(row.IsActive),
                        IsEmailVerified = row.IsEmailVerified == null ? false : Convert.ToBoolean(row.IsEmailVerified),
                        EmailVerificationToken = Convert.ToString(row.EmailVerificationToken) ?? string.Empty,
                        PasswordResetToken = Convert.ToString(row.PasswordResetToken) ?? string.Empty,
                        PasswordResetExpiry = row.PasswordResetExpiry == null ? (DateTime?)null : Convert.ToDateTime(row.PasswordResetExpiry),
                        PasswordResetOTP = Convert.ToString(row.PasswordResetOTP) ?? string.Empty,
                        OTPExpiry = row.OTPExpiry == null ? (DateTime?)null : Convert.ToDateTime(row.OTPExpiry),
                        CreatedAt = row.CreatedAt == null ? DateTime.Now : Convert.ToDateTime(row.CreatedAt),
                        UpdatedAt = row.UpdatedAt == null ? (DateTime?)null : Convert.ToDateTime(row.UpdatedAt)
                    };
                    
                    return user;
                }
                catch (Exception ex)
                {
                    // Log the error or handle as needed
                    // Return null if conversion fails
                    return null;
                }
            }
        }

        public void Create(User user)
        {
            // Set default values
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            user.IsActive = true;
            
            Add(user);
        }
    }
}