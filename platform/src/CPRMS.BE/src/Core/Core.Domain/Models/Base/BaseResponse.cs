using Core.Utility.Enums;
using Newtonsoft.Json;

namespace Core.Domain.Models.Base
{
    public class BaseResponse<T>
    {
        public ResponseState State { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public bool HasPermission { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? CorrelationId { get; set; } = null;
        public bool IsView { get; set; } = false;
    }
    public enum ResponseState
    {
        Ok = 0,
        Error = 1
    }
}
