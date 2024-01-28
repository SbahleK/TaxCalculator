using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Data;
using TaxCalculator.Data.Repositories;

namespace TaxCalculator.Tests.DataLayerTests
{
	public class StartupTests
	{
		[Test]
		public void When_All_Parameters_Are_Good_Should_Succeed()
		{
			// arrange

			var services = new ServiceCollection();

			// act

			var result = Startup.AddDataLayer(services);

			// assert

			result.SingleOrDefault(x => x.ServiceType == typeof(ITaxRepository))!.ImplementationType.ShouldBe(typeof(TaxRepository));
			result.SingleOrDefault(x => x.ServiceType == typeof(DatabaseContext))!.ImplementationType.ShouldBe(typeof(DatabaseContext));
		}
	}
}
