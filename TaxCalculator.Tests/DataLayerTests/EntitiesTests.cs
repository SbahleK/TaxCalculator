using TaxCalculator.Data.Entities;

namespace TaxCalculator.Tests.DataLayerTests
{
    public class Tests
    {
		[Test]
		public  void CalculatedTax_MustReturnDefaultValue()
		{
			var instance = new CalculatedTax();

			Assert.That(instance, Is.Not.Null);
			Assert.Multiple(() =>
			{
				Assert.That(instance.Income, Is.EqualTo(0));
				Assert.That(instance.Id, Is.EqualTo(0));
				Assert.That(instance.TaxCalculationTypeId, Is.EqualTo(0));
				Assert.That(instance.CreatedOn, Is.EqualTo(DateTimeOffset.MinValue));
				Assert.That(instance.TaxCalculationType, Is.EqualTo(null));
				Assert.That(instance.TaxCharged, Is.EqualTo(0));
			});
		}

		[Test]
		public void ProgressiveTaxRate_MustReturnDefaultValue()
		{
			var instance = new ProgressiveTaxRate();

			Assert.That(instance, Is.Not.Null);
			Assert.Multiple(() =>
			{
				Assert.That(instance.Rate, Is.EqualTo(null));
				Assert.That(instance.Id, Is.EqualTo(0));
				Assert.That(instance.To, Is.EqualTo(0));
				Assert.That(instance.From, Is.EqualTo(0));
			});
		}

		[Test]
		public void TaxCalculationType_MustReturnDefaultValue()
		{
			var instance = new TaxCalculationType();

			Assert.That(instance, Is.Not.Null);
			Assert.Multiple(() =>
			{
				Assert.That(instance.PostalCode, Is.EqualTo(null));
				Assert.That(instance.Id, Is.EqualTo(0));
				Assert.That(instance.Type, Is.EqualTo(null));
			});
		}
	}
}