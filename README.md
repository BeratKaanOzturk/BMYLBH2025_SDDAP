# 📦 Inventory Management System

**BMYLBH2025_SDDAP** - A comprehensive inventory management solution built for the Software Design Development and Practice course.

## 🌟 About the Project

This modern inventory management system enables businesses to efficiently track product stocks, manage suppliers, process orders, and monitor inventory levels in real-time. Built with a clean architecture separating frontend and backend concerns, it provides a robust foundation for inventory operations.

## ✨ Key Features

### 🔐 **Authentication & Security**
- Modern, redesigned login interface with card-based layout
- User registration and login system
- Email verification for new accounts
- Password reset functionality
- Session management and security
- Professional UI with Segoe UI typography and modern buttons

### 📊 **Dashboard & Analytics**
- Real-time inventory overview with modern card-based design
- Summary cards showing:
  - 💰 Total Inventory Value
  - ⚠️ Low Stock Items Alert
  - 📦 Total Products Count
  - 🛒 Total Orders Count (NEW!)
- Visual status indicators and color-coded alerts
- Comprehensive error handling with try-catch blocks
- Auto-refresh functionality for real-time data

### 🏷️ **Product Management**
- Add, edit, and delete products
- Category-based organization
- Minimum stock level monitoring
- Product pricing and descriptions
- Advanced search and filtering

### 🗂️ **Category Management**
- Create and manage product categories
- Hierarchical category structure
- Category-based product filtering
- Real-time product count per category

### 🛒 **Order Management**
- Create new purchase orders
- Order status tracking (Pending, Confirmed, Processing, Completed, Cancelled)
- Supplier integration
- Order details with multiple products
- Order history and reporting

### 📈 **Inventory Tracking**
- Real-time stock level monitoring
- Automatic low stock alerts
- Inventory value calculations
- Stock movement history
- Batch inventory updates

### 📧 **Notification System**
- Email notifications for:
  - Account verification
  - Low stock alerts
  - New order confirmations
  - Password reset requests
- Automated email delivery system

### 🔍 **Search & Filtering**
- Advanced search across all modules
- Real-time filtering capabilities
- Smart search suggestions
- Multi-criteria filtering options

## 🛠️ Technologies

### **Frontend**
- **C# Windows Forms** - Modern, responsive desktop application
- **.NET Framework** - Robust application framework
- **Modern UI Design** - Clean, professional interface with card-based layouts
- **Segoe UI Font** - Contemporary typography
- **Color-coded Status System** - Visual indicators for different states

### **Backend**
- **ASP.NET Web API** - RESTful API architecture
- **C#** - Server-side business logic
- **Entity Framework** - Object-relational mapping
- **SQLite Database** - Lightweight, embedded database
- **Dependency Injection** - Clean architecture patterns
- **Repository Pattern** - Data access abstraction

### **Development Tools**
- **Visual Studio Enterprise** - Primary IDE for debugging and testing
- **Cursor AI** - Code editing and AI assistance
- **MSTest Framework** - Unit testing
- **Moq** - Mocking framework for tests
- **FluentAssertions** - Fluent testing assertions

### **Additional Libraries**
- **Dapper** - Micro-ORM for database operations
- **Newtonsoft.Json** - JSON serialization
- **System.Data.SQLite** - SQLite database provider
- **Unity Container** - Dependency injection container

## 📁 Project Structure

```
BMYLBH2025_SDDAP/
├── Backend/                          # Backend API Solution
│   ├── BMYLBH2025_SDDAP/            # Main API Project
│   │   ├── Controllers/              # API Controllers
│   │   ├── Models/                   # Data Models
│   │   ├── Services/                 # Business Logic
│   │   ├── App_Data/                 # SQLite Database
│   │   └── Web.config               # Configuration
│   ├── BMYLBH2025_SDDAP.Tests/      # Backend Tests
│   └── packages/                     # NuGet Packages
├── Frontend/                         # Windows Forms Application
│   ├── BMYLBH2025_SDDAP/            # Main Desktop App
│   │   ├── Forms/                    # UI Forms
│   │   ├── Models/                   # Client-side Models
│   │   ├── Services/                 # API Client Services
│   │   └── Properties/               # App Properties
│   ├── BMYLBH2025_SDDAP.Tests/      # Frontend Tests
│   └── packages/                     # NuGet Packages
├── deployment/                       # Deployment Artifacts
├── docker/                          # Containerization
├── scripts/                         # Automation Scripts
└── Docs/                            # Documentation
```

## 🚀 Getting Started

### **Prerequisites**
- **Visual Studio 2019/2022** (Community or higher)
- **.NET Framework 4.7.2** or higher
- **IIS Express** (included with Visual Studio)
- **SQLite** (embedded, no separate installation needed)

### **Installation Steps**

1. **Clone the Repository**
   ```bash
   git clone https://github.com/your-username/BMYLBH2025_SDDAP.git
   cd BMYLBH2025_SDDAP
   ```

2. **Setup Backend API**
   ```bash
   cd Backend/BMYLBH2025_SDDAP
   # Open BMYLBH2025_SDDAP.sln in Visual Studio
   # Restore NuGet packages
   # Build solution (Ctrl+Shift+B)
   ```

