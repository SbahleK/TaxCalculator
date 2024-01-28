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
	public class TaxServiceTests
	{
		TaxService _service;
		public AutoMoqer _mocker;

		[SetUp]
		public void SetUp()
		{
			_mocker = new AutoMoqer(new Config { MockBehavior = MockBehavior.Loose });
			_service = _mocker.Create<TaxService>();
		}

		[Test]
		public async Task GetProgressiveTaxRatesAsync_MustReturnListOfProgressiveTaxes()
		{
			// arrange
			var response = new List<CalculatedTax>()
			{ 
				new() 
				{
					Id = 1,
					TaxCalculationTypeId = 1,
					TaxCharged = 20,
					Income = 300,
					CreatedOn = DateTimeOffset.Now,
					TaxCalculationType = new TaxCalculationType() { Id = 1}
				}
			};

			_mocker
				.GetMock<ITaxRepository>()
				.Setup(x => x.GetCalculatedTaxesAsync())
				.ReturnsAsync(response);

			// act
			var result = await _service.GetCalcilatedTaxesAsync();

			// assert
			Assert.That(result, Is.Not.Empty);
			_mocker
				.GetMock<ITaxRepository>()
				.Verify(x => x.GetCalculatedTaxesAsync(), Times.Once);
		}

		[Test]
		public async Task CalculateTaxAsync_MustReturnCalculatedTax()
		{
			// arrange
			var response = new TaxCalculationType()
			{
				Id = 1,
				Type = "Progressive",
				PostalCode = "12345",
			};

			var calculatedTax = new CalculatedTax()
			{
				Id = 1,
				TaxCalculationTypeId = 1,
				TaxCharged = 20,
				Income = 300,
				CreatedOn = DateTimeOffset.Now,
				TaxCalculationType = new TaxCalculationType() { Id = 1 }
			};

			var taxCalculatorMock = new Mock<ITaxCalculator>();

			_mocker
				.GetMock<ITaxRepository>()
				.Setup(x => x.GetTaxCalculationTypeByPostalCodeAsync(It.IsAny<string>()))
				.ReturnsAsync(response);

			_mocker
				.GetMock<ITaxCalculatorFactory>()
				.Setup(x => x.GetTaxCalculator(It.IsAny<string>()))
				.Returns(taxCalculatorMock.Object);

			taxCalculatorMock
				.Setup(x => x.CalculateTaxAsync(It.IsAny<decimal>(), It.IsAny<long>()))
				.ReturnsAsync(calculatedTax);

			// act
			var result = await _service.CalculateTaxAsync(8500, "1234");

			// assert
			Assert.That(result, Is.Not.Null);
	
			_mocker
				.GetMock<ITaxRepository>()
				.Verify(x => x.GetTaxCalculationTypeByPostalCodeAsync(It.IsAny<string>()), Times.Once);

			_mocker
				.GetMock<ITaxCalculatorFactory>()
				.Verify(x => x.GetTaxCalculator(It.IsAny<string>()), Times.Once);

			taxCalculatorMock
				.Verify(x => x.CalculateTaxAsync(It.IsAny<decimal>(), It.IsAny<long>()), Times.Once);
		}

		[Test]
		public void CalculateTaxAsync_MustThrowInvalidIncomeException()
		{
			// arrange
			var income = -1;

			// act
			Assert.ThrowsAsync<ArgumentException>(() =>  _service.CalculateTaxAsync(income, "12345"));
		}
	}
}
