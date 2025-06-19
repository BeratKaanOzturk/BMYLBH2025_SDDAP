using Dapper;
using System.Collections.Generic;
using System.Linq;

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
    }

    public interface IUserRepository : IBaseRepository<User>
    {
        // Additional methods specific to User can be defined here
        User GetByUsername(string username);
        User GetByEmail(string email);
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
                    "INSERT INTO Users (Username, Password, Email, Role, IsActive) VALUES (@Username, @Password, @Email, @Role, @IsActive)",
                    entity);
            }
        }

        public void Update(User entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                con.Execute(
                    "UPDATE Users SET Username = @Username, Password = @Password, Email = @Email, Role = @Role, IsActive = @IsActive WHERE UserID = @UserID",
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
                return con.QueryFirstOrDefault<User>($"SELECT * FROM Users WHERE Username = @Username",
                    new { Username = username });
            }
        }

        public User GetByEmail(string email)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                return con.QueryFirstOrDefault<User>($"SELECT * FROM Users WHERE Email = @Email",
                    new { Email = email });
            }
        }
    }
}