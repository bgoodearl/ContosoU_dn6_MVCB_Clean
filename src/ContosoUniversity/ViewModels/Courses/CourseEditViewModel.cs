﻿using CU.Application.Shared.ViewModels.Courses;

namespace ContosoUniversity.ViewModels.Courses
{
    public class CourseEditViewModel : Shared.CourseSharedViewModel
    {
        public CourseEditDto Course { get; set; } = new CourseEditDto();
        public bool NewCourse { get; set; }
    }
}
