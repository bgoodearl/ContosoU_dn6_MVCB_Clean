using Ardalis.GuardClauses;
using CU.Application.Common.Interfaces;
using CU.Application.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace ContosoUniversity.Controllers
{
    public class CUControllerBase : Controller
    {
        public CUControllerBase(IHttpContextAccessor httpContextAccessor)
        {
            Guard.Against.Null(httpContextAccessor, nameof(httpContextAccessor));
            HttpContextAccessor = httpContextAccessor;
            Guard.Against.Null(httpContextAccessor.HttpContext, nameof(httpContextAccessor.HttpContext));
            Guard.Against.Null(httpContextAccessor.HttpContext.RequestServices, nameof(httpContextAccessor.HttpContext.RequestServices));
        }

        #region Read Only variables

        protected IHttpContextAccessor HttpContextAccessor { get; }
        private ISender _mediator = null!;

        #endregion Read Only variables

        #region Repositories / DB access

        private ISchoolRepositoryFactory _schoolRepositoryFactory = null!;
        private ISchoolViewDataRepositoryFactory _schoolViewDataRepositoryFactory = null!;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        protected ISender Mediator => _mediator ??= HttpContextAccessor.HttpContext.RequestServices.GetRequiredService<ISender>();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        protected ISchoolRepositoryFactory SchoolRepositoryFactory =>
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _schoolRepositoryFactory ??= HttpContextAccessor.HttpContext.RequestServices.GetRequiredService<ISchoolRepositoryFactory>();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        protected ISchoolViewDataRepositoryFactory SchoolViewDataRepositoryFactory =>
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _schoolViewDataRepositoryFactory ??= HttpContextAccessor.HttpContext.RequestServices.GetRequiredService<ISchoolViewDataRepositoryFactory>();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        protected ISchoolRepository GetSchoolRepository()
        {
            return SchoolRepositoryFactory.GetSchoolRepository();
        }

        protected ISchoolViewDataRepository GetSchoolViewDataRepository()
        {
            return SchoolViewDataRepositoryFactory.GetViewDataRepository();
        }

        #endregion Repositories / DB access

        protected async Task<TResponse> SendQueryAsync<TResponse>(IRequest<TResponse> request)
        {
            return await Mediator.Send(request);
        }

    }
}
