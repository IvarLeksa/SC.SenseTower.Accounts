using System.Text.Json.Serialization;

namespace SC.SenseTower.Accounts.Dto.Identity
{
    public class UserInfoResponseDto
    {
        [JsonPropertyName("sub")]
        public Guid UserId { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("preferred_username")]
        public string PreferredUserName { get; set; }

        [JsonPropertyName("name")]
        public string Login { get; set; }

        public string AccessToken { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
