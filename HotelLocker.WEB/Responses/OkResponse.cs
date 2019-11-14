namespace HotelLocker.WEB.Responses
{
    public class OkResponse
    {
        public object Data { get; set; }

        public OkResponse(object data)
        {
            Data = data;
        }
    }
}
