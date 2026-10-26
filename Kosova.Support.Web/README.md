# Kosova.Support

Kosova.Support is a **prototype / demonstration website** for Kosovo public-service digitization built with ASP.NET Core MVC.

## Features
- ASP.NET Core Identity authentication (registration, login, email confirmation-ready, password reset, optional 2FA support)
- Role-based access control (Citizen, Business, Visitor, ServiceAdmin, Admin)
- Multilingual support (Albanian default, English, Serbian)
- Public services catalog and search
- Online applications with draft/save/submit workflow
- Document uploads with file validation and scanning integration placeholder
- Appointment booking and cancellation
- Payment sandbox provider abstraction and receipt download
- Citizen dashboard and admin dashboard
- Emergency alerts, news, events, FAQ, support tickets, and system status
- Audit logs for key state changes
- Swagger/OpenAPI endpoint for status API
- Rate limiting for public forms

## Tech stack
- .NET 10, ASP.NET Core MVC + Razor Pages
- Entity Framework Core + SQL Server
- ASP.NET Core Identity
- Bootstrap 5

## Local setup
1. Copy `../.env.example` values into your local environment variables.
2. Configure `ConnectionStrings__DefaultConnection` for SQL Server.
3. Run migrations and start the app:

```bash
cd Kosova.Support.Web
dotnet ef database update
dotnet run
```

4. Open `https://localhost:xxxx`.

### Seeded admin account (demo)
- Email: `admin@kosova.support.demo`
- Password: `Admin123!Demo`

Change immediately for any non-demo environment.

## Docker
From repository root:

```bash
docker compose --env-file .env.example up --build
```

App: `http://localhost:8080`
Swagger: `http://localhost:8080/swagger`

## Security and configuration notes
- Never commit real secrets. Use environment variables.
- Payment integration is sandbox-only and not connected to live providers.
- Replace placeholder email and payment settings before production.
- This site uses fictional/demo data and is not an official government portal.
