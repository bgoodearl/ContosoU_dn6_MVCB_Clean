USE ContosoU_dn6_dev; --Database used for Migrations
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303041924_CU6_M03_AddCourseInstructorLink')
BEGIN
    CREATE TABLE [CourseInstructor] (
        [CourseID] int NOT NULL,
        [InstructorID] int NOT NULL,
        CONSTRAINT [PK_CourseInstructor] PRIMARY KEY ([CourseID], [InstructorID]),
        CONSTRAINT [FK_CourseInstructor_Course] FOREIGN KEY ([CourseID]) REFERENCES [Course] ([CourseID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CourseInstructor_Instructor] FOREIGN KEY ([InstructorID]) REFERENCES [Instructor] ([ID]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303041924_CU6_M03_AddCourseInstructorLink')
BEGIN
    CREATE INDEX [IX_CourseInstructor_InstructorID] ON [CourseInstructor] ([InstructorID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303041924_CU6_M03_AddCourseInstructorLink')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220303041924_CU6_M03_AddCourseInstructorLink', N'6.0.2');
END;
GO

COMMIT;
GO

