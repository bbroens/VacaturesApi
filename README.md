# Basic Vacatures API

A lean but flexible asp.net REST API which provides vacatures as JSON objects and offers CRUD operations. 
These vacatures can be requested by an HTTP client or front-end. The project includes a http file you can use to test all endpoints.


## Description

The API offers simple, protected CRUD operations on the vacatures, so that vacatures can be fetched, listed, created, 
updated or deleted. A front-end client using these end points is not included in the solution, 
but the end points are fully functional and can be addressed with **Postman** or any HTTP request tool.

The API works with an **external SQL database**, which can be configured in the `appsettings.json` file.
This application builds locally or into a **Docker container**, which can be deployed wherever you'd like.


## Vertical Slice Architecture

The architecture is designed to be simple and easy to understand, based on following requirements:

* The API should be **fast and lightweight**;
* The API should be **easy to adapt and extend**;
* The solution should follow **modern best practices**;

Because of the above, it makes sense to implement a **Vertical Slice Architecture**. 
.This way we can iterate fast, focus on features and expand the domain with minimal jumps across layers. 
All while still keeping responsibilities neatly separated.

My VSA architecture has features in self-contained folders, with each feature having their own **command/query, 
validator, handler andhttp endpoint**.

The solution implements a basic **CQRS** pattern using MediatR, where a feature can pass a command (mutating data) or 
a query (just fetching data). The handlers are responsible for the actual logic, while the endpoint controllers are kept thin.

Requests are validated using **FluentValidation** and endpoint results are rate limited and **paginated** and 
use **response cache** where multiple can be fetched.

The management (admin) endpoints are protected by role-based **Identity auth** using **JWT tokens**. To register API users and login,
separate endpoints are provided.

If you prefer, the solution can be shaped into Onion/clean architecture. 

For an additional read on Vertical Slice Architecture, [check this post](https://www.jimmybogard.com/vertical-slice-architecture/) or [this article with another example](https://code-maze.com/vertical-slice-architecture-aspnet-core/).


## Packages and middleware

* [Serilog](https://serilog.net/) for logging,
* [MediatR](https://github.com/jbogard/MediatR) for internal signaling and CQRS,
* [Swagger](https://swagger.io/) for API documentation,
* [Docker](https://www.docker.com/) for containerization,
* [EF Core](https://docs.microsoft.com/en-us/ef/core/) for the database,
* [Automapper](https://automapper.org/) for mapping simple DTOs and entities,
* [FluentValidation](https://fluentvalidation.net/) for validation,
* [Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity/) for role-based auth using JWT,
* Custom GlobalExceptionHandler middleware catching and logging exceptions,
* Global rate limiter middleware limiting requests on expensive endpoints.
* Global response caching middleware caching responses on expensive endpoints.


## Getting Started

### Setting up the database

The database is configured in the `appsettings.json` file. For development I used a local SQL Server Express.

Once you configured the connection string, you should create the database by running the initial migration:

```
dotnet ef database update --project VacaturesApi
```
That's it for migrations. The database will be populated with some sample data on first run.

### Executing program and accessing the endpoints

* You can run the program in your IDE or from the command line. For example: `dotnet run`.
* Swagger is enabled and will be available at http://localhost:5001/swagger, but you can also use the api.http file or use Postman to test the API.
* All endpoints have working examples in the api.http file.
* Create, Update and Delete requires a valid JWT token, which can be obtained by creating an API user and logging in:

#### Using Postman, your IDE or any HTTP request runner:

```
POST https://localhost:5001/api/auth/register
Content-Type: application/json

{
  "email": "testuser@vacatures.api",
  "password": "Password123!",
  "firstName": "Test",
  "lastName": "User",
  "roles": ["Contributor"]
}
```

The user is now created in the Identity Db. You can now log in with the same credentials and obtain a JWT token.
**This returned JWT token can then be used to make authorized requests to the API.**

```
POST https://localhost:5001/api/auth/login
Content-Type: application/json

{
  "email": "testuser@vacatures.api",
  "password": "Password123!"
}
```

Use the returned token in the Authorization header to make authorized requests to the API.

If you want to disable auth for a quick test, you can remove the `[Authorize(Roles = "Contributor")]` attribute from the endpoints.


### Logs and troubleshooting

The logs are available in the `Logs` folder, which you can find in the root folder of the solution.
Output from the application is written to the console.


## Extending the application

This API is a good starting point for working with CQRS and vertical slice architecture using modern best practices. 
I tried to keep it simple and easy to adapt, so you can focus on adding the features you need.