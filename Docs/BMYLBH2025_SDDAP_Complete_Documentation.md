# BMYLBH2025_SDDAP: Inventory Management System
## Back-End & Front-End Project Documentation
### 20.01.2025

---

## 1. Project Overview

This project is a comprehensive inventory management system built with Windows Forms frontend and RESTful backend service for managing products, inventory, suppliers, and orders of registered users. The system supports authentication, authorization, CRUD operations, real-time inventory tracking, and notifications.

### a. Target Users
- **Internal admin panel**: System administrators managing users and system configuration
- **Inventory management application**: Store managers and clerks handling daily operations
- **Reporting system**: Management personnel requiring analytics and reports
- **3rd party integrators**: External systems requiring inventory data access

### b. Architecture Style
- **Frontend**: Windows Forms Desktop Application
- **Backend**: REST API
- **Database**: SQLite Embedded Database

### c. Version
v1.0.0

### d. Maintainers
- Development Team Lead
- Senior Developer
- Database Administrator
- Quality Assurance Lead

### e. Release Cycle
- Monthly feature releases
- Weekly bug fixes and minor updates
- Critical security patches as needed

### f. Scope
- User registration and authentication
- Product catalog management
- Real-time inventory tracking
- Supplier and order management
- Low stock notifications
- Comprehensive reporting
- Role-based access control

### g. Out of Scope
- Web-based frontend interface
- Payment processing integration
- Third-party logistics integration
- Mobile application development

---

## 2. Technology Stack

### a. Programming Language
C# .NET Framework 4.8

### b. Frontend Framework
Windows Forms Application

### c. Backend Framework
ASP.NET MVC 5.2.9 Web API

### d. Database
SQLite 1.0.119.0 (Embedded Database)

### e. ORM & Data Access
- Entity Framework 6.4.4
- Dapper 2.1.66 (Micro ORM)

### f. Authentication
JWT Token-based authentication with email verification

### g. Dependency Injection
Unity 5.11.10

### h. Testing Framework
- MSTest 2.2.10
- Moq 4.20.70 (Mocking)
- FluentAssertions 6.12.0

### i. Development Tools
- Visual Studio 2022 Enterprise
- Cursor AI
- Git for version control
- PowerShell for automation

### j. Serialization
Newtonsoft.Json 13.0.3

---

## 3. Setup & Development Environment

### a. Prerequisites
- Windows 10/11 (64-bit)
- Microsoft .NET Framework 4.8
- Visual Studio 2019/2022 (Enterprise/Professional)
- Git for Windows
- SQLite Browser (optional)
- Postman (for API testing)

### b. Local Setup
```bash
# Clone the repository
git clone https://github.com/bmylbh2025/BMYLBH2025_SDDAP.git
cd BMYLBH2025_SDDAP

# Backend Setup
cd Backend/BMYLBH2025_SDDAP
# Open BMYLBH2025_SDDAP.sln in Visual Studio
# Build solution (Ctrl+Shift+B)
# Set as startup project and run (F5)

# Frontend Setup
cd Frontend/BMYLBH2025_SDDAP
# Open BMYLBH2025_SDDAP.sln in Visual Studio
# Build solution (Ctrl+Shift+B)
# Set as startup project and run (F5)

# Database Setup
# SQLite database is automatically created on first run
# Located at: Backend/BMYLBH2025_SDDAP/App_Data/BMYLBH2025_SDDAP.sqlite
```

---

## 4. Folder Structure

