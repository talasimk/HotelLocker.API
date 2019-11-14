namespace HotelLocker.WEB.Responses
{
    public class BadResponse
    {
        public string Details { get; set; }

        public int? ErrorCode { get; set; }

        public BadResponse(int? errorCode, string details = "")
        {
            ErrorCode = errorCode;
            Details = details;
        }
    }
}
