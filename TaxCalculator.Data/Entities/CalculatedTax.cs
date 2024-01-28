using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Data.Entities
{
	public class CalculatedTax
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		[ForeignKey(nameof(TaxCalculationTypeId))]
		public long TaxCalculationTypeId { get; set; }
		public decimal Income { get; set; }
		public decimal TaxCharged { get; set; }
		public DateTimeOffset CreatedOn { get; set; }
		public TaxCalculationType TaxCalculationType { get; set; } = default!;
	}
}
