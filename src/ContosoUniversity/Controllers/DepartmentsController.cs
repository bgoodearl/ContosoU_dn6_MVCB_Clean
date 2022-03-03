﻿using CU.Application.Common.Interfaces;
using CU.Application.Shared.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Controllers
{
    [Route("[Controller]/[Action]")]
    public class DepartmentsController : CUControllerBase
    {
        public DepartmentsController(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
        }

        [Route("~/[Controller]")]
        public async Task<IActionResult> Index()
        {
            using (ISchoolRepository repo = GetSchoolRepository())
            {
                List<DepartmentListItem> departments = await repo.GetDepartmentListItemsNoTrackingAsync();
                return View(departments);
            }
        }
    }
}
