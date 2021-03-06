﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaxManager.Api.Contexts;

namespace TaxManager.Api.Migrations
{
    [DbContext(typeof(TaxContext))]
    partial class TaxContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaxManager.Api.Entities.Municipality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Municipalities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Vilnius"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Kaunas"
                        });
                });

            modelBuilder.Entity("TaxManager.Api.Entities.TaxEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<int>("MunicipalityId")
                        .HasColumnType("int");

                    b.Property<int>("TaxType")
                        .HasColumnType("int");

                    b.Property<decimal>("TaxValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("MunicipalityId");

                    b.ToTable("TaxEntries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateFrom = new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateTo = new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MunicipalityId = 1,
                            TaxType = 1,
                            TaxValue = 0.2m
                        },
                        new
                        {
                            Id = 2,
                            DateFrom = new DateTime(2016, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateTo = new DateTime(2016, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MunicipalityId = 1,
                            TaxType = 2,
                            TaxValue = 0.4m
                        },
                        new
                        {
                            Id = 3,
                            DateFrom = new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateTo = new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MunicipalityId = 1,
                            TaxType = 4,
                            TaxValue = 0.1m
                        },
                        new
                        {
                            Id = 4,
                            DateFrom = new DateTime(2016, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateTo = new DateTime(2016, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MunicipalityId = 1,
                            TaxType = 4,
                            TaxValue = 0.1m
                        });
                });

            modelBuilder.Entity("TaxManager.Api.Entities.TaxEntry", b =>
                {
                    b.HasOne("TaxManager.Api.Entities.Municipality", "Municipality")
                        .WithMany("TaxEntries")
                        .HasForeignKey("MunicipalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
