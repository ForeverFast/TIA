using Newtonsoft.Json;

namespace TIA.WebInterface.Models
{
    public class TokenApiModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
