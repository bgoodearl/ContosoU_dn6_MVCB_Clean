
namespace CU.Application.Shared.ViewModels.Courses
{
    public class CoursesViewModel
    {
        public int? CourseID { get; set; }
        public IEnumerable<CourseListItem> CourseList { get; set; } = new List<CourseListItem>();
        public int ViewMode { get; set; }
    }
}
