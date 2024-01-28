using TaxCalculator.Services.TaxCalculators;

namespace TaxCalculator.Services
{
	public interface ITaxCalculatorFactory
	{
		ITaxCalculator GetTaxCalculator(string type);
	}
}