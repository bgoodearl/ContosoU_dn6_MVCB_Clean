﻿using CU.SharedKernel.Base;
using CU.SharedKernel.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
#if false
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
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
#endif
}

