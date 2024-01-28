using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Data.Entities
{
	public class ProgressiveTaxRate
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		public string Rate { get; set; } = default!;
		public decimal From { get; set; }
		public decimal To { get; set; }
	}
}
