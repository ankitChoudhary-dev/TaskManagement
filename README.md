# Task Management System

## Overview

Task Management System is a full-stack web application developed using ASP.NET Core Web API, Angular, SQL Server, and Entity Framework Core. The application provides a secure platform for managing projects and tasks through role-based access control and JWT authentication.

The solution is divided into two independent applications:

- **TaskManagement.API** – RESTful backend built with ASP.NET Core Web API
- **TaskManagement.UI** – Angular frontend that consumes the REST API

The backend exposes REST endpoints for authentication, project management, task management, and dashboard statistics, while the Angular application provides a responsive user interface for interacting with these services.

The project follows common enterprise development practices including dependency injection, repository pattern, AutoMapper, FluentValidation, centralized exception handling, and layered architecture to improve maintainability, scalability, and code quality.

---

# Project Objectives

The application was developed to demonstrate the implementation of a complete task management solution with the following capabilities:

- User authentication using JWT
- Role-based authorization
- Project management
- Task management
- Dashboard reporting
- Search and filtering
- Server-side validation
- Clean project architecture
- RESTful API design
- Angular frontend integration

The implementation also includes several recommended practices from the assignment, including AutoMapper and FluentValidation. :contentReference[oaicite:0]{index=0}

---

# Technology Stack

## Backend

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- JWT Authentication
- AutoMapper
- FluentValidation
- Dependency Injection
- Repository Pattern
- Swagger / OpenAPI

## Frontend

- Angular
- TypeScript
- HTML5
- CSS3
- Angular Router
- Reactive Forms
- HTTP Interceptor
- Route Guards

## Database

- Microsoft SQL Server

## Development Tools

- Visual Studio 2022
- Visual Studio Code
- SQL Server Management Studio (SSMS)
- Postman
- Git
- GitHub

---

# Solution Structure

The repository contains two separate applications.

## TaskManagement.API

The backend application exposes REST APIs responsible for:

- User registration and login
- JWT token generation
- Authentication and authorization
- Project management
- Task management
- Dashboard statistics
- Database access
- Request validation
- Global exception handling

## TaskManagement.UI

The frontend application communicates with the backend API and provides functionality for:

- User authentication
- Dashboard
- Project management
- Task management
- Task assignment
- Search and filtering
- Status updates
- Responsive user interface

---

# Key Features

## Authentication

- User registration
- Secure login
- JWT-based authentication
- Role-based authorization
- Protected API endpoints
- Protected Angular routes

## Dashboard

The dashboard provides summary statistics including:

- Total Projects
- Total Tasks
- Pending Tasks
- Tasks In Progress
- Completed Tasks

## Project Management

- Create project
- View project list
- Update project details
- Delete project
- Track project status

## Task Management

- Create task
- Update task
- Delete task
- Assign task to users
- Update task status
- Set task priority
- Search tasks by title
- Filter tasks by status
- Filter tasks by priority

## Validation

### Backend

- FluentValidation
- Request model validation
- Business rule validation

### Frontend

- Angular Reactive Forms
- Client-side validation
- User-friendly validation messages

## Error Handling

- Global exception middleware
- Consistent API error responses
- Request validation handling
- Logging support

---

# Engineering Practices

The project incorporates several software engineering practices commonly used in enterprise applications.

- Layered architecture
- Dependency Injection
- Repository Pattern
- AutoMapper
- FluentValidation
- JWT Authentication
- Role-Based Authorization
- RESTful API Design
- Global Exception Handling
- Angular Route Guards
- HTTP Interceptor
- Reactive Forms
- Clean separation of concerns

---

# Repository Structure

The repository is organized into separate backend and frontend applications to maintain a clear separation of responsibilities.

```
TaskManagement
│
├── TaskManagement.API/
│   ├── Controllers/
│   ├── DTOs/
│   ├── Entities/
│   ├── Services/
│   ├── Repositories/
│   ├── Interfaces/
│   ├── Data/
│   ├── Middleware/
│   ├── Validators/
│   ├── Mapping/
│   ├── Helpers/
│   ├── Migrations/
│   ├── Program.cs
│   └── appsettings.json
│
├── TaskManagement.UI/
│   ├── src/
│   │   ├── app/
│   │   ├── assets/
│   │   ├── environments/
│   │   └── ...
│   ├── angular.json
│   └── package.json
│
├── SQLQuery1.sql
└── README.md
```

The solution consists of two independent projects that communicate through REST APIs.

---

