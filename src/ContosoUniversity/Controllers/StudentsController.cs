using CU.Application.Common.Interfaces;
using CU.Application.Shared.ViewModels.Students;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Controllers
{
    [Route("[Controller]/[Action]")]
    public partial class StudentsController : CUControllerBase
    {
        public StudentsController(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
        }

        [Route("~/[Controller]")]
        public async Task<IActionResult> Index()
        {
            using (ISchoolRepository repo = GetSchoolRepository())
            {
                List<StudentListItem> students = await repo.GetStudentListItemsNoTrackingAsync();
                return View(students);
            }
        }
    }
}
