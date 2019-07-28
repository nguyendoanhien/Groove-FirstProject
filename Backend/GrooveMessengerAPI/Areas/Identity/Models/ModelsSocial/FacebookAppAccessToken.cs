using Newtonsoft.Json;

namespace GrooveMessengerAPI.Areas.Identity.Models.ModelsSocial
{
    public class FacebookAppAccessToken
    {
        [JsonProperty("token_type")] public string TokenType { get; set; }

        [JsonProperty("access_token")] public string AccessToken { get; set; }
    }
}