using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Backend.Models.Commons
{
    [DataContract]
    public class BaseResponseModel<TStatus, TData>
    {
        [IgnoreDataMember]
        public int HttpStatusCode { get; set; }

        [Required]
        public TStatus Status { get; set; }

        public string? Message { get; set; }

        public TData Data { get; set; }
    }

    public class ResponseModel<TData> : BaseResponseModel<DefaultResponseStatus, TData> { }

    public class ResponseModel : BaseResponseModel<DefaultResponseStatus, object> { }
}
