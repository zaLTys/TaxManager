using Microsoft.EntityFrameworkCore;
using TaxManager.Core.Models;

namespace TaxManager.Api.Contexts
{
    class TaxRepositoryContext : DbContext
    {
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<TaxEntry> TaxEntries { get; set; }

        public TaxRepositoryContext(DbContextOptions<TaxRepositoryContext> options)
            : base(options)
        {
            //  Database.EnsureCreated();
        }
    }
}
