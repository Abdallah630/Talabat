using System.Net;
using System.Text.Json;
using Talabat.API.Error;

namespace Talabat.API.Middlewares
{
	public class ExceptionMiddleware
	{
		public RequestDelegate _next { get; }
		public ILogger<ExceptionMiddleware> _logger { get; }
		public IWebHostEnvironment _environment { get; }
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IWebHostEnvironment environment)
        {
			_next = next;
			_logger = logger;
			_environment = environment;
		}


		public async Task InvokeAsync(HttpContext context) 
		{
			//Tak an Action With the Request
			try
			{
				//Go to the _next Middleware
				await _next.Invoke(context);
			}
			catch (Exception ex)
			{

				//Tak an Action the Response
				_logger.LogError(ex.Message); // Development Environment
											  // Log Exception in (Database| Files) //Production Environment

				//Return Response 
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				context.Response.ContentType = "application/json";
				var response = _environment.IsDevelopment() ?
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.StackTrace.ToString())
					:
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
				var option = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
				var json = JsonSerializer.Serialize(response,option);

				await context.Response.WriteAsync(json);
			}

		}
	}
}
