using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Data;
using TaxCalculator.Data.Entities;
using TaxCalculator.Data.Repositories;

namespace TaxCalculator.Services.TaxCalculators
{
    public class FlatRateTaxCalculator(ITaxRepository taxRepository) : ITaxCalculator
    {
        public async Task<CalculatedTax> CalculateTaxAsync(decimal income, long taxCalculationTypeId)
        {
            // 17.5% tax charged on income
            var taxPercentage = 0.175m;
			var tax = income * taxPercentage;

			var result = await taxRepository.AddCalculatedTaxAsync(income, tax, taxCalculationTypeId);
			return result;
		}
    }
}
