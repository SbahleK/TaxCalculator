using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data;
using TaxCalculator.Data.Entities;
using TaxCalculator.Data.Repositories;

namespace TaxCalculator.Services.TaxCalculators
{
    public class ProgressiveTaxCalculator(ITaxRepository taxRepository) : ITaxCalculator
    {
		public async Task<CalculatedTax> CalculateTaxAsync(decimal income, long taxCalculationTypeId)
        {
			decimal tax = 0;
			decimal remainingIncome = income;
			var progressiveTaxRates = await taxRepository.GetProgressiveTaxRatesAsync();

			for (int i = 0; i < progressiveTaxRates.Count; i++)
			{
				if (income > progressiveTaxRates[i].To)
				{
					var taxableBracketIncome = (i == 0) ? progressiveTaxRates[i].To : (progressiveTaxRates[i].To - progressiveTaxRates[i - 1].To);

					tax += taxableBracketIncome * GetRatePercentage(progressiveTaxRates[i].Rate);
					remainingIncome -= taxableBracketIncome;
				}
				else
				{
					tax += remainingIncome * GetRatePercentage(progressiveTaxRates[i].Rate);
					break;
				}
			}

			var result = await taxRepository.AddCalculatedTaxAsync(income, tax, taxCalculationTypeId);
			return result;
		}

		private static decimal GetRatePercentage(string percentString)
		{
			string cleanedString = percentString.Replace("%", "").Trim();

			if (decimal.TryParse(cleanedString, out decimal result))
			{
				result /= 100;
				return result;
			}

			return result;
		}
	}
}
