using HotelLocker.Common.DataObjects;
using System.Security.Claims;

namespace HotelLocker.BLL.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static UserData ConvertToUserData(this ClaimsPrincipal claimsPrincipal)
        {
            int id = int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
            string role = claimsPrincipal.FindFirstValue(ClaimTypes.Role);
            string[] roles = role.Split(" ");
            return new UserData()
            {
                Id = id,
                Role = roles[0]
            };
        }
    }
}
