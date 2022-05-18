USE ContosoU_dn6_dev; --Database used for Migrations
BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220518194941_CU6_M04a_Course_CPT')
BEGIN
    CREATE TABLE [_coursesPresentationTypes] (
        [CourseID] int NOT NULL,
        [LookupTypeId] smallint NOT NULL,
        [CoursePresentationTypeCode] nvarchar(2) NOT NULL,
        CONSTRAINT [PK__coursesPresentationTypes] PRIMARY KEY ([CourseID], [LookupTypeId], [CoursePresentationTypeCode]),
        CONSTRAINT [FK__coursesPresentationTypes_Course_CourseID] FOREIGN KEY ([CourseID]) REFERENCES [Course] ([CourseID]) ON DELETE CASCADE,
        CONSTRAINT [FK__coursesPresentationTypes_xLookups2cKey_LookupTypeId_CoursePresentationTypeCode] FOREIGN KEY ([LookupTypeId], [CoursePresentationTypeCode]) REFERENCES [xLookups2cKey] ([LookupTypeId], [Code]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220518194941_CU6_M04a_Course_CPT')
BEGIN
    CREATE INDEX [IX__coursesPresentationTypes_LookupTypeId_CoursePresentationTypeCode] ON [_coursesPresentationTypes] ([LookupTypeId], [CoursePresentationTypeCode]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220518194941_CU6_M04a_Course_CPT')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220518194941_CU6_M04a_Course_CPT', N'6.0.2');
END;
GO

COMMIT;
GO

