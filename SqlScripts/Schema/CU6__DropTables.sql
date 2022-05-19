USE ContosoU_dn6_dev;
GO

Declare   @DoClean bit;
--Declare @DoClean bit=1;

--exec sp_help 'Enrollment';

If ISNULL(@DoClean,0) != 1 Begin
	RAISERROR('Run again with @DoClean=1 to drop tables', 16, 1);
End Else Begin
	Print '';

	IF OBJECT_ID(N'[_coursesPresentationTypes]') IS NOT NULL BEGIN
		DROP TABLE dbo._coursesPresentationTypes;
		Print 'Dropped table _coursesPresentationTypes';
	END ELSE BEGIN
		Print 'Table _coursesPresentationTypes does not exist';
	END;

	IF OBJECT_ID(N'[CourseInstructor]') IS NOT NULL BEGIN
		DROP TABLE dbo.CourseInstructor;
		Print 'Dropped table CourseInstructor';
	END ELSE BEGIN
		Print 'Table CourseInstructor does not exist';
	END;

	IF OBJECT_ID(N'[Enrollment]') IS NOT NULL BEGIN
		DROP TABLE dbo.Enrollment;
		Print 'Dropped table Enrollment';
	END ELSE BEGIN
		Print 'Table Enrollment does not exist';
	END;

	IF OBJECT_ID(N'[OfficeAssignment]') IS NOT NULL BEGIN
		DROP TABLE dbo.OfficeAssignment;
		Print 'Dropped table OfficeAssignment';
	END ELSE BEGIN
		Print 'Table OfficeAssignment does not exist';
	END;

	IF OBJECT_ID(N'[Course]') IS NOT NULL BEGIN
		DROP TABLE dbo.Course;
		Print 'Dropped table Course';
	END ELSE BEGIN
		Print 'Table Course does not exist';
	END;

	IF OBJECT_ID(N'[Department]') IS NOT NULL BEGIN
		DROP TABLE dbo.Department;
		Print 'Dropped table Department';
	END ELSE BEGIN
		Print 'Table Department does not exist';
	END;

	IF OBJECT_ID(N'[Instructor]') IS NOT NULL BEGIN
		DROP TABLE dbo.Instructor;
		Print 'Dropped table Instructor';
	END ELSE BEGIN
		Print 'Table Instructor does not exist';
	END;

	IF OBJECT_ID(N'[Student]') IS NOT NULL BEGIN
		DROP TABLE dbo.Student;
		Print 'Dropped table Student';
	END ELSE BEGIN
		Print 'Table Student does not exist';
	END;

	IF OBJECT_ID(N'[xLookups2cKey]') IS NOT NULL BEGIN
		DROP TABLE dbo.xLookups2cKey;
		Print 'Dropped table xLookups2cKey';
	END ELSE BEGIN
		Print 'Table xLookups2cKey does not exist';
	END;

	IF OBJECT_ID(N'[xLookupTypes]') IS NOT NULL BEGIN
		DROP TABLE dbo.xLookupTypes;
		Print 'Dropped table xLookupTypes';
	END ELSE BEGIN
		Print 'Table xLookupTypes does not exist';
	END;

	IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NOT NULL BEGIN
		DROP TABLE dbo.__EFMigrationsHistory;
		Print 'Dropped table __EFMigrationsHistory';
	END ELSE BEGIN
		Print 'Table __EFMigrationsHistory does not exist';
	END;

End;

GO
