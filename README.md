# TaxCalc
Sample progressive tax calculator

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)
* [Other](#nice-to-have)

## General info
The TaxCalc is a small full stack solution to do tax calculations using the progressive tax method. Given a postal code and an annual income, the utility will provide the user with the tax due saving the result for each session in a SQl Server database.
	
## Technologies
Project is created with:
* .NET Core 2.2
* SQL Server
* HTML/Jquery/CSS
* Unit tests using NUnit
* Entity Framwork Core
* Autofac as IOC container
* Web API with a Swagger interface for easy access

The following design principles were observed:
* Onion layered Architecture
* SOLID principles
	
## Setup
To run this project

* First restored the SQL Server database from the backup in db\db.bak
* Set both the Web and Api project as startup
* Press F5 to run

### Nice to have
Due to time constraints not everything could be implemented. The following will be nice to haves for a future version.
- Add Fluent validator on query model
- Use Automapper for db to presentation layer mapping
- Seperate database query from commands
- More robust error handling - Logging and global error handler
- API Authentication
- Lazy loading
- Queueing
- More UI tests
- Deploy to cloud ...

However, for the purposes of this specific exercise, the implementation was succesful.
