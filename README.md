# Basic Vacatures API

This repository contains a minimalistic asp.net REST API which exposed vacatures. These vacatures can be requested by any allowed client or front-end.



## Description

The API offers simple **CRUD** operations on the vacatures, so that vacatures can be created, updated or deleted as well. A management client using these end points is not included in the solution, but the end points work and can be addressed.

The API works with an **external SQL database**, which can be changed in the appsettings.json file.

This application builds into a **Docker container**, which can be deployed wherever you'd like.


## Architecture

Because this is a minimalistic application, the architecture is not very complex. I have taken into account the following requirements:

* The API should be **fast and light**;
* The API serves a **well-defined domain** model (vacatures);
* The solution should be **easy to adapt and extend**;
* The solution should follow **modern best practises**;

Because of the above, it makes sense to follow a **Vertical Slice Architecture** pattern. This way we can iterate fast, focus on features and expand the domain with minimal refactoring.

This flavor of the VSA pattern has  features in self-contained folders, with each feature having their own **command/query, DTO and endpoint**.

The solution implements a basic **CQRS** pattern, where a feature can send a command (mutating data) or a query (just fetching data). Separating the two makes the solution ready for more advanced techniques such as separate read/write databases.

If needed, you could move the domain, data and infrastructure into separate layers/projects. The solution can be shaped into Onion/clean architecture, but for the solution as-is that might be unnecessary.

For an additional read on Vertical Slice Architecture, [check this post](https://www.jimmybogard.com/vertical-slice-architecture/) or [this article with an example](https://code-maze.com/vertical-slice-architecture-aspnet-core/).

## Getting Started

### Dependencies

* SeriLog for logging (overrides the default logger);
* MediatR for internal signaling and CQRS.

### Executing program

* You can run the program in your IDE or from the command line.
* Swagger is not enabled, but you can use the api.http file or use Postman to test the API.

### Docker
1. Build the image;
2. Run the image