# ECommerce Authentication API

## Overview
The **ECommerce Authentication API** is a clean-architecture microservice that centralizes user registration and login for an e-commerce platform. The service is built on ASP.NET Core 8, persists data in PostgreSQL through Dapper, and layers business logic into dedicated Core and Infrastructure libraries. AutoMapper keeps data transfer objects isolated from database entities, FluentValidation enforces request integrity, and a custom exception-handling middleware produces consistent error responses. Swagger UI is enabled for rapid exploration of the HTTP endpoints.

## Solution structure
```
ECommerce.API.sln
├── ECommerce.API             # ASP.NET Core Web API (presentation layer)
├── ECommerce.Core            # Business logic, DTOs, validators, AutoMapper profiles
└── ECommerce.Infrastructure  # Data access with Dapper and PostgreSQL
```

## Technology stack
- .NET 8 / ASP.NET Core Web API
- PostgreSQL
- Dapper micro-ORM
- AutoMapper
- FluentValidation
- Dependency Injection (Microsoft.Extensions.DependencyInjection)
- Custom exception-handling middleware
- Swagger (Swashbuckle)
- Docker support via multi-stage build

## Architecture highlights
- **Presentation layer (`ECommerce.API`)** hosts controllers, middleware, and cross-cutting concerns such as Swagger and CORS.
- **Core layer** encapsulates domain entities, DTOs, validators, mapping profiles, and service contracts/implementations. `UsersService` coordinates registration and login flows.
- **Infrastructure layer** contains Dapper repositories and the `DapperDbContext` responsible for opening PostgreSQL connections.
- Dependency injection extension methods (`AddCore`, `AddInfrastructure`) wire up repositories, services, validators, AutoMapper, and middleware inside `Program.cs`.
- Requests flow from controller → service → repository → PostgreSQL, with DTO/entity conversions handled by AutoMapper profiles.

## Prerequisites
1. [.NET 8 SDK](https://dotnet.microsoft.com/download)
2. PostgreSQL 13+ (local instance or managed service)
3. Optional: Docker if you prefer containerized execution

## Database setup
1. Create a PostgreSQL database (default name `ECommerceUsers` in the sample settings).
2. Provision the `Users` table that the repository expects:

   ```sql
   CREATE TABLE IF NOT EXISTS public."Users" (
       "UserID"    UUID        PRIMARY KEY,
       "Email"     VARCHAR(50) NOT NULL UNIQUE,
       "PersonName" VARCHAR(50) NOT NULL,
       "Gender"    VARCHAR(20) NOT NULL,
       "Password"  VARCHAR(50) NOT NULL
   );
   ```

3. Supply a connection string named **`PostgreSQL`** via configuration. The default `appsettings.json` uses `PostgreSql`; either rename that key or set an environment variable such as:

   ```bash
   export ConnectionStrings__PostgreSQL="Host=localhost;Port=5433;Database=ECommerceUsers;Username=postgres;Password=admin"
   ```

   > ℹ️ Dapper opens the connection during application startup; ensure the database is reachable before launching the API.

## Running locally
```bash
# Restore packages
dotnet restore ECommerceAuth.API/ECommerce.API.sln

# Run the web API (listens on https://localhost:5001 and http://localhost:5000 by default)
dotnet run --project ECommerceAuth.API/ECommerce.API/ECommerce.API.csproj
```

Navigate to `https://localhost:5001/swagger` (or the HTTP equivalent) to explore the Swagger UI. CORS is pre-configured to allow requests from `http://localhost:4200` for an Angular front-end.

## Docker workflow
```bash
# From the repository root
cd ECommerceAuth.API

# Build the container image
docker build -t ecommerce-auth-api -f ECommerce.API/Dockerfile .

# Run the container (exposes ports 8080/8081 from the Dockerfile)
docker run --rm -p 8080:8080 -p 8081:8081 \
  -e ConnectionStrings__PostgreSQL="Host=host.docker.internal;Port=5433;Database=ECommerceUsers;Username=postgres;Password=admin" \
  ecommerce-auth-api
```

Adjust the connection string to match your environment. When running inside Docker, reference `host.docker.internal` (or the appropriate hostname) to reach a database on the host machine.

## API endpoints
| Method | Route                   | Description                | Request body | Successful response |
|--------|------------------------|----------------------------|--------------|---------------------|
| POST   | `/api/auth/register`   | Registers a new user       | `RegisterRequestDTO` | `AuthenticationResponse` with `Success=true` and placeholder JWT token |
| POST   | `/api/auth/login`      | Authenticates an existing user | `LoginRequestDTO` | `AuthenticationResponse` if credentials are valid |

### Request DTOs
```json
// RegisterRequestDTO
{
  "email": "customer@example.com",
  "password": "P@ssword1",
  "personName": "Ada Lovelace",
  "gender": "Female"
}

// LoginRequestDTO
{
  "email": "customer@example.com",
  "password": "P@ssword1"
}
```

### Successful response shape
```json
{
  "userID": "7e828d09-3a7f-4424-8c32-4863c5c35b8b",
  "email": "customer@example.com",
  "personName": "Ada Lovelace",
  "gender": "Female",
  "token": "token",
  "success": true
}
```

> ⚠️ `UsersService` currently returns a placeholder string for `token`. Integrate your preferred JWT/identity provider before production use.

## Validation & error handling
- FluentValidation enforces constraints (non-empty email/password, valid email format, enum validation for gender, etc.). Invalid models automatically result in 400 responses with validation details.
- The custom `ExceptionHandlingMiddleware` logs unhandled exceptions and responds with HTTP 500 plus a JSON payload containing the exception type and message. Repository/service methods throw tailored exceptions (`UnauthorizedAccessException`, `ApplicationException`) that bubble through this middleware.

Example error response:
```json
{
  "message": "User not found",
  "type": "System.Exception"
}
```

## Mapping & business logic
- AutoMapper profiles in `ECommerce.Core.MappingProfiles` convert between `ApplicationUser` entities and `AuthenticationResponse` DTOs while allowing null collections and destination values.
- `UsersService` orchestrates registration and login, delegating persistence to `IUserRepository`. Both flows attach `Success = true` and the placeholder token to the mapped response.

## Extensibility notes
- Replace the placeholder token generation with a proper JWT or identity provider integration.
- Hash and salt passwords before persisting them; the current implementation stores plaintext credentials.
- Introduce unit/integration tests to cover service and repository behavior.
- Consider migrating to Entity Framework Core or adding migrations/seeding scripts if richer data access patterns are required.

## Troubleshooting
- **Connection string errors:** Ensure the configuration key is spelled `PostgreSQL` to align with `DapperDbContext`.
- **Database connectivity:** Confirm the `Users` table exists and the PostgreSQL server is reachable at application startup.
- **CORS issues:** Update the allowed origins in `Program.cs` if your UI is hosted on a different domain.

## License
This project does not currently include an explicit license. Add one before distributing binaries or hosting the service publicly.
