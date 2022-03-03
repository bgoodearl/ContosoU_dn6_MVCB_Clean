using Ardalis.GuardClauses;
using CU.SharedKernel.Base;
using CU.SharedKernel.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment : EntityBaseT<int>, IHasDomainEvents
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Enrollment()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public Enrollment(Course course, Student student)
        {
            Guard.Against.Null(course);
            Guard.Against.Zero(course.CourseID, nameof(course.CourseID));
            Guard.Against.Null(student);
            Guard.Against.Zero(student.ID, nameof(student.ID));
            Course = course;
            CourseID = course.CourseID;
            Student = student;
            StudentID = student.ID;
        }


        public int EnrollmentID { get; set; }

        [NotMapped]
        public override int Id { get { return EnrollmentID; } }

        public int CourseID { get; set; }
        public int StudentID { get; set; }
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}

