using Ardalis.GuardClauses;
using CU.SharedKernel.Base;
using CU.SharedKernel.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Course : EntityBaseT<int>, IHasDomainEvents
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Course()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        public Course(int courseID, string title, Department department)
        {
            Guard.Against.OutOfRange(courseID, nameof(courseID), 1, int.MaxValue);
            Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Guard.Against.Null(department, nameof(department));
            Guard.Against.Zero(department.DepartmentID, nameof(department.DepartmentID));
            CourseID = courseID;
            Title = title;
            Department = department;
            DepartmentID = department.DepartmentID;
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Display(Name = "Number")]
        public int CourseID { get; set; }

        [NotMapped]
        public override int Id { get { return CourseID; } }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Range(0, 5)]
        public int Credits { get; set; }

        public int DepartmentID { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual Department Department { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        //public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}