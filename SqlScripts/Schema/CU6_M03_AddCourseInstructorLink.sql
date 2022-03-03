USE ContosoUniversity22; --Database used for tests
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [CourseInstructor] (
    [CourseID] int NOT NULL,
    [InstructorID] int NOT NULL,
    CONSTRAINT [PK_CourseInstructor] PRIMARY KEY ([CourseID], [InstructorID]),
    CONSTRAINT [FK_CourseInstructor_Course] FOREIGN KEY ([CourseID]) REFERENCES [Course] ([CourseID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CourseInstructor_Instructor] FOREIGN KEY ([InstructorID]) REFERENCES [Instructor] ([ID]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_CourseInstructor_InstructorID] ON [CourseInstructor] ([InstructorID]);
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NOT NULL BEGIN
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220303041924_CU6_M03_AddCourseInstructorLink', N'6.0.2');
END;
GO

COMMIT;
GO

