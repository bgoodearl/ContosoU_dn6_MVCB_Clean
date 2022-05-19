USE ContosoU_dn6_dev; --Database used for Migrations
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303033131_CU6_M02_AddEnrollment')
BEGIN
    CREATE TABLE [Enrollment] (
        [EnrollmentID] int NOT NULL IDENTITY,
        [CourseID] int NOT NULL,
        [StudentID] int NOT NULL,
        [Grade] int NULL,
        CONSTRAINT [PK_Enrollment] PRIMARY KEY ([EnrollmentID]),
        CONSTRAINT [FK_Enrollment_Course_CourseID] FOREIGN KEY ([CourseID]) REFERENCES [Course] ([CourseID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Enrollment_Student_StudentID] FOREIGN KEY ([StudentID]) REFERENCES [Student] ([ID]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303033131_CU6_M02_AddEnrollment')
BEGIN
    CREATE INDEX [IX_Enrollment_CourseID] ON [Enrollment] ([CourseID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303033131_CU6_M02_AddEnrollment')
BEGIN
    CREATE INDEX [IX_Enrollment_StudentID] ON [Enrollment] ([StudentID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303033131_CU6_M02_AddEnrollment')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220303033131_CU6_M02_AddEnrollment', N'6.0.2');
END;
GO

COMMIT;
GO

