using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using CU.Application.Common.Interfaces;

namespace CU.Infrastructure.Persistence
{
    public partial class SchoolDbContext : DbContext, ISchoolDbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SchoolDbContext(DbContextOptions options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            : base(options)
        {
            ContextInstance = ++contextInstanceSeed;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SchoolDbContext(string connectionString)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            : base(GetOptions(connectionString))
        {
            ContextInstance = ++contextInstanceSeed;
        }

        internal static DbContextOptions<SchoolDbContext> GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<SchoolDbContext>(), connectionString).Options;
        }

        private static int contextInstanceSeed = 0;
        protected int ContextInstance { get; }

        #region Persistent Entities

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<OfficeAssignment> OfficeAssignments => Set<OfficeAssignment>();
        public DbSet<Student> Students => Set<Student>();

        #endregion Persistent Entities

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SeedInitialDataAsync()
        {
            return await SchoolDbContextSeed.SeedDefaultDataAsync(this);
        }
    }
}
