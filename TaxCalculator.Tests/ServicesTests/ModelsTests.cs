using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Data.Entities;
using TaxCalculator.Services.Models;

namespace TaxCalculator.Tests.ServicesTests
{
	public class ModelsTests
	{
		[Test]
		public void TaxCalculatorResponse_MustReturnDefaultValue()
		{
			var instance = new TaxCalculatorResponse();

			Assert.That(instance, Is.Not.Null);
			Assert.Multiple(() =>
			{
				Assert.That(instance.AnnualIncome, Is.EqualTo(0));
				Assert.That(instance.CalculatedOn, Is.EqualTo(DateTimeOffset.MinValue));
				Assert.That(instance.PostalCode, Is.EqualTo(null));
				Assert.That(instance.TaxCharged, Is.EqualTo(0));
			});
		}
	}
}
