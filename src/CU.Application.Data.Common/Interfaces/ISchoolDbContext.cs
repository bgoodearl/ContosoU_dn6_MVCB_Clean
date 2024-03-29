﻿using ContosoUniversity.Models;
using ContosoUniversity.Models.Lookups;
using Microsoft.EntityFrameworkCore;

namespace CU.Application.Data.Common.Interfaces
{
    public interface ISchoolDbContext : IDisposable
    {
        DbSet<Course> Courses { get; }
        DbSet<Department> Departments { get; }
        DbSet<Enrollment> Enrollments { get; }
        DbSet<Instructor> Instructors { get; }
        DbSet<OfficeAssignment> OfficeAssignments { get; }
        DbSet<Student> Students { get; }


        #region Lookups

        DbSet<LookupBaseWith2cKey> LookupsWith2cKey { get; }
        DbSet<LookupType> LookupTypes { get; }

        DbSet<CoursePresentationType> CoursePresentationTypes { get; }
        DbSet<DepartmentFacilityType> DepartmentFacilityTypes { get; }
        DbSet<RandomLookupType> RandomLookupTypes { get; }

        #endregion Lookups


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<bool> SeedDataNeededAsync();
        Task<int> SeedInitialDataAsync();
    }
}
