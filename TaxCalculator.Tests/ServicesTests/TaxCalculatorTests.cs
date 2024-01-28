using AutoMoq;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Data;
using TaxCalculator.Data.Entities;
using TaxCalculator.Data.Repositories;
using TaxCalculator.Services;
using TaxCalculator.Services.TaxCalculators;

namespace TaxCalculator.Tests.ServicesTests
{
	public class TaxCalculatorTests
	{
		public TaxRepository _repo;
		public DatabaseContext _database;
		public ProgressiveTaxCalculator _progressiveTaxCalculator;
		public FlatRateTaxCalculator _flatRateTaxCalculator;
		public FlatValueTaxCalculator _flatValueTaxCalculator;

		[SetUp]
		public void SetUp()
		{
			_database = new DatabaseContext();
			_database.Database.Migrate();
			_repo = new TaxRepository(_database);

			_progressiveTaxCalculator = new ProgressiveTaxCalculator(_repo);
			_flatRateTaxCalculator = new FlatRateTaxCalculator(_repo);
			_flatValueTaxCalculator = new FlatValueTaxCalculator(_repo);
		}

		[Test]
		[TestCase(8350)]
		[TestCase(1000)]
		[TestCase(5000)]
		public async Task CalculateTaxAsync_GivenIncome_MustReturnCorrectTaxValueForTheFirstBracket(decimal income)
		{
			// arrange
			var tax = income * 0.1m;

			// act
			var result = await _progressiveTaxCalculator.CalculateTaxAsync(income, 1);

			// assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.TaxCharged, Is.EqualTo(tax));
		}

		[Test]
		[TestCase(8351)]
		[TestCase(25000)]
		[TestCase(33000)]
		public async Task CalculateTaxAsync_GivenIncome_MustReturnCorrectTaxValueForTheSecondBracket(decimal income)
		{
			// arrange
			var firstBracketThreshhold = 8350;
			var tax = firstBracketThreshhold * 0.1m;
			var remainingIncome = income - firstBracketThreshhold;
			tax += remainingIncome  * 0.15m;

			// act
			var result = await _progressiveTaxCalculator.CalculateTaxAsync(income, 1);

			// assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.TaxCharged, Is.EqualTo(tax));
		}

		[Test]
		[TestCase(56000)]
		[TestCase(70000)]
		[TestCase(35000)]
		public async Task CalculateTaxAsync_GivenIncome_MustReturnCorrectTaxValueForTheThirdBracket(decimal income)
		{
			// arrange
			var firstBracketThreshhold = 8350;
			var SecondBracketTaxableIncome = 33950 - firstBracketThreshhold;
			var remainingIncome = income - (firstBracketThreshhold + SecondBracketTaxableIncome);
			var tax = (firstBracketThreshhold * 0.1m) + (SecondBracketTaxableIncome * 0.15m) + (remainingIncome * 0.25m);
		
			// act
			var result = await _progressiveTaxCalculator.CalculateTaxAsync(income, 1);

			// assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.TaxCharged, Is.EqualTo(tax));
		}

		[Test]
		[TestCase(10000)]
		[TestCase(250000)]
		[TestCase(100)]
		public async Task CalculateTaxAsync_GivenIncome_MustReturnCorrectTaxValueForAFlatRate(decimal income)
		{
			// arrange
			var tax = income * 0.175m;

			// act
			var result = await _flatRateTaxCalculator.CalculateTaxAsync(income, 1);

			// assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.TaxCharged, Is.EqualTo(tax));
		}

		[Test]
		[TestCase(250000)]
		[TestCase(400000)]
		[TestCase(200000)]
		public async Task CalculateTaxAsync_GivenIncome_MustReturnCorrectTaxValueForAFlatValue(decimal income)
		{
			// arrange
			var tax = 10000;

			// act
			var result = await _flatValueTaxCalculator.CalculateTaxAsync(income, 1);

			// assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.TaxCharged, Is.EqualTo(tax));
		}


		[Test]
		[TestCase(10000)]
		[TestCase(199999)]
		[TestCase(100)]
		public async Task CalculateTaxAsync_GivenIncomeLessThan200k_MustReturnCorrectTaxValueForAFlatValue(decimal income)
		{
			// arrange
			var tax = income * 0.05m;

			// act
			var result = await _flatValueTaxCalculator.CalculateTaxAsync(income, 1);

			// assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.TaxCharged, Is.EqualTo(tax));
		}

	}
}
