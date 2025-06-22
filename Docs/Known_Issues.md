# BMYLBH2025_SDDAP Known Issues
## Inventory Management System v1.0.0

## 🚨 Critical Issues

**CI-001: SQLite Database Concurrent Access**  
- **Status:** ❌ Active | **Priority:** Critical  
- **Issue:** Database locks with multiple users writing simultaneously  
- **Workaround:** One user at a time for data entry  
- **Solution:** SQL Server migration planned (v1.1.0)

**CI-002: Email Verification System**  
- **Status:** ❌ Active | **Priority:** Critical  
- **Issue:** Verification emails not sent with SMTP misconfiguration  
- **Workaround:** Manually set IsEmailVerified=true in database  

## ⚠️ High Priority Issues

**HPI-001: Token Auto-Refresh**  
- JWT tokens don't auto-refresh, causing unexpected logouts  
- **Workaround:** Extend token duration in App.config  

**HPI-002: Memory Leak in Excel Export**  
- RAM usage spikes when exporting 10,000+ records  
- **Workaround:** Export in chunks (max 5,000 records)  

**HPI-003: Concurrent Product Editing**  
- Data inconsistency when multiple users edit same product  
- **Workaround:** Refresh before editing, coordinate users  

## 📊 Other Active Issues
- Form resize scaling problems
- Date format inconsistencies (DD/MM vs MM/DD)
- Case-sensitive search function
- Initial startup takes 15-20 seconds
- DataGridView scroll performance with 1000+ records
- Missing tooltips on buttons
- No dark mode support
- Windows 7 incompatibility
- 4K monitor scaling issues

## 📈 Statistics
**Total Issues:** 22 | **Active:** 16 | **Critical:** 2 | **High:** 3

## 🚀 Upcoming Fixes
- **v1.1.0 (Feb 2025):** SQL Server support, refresh tokens
- **v1.2.0 (Mar 2025):** Multi-language, performance optimizations
- **v1.3.0 (Apr 2025):** Real-time notifications, web interface

## 📞 Support
- **GitHub Issues:** Bug reports and feature requests
- **Email:** sddap.bmylbh25@gmail.com (urgent issues)
- **Documentation:** `Docs/` folder for detailed guides

---
**Last Updated:** Jun 22, 2025 | **Version:** v1.0.0 