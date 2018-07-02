using System;
using System.Net;

namespace Open.DropBox
{
    //public enum DropboxStatusCode
    //{
    //    UserOverQuota = 507,
    //    BadToken = 401,
    //    BadOAuth = 403,
    //    PathNotFound = 404,
    //    NotChanged = 304,
    //    BadInput = 400,
    //    NotExpectedMethod = 405,
    //}
    public class DropboxException : Exception
    {

        public DropboxException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public DropboxException(HttpStatusCode statusCode, Error error) :
            base(error.ErrorSummary)
        {
            StatusCode = statusCode;
            Error = error;
        }

        public Error Error { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
    }
}
