USE ContosoU_dn6_dev; --Database used for Migrations
BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220525025336_CU6_M04b_Department_DFT')
BEGIN
    CREATE TABLE [_departmentsFacilityTypes] (
        [DepartmentID] int NOT NULL,
        [LookupTypeId] smallint NOT NULL,
        [DepartmentFacilityTypeCode] nvarchar(2) NOT NULL,
        CONSTRAINT [PK__departmentsFacilityTypes] PRIMARY KEY ([DepartmentID], [LookupTypeId], [DepartmentFacilityTypeCode]),
        CONSTRAINT [FK__departmentsFacilityTypes_Department_DepartmentID] FOREIGN KEY ([DepartmentID]) REFERENCES [Department] ([DepartmentID]) ON DELETE CASCADE,
        CONSTRAINT [FK__departmentsFacilityTypes_xLookups2cKey_LookupTypeId_DepartmentFacilityTypeCode] FOREIGN KEY ([LookupTypeId], [DepartmentFacilityTypeCode]) REFERENCES [xLookups2cKey] ([LookupTypeId], [Code]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220525025336_CU6_M04b_Department_DFT')
BEGIN
    CREATE INDEX [IX__departmentsFacilityTypes_LookupTypeId_DepartmentFacilityTypeCode] ON [_departmentsFacilityTypes] ([LookupTypeId], [DepartmentFacilityTypeCode]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220525025336_CU6_M04b_Department_DFT')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220525025336_CU6_M04b_Department_DFT', N'6.0.2');
END;
GO

COMMIT;
GO