```
BMYLBH2025_SDDAP/
├── Backend/
│   ├── BMYLBH2025_SDDAP/           # Web API Project
│   │   ├── Controllers/            # API Endpoints
│   │   ├── Models/                 # Data Models
│   │   ├── Services/               # Business Logic
│   │   ├── Repositories/           # Data Access Layer
│   │   ├── App_Data/              # Database Files
│   │   └── App_Start/             # Configuration
│   └── BMYLBH2025_SDDAP.Tests/    # Backend Unit Tests
├── Frontend/
│   ├── BMYLBH2025_SDDAP/          # Windows Forms Project
│   │   ├── Forms/                 # UI Forms
│   │   ├── Models/                # Client Models
│   │   ├── Services/              # Client Services
│   │   └── Properties/            # Application Properties
│   └── BMYLBH2025_SDDAP.Tests/    # Frontend Unit Tests
├── Docs/                          # Documentation
├── Scripts/                       # Automation Scripts
└── Deployment/                    # Deployment Files
```

---

## 5. API Documentation

### a. API Base URL
- **Local Development**: `http://localhost:8080/api`
- **Production**: `https://api.bmylbh2025-sddap.com/api`

### b. Authentication
All API endpoints (except authentication) require Bearer token:
```
Authorization: Bearer <access_token>
```

### c. Endpoints

#### i. /api/auth:
- **POST /login**:
  - **Description**: Authenticate user and receive access token
  - **Headers**: 
    - `Content-Type: application/json`
  - **Body**:
    ```json
    {
        "email": "user@example.com",
        "password": "userpassword"
    }
    ```
  - **Response**:
    ```json
    {
        "success": true,
        "message": "Login successful",
        "data": {
            "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
            "user": {
                "userId": 123,
                "email": "user@example.com",
                "fullName": "John Doe",
                "role": "User"
            },
            "expiresAt": "2025-01-21T10:30:00.000Z"
        }
    }
    ```

- **POST /register**:
  - **Description**: Register new user account
  - **Body**:
    ```json
    {
        "email": "newuser@example.com",
        "password": "securepassword123",
        "fullName": "Jane Smith",
        "confirmPassword": "securepassword123"
    }
    ```

- **POST /forgot-password**:
  - **Description**: Initiate password reset with OTP
  - **Body**:
    ```json
    {
        "email": "user@example.com"
    }
    ```

