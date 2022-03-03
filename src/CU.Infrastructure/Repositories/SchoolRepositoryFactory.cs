using Ardalis.GuardClauses;
using CU.Application.Common.Interfaces;
using CU.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CU.Infrastructure.Repositories
{
    public class SchoolRepositoryFactory : ISchoolRepositoryFactory
    {
        private string ConnectionString { get; }
        public SchoolRepositoryFactory(string connectionString)
        {
            Guard.Against.NullOrWhiteSpace(connectionString, nameof(connectionString));
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public ISchoolRepository GetSchoolRepository()
        {
            DbContextOptions<SchoolDbContext> options = SchoolDbContext.GetOptions(ConnectionString);
            return new SchoolRepository(new SchoolDbContext(options));
        }

    }
}
