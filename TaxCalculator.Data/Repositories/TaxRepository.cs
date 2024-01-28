using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data.Entities;

namespace TaxCalculator.Data.Repositories
{
	public class TaxRepository(DatabaseContext dbContext) : ITaxRepository
	{
		public async Task<IList<ProgressiveTaxRate>> GetProgressiveTaxRatesAsync()
		{
			return await dbContext.ProgressiveTaxRates.OrderBy(x => x.Rate).ToListAsync();
		}

		public async Task<TaxCalculationType> GetTaxCalculationTypeByPostalCodeAsync(string postalCode)
		{
			var taxCalculationType = await dbContext.TaxCalculationTypes.FirstOrDefaultAsync(x => x.PostalCode == postalCode);
			return taxCalculationType ?? throw new ArgumentNullException(postalCode, "Postal code not found!");
		}
		public async Task<CalculatedTax> AddCalculatedTaxAsync(decimal income, decimal tax, long taxCalculationTypeId)
		{
			var result = new CalculatedTax
			{
				Income = income,
				TaxCalculationTypeId = taxCalculationTypeId,
				TaxCharged = tax,
				CreatedOn = DateTimeOffset.UtcNow
			};
			await dbContext.AddAsync(result);
			await dbContext.SaveChangesAsync();

			return result;
		}

        public async Task<IList<CalculatedTax>> GetCalculatedTaxesAsync()
        {
			return await dbContext.CalculatedTaxes
				.Include(x => x.TaxCalculationType)
				.ToListAsync();
        }
    }
}
