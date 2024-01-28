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
    public class FlatValueTaxCalculator(ITaxRepository taxRepository) : ITaxCalculator
    {
        public async Task<CalculatedTax> CalculateTaxAsync(decimal income, long taxCalculationTypeId)
        {
            // default flat value of 10000 per year for income greater than 200000
            var tax = 10000m;

            if(income <= 200000m)
            {
                tax = income * 0.05m;
			}

			var result = await taxRepository.AddCalculatedTaxAsync(income, tax, taxCalculationTypeId);
			return result;
		}
    }
}
