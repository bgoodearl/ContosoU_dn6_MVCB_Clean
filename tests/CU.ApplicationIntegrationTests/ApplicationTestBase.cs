using CU.Application.Data.Common.Interfaces;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit.Abstractions;
using CIP = CU.Infrastructure.Persistence;

namespace CU.ApplicationIntegrationTests
{
    public class ApplicationTestBase : DbContextTestBase
    {
        public ApplicationTestBase(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base (testOutputHelper, fixture)
        {
        }

        protected async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            using (var scope = _fixture.GetServiceScopeFactory(_testOutputHelper).CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ISchoolDbContext>();
                CIP.SchoolDbContext? schoolDbContext = context as CIP.SchoolDbContext;
                schoolDbContext.Should().NotBeNull();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                return await schoolDbContext.FindAsync<TEntity>(keyValues);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }

        protected async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            _testOutputHelper.Should().NotBeNull();
            if (_testOutputHelper != null)
            {
                IServiceScopeFactory? scopeFactory = _fixture.GetService<IServiceScopeFactory>(_testOutputHelper);
                scopeFactory.Should().NotBeNull();

                if (scopeFactory != null)
                {
                    using var scope = scopeFactory.CreateScope();

                    var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

                    return await mediator.Send(request);
                }
            }
            throw new InvalidOperationException("Unexpected problem setting up for SendAsync");
        }

    }
}
