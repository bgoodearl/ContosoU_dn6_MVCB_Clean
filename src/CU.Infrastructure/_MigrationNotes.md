# Contoso University Clean 6 - Migration Notes

## EF Core docs

[Managing Database Schemas](https://docs.microsoft.com/en-us/ef/core/managing-schemas/)
(c. Sep 2020)<br/>

[Additional EF Core 6 resource links](../../_docs/CC6_EFCore6Resources.md)<br/>

## Migrations

Before running any migrations, copy appSettings.migrations.json from
`...\_configSource\src\CU.EFDataApp`
to `...\src\CU.EFDataApp` and update the connection string to point to
the test database used for building migrations.

### .NET EF Core 6 Package Manager Console

[Entity Framework Core tools reference - Package Manager Console in Visual Studio](https://docs.microsoft.com/en-us/ef/core/cli/powershell)

Quick check of environment for Package Manager Console
```powershell
Get-Migration -Project CU.Infrastructure -StartupProject CU.EFDataApp
```
(add -verbose to the end of the command above to confirm the correct database and server)


