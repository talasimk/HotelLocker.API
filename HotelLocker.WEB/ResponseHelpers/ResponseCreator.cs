namespace HotelLocker.WEB.ResponseHelpers
{
    using HotelLocker.WEB.Responses;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public static class ResponseCreator
    {

        public static async Task<string> CreateSuccessResponseAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string responseText = await new StreamReader(response.Body).ReadToEndAsync();
            Object responseObj = JsonConvert.DeserializeObject<Object>(responseText);
            OkResponse okResponse = new OkResponse(responseObj);
            response.Body.Seek(0, SeekOrigin.Begin);
            return JsonConvert.SerializeObject(okResponse);
        }

        public static string CreateBadResponse(int errorCode, string details)
        {
            BadResponse badResponse = new BadResponse(errorCode, details);
            return JsonConvert.SerializeObject(badResponse);
        }
    }
}
