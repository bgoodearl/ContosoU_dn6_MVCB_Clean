using CU.Application.Shared.Interfaces;
using CU.Application.Shared.ViewModels;
using CU.Application.Shared.ViewModels.Courses;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using static CU.Application.Shared.CommonDefs;

namespace ContosoUniversity.Components
{
    public partial class CourseEdit
    {
        [Parameter] public CourseEditDto Course2Edit { get; set; } = new CourseEditDto();

        [Parameter] public EventCallback<CourseEventArgs> CourseAction { get; set; }

        [Parameter] public bool NewCourse { get; set; }

        [Inject]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected ISchoolViewDataRepositoryFactory SchoolViewDataRepositoryFactory { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Inject]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected ILogger<CourseEdit> Logger { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected int CourseID { get; set; }

        protected List<IdItem> DepartmentList { get; set; } = new List<IdItem>();

        protected string? Message { get; set; }

        private async Task HandleValidSubmitAsync()
        {
            Message = null;
            try
            {
                ISchoolViewDataRepository dataHelper = SchoolViewDataRepositoryFactory.GetViewDataRepository();

                if (NewCourse)
                {
                    if (Course2Edit != null)
                    {
                        CourseActionResult saveResult = await dataHelper.SaveNewCourseAsync(Course2Edit);
                        if (saveResult.ChangeCount < 1)
                        {
                            if (!string.IsNullOrWhiteSpace(saveResult.ErrorMessage))
                            {
                                Message = saveResult.ErrorMessage;
                                Logger.LogWarning($"CourseEdit - SaveNew - {saveResult.ErrorMessage}");
                            }
                            else
                            {
                                Message = "No Changes";
                            }
                        }
                        else
                        {
                            await OnReturnToList();
                        }
                    }
                }
                else
                {
                    if (CourseID == -1)
                    {
                        Message = "Initialization Error - Contact Support";
                    }
                    else
                    {
                        if (Course2Edit != null)
                        {
                            CourseActionResult saveResult = await dataHelper.SaveCourseEditChangesAsync(CourseID, Course2Edit);
                            if (saveResult.ChangeCount < 1)
                            {
                                if (!string.IsNullOrWhiteSpace(saveResult.ErrorMessage))
                                {
                                    Message = saveResult.ErrorMessage;
                                    Logger.LogWarning($"CourseEdit ({CourseID} - {Course2Edit.CourseID}) - Save - {saveResult.ErrorMessage}");
                                }
                                else
                                {
                                    Message = "No Changes";
                                }
                            }
                            else
                            {
                                await OnReturnToList();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "CourseEdit-Save id={0} - {1}: {2}",
                    Course2Edit != null ? Course2Edit.CourseID : (int?)null,
                    ex.GetType().Name, ex.Message);
                Message = $"Failed to save changes for Course {CourseID}";
            }
        }

        protected override async Task OnInitializedAsync()
        {
            CourseID = -1;
            ISchoolViewDataRepository dataHelper = SchoolViewDataRepositoryFactory.GetViewDataRepository();
            if (NewCourse)
            {
                CourseID = 0;
                Course2Edit = new CourseEditDto
                {
                    CourseID = 0,
                    Title = ""
                };
                DepartmentList = await dataHelper.GetDepartmentsListAsync();
            }
            else
            {
                if (Course2Edit != null)
                {
                    CourseID = Course2Edit.CourseID;
                    if (Course2Edit.DepartmentID != 0)
                    {
                        Course2Edit.DepartmentIDstr = Course2Edit.DepartmentID.ToString();
                    }
                    DepartmentList = await dataHelper.GetDepartmentsListAsync();
                }
                else
                {
                    DepartmentList = new List<IdItem>();
                }
            }
        }

        public async Task OnReturnToList()
        {
            CourseEventArgs args = new CourseEventArgs
            {
                UIMode = UIMode.List
            };
            await CourseAction.InvokeAsync(args);
        }
    }
}
