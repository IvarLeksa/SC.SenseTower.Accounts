namespace SC.SenseTower.Accounts.Dto.Identity
{
    public class UserInfoDto
    {
        public Guid UserId { get; set; }

        public string Role { get; set; }

        public string Login { get; set; }

        public string AccessToken { get; set; }
    }
}
