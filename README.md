# BenriShop

# Install necessary NuGet packages
Add the following NuGet packages to work with SQL Server database and scaffolding, and run the following commands in Package Manager Console (Click Tools -> NuGet Package Manager ->  Package Manager Console).

This package helps generate controllers and views.

Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design -Version 3.1.4
This package helps create database context and model classes from the database.

Install-Package Microsoft.EntityFrameworkCore.Tools -Version 3.1.8
Database provider allows Entity Framework Core to work with SQL Server.

Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 3.1.8
It provides support for creating and validating a JWT token.

Install-Package System.IdentityModel.Tokens.Jwt -Version 5.6.0
This is the middleware that enables an ASP.NET Core application to receive a bearer token in the request pipeline.

Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 3.1.8