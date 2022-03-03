USE ContosoUniversity22; --Database used for tests
GO

Declare @UseMigrationHistory bit = 0;
--Declare @UseMigrationHistory bit = 1;
DECLARE @CurrentMigration [nvarchar](max)

If ISNULL(@UseMigrationHistory,0) = 1 Begin

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
End;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Instructor] (
    [ID] int NOT NULL IDENTITY,
    [LastName] nvarchar(50) NOT NULL,
    [FirstName] nvarchar(50) NOT NULL,
    [HireDate] datetime NOT NULL,
    CONSTRAINT [PK_Instructor] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Student] (
    [ID] int NOT NULL IDENTITY,
    [LastName] nvarchar(50) NOT NULL,
    [FirstMidName] nvarchar(50) NOT NULL,
    [EnrollmentDate] datetime NOT NULL,
    CONSTRAINT [PK_Student] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Department] (
    [DepartmentID] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [Budget] money NOT NULL,
    [StartDate] datetime NOT NULL,
    [InstructorID] int NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY ([DepartmentID]),
    CONSTRAINT [FK_Department_Instructor_InstructorID] FOREIGN KEY ([InstructorID]) REFERENCES [Instructor] ([ID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [OfficeAssignment] (
    [InstructorID] int NOT NULL,
    [Location] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_OfficeAssignment] PRIMARY KEY ([InstructorID]),
    CONSTRAINT [FK_OfficeAssignment_Instructor_InstructorID] FOREIGN KEY ([InstructorID]) REFERENCES [Instructor] ([ID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Course] (
    [CourseID] int NOT NULL,
    [Title] nvarchar(50) NOT NULL,
    [Credits] int NOT NULL,
    [DepartmentID] int NOT NULL,
    CONSTRAINT [PK_Course] PRIMARY KEY ([CourseID]),
    CONSTRAINT [FK_Course_Department_DepartmentID] FOREIGN KEY ([DepartmentID]) REFERENCES [Department] ([DepartmentID]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Course_DepartmentID] ON [Course] ([DepartmentID]);
GO

CREATE INDEX [IX_Department_InstructorID] ON [Department] ([InstructorID]);
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NOT NULL BEGIN
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220303023706_CU6_M01_ExistingSchemaBase_2022', N'6.0.2');
END;
GO

COMMIT;
GO

