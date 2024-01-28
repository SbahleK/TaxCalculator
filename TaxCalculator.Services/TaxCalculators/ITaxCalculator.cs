using TaxCalculator.Data.Entities;

namespace TaxCalculator.Services.TaxCalculators
{
	public interface ITaxCalculator
    {
        public Task<CalculatedTax> CalculateTaxAsync(decimal income, long taxCalculationTypeId);
    }
}
