# BankApi

**BankApi** is a sample RESTful web API built with .NET 8, designed to demonstrate implementing an API that leverages Couchbase as the backend database. It provides endpoints for managing customers and their accounts.

## Features

- RESTful API with ASP.NET Core
- Couchbase as the data store
- Dependency injection with interfaces for easy testing and swapping implementations
- JSON serialization with type handling
- Serilog for logging
- Swagger for API documentation

## Technologies Used

- [.NET 8](https://dotnet.microsoft.com/en-us/)
- [Couchbase](https://www.couchbase.com/)
- [Serilog](https://serilog.net/)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)
- [Swagger (Swashbuckle)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

## Getting Started
Open a terminal and run:
docker compose /Couchbase/Docker-Compose/docker-compose.yaml
Open a browser and go to http://localhost:8091
Login with Username: Admin, Password: password

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Couchbase Server](https://www.couchbase.com/downloads)
- Docker Desktop/Rancher Desktop, for running Couchbase locally

### Configuration

Edit `Program.cs` or `appsettings.json` (if added) to configure the Couchbase connection:

```csharp
options.ConnectionString = "http://localhost";  // or container IP
options.UserName = "Admin";
options.Password = "password";
