using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;

namespace CU.Infrastructure.Persistence
{
    public partial class SchoolDbContext //SchoolContext2.cs
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region School Entities

            modelBuilder.Entity<Course>(e =>
            {
                e.HasKey(c => c.CourseID);
                e.Property(c => c.CourseID).ValueGeneratedNever();
                e.ToTable("Course");
                e.HasOne(e => e.Department).WithMany()
                    .HasForeignKey(e => e.DepartmentID).IsRequired(true).OnDelete(DeleteBehavior.Restrict);

                e.HasMany(c => c.Instructors).WithMany(i => i.Courses)
                    .UsingEntity<Dictionary<string, object>>(
                        "CourseInstructor",
                        l => l.HasOne<Instructor>().WithMany().HasForeignKey("InstructorID")
                        .OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_CourseInstructor_Instructor"),
                        r => r.HasOne<Course>().WithMany().HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_CourseInstructor_Course"),
                        j =>
                        {
                            j.HasKey("CourseID", "InstructorID");
                            j.ToTable("CourseInstructor");
                        });
            });

            modelBuilder.Entity<Department>(e =>
            {
                e.HasKey(c => c.DepartmentID);
                e.ToTable("Department");
                e.Property(g => g.RowVersion).IsRequired().IsRowVersion().IsConcurrencyToken();
                e.Property(d => d.StartDate).HasColumnType("datetime");
                e.HasOne(d => d.Administrator).WithMany()
                    .HasForeignKey(d => d.InstructorID).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Enrollment>(e =>
            {
                e.HasKey(c => c.EnrollmentID);
                e.ToTable("Enrollment");
                e.HasOne(e => e.Course).WithMany(c => c.Enrollments).HasForeignKey(e => e.CourseID).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(e => e.Student).WithMany(s => s.Enrollments).HasForeignKey(e => e.StudentID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Instructor>(e =>
            {
                e.HasKey(c => c.ID);
                e.ToTable("Instructor");
                e.Property(i => i.HireDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OfficeAssignment>(e =>
            {
                e.HasKey(e => e.InstructorID);
                e.ToTable("OfficeAssignment");
                e.HasOne(oa => oa.Instructor).WithOne(i => i.OfficeAssignment).HasForeignKey<OfficeAssignment>(oa => oa.InstructorID).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(c => c.ID);
                e.ToTable("Student");
                e.Property(s => s.EnrollmentDate).HasColumnType("datetime");
            });

            #endregion School Entities
        }
    }
}
