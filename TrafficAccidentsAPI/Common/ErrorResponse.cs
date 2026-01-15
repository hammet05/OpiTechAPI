namespace TrafficAccidentsAPI.Common
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ErrorDetail>? Errors { get; set; }
    }
}
