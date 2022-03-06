﻿using CU.Application.Shared.ViewModels;
using CU.Application.Shared.ViewModels.Students;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using static CU.Application.Shared.CommonDefs;

namespace ContosoUniversity.Components.Students
{
    public partial class Students
    {
        [Parameter] public SchoolItemViewModel? StudentsVM { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [Inject] protected ILogger<Students> Logger { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        //protected StudentEditDto Student2Edit { get; set; }
        protected string? Message { get; set; }
        protected UIMode UIMode { get; set; }
        protected StudentListItem? SelectedStudent { get; set; }
        //protected StudentItem SelectedCourseDetails { get; set; }
        protected StudentEditDto? Student2Edit { get; set; }


        public async Task StudentAction(SchoolItemEventArgs args)
        {
            if (args != null)
            {
                Message = null;
                try
                {
                    if (args.ItemID != 0)
                    {

                    }
                    else
                    {
                        if (args.UIMode == UIMode.List)
                        {
                            if (StudentsVM != null)
                            {
                                StudentsVM.ViewMode = 0; //Clear initial ViewMode from page load
                            }
                            UIMode = args.UIMode;
                        }
                        else if (args.UIMode == UIMode.Create)
                        {
                            Student2Edit = new StudentEditDto
                            {
                                EnrollmentDate = DateTime.Now.Date
                            };
                            UIMode = args.UIMode;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Students-StudentAction id={0}, uiMode={1} - {2}: {3}",
                        args.ItemID, args.UIMode, ex.GetType().Name, ex.Message);
                    Message = $"Error setting up {args.UIMode} with StudentID = {args.ItemID} - contact Support";
                }
            }
        }

        #region events

        protected async Task OnCreateStudent()
        {
            SchoolItemEventArgs args = new SchoolItemEventArgs
            {
                UIMode = UIMode.Create
            };
            await StudentAction(args);
        }

        #endregion events

    }
}
