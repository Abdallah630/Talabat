namespace Talabat.API.Error
{
	//Validation Error في حالة Response الهيمثل ال  Class ده ال 
	public class ApiValidationResponse : ApiResponse
	{
        public IEnumerable<string> Errors { get; set; }

		public ApiValidationResponse()
			:base(400)
		{
			Errors = new List<string>();
		}
	}

}



