# Online Food Delivery System

ASP.NET Core Web API template generated from the UML diagrams for a university Software Design assignment.

## Architecture

The solution follows a 3-layered architecture:

- `PresentationLayer`: Web API controllers, request/response endpoints, Swagger/OpenAPI, authentication setup.
- `BusinessLayer`: DTOs, service interfaces, service implementations, validation rules, payment strategy pattern.
- `DataAccessLayer`: Entity Framework Core DbContext, entities, repositories, SQL Server configuration.

## Main Features

- Customer registration and login with JWT token generation.
- Restaurant browsing and menu management.
- Cart operations and order placement.
- Payment strategy pattern for `Momo`, `ZaloPay`, and `CreditCard`.
- Delivery task assignment and delivery status updates.
- Notifications and reviews.
- Admin user/report endpoints.
- Docker and SQL Server support.

## Project Structure

```text
OnlineFoodDeliverySystem
├── PresentationLayer
│   ├── Controllers
│   ├── Models
│   ├── Program.cs
│   └── appsettings.json
├── BusinessLayer
│   ├── DTOs
│   ├── Interfaces
│   ├── PaymentStrategies
│   ├── Services
│   └── Validators
├── DataAccessLayer
│   ├── Entities
│   ├── Repositories
│   └── AppDbContext.cs
├── docs
│   └── diagrams
├── Dockerfile
├── docker-compose.yml
└── OnlineFoodDeliverySystem.sln
```

## Run Locally

```bash
dotnet restore
dotnet ef database update --project DataAccessLayer --startup-project PresentationLayer
dotnet run --project PresentationLayer
```

Open Swagger UI:

```text
https://localhost:7044
http://localhost:5136
```

Ports can differ depending on `PresentationLayer/Properties/launchSettings.json`.

## Run With Docker

```bash
docker compose up --build
```

The API will be available at:

```text
http://localhost:8080
```

## Demo Accounts

Seeded users use `demo` as the current seed password placeholder in the database model:

- `customer@example.com`
- `owner@example.com`
- `driver@example.com`
- `admin@example.com`

For production, replace the sample password hashing with ASP.NET Core Identity or a strong password hasher.

## Git Commands

```bash
git init
git add .
git commit -m "Initial commit"
git branch -M main
git remote add origin https://github.com/qbao0111/OnlineFoodDeliverySystem.git
git push -u origin main
```

## Notes

- PlantUML source files are stored under `docs/diagrams`.
- Create EF migrations with:

```bash
dotnet ef migrations add InitialCreate --project DataAccessLayer --startup-project PresentationLayer
```
