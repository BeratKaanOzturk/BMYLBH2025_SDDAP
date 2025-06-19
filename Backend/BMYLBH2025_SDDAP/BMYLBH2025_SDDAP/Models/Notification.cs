using System;

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
        public bool IsRead { get; set; }
        public NotificationType Type { get; set; }
        public NotificationPriority Priority { get; set; }
        
        // Navigation Properties
        public virtual User User { get; set; }

        public Notification()
        {
            Date = DateTime.Now;
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
}