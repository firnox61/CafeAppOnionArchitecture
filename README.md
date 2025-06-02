# ☕ Cafe Management System - Onion Architecture (.NET 8)

A modular and scalable **Cafe Management System** built with **Onion Architecture** using ASP.NET Core 8, Entity Framework Core, AutoMapper, FluentValidation, JWT, and Autofac.

---

## 📐 Architecture Overview

The project follows **Onion Architecture**, which promotes a separation of concerns and allows better testability, scalability, and maintainability.


---

## 📦 Project Structure

### ✅ Cafe.Domain
- Contains `Entities` like `Product`, `Order`, `User`, `Table`, etc.
- Common interfaces: `IEntity`, etc.
- No dependencies to other layers.

### ✅ Cafe.Application
- Business logic (`Service` classes, `Interfaces`)
- `DTOs` for communication between layers
- `AutoMapper` profiles
- `BusinessRules`, `ValidationTool`, `Results` (IResult, IDataResult, etc.)

### ✅ Cafe.Infrastructure
- Implements interfaces from `Application` layer
- `EF Core` data access via `Repositories/EntityFramework`
- `Security`: `JwtHelper`, `HashingHelper`
- `Caching`: `ICacheManager`, `MemoryCacheManager`
- `Aspects`: Logging, Caching, Validation, Performance, Transaction

### ✅ Cafe.WebAPI
- API endpoints (`Controllers`)
- `ExceptionMiddleware` for global error handling
- `DependencyInjection` setup (Autofac + IServiceCollection)
- Swagger configuration
- JWT Authentication setup
- Static file serving from `wwwroot/`

---

## 🔧 Technologies Used

- [.NET 8](https://dotnet.microsoft.com/)
- **Entity Framework Core** (Code First)
- **AutoMapper** (Object-to-Object Mapping)
- **FluentValidation** (Input validation)
- **Autofac** (Dependency Injection + AOP)
- **JWT Authentication**
- **Swagger / Swashbuckle**
- **ClosedXML** (Excel export)
- **Onion Architecture**
- **SOLID & Clean Code principles**

---

## 🚀 Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server or LocalDB
- Visual Studio 2022+ or Rider

---


