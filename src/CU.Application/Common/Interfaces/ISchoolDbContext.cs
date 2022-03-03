﻿using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace CU.Application.Common.Interfaces
{
    public interface ISchoolDbContext : IDisposable
    {
        DbSet<Course> Courses { get; }
        DbSet<Department> Departments { get; }
        //DbSet<Enrollment> Enrollments { get; }
        DbSet<Instructor> Instructors { get; }
        DbSet<OfficeAssignment> OfficeAssignments { get; }
        DbSet<Student> Students { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<int> SeedInitialDataAsync();
    }
}
