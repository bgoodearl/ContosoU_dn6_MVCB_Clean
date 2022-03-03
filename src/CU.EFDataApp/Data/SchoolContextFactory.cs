using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using CU.Infrastructure.Persistence;

namespace CU.EFDataApp.Data
{
    public class SchoolContextFactory : IDesignTimeDbContextFactory<SchoolDbContext>
    {
        public SchoolDbContext CreateDbContext(string[] args)
        {
            const string connectionStringPath = "ConnectionStrings:SchoolDbContext";
            var config = ConfigHelper.GetConfiguration();
            var connectionString = config[connectionStringPath];
            if (string.IsNullOrWhiteSpace(connectionString)) throw new InvalidOperationException($"{connectionStringPath} not found");
            var optionsBuilder = SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<SchoolDbContext>(), connectionString);
            return new SchoolDbContext(optionsBuilder.Options);
        }
    }
}
