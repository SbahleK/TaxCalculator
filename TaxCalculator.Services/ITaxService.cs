using TaxCalculator.Services.Models;

namespace TaxCalculator.Services
{
	public interface ITaxService
	{
		public Task<TaxCalculatorResponse> CalculateTaxAsync(decimal income, string postalCode);
		public Task<IList<TaxCalculatorResponse>> GetCalcilatedTaxesAsync();
    }
}
