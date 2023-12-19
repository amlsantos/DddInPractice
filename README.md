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

## The domain problem

The first bounded context, is the snack machine.
We model, the way, a SnackMachine works with coins and notes.
First we encapsulated the definition of Money.
Second, we have a way to insert, money into the machine.

Also, if we change our mind, we are able, to get inserted money back.
Finally, we can buy something. In this case, the insert money, goes the machine permanently. The user gets back the change and the snack.

The second bounded context, is an ATM. The user can withdraw cash. 
The bank card, of the user is charged, with a fee of 1%.
The ATM is also responsable, for Keeping track of all money charged.

Now as we have these two models (one for snack machine and other for the ATM), we have an third bounded context, named Management.
It is responsible for managing the devices.
This sub-system keeps track of all payments. Is responsable for moving cash, from the snack machine into the ATMs.
So we are using events, to communicate between different bounded contexts. 
Whenever we to move cash from the SnackMachine into an ATM, we use events.

![Screenshot_1](https://github.com/amlsantos/SnachMachine-DDD/assets/6472330/dae602ea-f60e-4da7-af80-6985b87b0615)

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
