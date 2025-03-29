# Job Candidate Hub API

## Overview

Job Candidate Hub API is a RESTful service designed to manage candidate information efficiently. The application follows Clean Architecture principles, ensuring maintainability, scalability, and testability. The solution is self-deploying, requiring no additional Visual Studio extensions to run.

## Features

- **REST API**: Fully functional API with CRUD operations for candidate management.
- **Clean Architecture**: Separation of concerns using Domain, Application, Infrastructure, and Web layers.
- **Generic Repository & Unit of Work**: Implements a structured and scalable approach to database access.
- **Caching Mechanism**: Uses **IMemoryCache** to improve performance by reducing database queries.
- **Unit Testing**: Includes well-structured unit tests for core functionalities.
- **Self-Deploying**: Can be run immediately upon opening the solution without additional dependencies.
- **Scalable & Maintainable**: Implements best practices to allow future improvements and enhancements.
- **Database Persistence**: Uses PostgreSQL for reliable data storage.
- **Exception Handling**: Uses built-in .NET logging and proper exception management.

## Technologies Used

- **.NET 8** (Web API)
- **Entity Framework Core** (ORM for database interactions)
- **PostgreSQL** (Relational database)
- **IMemoryCache** (Caching)
- **XUnit & Moq** (Unit Testing framework)
- **Swagger** (API documentation)

## Installation & Setup

### Prerequisites

- .NET 8 SDK installed
- PostgreSQL database running

### Steps to Run Locally

1. Clone the repository:
   ```sh
   git clone https://github.com/devnurmuhammad/JobCandidateHub.git
   cd JobCandidateHub
   ```
2. Set up database connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=JobCandidateHub;Username=your-user;Password=your-password"
   }
   ```
3. Apply migrations:
   ```sh
   dotnet ef database update
   ```
4. Run the application:
   ```sh
   dotnet run
   ```

This will start both the API and PostgreSQL database in a containerized environment.

## Caching Strategy

To enhance performance, **IMemoryCache** is used for frequently accessed queries like retrieving all candidates.

- **Cache Key:** `all_candidates`
- **Expiration Time:** 5 minutes
- **Invalidation:** Cache is cleared on Create, Update, or Delete actions.

## Unit Testing

Unit tests are written using **XUnit** and **Moq** to ensure code reliability. Tests cover:

- Service Layer (CandidateService)
- Repository Layer (Generic Repository)
- Caching Mechanism

To run tests:

```sh
   dotnet test
```

## Contribution Guidelines

1. Fork the repository and create a new branch.
2. Follow the coding standards and commit in logical units.
3. Ensure unit tests pass before submitting a pull request.
4. Describe changes clearly in the pull request description.

## License

This project is licensed under the MIT License.

