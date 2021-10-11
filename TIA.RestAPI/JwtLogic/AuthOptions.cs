using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TIA.RestAPI.JwtLogic
{
    public class AuthOptions
    {
        public const string ISSUER = "TIAAuthServer"; // издатель токена
        public const string AUDIENCE = "TIAAuthClient"; // потребитель токена
        const string KEY = "dada_nicetokenkey!1977";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
