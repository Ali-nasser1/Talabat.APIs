
# Delivery Food Website Documentation

  

## Overview

  

This is a **Talabat Clone Project** designed as a full-stack web application for ordering and delivering food. The application is built using **.NET 6** with a **RESTful API** backend and an Angular front-end. It follows the **Onion Architecture** and incorporates various design patterns and best practices to ensure scalability, maintainability, and security.

  

## Key Technologies

  

-  **Backend:**

-  **.NET 6**: The primary framework used for developing the API.

-  **Entity Framework Core (EF Core)**: ORM used for data access, allowing interactions with **MS SQL Server** using LINQ.

-  **JWT Authentication & Authorization**: Secure access to the API using **JSON Web Tokens (JWT)**.

-  **ASP.NET Core Identity**: For managing user authentication and authorization.

-  **Stripe Payment Integration**: Secure online payment processing.

-  **Redis Cache**: Caching solution for improving performance and reducing load on the database.

-  **AutoMapper**: Used for object-object mapping, enabling easy conversion between DTOs and entities.

-  **RESTful API**: Following REST principles for API design.

-  **Onion Architecture**: Decoupling the core application logic from external dependencies.

-  **Frontend:**

-  **Angular**: The modern web application framework used for building the user interface.

  

-  **Database:**

-  **MS SQL Server**: The relational database management system used for data storage.

  

## Supported Platforms

  

-  **Operating Systems**: The application can be developed and deployed on **Mac**, **Linux**, and **Windows**.

-  **Version Control**: The project supports integration with various version control systems.

  

## Design Patterns

  

-  **Repository Design Pattern**: Provides a layer of abstraction over data access, promoting loose coupling and easier testing.

-  **Specification Design Pattern**: Facilitates flexible querying capabilities in the repository layer.

-  **Unit of Work Design Pattern**: Ensures that a series of operations are completed successfully as a single transaction.

-  **Builder Design Pattern**: Helps in constructing complex objects step by step.

  

## Key Features

  

-  **Onion Architecture**: Ensures that the core application logic is independent of frameworks and external dependencies, promoting better separation of concerns.

-  **Open/Closed Principle (OCP)**: The application is designed to be open for extension but closed for modification, allowing new features to be added without altering existing code.

  

-  **Error Handling**: Comprehensive handling of various error types and status codes:

-  **400**: Bad Request - For invalid input or requests.

-  **401**: Unauthorized - For unauthorized access attempts.

-  **404**: Not Found - For resources that cannot be found.

-  **500**: Internal Server Error - For unexpected server errors.

  

-  **Loading Techniques**:

-  **Lazy Loading**: Loads related entities only when accessed, improving performance.

-  **Eager Loading**: Loads related entities immediately with the initial query, reducing the number of database calls.

  

-  **Custom Middleware**:

-  **Authentication Middleware**: Custom logic for handling user authentication.

-  **Authorization Middleware**: Custom logic for enforcing user authorization rules.

  

## Getting Started

  

### Prerequisites

  

-  **.NET 6 SDK** installed on your development environment.

-  **Node.js** and **Angular CLI** for running and building the front-end application.

-  **MS SQL Server** for the database.

-  **Redis** server for caching.

  

### Setup Instructions

  

1.  **Clone the Repository:**

```bash

git clone https://github.com/Ali-nasser1/Talabat.APIs.git

cd Talabat.APIs
