using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace BMYLBH2025_SDDAP.Models
{
    public enum NotificationType
    {
        LowStock,
        NewOrder,
        OrderStatusUpdate,
        UserRegistration,
        SystemAlert
    }
    
    public enum NotificationPriority
    {
        Low,
        Medium,
        High,
        Critical
    }

    public class Notification
    {
        // Primary Key
        public int NotificationID { get; set; }
        
        // Foreign Key
        public int UserID { get; set; }
        
        // Basic Information
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public NotificationType Type { get; set; }
        public NotificationPriority Priority { get; set; }
        
        // Navigation Properties
        public virtual User User { get; set; }

        public Notification()
        {
            Date = DateTime.Now;
            CreatedAt = DateTime.Now;
            IsRead = false;
            Priority = NotificationPriority.Medium;
        }
        
        public Notification(int userId, string message, NotificationType type) : this()
        {
            UserID = userId;
            Message = message;
            Type = type;
        }

        // Business Methods
        public void MarkAsRead()
        {
            IsRead = true;
        }
        
        public void MarkAsUnread()
        {
            IsRead = false;
        }

        public bool Send()
        {
            try
            {
                // Implementation would depend on notification service
                // For now, just mark as sent
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public string GetFormattedDate()
        {
            return Date.ToString("yyyy-MM-dd HH:mm:ss");
        }
        
        public bool IsRecentNotification(int hoursThreshold = 24)
        {
            return DateTime.Now.Subtract(Date).TotalHours <= hoursThreshold;
        }
        
        public string GetPriorityBadge()
        {
            return Priority switch
            {
                NotificationPriority.Critical => "🔴",
                NotificationPriority.High => "🟠",
                NotificationPriority.Medium => "🟡",
                NotificationPriority.Low => "🟢",
                _ => "⚪"
            };
        }
    }
    
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        // Business-specific methods
        IEnumerable<Notification> GetByUserId(int userId);
        IEnumerable<Notification> GetUnreadNotifications(int userId);
        IEnumerable<Notification> GetByType(NotificationType type);
        IEnumerable<Notification> GetByPriority(NotificationPriority priority);
        bool MarkAsRead(int notificationId);
        bool MarkAllAsRead(int userId);
        int GetUnreadCount(int userId);
        void DeleteOldNotifications(int daysOld = 30);
    }
    
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        
        public NotificationRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        
        public IEnumerable<Notification> GetAll()
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Notifications ORDER BY CreatedAt DESC";
                return con.Query<Notification>(sql).ToList();
            }
        }
        
        public Notification GetById(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Notifications WHERE NotificationID = @Id";
                return con.QuerySingleOrDefault<Notification>(sql, new { Id = id });
            }
        }
        
        public void Add(Notification entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    INSERT INTO Notifications (UserID, Message, Type, Priority, IsRead, CreatedAt) 
                    VALUES (@UserID, @Message, @Type, @Priority, @IsRead, @CreatedAt);
                    SELECT CAST(last_insert_rowid() AS INTEGER);";
                    
                entity.CreatedAt = DateTime.Now;
                entity.IsRead = false;
                var insertedId = con.QuerySingle<int>(sql, entity);
                entity.NotificationID = insertedId;
            }
        }
        
        public void Update(Notification entity)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    UPDATE Notifications 
                    SET UserID = @UserID, 
                        Message = @Message, 
                        Type = @Type, 
                        Priority = @Priority, 
                        IsRead = @IsRead 
                    WHERE NotificationID = @NotificationID";
                    
                con.Execute(sql, entity);
            }
        }
        
        public void Delete(int id)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "DELETE FROM Notifications WHERE NotificationID = @Id";
                con.Execute(sql, new { Id = id });
            }
        }
        
        public IEnumerable<Notification> GetByUserId(int userId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Notifications WHERE UserID = @UserId ORDER BY CreatedAt DESC";
                return con.Query<Notification>(sql, new { UserId = userId }).ToList();
            }
        }
        
        public IEnumerable<Notification> GetUnreadNotifications(int userId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    SELECT * FROM Notifications 
                    WHERE UserID = @UserId AND IsRead = 0 
                    ORDER BY CreatedAt DESC";
                    
                return con.Query<Notification>(sql, new { UserId = userId }).ToList();
            }
        }
        
        public IEnumerable<Notification> GetByType(NotificationType type)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Notifications WHERE Type = @Type ORDER BY CreatedAt DESC";
                return con.Query<Notification>(sql, new { Type = type.ToString() }).ToList();
            }
        }
        
        public IEnumerable<Notification> GetByPriority(NotificationPriority priority)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT * FROM Notifications WHERE Priority = @Priority ORDER BY CreatedAt DESC";
                return con.Query<Notification>(sql, new { Priority = priority.ToString() }).ToList();
            }
        }
        
        public bool MarkAsRead(int notificationId)
        {
            try
            {
                using (var con = _connectionFactory.CreateConnection())
                {
                    const string sql = "UPDATE Notifications SET IsRead = 1 WHERE NotificationID = @NotificationId";
                    var result = con.Execute(sql, new { NotificationId = notificationId });
                    return result > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public bool MarkAllAsRead(int userId)
        {
            try
            {
                using (var con = _connectionFactory.CreateConnection())
                {
                    const string sql = "UPDATE Notifications SET IsRead = 1 WHERE UserID = @UserId";
                    var result = con.Execute(sql, new { UserId = userId });
                    return result > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public int GetUnreadCount(int userId)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = "SELECT COUNT(*) FROM Notifications WHERE UserID = @UserId AND IsRead = 0";
                return con.QuerySingle<int>(sql, new { UserId = userId });
            }
        }
        
        public void DeleteOldNotifications(int daysOld = 30)
        {
            using (var con = _connectionFactory.CreateConnection())
            {
                const string sql = @"
                    DELETE FROM Notifications 
                    WHERE CreatedAt < @CutoffDate";
                    
                var cutoffDate = DateTime.Now.AddDays(-daysOld);
                con.Execute(sql, new { CutoffDate = cutoffDate });
            }
        }
    }
}