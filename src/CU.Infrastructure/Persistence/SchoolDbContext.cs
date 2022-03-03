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
            InitializeDbSets();
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SchoolDbContext(string connectionString)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            : base(GetOptions(connectionString))
        {
            ContextInstance = ++contextInstanceSeed;
            InitializeDbSets();
        }

        /// <summary>
        /// InitializeDbSets - using lamda expressions for initializing DbSets
        /// caused problems after handling exception
        /// </summary>
        private void InitializeDbSets()
        {
            Courses = Set<Course>();
            Departments = Set<Department>();
            Enrollments = Set<Enrollment>();
            Instructors = Set<Instructor>();
            OfficeAssignments = Set<OfficeAssignment>();
            Students = Set<Student>();
        }

        internal static DbContextOptions<SchoolDbContext> GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<SchoolDbContext>(), connectionString).Options;
        }

        private static int contextInstanceSeed = 0;
        protected int ContextInstance { get; }

        #region Persistent Entities

        public DbSet<Course> Courses { get; private set; } // => Set<Course>();
        public DbSet<Department> Departments { get; private set; } //=> Set<Department>();
        public DbSet<Enrollment> Enrollments { get; private set; } //=> Set<Enrollment>();
        public DbSet<Instructor> Instructors { get; private set; } //=> Set<Instructor>();
        public DbSet<OfficeAssignment> OfficeAssignments { get; private set; } //=> Set<OfficeAssignment>();
        public DbSet<Student> Students { get; private set; } //=> Set<Student>();

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
