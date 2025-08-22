using System;
namespace NellsPay.Send.ResponseModels
{
	public class RegisterResponse : ApiErrorResponse
	{
		public string UserId { get; set; }
	}
}

