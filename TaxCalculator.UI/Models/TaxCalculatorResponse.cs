using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.UI.Models
{
	public class TaxCalculatorResponse
	{
		public decimal AnnualIncome { get; set; }
		public string PostalCode { get; set; } = default!;
		public decimal TaxCharged { get; set; }
		public DateTimeOffset CalculatedOn { get; set;}
	}
}
