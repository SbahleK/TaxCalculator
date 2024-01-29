using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using TaxCalculator.UI.Models;

namespace TaxCalculator.UI.Pages
{
	public class IndexModel(IConfiguration configuration) : PageModel
	{
        public required string TaxCharged { get; set; }
		[BindProperty]
		public required string PostalCode { get; set; }
		[BindProperty]
		public required string AnnualIncome { get; set; }
        public required string Message { get; set; }
        public void onGet() { }
      
        public async Task OnPostAsync()
        {
			using HttpClient client = new();

			PostalCode = Request.Form["postalCode"]!;
			AnnualIncome = Request.Form["annaulIncome"]!;

			
			string apiUrl = $"{configuration["ApiUrl"]}/calculate-tax?postalCode={PostalCode}&annualIncome={Convert.ToDecimal(AnnualIncome)}";

			HttpResponseMessage response = await client.GetAsync(apiUrl);

			if (response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();
				JsonSerializerOptions options = new()
				{
					PropertyNameCaseInsensitive = true
				};
				var result = JsonSerializer.Deserialize<TaxCalculatorResponse>(content, options);
				TaxCharged = result!.TaxCharged.ToString();
			}
			else
			{
				Message = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
