using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BMYLBH2025_SDDAP.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml = true);
        Task<bool> SendVerificationEmailAsync(string to, string verificationToken);
        Task<bool> SendLowStockAlertAsync(string to, List<string> lowStockItems);
        Task<bool> SendWelcomeEmailAsync(string to, string userName);
    }

    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;
        private readonly string _fromName;
        private readonly bool _enableSsl;

        public EmailService()
        {
            // Configure these in Web.config or use default values for development
            _smtpServer = ConfigurationManager.AppSettings["SmtpServer"] ?? "smtp.gmail.com";
            _smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"] ?? "587");
            _smtpUsername = ConfigurationManager.AppSettings["SmtpUsername"] ?? "your-email@gmail.com";
            _smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"] ?? "your-app-password";
            _fromEmail = ConfigurationManager.AppSettings["FromEmail"] ?? "noreply@inventory.com";
            _fromName = ConfigurationManager.AppSettings["FromName"] ?? "Inventory Management System";
            _enableSsl = bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"] ?? "true");
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml = true)
        {
            try
            {
                using (var client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                    client.EnableSsl = _enableSsl;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_fromEmail, _fromName),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = isHtml
                    };

                    mailMessage.To.Add(to);

                    await client.SendMailAsync(mailMessage);
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (in a real application, use proper logging)
                System.Diagnostics.Debug.WriteLine($"Email sending failed: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendVerificationEmailAsync(string to, string verificationToken)
        {
            var subject = "Verify Your Email Address - Inventory Management System";
            var body = GenerateVerificationEmailHtml(verificationToken);
            
            return await SendEmailAsync(to, subject, body, true);
        }

        public async Task<bool> SendLowStockAlertAsync(string to, List<string> lowStockItems)
        {
            var subject = "⚠️ Low Stock Alert - Inventory Management System";
            var body = GenerateLowStockAlertHtml(lowStockItems);
            
            return await SendEmailAsync(to, subject, body, true);
        }

        public async Task<bool> SendWelcomeEmailAsync(string to, string userName)
        {
            var subject = "Welcome to Inventory Management System! 🎉";
            var body = GenerateWelcomeEmailHtml(userName);
            
            return await SendEmailAsync(to, subject, body, true);
        }

        #region Email Templates

        private string GenerateVerificationEmailHtml(string verificationToken)
        {
            var verificationUrl = $"https://localhost:44313/api/auth/verify-email?token={verificationToken}";
            
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <title>Email Verification</title>
                    <style>
                        body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 0; padding: 20px; background-color: #f5f5f5; }}
                        .container {{ max-width: 600px; margin: 0 auto; background-color: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
                        .header {{ text-align: center; margin-bottom: 30px; }}
                        .logo {{ font-size: 24px; font-weight: bold; color: #3498db; }}
                        .content {{ line-height: 1.6; color: #333; }}
                        .button {{ display: inline-block; padding: 12px 30px; background-color: #3498db; color: white; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
                        .footer {{ margin-top: 30px; padding-top: 20px; border-top: 1px solid #eee; font-size: 12px; color: #666; text-align: center; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <div class='logo'>📦 Inventory Management System</div>
                        </div>
                        <div class='content'>
                            <h2>Email Verification Required</h2>
                            <p>Thank you for registering with our Inventory Management System!</p>
                            <p>To complete your registration and activate your account, please click the verification button below:</p>
                            <p style='text-align: center;'>
                                <a href='{verificationUrl}' class='button'>✅ Verify Email Address</a>
                            </p>
                            <p><strong>Note:</strong> This verification link will expire in 24 hours for security reasons.</p>
                            <p>If you didn't create an account with us, please ignore this email.</p>
                        </div>
                        <div class='footer'>
                            <p>© 2025 Inventory Management System. All rights reserved.</p>
                            <p>This is an automated email. Please do not reply to this message.</p>
                        </div>
                    </div>
                </body>
                </html>";
        }

        private string GenerateLowStockAlertHtml(List<string> lowStockItems)
        {
            var itemsList = string.Join("", lowStockItems.ConvertAll(item => $"<li style='margin: 5px 0; padding: 5px; background-color: #fff3cd; border-left: 4px solid #ffc107;'>{item}</li>"));
            
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <title>Low Stock Alert</title>
                    <style>
                        body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 0; padding: 20px; background-color: #f5f5f5; }}
                        .container {{ max-width: 600px; margin: 0 auto; background-color: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
                        .header {{ text-align: center; margin-bottom: 30px; }}
                        .logo {{ font-size: 24px; font-weight: bold; color: #e74c3c; }}
                        .alert {{ background-color: #f8d7da; border: 1px solid #f5c6cb; color: #721c24; padding: 15px; border-radius: 5px; margin: 20px 0; }}
                        .content {{ line-height: 1.6; color: #333; }}
                        .items-list {{ background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 15px 0; }}
                        .items-list ul {{ margin: 0; padding-left: 20px; }}
                        .footer {{ margin-top: 30px; padding-top: 20px; border-top: 1px solid #eee; font-size: 12px; color: #666; text-align: center; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <div class='logo'>⚠️ Inventory Management System</div>
                        </div>
                        <div class='alert'>
                            <strong>Low Stock Alert!</strong> Some items in your inventory are running low.
                        </div>
                        <div class='content'>
                            <h2>Items Requiring Attention</h2>
                            <p>The following products have reached or fallen below their minimum stock levels:</p>
                            <div class='items-list'>
                                <ul>
                                    {itemsList}
                                </ul>
                            </div>
                            <p><strong>Action Required:</strong> Please consider restocking these items to avoid potential stockouts.</p>
                            <p>You can manage your inventory by logging into the system dashboard.</p>
                        </div>
                        <div class='footer'>
                            <p>© 2025 Inventory Management System. All rights reserved.</p>
                            <p>This is an automated alert. You can configure notification settings in your dashboard.</p>
                        </div>
                    </div>
                </body>
                </html>";
        }

        private string GenerateWelcomeEmailHtml(string userName)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <title>Welcome</title>
                    <style>
                        body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 0; padding: 20px; background-color: #f5f5f5; }}
                        .container {{ max-width: 600px; margin: 0 auto; background-color: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
                        .header {{ text-align: center; margin-bottom: 30px; }}
                        .logo {{ font-size: 24px; font-weight: bold; color: #27ae60; }}
                        .content {{ line-height: 1.6; color: #333; }}
                        .features {{ background-color: #f8f9fa; padding: 20px; border-radius: 5px; margin: 20px 0; }}
                        .feature-item {{ margin: 10px 0; }}
                        .footer {{ margin-top: 30px; padding-top: 20px; border-top: 1px solid #eee; font-size: 12px; color: #666; text-align: center; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <div class='logo'>🎉 Welcome to Inventory Management System!</div>
                        </div>
                        <div class='content'>
                            <h2>Hello {userName}!</h2>
                            <p>Welcome to our comprehensive Inventory Management System. We're excited to have you on board!</p>
                            
                            <div class='features'>
                                <h3>🚀 Key Features Available:</h3>
                                <div class='feature-item'>📦 <strong>Product Management</strong> - Add, edit, and organize your products</div>
                                <div class='feature-item'>📊 <strong>Real-time Inventory Tracking</strong> - Monitor stock levels instantly</div>
                                <div class='feature-item'>⚠️ <strong>Low Stock Alerts</strong> - Never run out of important items</div>
                                <div class='feature-item'>📈 <strong>Analytics & Reports</strong> - Insights to optimize your inventory</div>
                                <div class='feature-item'>🔔 <strong>Smart Notifications</strong> - Stay informed about critical updates</div>
                            </div>
                            
                            <p>To get started, simply log in to your account and explore the dashboard. Our intuitive interface makes inventory management effortless.</p>
                            
                            <p>If you have any questions or need assistance, don't hesitate to reach out to our support team.</p>
                            
                            <p>Happy inventory managing! 📦✨</p>
                        </div>
                        <div class='footer'>
                            <p>© 2025 Inventory Management System. All rights reserved.</p>
                            <p>Need help? Contact us at support@inventory.com</p>
                        </div>
                    </div>
                </body>
                </html>";
        }

        #endregion
    }
} 