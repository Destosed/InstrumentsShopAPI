using Microsoft.IdentityModel.Tokens;

namespace InstrumentsShopAPI.Services
{
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }

        SecurityKey GetKey();

    }
}