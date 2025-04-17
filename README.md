# Basic Vacatures API

A lean and flexible asp.net REST API which provides "vacatures" as JSON objects and offers CRUD operations. 
These vacatures can be requested by an HTTP client or front-end. The project includes an http file which you can use to test all endpoints.


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
validator, handler and http endpoint**.

The solution implements a basic **CQRS** pattern with a custom dispatcher similar to MediatR, where a feature can pass a command (mutating data) or 
a query (just fetching data). The handlers are responsible for the actual logic, while the endpoint controllers are kept thin.

Requests are validated using **FluentValidation** and endpoint results are rate limited and **paginated** and 
use **response cache** where multiple can be fetched.

The management (admin) endpoints are protected by role-based **Identity auth** using **JWT tokens**. To register API users and login,
separate endpoints are provided.

If you prefer, the solution can be shaped into Onion/clean architecture. 

For an additional read on Vertical Slice Architecture, [check this post](https://www.jimmybogard.com/vertical-slice-architecture/) or [this article with another example](https://code-maze.com/vertical-slice-architecture-aspnet-core/).


## Packages and middleware

* [Serilog](https://serilog.net/) for logging,
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

### 1. Set up the database

The database is configured in the `appsettings.json` file. For development, I used a local SQL Server Express. 

_Alternatively, you can also use a Linux SQL Server Docker container instead of a local instance:_

1. Pull the latest SQL Server image
```
sudo docker pull mcr.microsoft.com/mssql/server:2022-latest
```
2. Run the SQL Server container with your password
```

sudo docker run -e ‘ACCEPT_EULA=Y’ -e ‘MSSQL_SA_PASSWORD=YourStrongPassword123!’
-p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
```

### 2. Configure the connection string
The connection string is configured in the `appsettings.json` file. Change this to use your own password and database name

If you use the Linux docker image for SQL server, keep in mind that the `Integrated Security` option might not be supported.
In that case, use your specified db user and password in the connection string.


### 3. Create initial database using migrations

Once you configured the connection string, you should create the database by running the initial migration:

```
dotnet ef database update --project VacaturesApi
```
That's it for migrations. The database will be populated with some sample data on first run of the application.

### 4. Running the application

* You can run the program from your IDE as usual, or from the command line: `dotnet run`.
* Swagger is enabled and will be available at http://localhost:5001/swagger, but you can also use the api.http file or use a tool like Postman to test the API.
* All endpoints have working examples in the api.http file.
* Create, Update and Delete end points are protected, and require a valid JWT token which can be obtained by creating an API user and logging in.


### 5. Registering a new user

#### First register a new user to the API using any HTTP request runner such as Postman (see api.http):

Run the following request to register a new user with `Contributor` level API access. 
You can use any email and password. Be sure to remember the credentials for the next step.

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

The user  is now created in the Identity Db. You can now log in with these credentials. 
In the next step, you will be able to get a JWT token using these credentials.

### 6. Accessing protected endpoints using JWT token

**By logging in, you will be returned a JWT token which can be used to make authorized requests to the API:**

Log in using the credentials you just created _(replace the email and password with your own)_:

```
POST https://localhost:5001/api/auth/login
Content-Type: application/json

{
  "email": "testuser@vacatures.api",
  "password": "Password123!"
}
```

**Use the returned token in the Authorization header for your subsequent requests to make authorized requests to the API.**

### 7. Configure JWT token secret key

In the `appsettings.json` file, be sure to change the JWT token secret key to a secure key of your own. 

The secret key is used to sign the JWT token and should be kept secret. If you want to open source your project, 

### Optional: Disable protected endpoints

If you want to disable auth security for an endpoint, you can remove the `[Authorize(Roles = "Contributor")]` attribute from the endpoint. 

Keep in mind that by removing this attribute, you make the endpoint accessible to all users.


### Logs and troubleshooting

The logs are available in the `Logs` folder, which you can find in the root folder of the solution.
Output from the application is written to the console.


## Extending the application

This API is a good starting point for working with CQRS and vertical slice architecture using modern best practices. 
I tried to keep it simple and easy to adapt, so you can focus on adding the features you need.


## External libraries

Earlier versions of this project used the MediatR library, but I decided to implement a custom dispatcher to keep the solution free of licensing costs.