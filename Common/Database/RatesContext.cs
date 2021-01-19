using Common.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Database
{
    public class RatesContext : DbContext
    {
        public RatesContext(DbContextOptions<RatesContext> options) : base(options) 
        { }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Rate> Rates { get; set; }
    }
}
