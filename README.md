![build](https://github.com/amlsantos/DddInPractice/actions/workflows/dotnet.yml/badge.svg)

# Domain Driven Design in Practice
This is the source code for the pluralsight course [Domain-Driven Design in Practice](https://www.pluralsight.com/courses/domain-driven-design-in-practice), using .NET 7.
The original source code, is available [here](https://github.com/vkhorikov/DddInAction). App was migrated, from .NET Framework 4.5.2 to .NET 7 (education purposes).

This sample WPF application, is intended to be a learning tool for Domain Driven Design best practices:
- Bounded Contexts with Aggregates;
- Entities and Value Objects;
- Repositories;
- Domain Events.

Incorporates several of these practices, in a way that is simple and easy to understand.

## Setup database

In order to run the WPF application, we need to create a database on (localdb) server. Please go to [DBCreationScript.sql](https://github.com/amlsantos/DddInPractice/blob/main/src/Logic/Utils/DBCreationScript.sql), and run the script, in order to create a database.

## Technologies
This demo application uses the following technologies:
 - .NET 7
 - C# 11
 - EF Core 7.0
 - Rider 2022
 - SQL Server 2022
 - XUnit 2.5
 - FluentAssertions 6.11
