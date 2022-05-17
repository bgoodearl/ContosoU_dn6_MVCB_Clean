USE ContosoUniversity_dev22; --Database used for Migrations
BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220516143358_CU6_M04_AddLookups')
BEGIN
    CREATE TABLE [xLookups2cKey] (
        [Code] nvarchar(2) NOT NULL,
        [LookupTypeId] smallint NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [_SubType] smallint NOT NULL,
        CONSTRAINT [PK_xLookups2cKey] PRIMARY KEY ([LookupTypeId], [Code])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220516143358_CU6_M04_AddLookups')
BEGIN
    CREATE UNIQUE INDEX [LookupTypeAndName] ON [xLookups2cKey] ([LookupTypeId], [Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220516143358_CU6_M04_AddLookups')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220516143358_CU6_M04_AddLookups', N'6.0.2');
END;
GO

COMMIT;
GO

