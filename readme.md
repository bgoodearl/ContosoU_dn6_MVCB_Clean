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

## IMPORTANT NOTES

### Initial setup after cloning repo or getting code in zip

Create your local database(es) and use the SQL scripts:<br/>
`...\SqlScripts\Schema\CU6_M01_ExistingSchemaBase_2022_idempotent.sql`<br/>
`...\SqlScripts\Schema\CU6_M02_AddEnrollment_idempotent.sql`<br/>
to create the tables for the database used for migrations.<br/>

Use the SQL scripts:<br/>
`...\SqlScripts\Schema\CU6_M01_ExistingSchemaBase_2022.sql`<br/>
`...\SqlScripts\Schema\CU6_M02_AddEnrollment.sql`<br/>
`...\SqlScripts\Schema\CU6_M03_AddCourseInstructorLink.sql`<br/>
to create the tables for the database used for integration tests and the web app.<br/>

Copy `...\_ConfigSource\src\ContosoUniversity\appsettings.Development_user_xxxx.json`
to `...\src\ContosoUniversity` replacing "xxxx" in the file name with the 
username of the account from which you are running Visual Studio, and
update the paths in the file to match your solution path.  Also,
and correct connection string for your environment.

Update paths for `internalLogFile` and `var_logdir`
in `appsettings.Development_user_...` after you copy and rename it.

Copy `...\_ConfigSource\src\CU.EFDataApp\appsettings.migrations.json`
to `...\src\CU.EFDataApp` and modify connection string for your environment.

Copy `...\_ConfigSource\src\tests\CU.ApplicationIntegrationTests\appsettings.LocalTesting.json`
to `...\src\tests\CU.ApplicationIntegrationTests` and modify connection string for your environment.

## Resource links

[Blazor Resource Links](./_docs/CC6_BlazorResources.md)<br/>
[Clean Architecture and related Resource Links](./_docs/CC6_CleanResources.md)<br/>
[EF Core 6 Resources](./_docs/CC6_EFCore6Resources.md)<br/>
[Logging Resources](./_docs/CC6_Logging.md)<br/>
[Other Resources](./_docs/CC6_Resources.md)<br/>
[Tools](./_docs/CC6_Tools.md)<br/>

### Seeding Data

Set connection string in ...\src\tests\CU.ApplicationIntegrationTests\appsettings.LocalTesting.json
for the database you want to seed.

Run a single test that uses GetISchoolDbContext (e.g. CanGetCoursesAsync()).

See code in TestFixture.GetISchoolDbContext(ITestOutputHelper testOutputHelper) for more information.

## Projects

Project Name                    | Description
-------------                   | ------------
ContosoUniversity               | Contoso University Web Application
ContosoUniversity.Components    | Blazor components for ContosoUniversity
ContosoUniversity.Models        | Persistent Data Object Models (Domain)
CU.Application                  | Application specific code
CU.Application.Common           | Interfaces allowing use of the Repository
CU.Application.Shared           | Interfaces and Classes shared among multiple CU projects
CU.Infrastructure               | Infrastructure, including Entity Framework DbContext, Repositories, and Migrations
CU.SharedKernel                 | Classes shared among multiple app projects
CU.ApplicationIntegrationTests  | 
Demo.Components                 | Demo Blazor components
