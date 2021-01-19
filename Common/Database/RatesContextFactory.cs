using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Common.Database
{
    public class RatesContextFactory : IDesignTimeDbContextFactory<RatesContext>
    {
        public RatesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RatesContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ArvatoProgrammingTask.Rates;Trusted_Connection=True;");

            return new RatesContext(optionsBuilder.Options);
        }
    }
}
