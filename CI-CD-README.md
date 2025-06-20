# CI/CD Pipeline Documentation - BMYLBH2025_SDDAP

## Overview
This document describes the Continuous Integration and Continuous Deployment (CI/CD) pipeline implemented for the BMYLBH2025_SDDAP project as part of **Week 9: CI/CD & Automated Deployment**.

## 📁 Project Structure
`
BMYLBH2025_SDDAP/
├── .github/workflows/ci-cd.yml      # GitHub Actions CI/CD Pipeline
├── scripts/build/                   # Build automation scripts
├── scripts/test/                    # Test execution scripts
├── scripts/deploy/                  # Deployment scripts
├── Backend/                         # ASP.NET Web API
├── Frontend/                        # Windows Forms Application
└── CI-CD-README.md                 # This documentation
`

## 🚀 CI/CD Pipeline Features

### **Automated Build Process**
- **Backend API**: ASP.NET Web API with Entity Framework
- **Frontend**: Windows Forms application
- **Parallel Builds**: Both projects build simultaneously
- **Error Handling**: Comprehensive error reporting

### **Testing Integration**
- **Unit Tests**: Automated execution of all unit tests
- **Test Reports**: Automatic generation of test results
- **Coverage Analysis**: Code coverage metrics

### **Deployment Automation**
- **Multi-Environment**: Staging and Production support
- **Artifact Management**: Build artifact collection
- **Health Checks**: Post-deployment verification

## 🛠️ Build Scripts Usage

### **Master Build Script**
`powershell
.\scripts\build\build-all.ps1 -Configuration Release -RunTests
`

### **Individual Builds**
`powershell
# Backend only
.\scripts\build\build-backend.ps1 -Configuration Debug

# Frontend only  
.\scripts\build\build-frontend.ps1 -Configuration Release
`

## 🧪 Testing
`powershell
.\scripts\test\run-all-tests.ps1 -Configuration Release
`

## 🚢 Deployment
`powershell
.\scripts\deploy\deploy.ps1 -Environment Staging -Configuration Release
`

## 📊 Success Metrics
✅ **Week 9 Requirements Met:**
- [x] Automated build on master branch commits
- [x] Unit test execution in CI pipeline  
- [x] Build artifact generation
- [x] Multi-environment deployment
- [x] Error handling and rollback
- [x] Comprehensive documentation

---
**BMYLBH2025_SDDAP CI/CD Pipeline - Week 9 Implementation**
