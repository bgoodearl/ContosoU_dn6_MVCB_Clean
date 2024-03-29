﻿using Ardalis.GuardClauses;
using CU.SharedKernel.Base;
//using CU.SharedKernel.Interfaces; //TODO: Restore when Domain Events are figured out
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Student : EntityBaseT<int> //, IHasDomainEvents //TODO: Restore when Domain Events are figured out
    {
        private Student()
        {

        }

        public Student(string lastName, string firstMidName, DateTime enrollmentDate)
        {
            Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
            Guard.Against.NullOrWhiteSpace(firstMidName, nameof(firstMidName));
            Guard.Against.OutOfSQLDateRange(enrollmentDate, nameof(enrollmentDate));
            LastName = lastName;
            FirstMidName = firstMidName;
            EnrollmentDate = enrollmentDate;
        }

        public int ID { get; set; }

        [NotMapped]
        public override int Id { get { return ID; } }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Column("LastName")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstMidName")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}