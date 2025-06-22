# BMYLBH2025_SDDAP: Inventory Management System
## Back-End & Front-End Project Documentation
### 22.06.2025

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
- Berat Kaan ÖZTÜRK
- Selçuk Yiğit ESEDOĞLU
- Muhammet Hikmet GÜNEŞ

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
- Git for version control
- PowerShell for automation

### j. Serialization
Newtonsoft.Json 13.0.3

---

## 3. Setup & Development Environment

### a. Prerequisites
- Microsoft .NET Framework 4.8
- Visual Studio 2019/2022 
- Git
- Postman

### b. Local Setup
```bash
# Clone the repository
git clone https://github.com/bmylbh2025/BMYLBH2025_SDDAP.git
cd BMYLBH2025_SDDAP

# Backend Setup
cd Backend/BMYLBH2025_SDDAP
# Open BMYLBH2025_SDDAP.sln in Visual Studio
# Build solution
# Run project

# Frontend Setup
cd Frontend/BMYLBH2025_SDDAP
# Open BMYLBH2025_SDDAP.sln in Visual Studio
# Build solution
# Run project

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
* The information here will be provided as a seperate PDF file.

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

---

## 7. Environment Variables

### .env.example (Backend):
```
# Database Configuration
DATABASE_CONNECTION_STRING=Data Source=.\App_Data\BMYLBH2025_SDDAP.sqlite;Version=3;
DATABASE_TIMEOUT=30

# API Configuration
API_BASE_URL=http://localhost:44313/api/
CORS_ORIGINS=http://localhost:44313

# Authentication
JWT_SECRET_KEY=JWTSECRETKEY
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
    <add key="ApiBaseUrl" value="http://localhost:44313/api/" />
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
- **Master Branch**: Protected, requires pull request

### b. Code Style
- **C# Standards**: Microsoft C# coding conventions
- **Naming**: PascalCase for public members, camelCase for private
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
- **Database Scripts**: `/scripts/database/`
- **Sample Data**: `/scripts/sample-data/`

### b. Onboarding Steps
1. **Environment Setup**:
   - Install prerequisites (Visual Studio, .NET Framework)
   - Clone repository and configure environment
2. **Development Setup**:
   - Build and run backend API
   - Build and run frontend application
   - Run test suite to verify setup
3. **Documentation Review**:
   - Read architecture documentation
   - Review API documentation
   - Understand database schema

### c. Additional Resources
- **UML Diagram**: `/docs/UML_ClassDiagram.png`
- **ER Diagram**: `/docs/ER_diagram.png`
- **API Documentation**: `/docs/API Docs.pdf`
- **FAQ**: `/docs/FAQ.md`
- **Known Issues**: `/docs/Known_Issues.md`
  