#### ii. /api/products:
- **GET /**:
  - **Description**: Retrieve all products with optional filtering
  - **Headers**:
    - `Authorization: Bearer <access_token>`
  - **Query Params**:
    - `categoryId` (int, optional): Filter by category
    - `search` (string, optional): Search in name/description
    - `lowStock` (boolean, optional): Filter low stock items
    - `page` (int, optional): Page number (default: 1)
    - `pageSize` (int, optional): Items per page (default: 50)
  - **Response**:
    ```json
    {
        "success": true,
        "data": {
            "products": [
                {
                    "productId": 1,
                    "name": "Laptop Computer",
                    "description": "High-performance laptop",
                    "price": 1299.99,
                    "minimumStockLevel": 5,
                    "categoryId": 2,
                    "categoryName": "Electronics",
                    "currentStock": 15,
                    "isLowStock": false
                }
            ],
            "pagination": {
                "currentPage": 1,
                "totalPages": 5,
                "totalItems": 247
            }
        }
    }
    ```

- **GET /{product_id}**:
  - **Description**: Retrieve specific product by ID
  - **Headers**:
    - `Authorization: Bearer <access_token>`
  - **Response**:
    ```json
    {
        "success": true,
        "data": {
            "productId": 1,
            "name": "Laptop Computer",
            "description": "High-performance laptop",
            "price": 1299.99,
            "currentStock": 15,
            "categoryName": "Electronics"
        }
    }
    ```

- **POST /**:
  - **Description**: Create new product (Admin/Manager only)
  - **Body**:
    ```json
    {
        "name": "Wireless Mouse",
        "description": "Ergonomic wireless mouse",
        "price": 29.99,
        "minimumStockLevel": 20,
        "categoryId": 2
    }
    ```

- **PUT /{product_id}**:
  - **Description**: Update existing product (Admin/Manager only)

- **DELETE /{product_id}**:
  - **Description**: Delete product (Admin only)

#### iii. /api/categories:
- **GET /**:
  - **Description**: Retrieve all categories
- **POST /**:
  - **Description**: Create new category (Admin/Manager only)

#### iv. /api/inventory:
- **GET /**:
  - **Description**: Retrieve current inventory levels
- **PUT /{product_id}**:
  - **Description**: Update inventory quantity

#### v. /api/orders:
- **GET /**:
  - **Description**: Retrieve all orders
- **POST /**:
  - **Description**: Create new purchase order

#### vi. /api/notifications:
- **GET /**:
  - **Description**: Retrieve user notifications
- **PUT /{notification_id}/read**:
  - **Description**: Mark notification as read

### d. General Request & Response Conventions

**Standard Response Format**:
```json
{
    "success": true/false,
    "message": "Operation description",
    "data": { /* Response payload */ },
    "errorType": "ErrorType" (if error),
    "timestamp": "2025-01-20T10:30:00.000Z"
}
```

**HTTP Status Codes**:
- 200: Success (GET, PUT, PATCH)
- 201: Created (POST)
- 204: No Content (DELETE)
- 400: Bad Request
- 401: Unauthorized
- 403: Forbidden
- 404: Not Found
- 422: Validation Error
- 500: Internal Server Error

---

## 6. Database Schema

### a. Tables
- **Users**: User accounts and authentication
- **Categories**: Product categorization
- **Products**: Product catalog
- **Inventory**: Stock levels and tracking
- **Suppliers**: Vendor information
- **Orders**: Purchase orders
- **OrderDetails**: Order line items
- **Notifications**: System alerts

### b. Relations
- `Products.CategoryID` → `Categories.CategoryID`
- `Inventory.ProductID` → `Products.ProductID`
- `Orders.SupplierID` → `Suppliers.SupplierID`
- `OrderDetails.OrderID` → `Orders.OrderID`
- `OrderDetails.ProductID` → `Products.ProductID`
- `Notifications.UserID` → `Users.UserID`

### c. Indexes
Applied on:
- `Users.Email` (Unique)
- `Users.Username` (Unique)
- `Products.CategoryID`
- `Products.Name`
- `Inventory.ProductID`
- `Orders.SupplierID`
- `OrderDetails.OrderID`
- `OrderDetails.ProductID`

### d. Schema Management
- Database auto-created on first application run
- Schema updates managed through Entity Framework migrations
- Backup and restore procedures documented

---

## 7. Environment Variables

### .env.example (Backend):
```
# Database Configuration
DATABASE_CONNECTION_STRING=Data Source=.\App_Data\BMYLBH2025_SDDAP.sqlite;Version=3;
DATABASE_TIMEOUT=30

# API Configuration
API_BASE_URL=http://localhost:8080/api/
CORS_ORIGINS=http://localhost:3000,http://localhost:8080

# Authentication
JWT_SECRET_KEY=your-secret-key-here
JWT_EXPIRATION_HOURS=24
PASSWORD_HASH_ROUNDS=12

# Email Configuration
EMAIL_SMTP_SERVER=smtp.gmail.com
EMAIL_SMTP_PORT=587
EMAIL_USERNAME=your-email@gmail.com
EMAIL_PASSWORD=your-app-password
EMAIL_FROM_ADDRESS=noreply@bmylbh2025-sddap.com

# Logging
LOG_LEVEL=Information
LOG_FILE_PATH=./Logs/application.log
```

### app.config (Frontend):
```xml
<configuration>
  <appSettings>
    <add key="ApiBaseUrl" value="http://localhost:8080/api/" />
    <add key="TokenExpirationHours" value="24" />
    <add key="AutoSaveInterval" value="300" />
    <add key="DefaultPageSize" value="50" />
  </appSettings>
