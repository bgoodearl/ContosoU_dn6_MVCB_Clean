using CU.Application.Shared.ViewModels.Courses;
using Microsoft.AspNetCore.Components;

namespace ContosoUniversity.Components
{
    public partial class CourseDetails
    {
        [Parameter] public CourseItem? Course { get; set; }
    }
}
