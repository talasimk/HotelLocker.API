namespace HotelLocker.WEB.ResponseHelpers
{
    using Microsoft.AspNetCore.Http;
    using System.IO;

    public class ErrorResposeWriter
    {
        public static void WriteExceptionResponse(HttpContext context, string response)
        {
            var originalBodyStream = context.Response.Body;
            try
            {
                using (var streamWriter = new StreamWriter(originalBodyStream))
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;
                    streamWriter.Write(response);
                }
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
    }
}
