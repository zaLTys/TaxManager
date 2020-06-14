using Microsoft.EntityFrameworkCore;
using TaxManager.Core.Models;

namespace TaxManager.Api.Contexts
{
    public class TaxRepositoryContext : DbContext
    {
        public DbSet<MunicipalityDto> Municipalities { get; set; }
        public DbSet<TaxEntryDto> TaxEntries { get; set; }

        public TaxRepositoryContext(DbContextOptions<TaxRepositoryContext> options)
            : base(options)
        {
            //  Database.EnsureCreated();
        }
    }
}
