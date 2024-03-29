﻿using ContosoUniversity.Components.EventModels;
using ContosoUniversity.Components.Navigation;
using CU.Application.Shared.Common.Models;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.Models.SchoolDtos;
using CU.Application.Shared.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using static CU.Application.Shared.CommonDefs;

namespace ContosoUniversity.Components.Courses
{
    public partial class CourseList
    {
        [Parameter] public SchoolItemViewModel? CoursesVM { get; set; }

        [Parameter] public EventCallback<SchoolItemEventArgs> SchoolItemAction { get; set; }

        [Inject] ISender? Mediator { get; set; }

        [Inject] ILogger<CourseList>? Logger { get; set; }

        protected bool Loading { get; set; }

        protected Pager? childPager;

        public IEnumerable<CourseListItemDto> CourseItemList { get; set; } = new List<CourseListItemDto>();


        #region data access

        protected async Task<LoadDataPageResult> LoadDataFromDb(LoadDataPagerEventArgs args)
        {
            LoadDataPageResult loadResult = new LoadDataPageResult();
            if ((Mediator != null) && !Loading)
            {
                Loading = true;
                GetCourseListItemsWithPaginationQuery query = new GetCourseListItemsWithPaginationQuery
                {
                    PageNumber = args.PageToLoad,
                    PageSize = args.PageSize
                };
                PaginatedList<CourseListItemDto> result = await Mediator.Send(query);
                if (result != null)
                {
                    if (Logger != null) { Logger.LogInformation($"CourseList.LoadDataFromDb page({query.PageNumber}) result page = {result.PageNumber}, itemCount = {result.Items.Count}"); }
                    CourseItemList = result.Items;
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

        public async Task OnItemDelete(CourseListItemDto item)
        {
            SchoolItemEventArgs args = new SchoolItemEventArgs
            {
                ItemID = item.CourseID,
                UIMode = UIMode.Delete
            };
            await SchoolItemAction.InvokeAsync(args);
        }

        public async Task OnItemDetails(CourseListItemDto item)
        {
            SchoolItemEventArgs args = new SchoolItemEventArgs
            {
                ItemID = item.CourseID,
                UIMode = UIMode.Details
            };
            await SchoolItemAction.InvokeAsync(args);
        }

        public async Task OnItemEdit(CourseListItemDto item)
        {
            SchoolItemEventArgs args = new SchoolItemEventArgs
            {
                ItemID = item.CourseID,
                UIMode = UIMode.Edit
            };
            await SchoolItemAction.InvokeAsync(args);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            bool shouldLoad = (Mediator != null) && !Loading && (CourseItemList.Count() == 0);
            if (Logger != null) { Logger.LogDebug($"CourseList.OnAfterRenderAsync: First Render == {firstRender}, shouldLoad={shouldLoad}"); }

            if (shouldLoad)
            {
                if (childPager != null)
                {
                    await childPager.ResetToFirstPage();
                }
            }
        }

        //Uncomment OnInitializedAsync() if needed for debugging problems with initial loading
        //protected override async Task OnInitializedAsync()
        //{
        //    bool shouldLoad = ((Mediator != null) && !Loading && (CourseItemList.Count() == 0));
        //    if (Logger != null) { Logger.LogDebug($"CourseList.OnInitializedAsync shouldLoad={shouldLoad}"); }
        //}

        #endregion events

    }
}
