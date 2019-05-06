# Firefly

This repository contains a solution to the Firefly coding challenge. 

It consists of ASP.NET Core based API as well as its corresponding unit and integration tests. 
Both testing projects use xUnit framework.

## How to test

Prerequisites
- Docker for Windows set to Linux containers
- .NET Core 2.2 SDK

### Start application

In the root folder, open command line and type

`docker-compose up --build`

The application should be accessible from

`http://localhost:5000`

If you prefer to run the application directly from Visual Studio, you will then have to spin up your Docker container using docker-compose-deps.yml file.
Here's the command:

`docker-compose -f docker-compose-deps.yml up --build`

When application is ran from VS it is similarly accessible through `http://localhost:5000` by default.

### Manual tests

Go to `http://localhost:5000/swagger/` in your browser and use the Swagger UI to make a request.

### Automatic tests

Either type `dotnet test` in the root folder or use test runner within Visual Studio. 

### Troubleshooting

Though unlikely, it is possible that the initialization of database schema might turn out to be flaky if the MSSQL server does not boot up in time (see **Potential improvements** section).
If that happens, please go to `.\docker-setup\mssql\mssql-init.sh` script and increase `sleep` time.

## Potential improvements
- Complete unit test coverage (though absolutely neccessary in production, I included only a couple of tests because of time constraints as the `IntegrationTests` project hopefully covers all the client facing scenarios) 
- Configurable password hashing iterations
- Iterate through migration files when spinning up docker container
- More sophisticated MSSQL schema initialization instead of sleep time in docker file.
- Make database schema initialization script idempotent (not to try to create it again if it exists).
- Stored procedures (f.e. InsertIfNotExists) to reduce roundtrips / improve performance
- Retry policy for sql queries
- Utilize OneOf nuget package for more object oriented MediatR handler responses
- Authorisation middleware for accessing restricted service endpoints based on a session mechanism
- Error middleware to return custom error responses instead of ASP.NET Core default InternalServerError response
- CMake script for spinning up the application, running the tests automatically and tearing down
- Reduce logging noise
- Postman collection

