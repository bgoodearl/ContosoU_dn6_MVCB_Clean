USE ContosoUniversity22; --Database used for tests
BEGIN TRANSACTION;
GO

CREATE TABLE [xLookups2cKey] (
    [Code] nvarchar(2) NOT NULL,
    [LookupTypeId] smallint NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [_SubType] smallint NOT NULL,
    CONSTRAINT [PK_xLookups2cKey] PRIMARY KEY ([LookupTypeId], [Code])
);
GO

CREATE UNIQUE INDEX [LookupTypeAndName] ON [xLookups2cKey] ([LookupTypeId], [Name]);
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NOT NULL BEGIN
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220516143358_CU6_M04_AddLookups', N'6.0.2');
END;
GO

COMMIT;
GO

