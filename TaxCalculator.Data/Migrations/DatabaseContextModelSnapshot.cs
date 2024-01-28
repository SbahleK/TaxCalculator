﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaxCalculator.Data;

#nullable disable

namespace TaxCalculator.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("TaxCalculator.Data.Entities.CalculatedTax", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Income")
                        .HasColumnType("TEXT");

                    b.Property<long>("TaxCalculationTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TaxCharged")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CalculatedTaxes");
                });

            modelBuilder.Entity("TaxCalculator.Data.Entities.ProgressiveTaxRate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("From")
                        .HasColumnType("TEXT");

                    b.Property<string>("Rate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("To")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ProgressiveTaxRates");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            From = 0m,
                            Rate = "10%",
                            To = 8350m
                        },
                        new
                        {
                            Id = 2L,
                            From = 8351m,
                            Rate = "15%",
                            To = 33950m
                        },
                        new
                        {
                            Id = 3L,
                            From = 33951m,
                            Rate = "25%",
                            To = 82250m
                        },
                        new
                        {
                            Id = 4L,
                            From = 82251m,
                            Rate = "28%",
                            To = 171550m
                        },
                        new
                        {
                            Id = 5L,
                            From = 171551m,
                            Rate = "33%",
                            To = 372950m
                        },
                        new
                        {
                            Id = 6L,
                            From = 372951m,
                            Rate = "35%",
                            To = 79228162514264337593543950335m
                        });
                });

            modelBuilder.Entity("TaxCalculator.Data.Entities.TaxCalculationType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TaxCalculationTypes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            PostalCode = "7441",
                            Type = "Progressive"
                        },
                        new
                        {
                            Id = 2L,
                            PostalCode = "A100",
                            Type = "Flat Value"
                        },
                        new
                        {
                            Id = 3L,
                            PostalCode = "7000",
                            Type = "Flat Rate"
                        },
                        new
                        {
                            Id = 4L,
                            PostalCode = "1000",
                            Type = "Progressive"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
