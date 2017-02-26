using Microsoft.IdentityModel.Tokens;

namespace NMPlusApp.Api
{
    public class JwtAuthenticationOptions
    {
        public TokenValidationParameters Parameters { get; set; } = new TokenValidationParameters();
        public string TokenEndpoint { get; set; }
    }
}
