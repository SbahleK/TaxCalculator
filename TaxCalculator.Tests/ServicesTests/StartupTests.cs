using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using TaxCalculator.Services;
using TaxCalculator.Services.TaxCalculators;

namespace TaxCalculator.Tests.ServicesTests
{
	public class StartupTests
	{
		[Test]
		public void When_All_Parameters_Are_Good_Should_Succeed()
		{
			// arrange

			var services = new ServiceCollection();

			// act

			var result = Startup.AddServices(services);

			// assert

			result.SingleOrDefault(x => x.ServiceType == typeof(ITaxService))!.ImplementationType.ShouldBe(typeof(TaxService));
			result.SingleOrDefault(x => x.ServiceType == typeof(TaxCalculatorFactory))!.ImplementationType.ShouldBe(typeof(TaxCalculatorFactory));
			result.SingleOrDefault(x => x.ImplementationType == typeof(ProgressiveTaxCalculator))!.ServiceType.ShouldBe(typeof(ITaxCalculator));
			result.SingleOrDefault(x => x.ImplementationType == typeof(FlatValueTaxCalculator))!.ServiceType.ShouldBe(typeof(ITaxCalculator));
			result.SingleOrDefault(x => x.ImplementationType == typeof(FlatRateTaxCalculator))!.ServiceType.ShouldBe(typeof(ITaxCalculator));
		}
	}
}
