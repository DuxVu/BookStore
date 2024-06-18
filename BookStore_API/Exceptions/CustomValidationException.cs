using System.Net;

namespace BookStore_API.Exceptions
{
    public class CustomValidationException : Exception
    {
        public HttpStatusCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime TraceTime { get; set; }

        public CustomValidationException(HttpStatusCode errorCode, string errorMessage, DateTime traceTime)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            TraceTime = traceTime;
        }
    }
}
