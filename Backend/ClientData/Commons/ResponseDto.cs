using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.ClientData.Commons
{
    [DataContract]
    public class ApiBaseResponse<TData>
    {
        public string? Message { get; set; }

        public TData Data { get; set; }
    }

    public class ApiResponse : ApiBaseResponse<object>
    {
        public ApiResponse(){}

        public ApiResponse(string message)
        {
            Message = message;
        }
    }

    public class ApiDataResult : IActionResult
    {
        private readonly int _statusCode;
        private readonly object _data;
        public ApiDataResult(object data, int statusCode = 200)
        {
            _data = data;
            _statusCode = statusCode;
        }
        
        public Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_data)
            {
                StatusCode = _statusCode
            };
            
            return objectResult.ExecuteResultAsync(context);
        }
    }
}
