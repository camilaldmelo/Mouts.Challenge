# Mouts Challenge - Sales API

## Overview

This project is a Sales Management API developed using **Clean Architecture**, **DDD (Domain-Driven Design)**, **CQRS**, and **MediatR**.

The API allows the creation, querying, updating, and cancellation of sales while enforcing business rules related to discounts and item limits.

---

# Technologies

## Backend

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core 8
* MediatR
* FluentValidation
* Swagger / OpenAPI

## Database

* MySQL 8+
* Pomelo.EntityFrameworkCore.MySql

## Architectural Patterns

* Clean Architecture
* Domain-Driven Design (DDD)
* CQRS
* Mediator Pattern
* Repository Pattern
* Unit of Work Pattern
* External Identity Pattern
* Domain Events

---

# Project Structure

```text
src
│
├── Mouts.Challenge.Api
│
├── Mouts.Challenge.Application
│
├── Mouts.Challenge.Domain
│
└── Mouts.Challenge.Infrastructure
```

## Layer Responsibilities

### API

Responsible for:

* HTTP endpoints
* Request/Response handling
* Swagger documentation

### Application

Responsible for:

* Commands
* Queries
* Handlers
* Validators
* Use Cases

### Domain

Responsible for:

* Entities
* Business Rules
* Domain Events

### Infrastructure

Responsible for:

* Entity Framework Core
* Database access
* Repository implementations
* Event publishing

---

# Business Rules

## Discounts

### Quantity below 4 items

No discount applied.

```text
1 - 4 items => 0%
```

### Quantity above 4 items

10% discount.

```text
5 - 9 items => 10%
```

### Quantity between 10 and 20 items

20% discount.

```text
10 - 20 items => 20%
```

### Quantity greater than 20 items

Not allowed.

```text
> 20 items => Exception
```

---

# Domain Events

The application publishes the following events:

* SaleCreated
* SaleModified
* SaleCancelled
* ItemCancelled

For this challenge, events are logged through the application logger.

---

# Database Configuration

## Create Database

Connect to MySQL:

```sql
CREATE DATABASE developerstore;
```

---

## Connection String

Configure the file:

```text
appsettings.json
```

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=developerstore;user=root;password=123456"
  }
}
```

---

# Running Migrations

## Create Migration

Run from the solution root:

```powershell
dotnet ef migrations add InitialCreate `
--project ".\Mouts.Challenge.Infrastructure\Mouts.Challenge.Infrastructure.csproj" `
--startup-project ".\Mouts.Challenge\Mouts.Challenge.Api.csproj"
```

## Apply Migration

```powershell
dotnet ef database update `
--project ".\Mouts.Challenge.Infrastructure\Mouts.Challenge.Infrastructure.csproj" `
--startup-project ".\Mouts.Challenge\Mouts.Challenge.Api.csproj"
```

---

# Running the API

Restore packages:

```bash
dotnet restore
```

Build solution:

```bash
dotnet build
```

Run API:

```bash
dotnet run --project .\Mouts.Challenge
```

---

# Swagger

When the application starts, Swagger is available at:

```text
https://localhost:{port}/swagger
```

---

# Endpoints

## Create Sale

```http
POST /api/sales
```

### Request

```json
{
  "saleNumber": "SALE-001",
  "saleDate": "2026-06-09T15:00:00",
  "customerId": 1,
  "customerName": "John Doe",
  "branchId": 1,
  "branchName": "Main Branch",
  "items": [
    {
      "productId": 1,
      "productName": "Notebook",
      "quantity": 5,
      "unitPrice": 1000
    }
  ]
}
```

---

## Get Sale By Id

```http
GET /api/sales/{id}
```

---

## Get All Sales

```http
GET /api/sales
```

---

## Update Sale

```http
PUT /api/sales/{id}
```

---

## Cancel Sale

```http
DELETE /api/sales/{id}
```

---

## Cancel Item

```http
PATCH /api/sales/{saleId}/items/{itemId}/cancel
```

---

# Validation

FluentValidation is used through MediatR Pipeline Behaviors.

Validation is executed before handlers are called.

Examples:

* Invalid customer
* Invalid product
* Quantity greater than 20
* Empty sale items

---

# Architectural Decisions

## External Identity Pattern

Customer, Branch and Product belong to external contexts.

Therefore, Sales stores only:

```text
CustomerId
CustomerName

BranchId
BranchName

ProductId
ProductName
```

This avoids direct coupling between bounded contexts.

---

# Future Improvements

* Authentication and Authorization
* Pagination
* Filtering
* Event Bus Integration
* Unit Tests
* Integration Tests
* Docker Support
* CI/CD Pipeline

---

# Author

Developed as part of the DeveloperStore technical challenge.
