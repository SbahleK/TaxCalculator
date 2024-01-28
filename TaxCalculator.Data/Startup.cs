using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Data.Repositories;

namespace TaxCalculator.Data
{
	public static class Startup
	{
		public static IServiceCollection AddDataLayer(this IServiceCollection services)
		{
			services.AddDbContext<DatabaseContext>();
			services.AddTransient<ITaxRepository, TaxRepository>();

			return services;
		}

		public static IApplicationBuilder UseDataLayer(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

				dbContext.Database.Migrate();
			}

			return app;
		}
	}
}
