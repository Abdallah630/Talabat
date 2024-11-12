namespace Talabat.API.Error
{
<<<<<<< HEAD
	public class ApiExceptionResponse:ApiResponse
	{
        public string? Details { get; set; }

		public ApiExceptionResponse(int statusCode,string? message = null,string? details = null)
			:base(statusCode,message)
		{
			Details = details;
		}
=======
	public class ApiExceptionResponse
	{
>>>>>>> 9d0da82ca6f7c5293cf22e4a1b987e908e7618ae
	}
}
