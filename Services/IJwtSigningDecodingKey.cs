using Microsoft.IdentityModel.Tokens;

namespace InstrumentsShopAPI.Services
{
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}