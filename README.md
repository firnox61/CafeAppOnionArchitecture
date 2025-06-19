from pathlib import Path

# Define the content from the canvas document
readme_content = """# ☕ Cafe Management System - Onion Architecture (.NET 8)

A modular, maintainable, and production-ready Cafe Management System developed with **Onion Architecture** using **ASP.NET Core 8**, **Entity Framework Core**, **AutoMapper**, **FluentValidation**, **JWT**, **Autofac**, **Hangfire**, and more.

---

## 📊 Project Architecture

This solution follows the principles of **Onion Architecture**, promoting a clean separation of concerns between layers:

### Layers:

- **Cafe.Domain**\
  Domain entities: `Product`, `Order`, `User`, `Table`, etc.\
  Interfaces like `IEntity`, `IAggregateRoot`

- **Cafe.Application**\
  Business logic, service contracts, DTOs, validators, AutoMapper profiles, and business rules

- **Cafe.Infrastructure**

  - **Persistence**: EF Core Repositories and DbContext
  - **Security**: JWT, hashing, encryption helpers
  - **Caching**: `ICacheManager` with memory cache
  - **AOP Aspects**: Logging, Performance, Transaction, Validation
  - **Hangfire**: Scheduled job setup

- **Cafe.Core**\
  Shared utilities: `Result` classes, dependency injection helpers, aspect interceptors

- **Cafe.WebAPI**\
  ASP.NET Core 8 Web API exposing endpoints for frontend integration\
  Features custom exception middleware, Swagger, JWT auth, rate limiting, static file serving, and Hangfire Dashboard.

---

## 📁 Main Features

- ✅ Modular Onion Architecture
- 🔑 JWT Authentication
- ✔️ Role-based Authorization
- ⚙ AOP with Autofac (Validation, Logging, Performance, Caching)
- 📥 Excel export with ClosedXML
- ⌚ Rate Limiting using `FixedWindowRateLimiter`
- 📊 Health Checks for database
- ⏰ Scheduled Tasks with Hangfire (e.g., log cleanup)
- 🔧 Global Exception Handling Middleware
- 🎨 Swagger UI for API testing

---

## 🏃 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server or LocalDB
- Visual Studio 2022+ / JetBrains Rider

### Setup Instructions

```bash
# Clone the repository
$ git clone https://github.com/yourusername/cafe-management-system.git
$ cd cafe-management-system

# Update DB connection string in `appsettings.json`
# Apply EF Core migrations (if not applied)
$ dotnet ef database update --project Cafe.Infrastructure --startup-project Cafe.WebAPI

# Run the project
$ dotnet run --project Cafe.WebAPI