# Backend Project

**Project:** `TaskManagement.API`

The backend is developed using ASP.NET Core Web API and serves as the central component of the application. It is responsible for handling business logic, data access, authentication, authorization, validation, and communication with the SQL Server database.

The API exposes REST endpoints that are consumed by the Angular frontend.

## Responsibilities

- User registration and login
- JWT token generation
- Role-based authorization
- Project management
- Task management
- Dashboard statistics
- Input validation
- Exception handling
- Database operations

---

## Backend Technologies

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- AutoMapper
- FluentValidation
- Dependency Injection
- Repository Pattern
- Swagger

---

## Major Components

### Controllers

Controllers expose REST endpoints and handle incoming HTTP requests. Each controller delegates business operations to the appropriate service layer instead of directly interacting with the database.

---

### Services

The service layer contains the application's business logic. It coordinates validation, repository operations, object mapping, and business rules before returning responses to the client.

---

### Repositories

Repositories encapsulate all database operations using Entity Framework Core. This abstraction separates persistence logic from business logic and improves maintainability.

---

### DTOs

DTOs (Data Transfer Objects) define the request and response models exchanged between the client and the API. They prevent exposing entity models directly to consumers.

---

### AutoMapper

AutoMapper is used to map Entity objects to DTOs and vice versa, reducing repetitive mapping code and keeping controllers and services clean.

---

### FluentValidation

FluentValidation provides centralized validation rules for request models. Validation logic is separated from controllers and entities, making it easier to maintain and extend.

---

### Middleware

The application includes custom middleware for handling unhandled exceptions globally and returning consistent API error responses.

---

### Authentication

Authentication is implemented using JWT (JSON Web Tokens). After successful login, the API generates a signed token which is included in the Authorization header for subsequent requests.

---

### Authorization

Role-based authorization restricts access to protected endpoints. Administrative operations such as project and task management are accessible only to users with the appropriate role.

---

# Frontend Project

**Project:** `TaskManagement.UI`

The frontend is built using Angular and communicates with the ASP.NET Core Web API through REST endpoints.

The application provides a responsive and user-friendly interface for authentication, project management, task management, and dashboard reporting.

---

## Responsibilities

- User authentication
- Dashboard
- Project management
- Task management
- Search and filtering
- Status updates
- Form validation
- API communication

---

## Frontend Technologies

- Angular
- TypeScript
- Angular Router
- Reactive Forms
- HTTP Client
- HTTP Interceptor
- Route Guards
- HTML5
- CSS3

---

## Angular Features

### Reactive Forms

All forms use Angular Reactive Forms with client-side validation to ensure data integrity before requests are submitted to the API.

---

### HTTP Interceptor

A custom HTTP interceptor automatically attaches the JWT access token to authenticated API requests.

---

### Route Guards

Protected routes require a valid authenticated session before granting access to application pages.

---

### Services

Angular services encapsulate all HTTP communication with the backend API, providing a clean separation between UI components and API logic.

---

### Components

The application is organized into reusable Angular components for authentication, dashboard, projects, tasks, and shared functionality.

---

### Routing

Angular Router provides client-side navigation and lazy loading support for different application modules.
---

# Prerequisites

Before running the application, ensure the following software is installed on your machine.

| Software | Version |
|----------|---------|
| .NET SDK | .NET 8 or later |
| Node.js | 18.x or later |
| Angular CLI | 16.x or later |
| SQL Server | SQL Server 2019 or later |
| SQL Server Management Studio (SSMS) | Latest |
| Visual Studio 2022 | Recommended |
| Visual Studio Code | Recommended |
| Git | Latest |

---

# Database Setup

The project uses Microsoft SQL Server as the primary database.

A SQL script has been included in the repository to create the required database schema and insert sample data.

## Step 1: Execute the SQL Script

Open SQL Server Management Studio (SSMS) and connect to your SQL Server instance.

Open the following SQL script from the repository:

```
TaskManagement/SQLQuery1.sql
```

Execute the entire script.

The script will automatically:

- Create the database
- Create all required tables
- Create relationships
- Insert sample data
- Prepare the application for execution

After the script completes successfully, the database will be ready for use.

---

# Backend Configuration

## Step 2: Configure the Connection String

Navigate to the following file:

```
TaskManagement.API/appsettings.json
```

Locate the connection string.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=TaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Replace `YOUR_SERVER_NAME` with your local SQL Server instance.

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-123ABC\\SQLEXPRESS;Database=TaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

