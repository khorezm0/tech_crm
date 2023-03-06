using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Backend.ClientData.Commons
{
    [DataContract]
    public class ApiBaseResponse<TStatus, TData>
    {
        [IgnoreDataMember]
        public int HttpStatusCode { get; set; }

        [Required]
        public TStatus Status { get; set; }

        public string? Message { get; set; }

        public TData Data { get; set; }
    }

    public class ApiResponse<TData> : ApiBaseResponse<DefaultResponseStatus, TData> { }

    public class ApiResponse : ApiBaseResponse<DefaultResponseStatus, object> { }
}
