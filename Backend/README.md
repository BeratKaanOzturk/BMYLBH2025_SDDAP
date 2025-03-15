# Backend

This folder contains the database operations and business logic of the Inventory Management System.

## Technologies

- C# 
- Entity Framework
- Microsoft SQL Server

## Folder Structure

- `DataAccess`: Database access layer
- `BusinessLogic`: Business logic layer
- `Models`: Data models
- `Services`: External service integrations (Email, etc.)
- `Scripts`: Database script files

## Database Schema

The system will include the following main tables:

- Users: User information
- Products: Product information
- Categories: Product categories
- Inventory: Stock movements
- Suppliers: Supplier information
- Orders: Order information
- OrderDetails: Order details
- Notifications: Notification records

## Development Notes

- Entity Framework Code First approach will be used
- Repository pattern will be implemented
- Database operations will be managed with LINQ queries
- Asynchronous methods will be used
