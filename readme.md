# Contoso University Clean 6

ASP.NET 6 MVC with Entity Framework Core 6 - demo app with Blazor Server components

This solution is one take at migrating a layered architecture MVC web app
to "Clean Architecture" using a practical approach that preserves as much
existing code as possible, while making it possible for future development
to use Clean Architecture.

The code for the starting point of this endeavour can be found
[in GitHub here](https://github.com/bgoodearl/ContosoUniversity_dnc31_MVC).

See "Clean Architecture and related Resource Links" below for
some of the materials I read before attempting this work.

A parallel solution using .NET Core 3.1 can be found [in GitHub here](https://github.com/bgoodearl/ContosoU_dnc31_MVCB_Clean).

## Developer Notes

[Dev Notes](./_docs/CC6__DevNotes.md)<br/>

## Resource links

[Clean Architecture and related Resource Links](./_docs/CC6_CleanResources.md)<br/>
[Tools](./_docs/CC6_Tools.md)<br/>

## Projects

Project Name                 | Description
-------------                | ------------
CU.SharedKernel              | Classes shared among multiple app projects
ContosoUniversity.Models     | Persistent Data Object Models (Domain)
