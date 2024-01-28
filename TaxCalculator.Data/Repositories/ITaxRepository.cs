using TaxCalculator.Data.Entities;

namespace TaxCalculator.Data.Repositories
{
	public interface ITaxRepository
	{
		public Task<IList<ProgressiveTaxRate>> GetProgressiveTaxRatesAsync();
		public Task<TaxCalculationType> GetTaxCalculationTypeByPostalCodeAsync(string postalCode);
		public Task<CalculatedTax> AddCalculatedTaxAsync(decimal income, decimal tax, long taxCalculationTypeId);
		public Task<IList<CalculatedTax>> GetCalculatedTaxesAsync();
    }
}
