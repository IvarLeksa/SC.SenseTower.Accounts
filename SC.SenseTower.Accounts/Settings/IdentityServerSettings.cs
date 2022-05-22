namespace SC.SenseTower.Accounts.Settings
{
    public class IdentityServerSettings
    {
        public string BaseUrl { get; set; }

        public string ClientId { get; set; } = "vr";

        public int MaxAttempts { get; set; } = 5;

        public int BreakAfter { get; set; } = 3;

        public int BreakForSeconds { get; set; } = 30;

        public string IsLoginFreeUrl { get; set; } = "isloginfree";

        public string IsEmailFreeUrl { get; set; } = "isemailfree";

        public string RegisterUrl { get; set; } = "register";

        public string UserInfoUrl { get; set; } = "userinfo";
    }
}
