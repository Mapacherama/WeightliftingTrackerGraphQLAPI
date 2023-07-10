# Weightlifting Tracker GraphQL API

## Introduction

Welcome to the Weightlifting Tracker API! This project is a robust backend designed to provide comprehensive tracking and management for weightlifters. Leveraging the power of .NET and GraphQL, it provides users with the ability to track their weights, nutrition, and overall progress in their weightlifting journey.

## Purpose & Objectives

The Weightlifting Tracker API aims to make weightlifting more manageable and structured. It offers the following functionalities:

- **Weight Tracking**: Track weightlifting progress over time, record and update weights lifted, and view progress graphs.
- **Nutrition Management**: Manage daily nutrition intake, set nutrition goals, track macros, and more.
- **User Management**: Create, update, delete, and view user profiles.

## Local Setup & Installation

1. **Prerequisites**: Ensure you have the following installed on your local system:

    .NET 6.0 SDK
    Visual Studio 2022 or later
2. **Clone the Repository**: Clone this repository to your local machine using git clone ```https://github.com/[username]/weightlifting-tracker-api.git```
3. **Restore Packages**: Navigate to the root directory of the project using your command prompt/terminal and run ```dotnet restore``` to restore necessary NuGet packages.
4. **Build the Project**: Run ```dotnet build``` to build the project.
5. **Run the Project**: Start the application using ```dotnet run```.

## Technology Stack

  - .NET 6.0: The project is built with .NET 5.0, a free, cross-platform, and open-source developer platform for building all types of applications.
  - GraphQL: The API uses GraphQL, a query language for APIs and a runtime for executing those queries with existing data.
  - Entity Framework Core: The data is handled using Entity Framework Core, an open-source object-database mapper for .NET.

## Running Tests

Unit tests have been written using NUnit and can be run using the .NET CLI. From the root directory of the project, execute dotnet test to run all the tests.

## Deployment

This application can be deployed to any hosting service that supports .NET 6.0. Follow the specific instructions provided by your chosen hosting provider.

## Contributing Guidelines

Contributions are always welcome!

## Code of Conduct

We believe in a welcoming and respectful community. Please read our CODE_OF_CONDUCT.md to understand the rules and responsibilities when participating in this project.
