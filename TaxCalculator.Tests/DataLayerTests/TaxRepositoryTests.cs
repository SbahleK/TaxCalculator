using AutoMoq;
using Microsoft.EntityFrameworkCore;
using Moq;
using TaxCalculator.Data;
using TaxCalculator.Data.Entities;
using TaxCalculator.Data.Repositories;

namespace TaxCalculator.Tests.DataLayerTests
{
	public class TaxRepositoryTests
	{
		TaxRepository _repo;
		public AutoMoqer _mocker;
		public DatabaseContext _database;

		[SetUp]
		public void SetUp()
		{
			_mocker = new AutoMoqer(new Config { MockBehavior = MockBehavior.Loose });

			_database = new DatabaseContext();
			_database.Database.Migrate();
			_mocker.SetInstance<DatabaseContext>(_database);
			_repo = _mocker.Create<TaxRepository>();
		}

		[Test]
		public async Task GetProgressiveTaxRatesAsync_MustReturnListOfProgressiveTaxes()
		{
			// act
			var result = await _repo.GetProgressiveTaxRatesAsync();

			// assert
			Assert.That(result, Is.Not.Empty);
		}

		[Test]
		public async Task GetCalculatedTaxesAsync_MustReturnListTaxes()
		{
			// arrange 
			_database.Add(new CalculatedTax { CreatedOn = DateTimeOffset.UtcNow, TaxCalculationTypeId =1, TaxCharged = 800, Income = 1 });
			_database.SaveChanges();

			// act
			var result = await _repo.GetCalculatedTaxesAsync();

			// assert
			Assert.That(result, Is.Not.Empty);
		}

		[Test]
		public async Task GetTaxCalculationTypeByPostalCodeAsync_MustReturnTaxCalculationType()
		{
			// arrange
			var postalCode = "1000";

			// act
			var result = await _repo.GetTaxCalculationTypeByPostalCodeAsync(postalCode);

			// assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.Type, Is.EqualTo("Progressive"));
		}

		[Test]
		public void GetTaxCalculationTypeByPostalCodeAsync_MustThrowException()
		{
			// arrange
			var postalCode = "Test";

			// act
			Assert.ThrowsAsync<ArgumentNullException>(() =>  _repo.GetTaxCalculationTypeByPostalCodeAsync(postalCode));
		}

		[Test]
		public async Task AddCalculatedTaxAsync_MustSucceed()
		{
			// arrange
			var taxTypeId = (await _database.TaxCalculationTypes.FirstAsync()).Id;
			var tax = 85;
			var income = 850;
			

			// act
			var result = await _repo.AddCalculatedTaxAsync(income, tax, taxTypeId);

			// assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.TaxCharged, Is.EqualTo(tax));
		}

		[TearDown] 
		public void TearDown()
		{
			_database.Database.EnsureDeleted();
		}
	}
}