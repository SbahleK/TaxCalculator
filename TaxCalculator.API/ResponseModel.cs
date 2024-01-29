namespace TaxCalculator.API
{
	public class ResponseModel
	{
		public bool Success { get; set; }
		public string? Message { get; set; }
		public int StatusCode { get; set; }
	}
}
