# Auction Backend RESTful Service in C#

# Overview 
* In this context, we examine the necessary steps involved in implementing a "Code-First Approach."
* It's important to highlight that this compilation was essembled using Visual Studio.
* This project constitutes an Auction backend service using ASP.NET Core, offering a robust foundation for building and overseeing several auction functionalities. It encompasses features such as Bid, Category, comment, and AuctionRecord, among others.

# Technology Utilised
* SQL server: relational database management system, offering reliability, scalability, and retrieval within the ASP.NET framework.
* C# programming language: serves as the primary language for development purposes.
* ASP.NET Core: A versatile framework designed for creating web applications and services, leveraging the  C# programming language.
* Entity Framework Core: An Object-Relational Mapping (ORM) framework for .NET facilitates database  access within the application.
* ASP.NET Core Identity: offers functionalities for applicationuser authentication and authorisation. capabilities.
* JWT Authentication: employed to secure API, utilising JSON Web Token.


## Sequence
1. Initiating a new project
2. Generate Model classes and integrate DBContext model class
3. Incorporating the context with dependency injection
4. Implementing the Database Connection string
5. Installing the package manager
6. Install necessary packages using the package manager console
7. Adding the Migrations
8. Integrating the Controllers
9. Publish to Microsoft Azure
10. Deployment

Published at: https://auctionbackendapi.azurewebsites.net/swagger/index.html

# APIs:

## Account
https://auctionbackendapi.azurewebsites.net/api/Account

## User
https://auctionbackendapi.azurewebsites.net/api/user

## Auction
https://auctionbackendapi.azurewebsites.net/api/Auction

## Category
https://auctionbackendapi.azurewebsites.net/api/Category

## Bid
https://auctionbackendapi.azurewebsites.net/api/Bid

## Comment
https://auctionbackendapi.azurewebsites.net/api/Comment

## AuctionRecord
https://auctionbackendapi.azurewebsites.net/api/AuctionRecord


# Tools to install:

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

Microsoft.EntityFrameworkCore.Design

dotnet new tool-manifest

dotnet tool install --local dotnet-ef

dotnet ef migrations add InitialCreate

dotnet ef database update

dotnet tool install --local dotnet-aspnet-codegenerator
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design

dotnet add package Microsoft.AspNet.Identity.EntityFramework --version 2.2.4
dotnet add package System.Security.Claims --version 4.3.0
dotnet add package Newtonsoft.Json --version 13.0.3
