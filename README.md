# Base Architecture

This repository contains a .NET 10 based N-Tier Architecture template designed for enterprise-level applications. It implements Aspect Oriented Programming (AOP), Repository Pattern, Unit of Work, and SOLID principles to provide a scalable and maintainable foundation for your projects.

## Requirements

- .NET 10 SDK
- PostgreSQL 18+
- Visual Studio 2022+ or VS Code

## Features

- **N-Tier Architecture** - Clean Architecture principles with separated layers
- **JWT Authentication** - Token-based authentication system
- **Role-Based Authorization** - Method-level authorization with SecuredOperation aspect
- **AOP Aspects** - Validation, Caching, Logging, Performance monitoring, Transaction management
- **Audit Trail** - Automatic database and traffic logging with Log4Net
- **Soft Delete** - Safe deletion mechanism with global query filters
- **Health Checks** - System health monitoring endpoints
- **Response Compression** - Brotli and Gzip compression support

## Project Structure

```
src/
├── Libraries/
│   ├── Core/           # Core layer: Aspects, Utilities, Base Classes
│   ├── Business/       # Business layer: Services, Managers
│   ├── DataAccess/     # Data Access layer: DbContext, Migrations
│   └── Entities/       # Entity layer: Models, DTOs, Enums
└── Presentations/
    └── WebAPI/         # Presentation layer: Controllers, Middlewares
```

## Technologies

| Category | Technology | Version |
|----------|------------|---------|
| Framework | .NET | 10.0 |
| Database | PostgreSQL | 18+ |
| ORM | Entity Framework Core | 10.0.1 |
| IoC Container | Autofac | 10.0.0 |
| AOP | Castle.DynamicProxy | 7.1.0 |
| Validation | FluentValidation | 12.1.1 |
| Logging | Log4Net | 3.2.0 |
| JWT | System.IdentityModel.Tokens.Jwt | 8.15.0 |

## Installation

1. Clone the repository:
```bash
git clone https://github.com/cynegeirus/dotnet-base-architecture.git
cd dotnet-base-architecture
```

2. Configure database connection in `src/Libraries/Core/configurationSettings.json`:
```json
{
  "Databasing": {
    "PostgreSQL": "Server=127.0.0.1;Port=5432;Database=BaseArchitecture;User Id=postgres;Password=YourPassword;Pooling=true;"
  }
}
```

3. Configure JWT settings in `src/Presentations/Services/WebAPI/appsettings.json`:
```json
{
  "TokenOptions": {
    "Audience": "www.yourdomain.com",
    "Issuer": "www.yourdomain.com",
    "AccessTokenExpiration": 180,
    "SecurityKey": "YourSecureKeyAtLeast32Characters!"
  }
}
```

4. Apply database migrations:
```bash
cd src/Libraries/DataAccess
dotnet ef database update
```

5. Run the application:
```bash
cd ../../Presentations/Services/WebAPI
dotnet run
```

## AOP Aspects Usage

The project includes several aspects for cross-cutting concerns:

```csharp
[SecuredOperation("Admin")]           // Role-based authorization
[ValidationAspect(typeof(Validator))] // Model validation
[CacheAspect]                         // Method result caching
[LogAspect(typeof(FileLogger))]       // Method call logging
[PerformanceAspect(5)]                // Performance monitoring (logs if > 5 sec)
[TransactionScopeAspect]              // Transaction management
```

## License

This project is licensed under the [MIT License](LICENSE). See the license file for details.

## Issues, Feature Requests or Support

Please use the Issue > New Issue button to submit issues, feature requests or support issues directly to me. You can also send an e-mail to [akin.bicer@outlook.com.tr](mailto:akin.bicer@outlook.com.tr).
