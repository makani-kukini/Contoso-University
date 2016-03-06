# Contoso-University

This project closely mirrors the tutorials available at [this asp.net tutorial](http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application), but with a few differences.

* The asp.net tutorial is written in C#, and this project is written in VB.NET
* The asp.net tutorial includes steps to publish to Azure, and this project includes steps to publish to a local instance of IIS and SQL Server
* The asp.net tutorial is written with the en-US culture in mind, and this project includes support with globalize.js, cldr and jquery-validation-globalize to perform client side validation in multiple cultures using the UI culture of the client's browser

#### Building and Configuration (Debug)
In the Nuget package manager console, run Update-Database to create and seed the database with test-data, then build and debug.

#### Building and Configuration (Staging)
Create a database on your (local) connection to SQL Server named "ContosoUniversity" and run the Grant.sql script in the Solution Items folder to give "IIS APPPOOL\DefaultAppPool" login permissions and create a user "ContosoUniversityUser" for that login with "db_owner" permissions.
Publish the project with the included publish profile and browse to [http://localhost/ContosoUniversity](http://localhost/ContosoUniversity).
