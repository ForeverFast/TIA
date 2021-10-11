using Newtonsoft.Json;

namespace TIA.WebInterface.Models
{
    public class TokenViewModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
    }
}
