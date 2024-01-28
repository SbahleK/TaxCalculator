using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Data.Entities
{
	public class TaxCalculationType 
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		public string Type { get; set; } = default!;
		public string PostalCode { get; set; } = default!;
	}
}
