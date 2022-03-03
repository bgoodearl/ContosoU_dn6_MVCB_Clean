using MediatR;
using CU.Application.Shared.Models.SchoolDtos;

namespace CU.Application.Shared.DataRequests.SchoolItems.Queries
{
    public class GetDepartmentListItemsQuery : IRequest<List<DepartmentListItemDto>>
    {
    }
}
