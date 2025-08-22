using System;
using NellsPay.Send.ResponseModels;
using Newtonsoft.Json;
using Refit;

namespace NellsPay.Send.Helpers
{
	public class ApiExceptionHelper
	{
        public static string GetExceptionMessage(ApiException ex)
        {
            string message = string.Empty;

            if (ex.Content != null)
            {
                var result = JsonConvert.DeserializeObject<ApiErrorResponse>(ex.Content);
                message = result == null ? ex.Message : result.Detail;
            }
            else
            {
                message = ex.Message;
            }

            return message;
        }


        public static ApiErrorResponse ProcessException(ApiException ex)
        {
            if (ex.Content != null)
            {
                var result = JsonConvert.DeserializeObject<ApiErrorResponse>(ex.Content);

                return result;
            }

            return new ApiErrorResponse
            {
                Status = 500,
                Detail = ex.Message
            };
        }

        public static ApiErrorResponse ProcessNetworkException()
        {
            return new ApiErrorResponse
            {
                Status = 500,
                Detail = "Network Error"
            };
        }
    }
}

