using System;

namespace BMYLBH2025_SDDAP.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }

        public Notification()
        {
            // Constructor
        }

        public void MarkAsRead()
        {
            // Mark as read
        }

        public bool Send()
        {
            // Send notification
            return false;
        }
    }
}