If you are using SQL Authentication, update the connection string accordingly.

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TaskManagementDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
}
```

---

# JWT Configuration

The JWT settings are available in the same `appsettings.json` file.

```json
"Jwt": {
    "Key": "YourSecretKey",
    "Issuer": "TaskManagementAPI",
    "Audience": "TaskManagementUI"
}
```

These values can be modified if required.

---

# Running the Backend API

Open a terminal inside the API project.

Navigate to the API directory.

```bash
cd TaskManagement.API
```

Restore the required NuGet packages.

```bash
dotnet restore
```

Build the project.

```bash
dotnet build
```

Run the application.

```bash
dotnet run
```

After the application starts successfully, the API will be available at:

```
https://localhost:7193
```

or

```
https://localhost:5001
```

depending on your local launch configuration.

---

# Swagger Documentation

Once the API is running, Swagger UI can be accessed from:

```
https://localhost:7193/swagger
```

Swagger provides interactive documentation for all available API endpoints and can be used to test the API directly.

---

# Frontend Configuration

Open the Angular project.

Navigate to the environment configuration file.

```
TaskManagement.UI/src/environments/environment.ts
```

Verify that the API URL matches your running backend.

Example:

```typescript
export const environment = {
    production: false,
    apiUrl: 'https://localhost:7193/api'
};
```

If your backend runs on a different port, update the URL accordingly.

---

# Running the Angular Application

Open a new terminal.

Navigate to the Angular project.

```bash
cd TaskManagement.UI
```

Install all required packages.

```bash
npm install
```

Start the Angular development server.

```bash
ng serve
```

or

```bash
ng serve --open
```

The application will be available at:

```
http://localhost:4200
```

---

# Running the Complete Application

Follow the steps below in sequence.

1. Execute `SQLQuery1.sql` using SQL Server Management Studio.
2. Update the SQL Server connection string in `appsettings.json`.
3. Run the ASP.NET Core Web API.
4. Verify that Swagger is accessible.
5. Start the Angular application.
6. Open `http://localhost:4200` in your browser.
7. Register a new user or log in using the sample credentials.
8. Begin managing projects and tasks.

---

# Project Workflow

```
                SQL Server Database
                        ▲
                        │
                        │
            Entity Framework Core
                        │
                        │
          ASP.NET Core Web API
                        │
             REST API (HTTPS)
                        │
                        │
                 Angular Frontend
                        │
                        │
                     End User
```

The Angular application communicates exclusively with the ASP.NET Core Web API through REST endpoints. The API performs authentication, validation, business logic, and database operations before returning responses to the client.

---

# Notes

- Ensure SQL Server is running before starting the API.
- The API must be running before launching the Angular application.
- Verify that the API URL configured in the Angular environment matches the backend URL.
- If CORS errors occur, ensure the API allows requests from the Angular application's origin.
- If HTTPS certificate warnings appear during development, trust the ASP.NET Core development certificate using:

```bash
dotnet dev-certs https --trust
```
---

# API Endpoints

The backend exposes a RESTful API that is consumed by the Angular frontend. All protected endpoints require a valid JWT access token in the `Authorization` header.

## Authentication

| Method | Endpoint | Description |
|---------|----------|-------------|
| POST | `/api/Auth/register` | Register a new user |
| POST | `/api/Auth/login` | Authenticate user and generate JWT token |

---

## Projects

| Method | Endpoint | Description |
|---------|----------|-------------|
| GET | `/api/Projects` | Retrieve all projects |
| GET | `/api/Projects/{id}` | Retrieve a project by ID |
| POST | `/api/Projects` | Create a new project |
| PUT | `/api/Projects/{id}` | Update an existing project |
| DELETE | `/api/Projects/{id}` | Delete a project |

---

## Tasks

| Method | Endpoint | Description |
|---------|----------|-------------|
| GET | `/api/Tasks` | Retrieve all tasks |
| GET | `/api/Tasks/{id}` | Retrieve task details |
| POST | `/api/Tasks` | Create a new task |
| PUT | `/api/Tasks/{id}` | Update a task |
| DELETE | `/api/Tasks/{id}` | Delete a task |

Additional features supported by the Tasks API include:

- Search tasks by title
- Filter tasks by priority
- Filter tasks by status
- Assign tasks to users
- Update task status

---

## Dashboard

| Method | Endpoint | Description |
|---------|----------|-------------|
| GET | `/api/Dashboard` | Retrieve dashboard statistics |

