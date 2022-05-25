USE ContosoUniversity22; --Database used for tests
BEGIN TRANSACTION;
GO

CREATE TABLE [_departmentsFacilityTypes] (
    [DepartmentID] int NOT NULL,
    [LookupTypeId] smallint NOT NULL,
    [DepartmentFacilityTypeCode] nvarchar(2) NOT NULL,
    CONSTRAINT [PK__departmentsFacilityTypes] PRIMARY KEY ([DepartmentID], [LookupTypeId], [DepartmentFacilityTypeCode]),
    CONSTRAINT [FK__departmentsFacilityTypes_Department_DepartmentID] FOREIGN KEY ([DepartmentID]) REFERENCES [Department] ([DepartmentID]) ON DELETE CASCADE,
    CONSTRAINT [FK__departmentsFacilityTypes_xLookups2cKey_LookupTypeId_DepartmentFacilityTypeCode] FOREIGN KEY ([LookupTypeId], [DepartmentFacilityTypeCode]) REFERENCES [xLookups2cKey] ([LookupTypeId], [Code]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX__departmentsFacilityTypes_LookupTypeId_DepartmentFacilityTypeCode] ON [_departmentsFacilityTypes] ([LookupTypeId], [DepartmentFacilityTypeCode]);
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NOT NULL BEGIN
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220525025336_CU6_M04b_Department_DFT', N'6.0.2');
END;
GO

COMMIT;
GO

