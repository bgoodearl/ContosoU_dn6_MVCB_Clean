using ContosoUniversity.Components.EventModels;
using ContosoUniversity.Components.Navigation;
using CU.Application.Shared.Common.Models;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.ViewModels;
using CU.Application.Shared.ViewModels.Instructors;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace ContosoUniversity.Components.Instructors
{
    public partial class InstructorList
    {
        [Parameter] public SchoolItemViewModel InstructorsVM { get; set; } = new SchoolItemViewModel();

        [Parameter] public EventCallback<SchoolItemEventArgs> InstructorAction { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [Inject] ISender Mediator { get; set; }

        [Inject] ILogger<InstructorList> Logger { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected bool Loading { get; set; }

        protected Pager? childPager;

        public IEnumerable<InstructorListItem> InstructorItemList { get; set; } = new List<InstructorListItem>();

        #region data access

        protected async Task<LoadDataPageResult> LoadDataFromDb(LoadDataPagerEventArgs args)
        {
            LoadDataPageResult loadResult = new LoadDataPageResult();
            if ((Mediator != null) && !Loading)
            {
                GetInstructorListItemsWithPaginationQuery query = new GetInstructorListItemsWithPaginationQuery
                {
                    PageNumber = args.PageToLoad,
                    PageSize = args.PageSize
                };
                PaginatedList<InstructorListItem> result = await Mediator.Send(query);
                if (result != null)
                {
                    if (Logger != null) { Logger.LogInformation($"InstructorList.LoadDataFromDb page({query.PageNumber}) result page = {result.PageNumber}, itemCount = {result.Items.Count}"); }
                    InstructorItemList = result.Items;
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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            bool shouldLoad = (Mediator != null) && !Loading && (InstructorItemList.Count() == 0);
            if (Logger != null) { Logger.LogDebug($"InstructorList.OnAfterRenderAsync: First Render == {firstRender}, shouldLoad={shouldLoad}"); }

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
        //    bool shouldLoad = ((Mediator != null) && !Loading && (InstructorItemList.Count() == 0));
        //    if (Logger != null) { Logger.LogDebug($"InstructorList.OnInitializedAsync shouldLoad={shouldLoad}"); }
        //}

        #endregion events
    }
}
