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
                return con.Query<User>("SELECT * FROM Users").ToList();
            }
        }

        public User GetById(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                return con.QueryFirstOrDefault<User>($"SELECT * FROM Users WHERE UserID={id}");
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
                con.Execute($"DELETE FROM Users WHERE UserID = {id}");
            }
        }

        public User GetByUsername(string username)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                return con.QueryFirstOrDefault<User>("SELECT * FROM Users WHERE Username = @Username",
                    new { Username = username });
            }
        }

        public User GetByEmail(string email)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                return con.QueryFirstOrDefault<User>("SELECT * FROM Users WHERE Email = @Email",
                    new { Email = email });
            }
        }

        public User GetByVerificationToken(string token)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                return con.QueryFirstOrDefault<User>("SELECT * FROM Users WHERE EmailVerificationToken = @Token",
                    new { Token = token });
            }
        }

        public User GetByPasswordResetOTP(string otp)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                return con.QueryFirstOrDefault<User>(
                    "SELECT * FROM Users WHERE PasswordResetOTP = @OTP AND OTPExpiry > @Now",
                    new { OTP = otp, Now = DateTime.UtcNow });
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