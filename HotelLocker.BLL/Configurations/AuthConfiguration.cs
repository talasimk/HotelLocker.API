using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace HotelLocker.BLL.Configurations
{
    public class AuthConfiguration
    {
        public string Issuer { get; private set; }
        public string Audience { get; private set; }
        public SymmetricSecurityKey Key { get; private set; }
        public string SecurityKey { get; private set; }
        public int ExpireMinutes { get; private set; }

        public AuthConfiguration(IConfiguration configuration)
        {
            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Jwt:Key"]));
            SecurityKey = configuration["Auth:Jwt:Key"];
            Issuer = configuration["Auth:Jwt:Issuer"];
            Audience = configuration["Auth:Jwt:Issuer"];
            ExpireMinutes = Int32.Parse(configuration["Auth:Jwt:ExpireMinutes"]);
        }
    }
}
