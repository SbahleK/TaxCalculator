namespace TaxCalculator.API
{
	public class ExceptionHandleMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandleMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				await HandleException(ex, httpContext);
			}
		}

		private async Task HandleException(Exception ex, HttpContext httpContext)
		{
			httpContext.Response.StatusCode = 400;

			await httpContext.Response.WriteAsJsonAsync(new ResponseModel
			{
				Message = ex.Message,
				StatusCode = 400,
				Success = false
			});
		}
	}

	public static class ExceptionHandleMiddlewareExtensions
	{
		public static IApplicationBuilder UseExceptionHandleMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ExceptionHandleMiddleware>();
		}
	}
}
