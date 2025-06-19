# ☕ Cafe Management System - Onion Architecture (.NET 8)

A modular, maintainable, and production-ready Cafe Management System developed with **Onion Architecture** using **ASP.NET Core 8**, **Entity Framework Core**, **AutoMapper**, **FluentValidation**, **JWT**, **Autofac**, **Hangfire**, and more.

---

## 📊 Project Architecture

This solution follows the principles of **Onion Architecture**, promoting a clean separation of concerns between layers:

### Layers:

- **Cafe.Domain**  Domain entities: `Product`, `Order`, `User`, `Table`, etc.  Interfaces like `IEntity`, `IAggregateRoot`

- **Cafe.Application**  Business logic, service contracts, DTOs, validators, AutoMapper profiles, and business rules

- **Cafe.Infrastructure**

  - **Persistence**: EF Core Repositories and DbContext
  - **Security**: JWT, hashing, encryption helpers
  - **Caching**: `ICacheManager` with memory cache
  - **AOP Aspects**: Logging, Performance, Transaction, Validation
  - **Hangfire**: Scheduled job setup

- **Cafe.Core**  Shared utilities: `Result` classes, dependency injection helpers, aspect interceptors

- **Cafe.WebAPI**  ASP.NET Core 8 Web API exposing endpoints for frontend integration  Features custom exception middleware, Swagger, JWT auth, rate limiting, static file serving, and Hangfire Dashboard.

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
```

---

## 📊 Health Check

```http
GET /health
```

Returns JSON status of registered health checks (e.g., database).

---

## 🔔 Rate Limiting

```csharp
// Enabled via FixedWindowLimiter: 5 requests per 10 seconds per IP
```

---

## ⚡ Technologies Used

| Area                 | Tech                     |
| -------------------- | ------------------------ |
| Backend Framework    | ASP.NET Core 8           |
| ORM                  | Entity Framework Core    |
| Dependency Injection | Autofac                  |
| Security             | JWT, Hashing             |
| Caching              | MemoryCache              |
| Logging              | FileLogger + AOP         |
| Scheduling           | Hangfire                 |
| Mapping              | AutoMapper               |
| Validation           | FluentValidation         |
| API Docs             | Swagger / Swashbuckle    |
| Excel Export         | ClosedXML                |
| Architecture         | Onion, SOLID, Clean Code |

---

## 📅 Scheduled Job Example (Hangfire)

```csharp
RecurringJob.AddOrUpdate<AuditLogCleanupJob>(
    "audit-log-cleanup",
    job => job.CleanupOldLogs(),
    Cron.Daily
);
```

Accessible at `/hangfire`.

---

## 🔑 Authentication

- JWT bearer tokens
- Configure in `appsettings.json` > `TokenOptions`

---

## 📲 API Endpoints

All endpoints are organized by controller:

- `/api/products`
- `/api/orders`
- `/api/payments`
- `/api/users`
- `/api/auth`
- ...and more

Swagger UI: `https://localhost:{port}/swagger`

---

## ⚡ Sample Token Configuration

```json
"TokenOptions": {
  "Issuer": "CafeAPI",
  "Audience": "CafeUsers",
  "SecurityKey": "supersecurekey123456"
}
```

---

## 🙌 Contributions

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

---

## ✉ Contact

Project developed by **[Your Name or Team]**

- GitHub: [github.com/yourusername](https://github.com/yourusername)
- Email: [your@email.com](mailto:your@email.com)

---

## ❤️ License

MIT License

> "Crafted with clean code and coffee."
