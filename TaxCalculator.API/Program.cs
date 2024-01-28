using TaxCalculator.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSession();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();
app.UseServices();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.MapGet("api/calculate-tax", async (ITaxService service, string postalCode, decimal annualIncome) =>
 await service.CalculateTaxAsync(annualIncome, postalCode)
).WithOpenApi();

app.MapGet("api/taxes", async (ITaxService service) =>
   await service.GetCalcilatedTaxesAsync()
).WithOpenApi();

app.Run();

