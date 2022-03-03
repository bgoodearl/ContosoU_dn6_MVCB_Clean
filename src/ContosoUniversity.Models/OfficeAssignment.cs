using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class OfficeAssignment
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private OfficeAssignment()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public OfficeAssignment(Instructor instructor, string location)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Guard.Against.Null(instructor, nameof(instructor));
            Guard.Against.Zero(instructor.ID, "instructor.ID");
            Guard.Against.NullOrWhiteSpace(location, nameof(location));
            Instructor = instructor;
            InstructorID = instructor.ID;
            Location = location;
        }

        //[Key]
        //[ForeignKey("Instructor")]
        public int InstructorID { get; set; }

        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; } = string.Empty;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual Instructor Instructor { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
