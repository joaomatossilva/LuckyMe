using System;
using System.Net;

namespace LuckyMe.Core.Business
{
    public class BusinessException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public BusinessException(HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            StatusCode = statusCode;
        }

        public BusinessException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
        {
            StatusCode = statusCode;
        }

        public BusinessException(string message, Exception inner, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message, inner)
        {
            StatusCode = statusCode;
        }
    }
}
