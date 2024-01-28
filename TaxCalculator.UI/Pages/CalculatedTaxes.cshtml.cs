﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using TaxCalculator.UI.Models;

namespace TaxCalculator.UI.Pages
{
	public class CalculatedTaxesModel : PageModel
	{
        public required List<TaxCalculatorResponse> Taxes { get; set; }

        public async Task OnGetAsync()
        {
            Taxes = [];
			using HttpClient client = new();
			string apiUrl = "https://localhost:7109/api/taxes";

			HttpResponseMessage response = await client.GetAsync(apiUrl);

			if (response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();
				var results = JsonSerializer.Deserialize<List<TaxCalculatorResponse>>(content, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});
				Taxes = results ?? [];
			}
		}
    }

}
