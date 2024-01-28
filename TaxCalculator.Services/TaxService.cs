using TaxCalculator.Data.Repositories;
using TaxCalculator.Services.Models;

namespace TaxCalculator.Services
{
    public class TaxService(ITaxRepository taxRepository, ITaxCalculatorFactory calculatorFactory) : ITaxService
	{
		public async Task<TaxCalculatorResponse> CalculateTaxAsync(decimal income, string postalCode)
		{
			if (income < 0)
			{
				throw new ArgumentException("Annual Income must not be less than 0");
			}

			var taxCalculationType = await taxRepository.GetTaxCalculationTypeByPostalCodeAsync(postalCode);	

			var taxCalculator = calculatorFactory.GetTaxCalculator(taxCalculationType.Type);
			var result = await taxCalculator.CalculateTaxAsync(income, taxCalculationType.Id);

			return new TaxCalculatorResponse { AnnualIncome = income, PostalCode = postalCode, TaxCharged = result.TaxCharged, CalculatedOn = result.CreatedOn };
		}

        public async Task<IList<TaxCalculatorResponse>> GetCalcilatedTaxesAsync()
        {
				var taxes = await taxRepository.GetCalculatedTaxesAsync();

				return taxes.Select(tax => new TaxCalculatorResponse
				{
					AnnualIncome = tax.Income,
					TaxCharged = tax.TaxCharged,
					CalculatedOn = tax.CreatedOn,
					PostalCode = tax.TaxCalculationType.PostalCode
				}).ToList();
		}
    }
}
