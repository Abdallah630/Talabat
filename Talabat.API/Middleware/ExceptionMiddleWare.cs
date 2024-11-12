using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;
using Talabat.API.Error;

namespace Talabat.API.Middleware
{
	//By Convention
	public class ExceptionMiddleWare
	{
		private readonly RequestDelegate _requestDelegate;
		private readonly Logger<ExceptionMiddleWare> _logger;
		private readonly IWebHostEnvironment _environment;

		public ExceptionMiddleWare(RequestDelegate requestDelegate, Logger<ExceptionMiddleWare> logger, IWebHostEnvironment environment)
		{
			_requestDelegate = requestDelegate;
			_logger = logger;
			_environment = environment;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				//Take an Action with the Request
				//Go to the Next MiddleWare
				await _requestDelegate.Invoke(httpContext);
			}
			catch (Exception ex)
			{
				//Take an Action the Response
				_logger.LogError(ex.Message); // Development Environment
				httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
				httpContext.Response.ContentType = "application/json";
				var response = _environment.IsDevelopment() ?
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
					:
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

				var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
				var json = JsonSerializer.Serialize(response);

				httpContext.Response.WriteAsync(json);
			}
		}
	}
}
