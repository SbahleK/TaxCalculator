using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Security.Principal;
using TaxCalculator.Data.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaxCalculator.Data
{
	public class DatabaseContext: DbContext
	{
		public DbSet<TaxCalculationType> TaxCalculationTypes { get; set; }
		public DbSet<ProgressiveTaxRate> ProgressiveTaxRates { get; set; }
		public DbSet<CalculatedTax> CalculatedTaxes { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=TaxCalculatorDb.db");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Seed initial data

			modelBuilder.Entity<TaxCalculationType>().HasData(
				new TaxCalculationType { Id = 1, PostalCode = "7441", Type = "Progressive" },
				new TaxCalculationType { Id = 2, PostalCode = "A100", Type = "Flat Value" },
				new TaxCalculationType { Id = 3, PostalCode = "7000", Type = "Flat Rate" },
				new TaxCalculationType { Id = 4, PostalCode = "1000", Type = "Progressive" }
			);

			modelBuilder.Entity<ProgressiveTaxRate>().HasData(
				new ProgressiveTaxRate { Id = 1, Rate = "10%", From = 0, To = 8350 },
				new ProgressiveTaxRate { Id = 2, Rate = "15%", From = 8351, To = 33950 },
				new ProgressiveTaxRate { Id = 3, Rate = "25%", From = 33951, To = 82250 },
				new ProgressiveTaxRate { Id = 4, Rate = "28%", From = 82251, To = 171550 },
				new ProgressiveTaxRate { Id = 5, Rate = "33%", From = 171551, To = 372950 },
				new ProgressiveTaxRate { Id = 6, Rate = "35%", From = 372951, To = Decimal.MaxValue }
		);
		}
	}
}