3. **Setup Frontend Application**
   ```bash
   cd Frontend/BMYLBH2025_SDDAP
   # Open BMYLBH2025_SDDAP.sln in Visual Studio
   # Restore NuGet packages
   # Build solution (Ctrl+Shift+B)
   ```

4. **Database Setup**
   - SQLite database is automatically created on first run
   - Sample data will be seeded automatically
   - Database file location: `Backend/BMYLBH2025_SDDAP/App_Data/BMYLBH2025_SDDAP.sqlite`

5. **Run the Application**
   - Start Backend API first (F5 in Backend solution)
   - Then start Frontend application (F5 in Frontend solution)
   - Use "TEST LOGIN" for quick access or register a new account

### **Default Access**
- **API Endpoint**: `http://localhost:port/api/`
- **Test Account**: Available via "TEST LOGIN" button
- **Registration**: Create new account with email verification

## 🧪 Testing

### **Running Tests**
```bash
# Backend Tests
cd Backend/BMYLBH2025_SDDAP.Tests
dotnet test

# Frontend Tests  
cd Frontend/BMYLBH2025_SDDAP.Tests
dotnet test

# Run All Tests
./scripts/test/run-all-tests.ps1
```

### **Test Coverage**
- **Unit Tests**: Business logic and data access
- **Integration Tests**: API endpoints and database operations
- **UI Tests**: Form interactions and user workflows
- **Mock Data**: Comprehensive test data generation

## 📊 Database Schema

### **Core Tables**
- **Users** - Authentication and user management
- **Categories** - Product categorization
- **Products** - Product catalog and details
- **Suppliers** - Supplier information
- **Orders** - Purchase order headers
- **OrderDetails** - Order line items
- **Inventory** - Stock levels and tracking

### **Key Relationships**
- Products → Categories (Many-to-One)
- Orders → Suppliers (Many-to-One)
- OrderDetails → Products (Many-to-One)
- OrderDetails → Orders (Many-to-One)
- Inventory → Products (One-to-One)

## 🎨 UI/UX Design

### **Design Principles**
- **Card-based Layout** - Clean, organized information presentation
- **Color-coded Status** - Visual indicators for different states
- **Modern Typography** - Segoe UI for professional appearance
- **Responsive Design** - Adapts to different screen sizes
- **Intuitive Navigation** - Tab-based interface with clear sections

### **Color Scheme**
- **Primary Blue**: `#3498DB` - Actions and primary buttons
- **Success Green**: `#2ECC71` - Positive actions and status
- **Warning Yellow**: `#F1C40F` - Attention and updates
- **Danger Red**: `#E74C3C` - Alerts and critical status
- **Info Purple**: `#9B59B6` - Secondary actions
- **Neutral Gray**: `#95A5A6` - Disabled and secondary text

## 🔧 Configuration

### **Backend Configuration** (`Web.config`)
```xml
<connectionStrings>
  <add name="DefaultConnection" 
       connectionString="Data Source=|DataDirectory|BMYLBH2025_SDDAP.sqlite" 
       providerName="System.Data.SQLite" />
</connectionStrings>
```

### **Frontend Configuration** (`App.config`)
```xml
<appSettings>
  <add key="ApiBaseUrl" value="http://localhost:port/api/" />
  <add key="EnableLogging" value="true" />
</appSettings>
```

## 🤝 Contributing

1. **Fork the repository**
2. **Create a feature branch** (`git checkout -b feature/amazing-feature`)
3. **Commit your changes** (`git commit -m 'Add amazing feature'`)
4. **Push to the branch** (`git push origin feature/amazing-feature`)
5. **Open a Pull Request**

## 📝 License

This project is created for educational purposes as part of the Software Design Development and Practice course.

## 👥 Team

**Course**: Software Design Development and Practice  
**Academic Year**: 2025  
**Project Code**: BMYLBH2025_SDDAP  

## 📞 Support

For questions and support:
- **Issues**: [GitHub Issues](https://github.com/your-username/BMYLBH2025_SDDAP/issues)
- **Documentation**: Check the `/Docs` folder
- **Wiki**: [Project Wiki](https://github.com/your-username/BMYLBH2025_SDDAP/wiki)

## 🚀 Recent Updates

### **Latest Features (v1.2)**
- ✅ **Modern Login Interface** - Completely redesigned with card layout
- ✅ **Order Count Dashboard** - New purple card showing total orders
- ✅ **Comprehensive Error Handling** - Try-catch blocks across all methods
- ✅ **Enhanced Order Management** - Improved supplier and product data loading
- ✅ **Visual Status Indicators** - Color-coded status for orders and inventory

### **Development Status**
- 🟢 **Authentication System** - Complete
- 🟢 **Dashboard & Analytics** - Complete  
- 🟢 **Product Management** - Complete
- 🟢 **Category Management** - Complete
- 🟢 **Order Management** - Complete
- 🟢 **Inventory Tracking** - Complete
- 🟡 **Email Notifications** - In Progress
- 🟡 **Advanced Reporting** - Planned

## 🎯 Future Enhancements

- 📊 **Advanced Analytics Dashboard** with charts and graphs
- 📱 **Mobile-Responsive Design** for tablet compatibility  
- 🔔 **Real-time Push Notifications** for critical alerts
- 📄 **PDF Report Generation** for inventory and orders
- 🌐 **Multi-language Support** for international use
- 🔄 **Automatic Backup System** for data safety

---

**Built with ❤️ for efficient inventory management**
