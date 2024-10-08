
namespace Talabat.API.Error
{
	public class ApiResponse
	{

		public int StatusCode { get; set; }
        public string? Message { get; set; }
		public ApiResponse(int statusCode, string? message = null)
		{
			StatusCode = statusCode;
			Message = message ?? GetDefaultMessage(statusCode);
		}

		private string? GetDefaultMessage(int statusCode)
		{
			return statusCode switch
			{
				400 => "BadRequest, you have made",
				401 => "UnAuthorized, you are not",
				404 => "Resource was not found",
				500 => "Errors are the path to the dark side, Errors lead to anger. Anger leads to hate. Hate leads to career change",
				_ => null,
			};
		}
	}
}
