using ActivitiesAndTasksAPI.Enums;
using System.Text.Json.Serialization;

namespace ActivitiesAndTasksAPI.DTOs
{
    public class ApiReturnResponse
    {
        [JsonIgnore]
        public HttpResponseCode HttpResponseCode { get; set; } = HttpResponseCode.BadRequest;
        public bool Error { get; set; } = true;
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
