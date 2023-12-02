# MUSALA SOFT - Social Network Project

## this project is completly buildable and runnable

## Nuget Packages Requirements

* Microsoft.EntityFrameworkCore.Design-----(v7.0.2)
* Pomelo.EntityFrameworkCore.MySql---------(v7.0.0)
* MySql.Data ------------------------------(v8.0.2)

## Backend local development

* Start by clone the repo:

git clone https://github.com/TouXch/SOCIAL_NETWORK_API.git

* Install mysql (v10.4.11)
* Import the database file (social_network.sql) attached in the solution folder
* If you need install the nuget packages requeriments using PM console

* start the server:

Build the project, the project automatically run in localhost

* Now you can open your browser and interact with these URLs:

Automatic interactive documentation with Swagger UI (provided by Visual Studio 2022)

* Fill the database

The database file have dummy data preoloaded

### Migrations

I didn't cover database migrations

###Unit Test
*Pre-requisites:
  - Bogus---------------------------------------(v34.0.2)
  - Microsoft.EntityFrameworkCore.InMemory------(v7.0.2)
* Unit Test can be run with the project SOCIAL_NETWORK_TEST
* Is recomended run each test separately for avoid trouble with duplicated dummy data filled with BOGUS library.