The dashboard provides summary information including:

- Total Projects
- Total Tasks
- Pending Tasks
- Tasks In Progress
- Completed Tasks

---

# Authentication Flow

The application uses JSON Web Token (JWT) authentication.

1. The user submits valid login credentials.
2. The API validates the credentials.
3. A JWT access token is generated.
4. The Angular application stores the token.
5. Every authenticated request includes the token in the `Authorization` header.
6. The API validates the token before processing protected requests.
7. Role-based authorization determines whether the user has permission to access the requested resource.

Example Authorization header:

```http
Authorization: Bearer <JWT_TOKEN>
```

---

# Application Architecture

The application follows a layered architecture to maintain separation of concerns and improve maintainability.

```
                    Angular Application
                            │
                            │ HTTP Requests
                            ▼
                ASP.NET Core Web API
                            │
                    Controllers
                            │
                            ▼
                       Services
                            │
                            ▼
                    Repositories
                            │
                            ▼
                 Entity Framework Core
                            │
                            ▼
                      SQL Server
```

Each layer has a single responsibility and communicates only with the layer directly beneath it.

---

# Design Decisions

## Layered Architecture

Business logic is separated from presentation and persistence layers, making the application easier to maintain and extend.

---

## Repository Pattern

Database operations are encapsulated within repository classes. This avoids exposing Entity Framework Core directly to controllers and promotes cleaner code.

---

## Dependency Injection

Application services and repositories are registered using ASP.NET Core's built-in dependency injection container. This reduces coupling between components and improves testability.

---

## DTOs (Data Transfer Objects)

DTOs are used to transfer data between the client and the API without exposing entity models directly.

Benefits include:

- Improved security
- Reduced payload size
- Better separation between domain models and API contracts

---

## AutoMapper

AutoMapper simplifies object mapping between entities and DTOs, reducing repetitive code and improving readability.

---

## FluentValidation

Validation rules are implemented using FluentValidation rather than embedding validation logic within controllers or entity classes.

Benefits include:

- Centralized validation
- Cleaner controllers
- Easier maintenance
- Better scalability

---

## JWT Authentication

JWT authentication enables stateless authentication between the Angular frontend and the ASP.NET Core backend.

Benefits include:

- Secure API access
- Stateless authentication
- Easy integration with SPA applications

---

## Global Exception Handling

A custom exception middleware provides centralized error handling across the application.

Advantages include:

- Consistent error responses
- Reduced duplicate code
- Easier debugging
- Improved maintainability

---

## Angular Route Guards

Protected routes require users to be authenticated before accessing secured pages.

Unauthorized users are redirected to the login page.

---

## HTTP Interceptor

The Angular HTTP interceptor automatically attaches the JWT access token to every authenticated API request.

This eliminates the need to manually include the authorization header in every service.

---

# Engineering Highlights

The project incorporates several practices commonly used in enterprise software development.

- RESTful API design
- Layered architecture
- Dependency Injection
- Repository Pattern
- Entity Framework Core
- SQL Server
- JWT Authentication
- Role-Based Authorization
- AutoMapper
- FluentValidation
- Global Exception Middleware
- Angular Reactive Forms
- Angular Route Guards
- HTTP Interceptor
- Clean separation of concerns
- Responsive user interface
- Swagger/OpenAPI documentation

---

# Assumptions

The following assumptions were made during development:

- SQL Server is available locally.
- The database is created using the provided SQL script.
- The backend and frontend are executed on localhost during development.
- JWT settings are configured in `appsettings.json`.
- Users must authenticate before accessing protected resources.
- Administrative operations require an authenticated user with the appropriate role.
- The Angular application communicates exclusively through the REST API.

---

# Future Improvements

The current implementation satisfies the machine test requirements. The following enhancements could be considered for future development:

- Refresh token implementation
- Password reset functionality
- Email notifications
- Soft delete support
- Audit logging
- File attachment support
- Docker containerization
- CI/CD pipeline integration
- Azure deployment
- Unit testing
- Integration testing
- Performance monitoring
- Caching using Redis
- SignalR for real-time notifications
- Background job processing using Hangfire

---

# Author

**Ankit Choudhary**

Software Engineer

GitHub: https://github.com/ankitChoudhary-dev

Email: ankitoffice9536@gmail.com

---

# License

This project was developed as part of a Full Stack Developer technical assessment. It is intended for educational, demonstration, and portfolio purposes.