</configuration>
```

---

## 8. Testing Strategy

### a. Unit Tests
- **Framework**: MSTest 2.2.10
- **Mocking**: Moq 4.20.70
- **Assertions**: FluentAssertions 6.12.0
- **Coverage Target**: >80%

### b. Integration Tests
- API endpoint testing
- Database integration testing
- Service layer integration

### c. End-to-End Tests
- Complete user workflows
- Authentication flow testing
- CRUD operations validation

### d. Code Coverage
- Minimum 80% overall coverage
- 90% coverage for critical business logic
- 100% coverage for security components

### e. Run All Tests
```bash
# Backend Tests
cd Backend/BMYLBH2025_SDDAP.Tests
dotnet test --collect:"XPlat Code Coverage"

# Frontend Tests
cd Frontend/BMYLBH2025_SDDAP.Tests
dotnet test --collect:"XPlat Code Coverage"

# Generate Coverage Report
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:./CoverageReport
```

---

## 9. Deployment Pipeline

### a. Build & Deployment Process
1. **Development**: Feature branches merged to develop
2. **Testing**: Automated tests run on pull requests
3. **Staging**: Deploy to staging environment for QA
4. **Production**: Manual deployment after approval

### b. Deployment Steps
```bash
# Backend Deployment
cd Backend/BMYLBH2025_SDDAP
dotnet publish -c Release -o ./publish
# Copy to IIS or hosting environment

