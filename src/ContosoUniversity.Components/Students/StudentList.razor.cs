﻿using ContosoUniversity.Components.EventModels;
using ContosoUniversity.Components.Navigation;
using CU.Application.Shared.Common.Models;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.Models.SchoolDtos;
using CU.Application.Shared.ViewModels;
using CU.Application.Shared.ViewModels.Students;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using static CU.Application.Shared.CommonDefs;

namespace ContosoUniversity.Components.Students
{
    public partial class StudentList
    {
        [Parameter] public SchoolItemViewModel? StudentsVM { get; set; }

        [Parameter] public EventCallback<SchoolItemEventArgs> StudentAction { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [Inject] ILogger<StudentList> Logger { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [Inject] ISender Mediator { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected bool Loading { get; set; }

        protected Pager? childPager;

        public IEnumerable<StudentListItem> StudentItemList { get; set; } = new List<StudentListItem>();

        #region data access

        protected async Task<LoadDataPageResult> LoadDataFromDb(LoadDataPagerEventArgs args)
        {
            LoadDataPageResult loadResult = new LoadDataPageResult();
            if ((Mediator != null) && !Loading)
            {
                Loading = true;
                GetStudentListItemsWithPaginationQuery query = new GetStudentListItemsWithPaginationQuery
                {
                    PageNumber = args.PageToLoad,
                    PageSize = args.PageSize
                };
                PaginatedList<StudentListItem> result = await Mediator.Send(query);
                if (result != null)
                {
                    if (Logger != null) { Logger.LogInformation($"StudentList.LoadDataFromDb page({query.PageNumber}) result page = {result.PageNumber}, itemCount = {result.Items.Count}"); }
                    StudentItemList = result.Items;
                    loadResult.PageNumber = result.PageNumber;
                    loadResult.TotalRecords = result.TotalCount;
                    loadResult.TotalPages = result.TotalPages;
                }
                else
                {
                    loadResult.TotalPages = 0;
                    loadResult.TotalRecords = 0;
                }
                this.StateHasChanged();
                Loading = false;
            }
            return loadResult;
        }

        #endregion data access

        #region events

        public async Task OnItemDelete(StudentListItem item)
        {
            SchoolItemEventArgs args = new SchoolItemEventArgs
            {
                ItemID = item.ID,
                UIMode = UIMode.Delete
            };
            await StudentAction.InvokeAsync(args);
        }

        public async Task OnItemDetails(StudentListItem item)
        {
            SchoolItemEventArgs args = new SchoolItemEventArgs
            {
                ItemID = item.ID,
                UIMode = UIMode.Details
            };
            await StudentAction.InvokeAsync(args);
        }

        public async Task OnItemEdit(StudentListItem item)
        {
            SchoolItemEventArgs args = new SchoolItemEventArgs
            {
                ItemID = item.ID,
                UIMode = UIMode.Edit
            };
            await StudentAction.InvokeAsync(args);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            bool shouldLoad = (Mediator != null) && !Loading && (StudentItemList.Count() == 0);
            if (Logger != null) { Logger.LogDebug($"StudentList.OnAfterRenderAsync: First Render == {firstRender}, shouldLoad={shouldLoad}"); }

            if (shouldLoad)
            {
                if (childPager != null)
                {
                    await childPager.ResetToFirstPage();
                }
            }
        }

        ////Uncomment OnInitializedAsync() if needed for debugging problems with initial loading
        //protected override async Task OnInitializedAsync()
        //{
        //    bool shouldLoad = ((Mediator != null) && !Loading && (StudentItemList.Count() == 0));
        //    if (Logger != null) { Logger.LogDebug($"StudentList.OnInitializedAsync shouldLoad={shouldLoad}"); }
        //}

        #endregion events

    }
}
