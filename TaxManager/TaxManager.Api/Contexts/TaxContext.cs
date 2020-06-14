using System;
using Microsoft.EntityFrameworkCore;
using TaxManager.Api.Entities;
using TaxManager.Api.Models;

namespace TaxManager.Api.Contexts
{
    public class TaxContext : DbContext
    {
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<TaxEntry> TaxEntries { get; set; }

        public TaxContext(DbContextOptions<TaxContext> options)
            : base(options)
        {
            //  Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipality>()
                .HasData(
                    new Municipality()
                    {
                        Id = 1,
                        Name = "Vilnius",
                    },
                    new Municipality()
                    {
                        Id = 2,
                        Name = "Kaunas",

                    });

            modelBuilder.Entity<TaxEntry>()
              .HasData(
                new TaxEntry()
                {
                    Id = 1,
                    MunicipalityId = 1,
                    DateFrom = new DateTime(2016, 1, 1),
                    DateTo = new DateTime(2017, 1, 1),
                    TaxValue = 0.2m,
                    TaxType = (int)TaxTypes.Yearly

                },
                new TaxEntry()
                {
                    Id = 2,
                    MunicipalityId = 1,
                    DateFrom = new DateTime(2016, 5, 1),
                    DateTo = new DateTime(2016, 6, 1),
                    TaxValue = 0.4m,
                    TaxType = (int)TaxTypes.Monthly
                },
                new TaxEntry()
                {
                    Id = 3,
                    MunicipalityId = 1,
                    DateFrom = new DateTime(2016, 1, 1),
                    DateTo = new DateTime(2016, 1, 1),
                    TaxValue = 0.1m,
                    TaxType = (int)TaxTypes.Daily,
                },
                new TaxEntry()
                {
                    Id = 4,
                    MunicipalityId = 1,
                    DateFrom = new DateTime(2016, 12, 25),
                    DateTo = new DateTime(2016, 12, 26),
                    TaxValue = 0.1m,
                    TaxType = (int)TaxTypes.Daily
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
