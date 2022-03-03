using CU.SharedKernel.Base;
using CU.SharedKernel.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Instructor : EntityBaseT<int>, IHasDomainEvents
    {
        public int ID { get; set; }

        [NotMapped]
        public override int Id { get { return ID; } }

        [Display(Name = "Last Name"), StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; } = string.Empty;

        [Column("FirstName"), Display(Name = "First Name"), StringLength(50, MinimumLength = 1)]
        public string FirstMidName { get; set; } = string.Empty;

        [DataType(DataType.Date), Display(Name = "Hire Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstMidName; }
        }

        //public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public virtual OfficeAssignment? OfficeAssignment { get; set; }
    }
}
