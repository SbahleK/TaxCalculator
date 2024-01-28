using AutoMoq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Data.Repositories;
using TaxCalculator.Data;
using TaxCalculator.Services;
using TaxCalculator.Data.Entities;
using Microsoft.Extensions.Logging;
using TaxCalculator.Services.TaxCalculators;

namespace TaxCalculator.Tests.ServicesTests
{
	public class TaxCalculatorFactoryTests
	{
		TaxCalculatorFactory _factory;
		public AutoMoqer _mocker;

		[SetUp]
		public void SetUp()
		{
			_mocker = new AutoMoqer(new Config { MockBehavior = MockBehavior.Loose });
			_factory = _mocker.Create<TaxCalculatorFactory>();
		}

		[Test]
		public void GetTaxCalculator_MustReturnProgressiveTaxCalculator()
		{
			// act
			var result =  _factory.GetTaxCalculator("Progressive");

			// assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.GetType(), Is.EqualTo(typeof(ProgressiveTaxCalculator)));
		}

		[Test]
		public void GetTaxCalculator_MustReturnFlatRateTaxCalculator()
		{
			// act
			var result = _factory.GetTaxCalculator("Flat Rate");

			// assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.GetType(), Is.EqualTo(typeof(FlatRateTaxCalculator)));
		}

		[Test]
		public void GetTaxCalculator_MustReturnFlatValueTaxCalculator()
		{
			// act
			var result = _factory.GetTaxCalculator("Flat Value");

			// assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.GetType(), Is.EqualTo(typeof(FlatValueTaxCalculator)));
		}

		[Test]
		public void GetTaxCalculator_MustThrowException()
		{
			// act
			Assert.Throws<ArgumentNullException>(() => _factory.GetTaxCalculator("Does not exist type!"));
		}
	}
}
