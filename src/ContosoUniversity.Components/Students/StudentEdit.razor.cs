using CU.Application.Shared.DataRequests.SchoolItems.Commands;
using CU.Application.Shared.ViewModels.Students;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using CASE = CU.Application.Shared.Common.Exceptions;
using static CU.Application.Shared.CommonDefs;
using CU.Application.Shared.ViewModels;

namespace ContosoUniversity.Components.Students
{
    public partial class StudentEdit
    {
        [Parameter] public bool NewStudent { get; set; }

        [Parameter] public StudentEditDto? Student2Edit { get; set; }

        [Parameter] public EventCallback<SchoolItemEventArgs> StudentAction { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [Inject] protected ILogger<StudentEdit> Logger { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [Inject] ISender Mediator { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected string? Message { get; set; }

        private async Task HandleValidSubmitAsync()
        {
            if (NewStudent)
            {
                if (Student2Edit != null)
                {
                    var command = new CreateStudentItemCommand(Student2Edit);

                    try
                    {
                        int newStudentId = await Mediator.Send(command);
                        if (newStudentId != 0)
                        {
                            await OnReturnToList();
                        }
                    }
                    catch (CASE.ValidationException ex)
                    {
                        Logger.LogError(ex, "StudentEdit: HandleValidSubmitAsync {0}: {1}",
                            ex.GetType().Name, ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex, "StudentEdit: HandleValidSubmitAsync {0}: {1}",
                            ex.GetType().Name, ex.Message);
                    }
                }
            }
            else
            {

            }
        }

        public async Task OnReturnToList()
        {
            SchoolItemEventArgs args = new SchoolItemEventArgs
            {
                UIMode = UIMode.List
            };
            await StudentAction.InvokeAsync(args);
        }

    }
}
