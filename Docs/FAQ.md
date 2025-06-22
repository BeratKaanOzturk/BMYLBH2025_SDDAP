# BMYLBH2025_SDDAP Frequently Asked Questions (FAQ)
## Inventory Management System v1.0.0
### Date: Jun 22, 2025

---

## 📋 Table of Contents
1. [General Questions](#general-questions)
2. [Installation and Configuration](#installation-and-configuration)
3. [User Account and Authentication](#user-account-and-authentication)
4. [Product and Inventory Management](#product-and-inventory-management)
5. [Order Management](#order-management)
6. [Technical Issues](#technical-issues)
7. [Performance and Optimization](#performance-and-optimization)
8. [Security](#security)

---

## 🔍 General Questions

### Q1: What is BMYLBH2025_SDDAP?
**A:** BMYLBH2025_SDDAP is a comprehensive inventory management system developed with Windows Forms frontend and RESTful API backend. It can manage products, stock, suppliers, and orders, offering real-time inventory tracking and notifications.

### Q2: What technologies was the system developed with?
**A:** 
- **Frontend:** Windows Forms (.NET Framework 4.8)
- **Backend:** ASP.NET MVC 5.2.9 Web API
- **Database:** SQLite 1.0.119.0 (Embedded Database)
- **ORM:** Entity Framework 6.4.4, Dapper 2.1.66
- **Testing:** MSTest, Moq, FluentAssertions

### Q3: Which operating systems does it run on?
**A:** It runs on Windows 10/11 (64-bit) operating systems. .NET Framework 4.8 is required.

### Q4: Does the system support multiple users?
**A:** Yes, the system supports multiple users with role-based access control. User, Admin, and other roles can be defined.

---

## ⚙️ Installation and Configuration

### Q5: How do I install the system?
**A:** 
1. Clone the repository: `git clone hhttps://github.com/BeratKaanOzturk/BMYLBH2025_SDDAP.git`
2. For Backend: Open `Backend/BMYLBH2025_SDDAP.sln` in Visual Studio
3. For Frontend: Open `Frontend/BMYLBH2025_SDDAP.sln` in Visual Studio
4. Build both projects (Ctrl+Shift+B)
5. Run Backend first (F5), then Frontend

### Q6: What prerequisites are required?
**A:**
- Windows 10/11 (64-bit)
- Microsoft .NET Framework 4.8
- Visual Studio 2019/2022 (Enterprise/Professional)
- Git for Windows
- SQLite Browser (optional)
- Postman (for API testing, optional)

### Q7: How is the database created?
**A:** The SQLite database is automatically created on first run. File location: `Backend/BMYLBH2025_SDDAP/App_Data/BMYLBH2025_SDDAP.sqlite`

### Q8: How do I check if the API is running?
**A:** Go to `http://localhost:44313/api/health` in your browser. If the system is running, it will return a JSON response.

---

## 👤 User Account and Authentication

### Q9: How do I create an account?
**A:** 
1. When the application starts, click "Register" button on the Login screen
2. Fill in the required information (Full Name, Email, Password)
3. After registration, a verification link is sent to your email address
4. Click the link in your email to verify your account

### Q10: I didn't verify my email, what should I do?
**A:** 
1. Enter your email address on the login screen
2. Use the "Resend verification email" option
3. If email doesn't arrive, check your spam folder
4. If still not received, contact system administrator

### Q11: What should I do if I forget my password?
**A:**
1. Click "Forgot Password" button on the login screen
2. Enter your email address
3. Enter the OTP code sent to your email
4. Set your new password

### Q12: I'm getting "token expired" error?
**A:** For security reasons, session duration is limited. You need to login again. This is a normal security practice.

---

## 📦 Product and Inventory Management

### Q13: How do I add a new product?
**A:**
1. Go to "Product Management" tab from the main panel
2. Click "Add New Product" button
3. Fill in product information (name, description, price, category)
4. Click "Save" button

### Q14: How do I update stock quantity?
**A:**
1. Go to "Inventory Management" tab
2. Find the product you want to update
3. Click "Update Stock" button
4. Enter new stock quantity and save

### Q15: How do low stock alerts work?
**A:** The system automatically checks minimum stock levels and sends notifications when stock falls below the set threshold. Minimum stock level can be set in product definition.

### Q16: How do I create a category?
**A:**
1. Go to "Category Management" tab
2. Click "Add New Category" button
3. Enter category name and description
4. Click "Save" button

---

## 🛒 Order Management

### Q17: How do I create a new order?
**A:**
1. Go to "Order Management" tab
2. Click "Create New Order" button
3. Select or enter customer information
4. Add ordered products and quantities
5. Click "Save Order" button

### Q18: How do I update order status?
**A:**
1. Find the order in "Order Management" tab
2. Click on the order row
3. Use "Update Status" button
4. Select new status (Pending, Preparing, Shipped, Delivered)

### Q19: Can I cancel an order?
**A:** Yes, only orders with "Pending" and "Preparing" status can be cancelled. Shipped orders cannot be cancelled.

---

## 🔧 Technical Issues

### Q20: Application won't start, what should I do?
**A:**
1. Check that .NET Framework 4.8 is installed
2. Make sure Backend is running first
3. Check that antivirus software isn't blocking the application
4. Try running Visual Studio as administrator

### Q21: I'm getting "Cannot connect to API" error?
**A:**
1. Check that Backend service is running (`http://localhost:44313/api/health`)
2. Check Windows Firewall settings
3. Temporarily disable antivirus software
4. Check API URL in App.config file

### Q22: I'm getting database errors?
**A:**
1. Check write permissions for `App_Data` folder
2. Ensure SQLite file is not being used by another application
3. If necessary, delete database file and restart application (data will be lost)

### Q23: Application is running slowly?
**A:**
1. Check database file size
2. Clean unnecessary logs
3. Check system resources (RAM, CPU)
4. Temporarily disable antivirus real-time scanning

---

## ⚡ Performance and Optimization

### Q24: How do I work with large datasets?
**A:**
- Use pagination feature (default: 50 records/page)
- Use filtering options to narrow results
- Regularly clean unnecessary data
- Ensure indexes are working correctly

### Q25: How do I optimize reports?
**A:**
- Narrow date range
- Select only necessary columns
- Generate reports during off-peak hours
- Export in chunks

---

## 🔒 Security

### Q26: Is my data secure?
**A:**
- All passwords are stored hashed
- JWT token-based authentication is used
- Database is stored locally
- API endpoints are protected with authorization

### Q27: How do I backup data?
**A:**
1. Copy `App_Data/BMYLBH2025_SDDAP.sqlite` file
2. Use PowerShell scripts for regular backups
3. Store backups in a secure location
4. For restore, replace the sqlite file

### Q28: Will there be conflicts with multi-user access?
**A:** SQLite is a file-based database and has limitations with concurrent write operations. For heavy usage, consider migrating to SQL Server.

---

## 📞 Support and Contact

### Q29: Where can I get more help?
**A:**
- Technical documentation: `Docs/` folder
- API documentation: `Docs/API Docs.pdf`
- GitHub Issues: For bug reports and feature requests
- Email support: sddap.bmylbh25@gmail.com

### Q30: How are system updates performed?
**A:**
- Monthly feature updates
- Weekly bug fixes
- Critical security patches as needed
- Pull latest version from Git and rebuild

---

**Note:** For questions not covered in this document, please contact the system administrator or create a new issue on the GitHub Issues page.

**Last Updated:** Jun 22, 2025  
**Version:** v1.0.0 