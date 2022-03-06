using MediatR;
using CU.Application.Shared.Models.SchoolDtos;

namespace CU.Application.Shared.DataRequests.SchoolItems.Queries
{
    public class GetCourseListItemsQuery : IRequest<List<CourseListItemDto>>
    {
        public CourseSortOrder SortOrder { get; set; }
        public int? InstructorID { get; set; }
    }
}
