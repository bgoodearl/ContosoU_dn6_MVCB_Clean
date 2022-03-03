﻿using CU.Application.Common.Interfaces;
using CU.Application.Shared.ViewModels.Instructors;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Controllers
{
    [Route("[Controller]/[Action]")]
    public class InstructorsController : CUControllerBase
    {
        public InstructorsController(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
        }

        [Route("~/[Controller]")]
        public async Task<IActionResult> Index()
        {
            using (ISchoolRepository repo = GetSchoolRepository())
            {
                List<InstructorListItem> instructors = await repo.GetInstructorListItemsNoTrackingAsync();
                return View(instructors);
            }
        }
    }
}
