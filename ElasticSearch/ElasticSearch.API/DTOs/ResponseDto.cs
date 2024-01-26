using System.Net;

namespace ElasticSearch.API.DTOs
{
    public record ResponseDto<T>
    {
        public T? Data { get; set; }

        public List<string>? Errors { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public static ResponseDto<T> Success(T data, HttpStatusCode httpStatusCode)
        {
           return new ResponseDto<T>
           {
               Data = data,
               HttpStatusCode = httpStatusCode
           };
        }

        public static ResponseDto<T> Fail(List<string> errors, HttpStatusCode httpStatusCode)
        {
            return new ResponseDto<T>
            {
                Errors = errors,
                HttpStatusCode = httpStatusCode
            };
        }

        public static ResponseDto<T> Fail(string errors, HttpStatusCode httpStatusCode)
        {
            return new ResponseDto<T>
            {
                Errors = new List<string> { errors},
                HttpStatusCode = httpStatusCode
            };
        }
    }
}
