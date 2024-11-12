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
<<<<<<< HEAD
}
=======
}
>>>>>>> 9d0da82ca6f7c5293cf22e4a1b987e908e7618ae
