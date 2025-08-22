using System;
namespace NellsPay.Send.ResponseModels
{
    public class AddRecipientResponse : ValidationErrorResponse
    {
        public string id { get; set; }
    }

     public class CustomerIdResponse
    {
        public string message { get; set; }
    }

    public class EditRecipientResponse : ValidationErrorResponse
    {
        public bool isSuccess { get; set; }
    }
}