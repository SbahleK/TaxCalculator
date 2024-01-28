using Microsoft.AspNetCore.Builder;
using TaxCalculator.Data;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Services.TaxCalculators;


namespace TaxCalculator.Services
{
    public static class Startup
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{

			services.AddDataLayer();
			services.AddTransient<ITaxService, TaxService>();
			services.AddTransient<TaxCalculatorFactory>();
			services.AddTransient<ITaxCalculator, ProgressiveTaxCalculator>();
			services.AddTransient<ITaxCalculator, FlatValueTaxCalculator>();
			services.AddTransient<ITaxCalculator, FlatRateTaxCalculator>();

			return services;
		}

		public static IApplicationBuilder UseServices(this IApplicationBuilder app)
		{
			app.UseDataLayer();

			return app;
		}
	}
}
