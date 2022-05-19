USE ContosoUniversity22; --Database used for tests
BEGIN TRANSACTION;
GO

CREATE TABLE [_coursesPresentationTypes] (
    [CourseID] int NOT NULL,
    [CoursePresentationTypesLookupTypeId] smallint NOT NULL,
    [CoursePresentationTypesCode] nvarchar(2) NOT NULL,
    CONSTRAINT [PK__coursesPresentationTypes] PRIMARY KEY ([CourseID], [CoursePresentationTypesLookupTypeId], [CoursePresentationTypesCode]),
    CONSTRAINT [FK__coursesPresentationTypes_Course_CourseID] FOREIGN KEY ([CourseID]) REFERENCES [Course] ([CourseID]) ON DELETE CASCADE,
    CONSTRAINT [FK__coursesPresentationTypes_xLookups2cKey_CoursePresentationTypesLookupTypeId_CoursePresentationTypesCode] FOREIGN KEY ([CoursePresentationTypesLookupTypeId], [CoursePresentationTypesCode]) REFERENCES [xLookups2cKey] ([LookupTypeId], [Code]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX__coursesPresentationTypes_CoursePresentationTypesLookupTypeId_CoursePresentationTypesCode] ON [_coursesPresentationTypes] ([CoursePresentationTypesLookupTypeId], [CoursePresentationTypesCode]);
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NOT NULL BEGIN
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220518194941_CU6_M04a_Course_CPT', N'6.0.2');
END;
GO

COMMIT;
GO

