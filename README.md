# Online Food Delivery System

An ASP.NET Core Web API for an online food delivery platform, designed from UML diagrams for a university Software Design assignment. The project demonstrates layered architecture, RESTful API design, Entity Framework Core persistence, JWT authentication, repository/service patterns, and a payment strategy implementation.

## Table of Contents

- [Overview](#overview)
- [Technology Stack](#technology-stack)
- [Architecture](#architecture)
- [Design Patterns](#design-patterns)
- [Project Structure](#project-structure)
- [Main Features](#main-features)
- [Database Design](#database-design)
- [Getting Started](#getting-started)
- [Configuration and Secrets](#configuration-and-secrets)
- [Entity Framework Core Migrations](#entity-framework-core-migrations)
- [Running the API](#running-the-api)
- [Swagger Testing Guide](#swagger-testing-guide)
- [Docker](#docker)
- [UML Diagrams](#uml-diagrams)
- [Git Workflow](#git-workflow)
- [Notes](#notes)

## Overview

The system models the main workflows of a food delivery application:

- Customers can browse restaurants, view menus, add items to cart, place orders, make payments, track orders, cancel orders, and review restaurants.
- Restaurant owners can manage restaurants and menu items.
- Drivers can view and accept delivery tasks, then update delivery status.
- Admins can manage users and view system reports.
- The system sends notifications for important order and delivery events.

The implementation is intentionally structured as a clean Web API backend so it can be extended into a full production-style application later.

## Technology Stack

- ASP.NET Core Web API
- C# / .NET 9
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication
- Swagger / OpenAPI
- Dependency Injection
- Docker and Docker Compose
- PlantUML for design diagrams

## Architecture

The solution follows a 3-layered architecture:

```text
Client / Swagger / Future Frontend
        |
        v
PresentationLayer
        |
        v
BusinessLayer
        |
        v
DataAccessLayer
        |
        v
SQL Server
```

### Presentation Layer

Responsible for HTTP API endpoints and request handling.

- Controllers
- Swagger/OpenAPI setup
- JWT authentication middleware
- Request/response routing

### Business Layer

Responsible for application logic and use-case orchestration.

- Service interfaces
- Service implementations
- DTOs
- Validation rules
- Payment strategy selection
- Notification workflow logic

### Data Access Layer

Responsible for persistence and database access.

- Entity Framework Core `DbContext`
- Entity classes
- Repository interfaces
- Repository implementations
- Database migrations

## Design Patterns

### Repository Pattern

Repositories isolate data access from business logic.

Examples:

- `IUserRepository`
- `IRestaurantRepository`
- `IOrderRepository`
- `IPaymentRepository`

### Service Layer Pattern

Services coordinate business operations and keep controllers thin.

Examples:

- `AuthService`
- `CartService`
- `OrderService`
- `PaymentService`
- `DeliveryService`
- `AdminService`

### Strategy Pattern

Payment behavior is implemented through interchangeable strategies.

Supported payment methods:

- `Momo`
- `ZaloPay`
- `CreditCard`

The `PaymentService` selects the correct `IPaymentStrategy` at runtime based on the request payment method.

## Project Structure

```text
OnlineFoodDeliverySystem
├── PresentationLayer
│   ├── Controllers
│   │   ├── AdminController.cs
│   │   ├── AuthController.cs
│   │   ├── CartController.cs
│   │   ├── DeliveriesController.cs
│   │   ├── MenuController.cs
│   │   ├── NotificationsController.cs
│   │   ├── OrdersController.cs
│   │   ├── PaymentsController.cs
│   │   ├── RestaurantsController.cs
│   │   └── ReviewsController.cs
│   ├── Models
│   ├── Program.cs
│   └── appsettings.json
├── BusinessLayer
│   ├── DTOs
│   ├── Interfaces
│   ├── PaymentStrategies
│   ├── Services
│   ├── Validators
│   └── DependencyInjection.cs
├── DataAccessLayer
│   ├── Entities
│   ├── Migrations
│   ├── Repositories
│   ├── AppDbContext.cs
│   └── DependencyInjection.cs
├── docs
│   └── diagrams
├── Dockerfile
├── docker-compose.yml
├── OnlineFoodDeliverySystem.sln
└── README.md
```

## Main Features

- User registration and login
- JWT-based authentication
- Role-based authorization for Customer, Restaurant, Driver, and Admin users
- Restaurant browsing
- Menu item management
- Cart management
- Order placement and tracking
- Payment processing through strategy pattern
- Delivery task management
- Notification service
- Restaurant review service
- Admin reporting endpoints
- SQL Server database integration
- Dockerized deployment option

## Database Design

Core entities:

- `User`
- `Customer`
- `RestaurantOwner`
- `Driver`
- `Admin`
- `Restaurant`
- `MenuItem`
- `Cart`
- `CartItem`
- `Order`
- `OrderItem`
- `Payment`
- `Delivery`
- `Notification`
- `Review`

Entity Framework Core uses table-per-hierarchy inheritance for user types. SQL Server cascade delete is disabled through `DeleteBehavior.NoAction` to avoid multiple cascade path issues.

## Getting Started

### Prerequisites

Install:

- .NET 9 SDK
- SQL Server or SQL Server Express
- SQL Server Management Studio
- Git
- Docker Desktop, optional

Verify .NET:

```bash
dotnet --version
```

### Clone the Repository

```bash
git clone https://github.com/qbao0111/OnlineFoodDeliverySystem.git
cd OnlineFoodDeliverySystem
```

### Restore Dependencies

```bash
dotnet restore
```

## Configuration and Secrets

Do not commit real database passwords, JWT secrets, or local machine configuration.

The public `PresentationLayer/appsettings.json` should contain safe development defaults only. For real local settings, use ASP.NET Core User Secrets.

Initialize or update secrets:

```bash
dotnet user-secrets init --project PresentationLayer
```

Set a local SQL Server connection string:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost\\SQLEXPRESS;Database=OnlineFoodDeliveryDb;Trusted_Connection=True;TrustServerCertificate=True" --project PresentationLayer
```

For SQL Server Authentication:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=OnlineFoodDeliveryDb;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True" --project PresentationLayer
```

Set a JWT key:

```bash
dotnet user-secrets set "Jwt:Key" "your-local-development-secret-key-at-least-32-characters" --project PresentationLayer
```

List configured secrets:

```bash
dotnet user-secrets list --project PresentationLayer
```

## Entity Framework Core Migrations

Install or update the EF Core CLI:

```bash
dotnet tool install --global dotnet-ef
```

If already installed:

```bash
dotnet tool update --global dotnet-ef
```

Apply existing migrations:

```bash
dotnet ef database update --project DataAccessLayer --startup-project PresentationLayer
```

Create a new migration after model changes:

```bash
dotnet ef migrations add MigrationName --project DataAccessLayer --startup-project PresentationLayer
```

The database will be created as:

```text
OnlineFoodDeliveryDb
```

## Running the API

From the repository root:

```bash
dotnet run --project PresentationLayer
```

Or run `PresentationLayer` directly from Visual Studio.

Swagger UI is available at the URL printed by the application, for example:

```text
http://localhost:5047
```

If DLL files are locked during build, stop the running API process:

```powershell
Get-Process PresentationLayer -ErrorAction SilentlyContinue | Stop-Process -Force
```

## Swagger Testing Guide

### Demo Accounts

All seeded demo accounts use the password:

```text
demo
```

| Role | Email |
| --- | --- |
| Customer | `customer@example.com` |
| Restaurant Owner | `owner@example.com` |
| Driver | `driver@example.com` |
| Admin | `admin@example.com` |

### Login

Use:

```http
POST /api/Auth/login
```

Example body:

```json
{
  "email": "admin@example.com",
  "password": "demo"
}
```

Copy the returned JWT token.

In Swagger, click `Authorize` and paste only the token value. Do not manually add `Bearer`; Swagger adds it automatically.

### Recommended Test Flow

1. Browse restaurants:

```http
GET /api/Restaurants
```

2. View a restaurant menu:

```http
GET /api/restaurants/1/menu
```

3. Login as customer and add an item to cart:

```http
POST /api/Cart/items
```

```json
{
  "customerId": 1,
  "menuItemId": 1,
  "quantity": 2
}
```

4. View cart:

```http
GET /api/Cart/1
```

5. Place an order:

```http
POST /api/Orders
```

```json
{
  "customerId": 1,
  "paymentMethod": "Momo"
}
```

Valid payment methods:

```text
Momo
ZaloPay
CreditCard
```

6. Track order:

```http
GET /api/Orders/{orderId}/track
```

7. Login as admin and view reports:

```http
GET /api/Admin/users
GET /api/Admin/reports
```

## Docker

Run the API and SQL Server using Docker Compose:

```bash
docker compose up --build
```

The API will be exposed at:

```text
http://localhost:8080
```

SQL Server will be exposed at:

```text
localhost,1433
```

## UML Diagrams

PlantUML source files are stored in:

```text
docs/diagrams
```

Included diagrams:

- `UseCaseDiagram.plantuml`
- `ClassDiagram.plantuml`
- `SequenceDiagram.plantuml`
- `PackageDiagram.plantuml`

These diagrams describe the system actors, main use cases, classes, package dependencies, and the customer order sequence.

## Git Workflow

Initial repository setup:

```bash
git init
git add .
git commit -m "Initial commit"
git branch -M main
git remote add origin https://github.com/qbao0111/OnlineFoodDeliverySystem.git
git push -u origin main
```

Daily workflow:

```bash
git status
git add .
git commit -m "Describe your change"
git push
```

Avoid committing local secrets:

```bash
git status
git diff PresentationLayer/appsettings.json
```

## Notes

- This project is built for academic demonstration and can be extended with ASP.NET Core Identity, refresh tokens, real payment gateways, SignalR order tracking, and automated tests.
- Demo password hashing uses SHA256 for simplicity. A production system should use ASP.NET Core Identity or a strong password hasher with salt.
- JWT issuer and audience validation are relaxed for local testing. Production deployments should validate issuer, audience, signing key, and token lifetime strictly.