# Frontend Deployment
cd Frontend/BMYLBH2025_SDDAP
dotnet publish -c Release -o ./publish
# Create installer package
```

### c. Rollback Strategy
- Keep previous 3 versions for quick rollback
- Database backup before each deployment
- Automated rollback scripts available

---

## 10. Error Handling & Logging

### a. Centralized Exception Handler
- Global exception handling in API controllers
- Structured error responses
- Error categorization and classification

### b. Logging Configuration
- **Framework**: Built-in .NET logging
- **Format**: JSON structured logs
- **Levels**: Trace, Debug, Information, Warning, Error, Critical

### c. Log Categories
- **Application Logs**: User actions and system events
- **Error Logs**: Exceptions and error conditions
- **Performance Logs**: Response times and performance metrics
- **Security Logs**: Authentication and authorization events

### d. Correlation ID
- Unique request tracking across components
- Request/response correlation for debugging
- Distributed tracing support

---

## 11. API Security & Rate Limiting

### a. Token Management
- **Access Token**: 24 hours expiration
- **Refresh Token**: 7 days expiration (if implemented)
- **Token Storage**: Secure client-side storage

### b. Password Security
- **Hashing**: BCrypt with salt rounds 12
- **Complexity**: Minimum 8 characters, mixed case, numbers
- **Reset**: OTP-based password reset flow

### c. Rate Limiting
- **General API**: 100 requests/minute/user
- **Authentication**: 10 requests/minute/IP
- **Heavy Operations**: 20 requests/minute/user

### d. Access Control
- **CORS**: Configured allowlist for origins
- **IP Whitelist**: Configurable per environment
- **Role-based Permissions**: Admin, Manager, User roles

### e. Security Headers
```
X-Content-Type-Options: nosniff
X-Frame-Options: DENY
X-XSS-Protection: 1; mode=block
Strict-Transport-Security: max-age=31536000
```

---

## 12. Monitoring & Observability

### a. Performance Metrics
- **Response Time**: p50, p95, p99 percentiles
- **Throughput**: Requests per second
- **Error Rate**: 4xx/5xx error percentages
- **Database**: Query duration and connection pool usage

### b. Health Checks
- **API Health**: `/api/health` endpoint
- **Database Health**: Connection and query tests
- **Dependencies**: External service availability

### c. Alerting
- **Critical Errors**: Immediate notification
- **Performance Degradation**: Threshold-based alerts
- **System Resources**: Memory, CPU, disk usage
- **Security Events**: Failed login attempts, suspicious activity

### d. Dashboards
- Real-time system metrics
- User activity analytics
- Business metrics (inventory levels, order volumes)
- Error tracking and resolution

---

## 13. Service-Level Agreements (SLA)

### a. Uptime
**Target**: 99.5% uptime during business hours (8 AM - 6 PM)

### b. Performance
- **Max Response Time**: <1 second (p95) for API calls
- **Application Startup**: <5 seconds
- **Report Generation**: <30 seconds for complex reports

### c. Support Response Times
- **Critical Issues**: <4 hours response
- **High Priority**: <24 hours response
- **Medium Priority**: <72 hours response
- **Low Priority**: <1 week response

### d. Data Protection
- **Compliance**: GDPR compliant data handling
- **Encryption**: Data at rest and in transit
- **Privacy**: User data protection and anonymization

### e. Backup & Recovery
- **Frequency**: Daily automated backups
- **Retention**: 30 days backup retention
- **Location**: Local and cloud backup storage
- **Recovery Time**: <2 hours for complete system restore
- **Disaster Recovery**: Offsite backup for disaster scenarios

---

## 14. Contribution Guidelines

### a. Git Strategy
- **Branching**: Feature branching (feature/feature-name)
- **Main Branch**: Protected, requires pull request
- **Develop Branch**: Integration branch for features
- **Release Branch**: Preparation for production releases

### b. Code Style
- **C# Standards**: Microsoft C# coding conventions
- **Naming**: PascalCase for public members, camelCase for private
- **Documentation**: XML documentation for public APIs
- **Formatting**: Consistent indentation and spacing

### c. Pull Request Checklist
- [ ] Code formatted according to standards
- [ ] Unit tests included and passing
- [ ] Integration tests updated if needed
- [ ] Documentation updated
- [ ] No security vulnerabilities introduced
- [ ] Performance impact assessed
- [ ] Backward compatibility maintained

### d. Code Review Process
1. Developer creates pull request
2. Automated tests must pass
3. Peer review by team member
4. Security review for sensitive changes
5. Merge after approval

---

## 15. Onboarding & Appendix

### a. Quick Start Resources
- **Postman Collection**: `/docs/BMYLBH2025_SDDAP.postman_collection.json`
- **Database Scripts**: `/scripts/database/`
- **Sample Data**: `/scripts/sample-data/`

### b. Onboarding Steps
1. **Environment Setup**:
   - Install prerequisites (Visual Studio, .NET Framework)
   - Clone repository and configure environment
   - Run database setup scripts
2. **Development Setup**:
   - Build and run backend API
   - Build and run frontend application
   - Run test suite to verify setup
3. **Documentation Review**:
   - Read architecture documentation
   - Review API documentation
   - Understand database schema

### c. Architecture Documentation
- **System Architecture**: `/docs/architecture.md`
- **Database Design**: `/docs/database-design.md`
- **Security Guidelines**: `/docs/security.md`
- **Performance Guidelines**: `/docs/performance.md`

### d. Additional Resources
- **ER Diagram**: `/docs/er_diagram.png`
- **API Specification**: `/docs/api-spec.yaml`
- **User Manual**: `/docs/user-manual.pdf`
- **Installation Guide**: `/docs/installation.md`

### e. FAQ & Troubleshooting
- **FAQ**: `/docs/faq.md`
- **Known Issues**: `/docs/known-issues.md`
- **Troubleshooting Guide**: `/docs/troubleshooting.md`
- **Performance Tuning**: `/docs/performance-tuning.md`

### f. Support Contacts
- **Technical Support**: support@bmylbh2025-sddap.edu
- **Development Team**: dev-team@bmylbh2025-sddap.edu
- **Documentation**: docs@bmylbh2025-sddap.edu
- **Security Issues**: security@bmylbh2025-sddap.edu

---

**Document Version**: 1.0  
**Last Updated**: January 20, 2025  
**Next Review**: March 20, 2025  
**Document Owner**: BMYLBH2025 Development Team

---

*© 2025 BMYLBH2025 Software Development Team. All rights reserved.*

*This document contains confidential and proprietary information. Unauthorized distribution is prohibited.* 