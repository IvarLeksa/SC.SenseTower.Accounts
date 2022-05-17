using SC.SenseTower.Accounts.Dto.Identity;

namespace SC.SenseTower.Accounts.Services
{
    public class IdentityService
    {
        public async Task<Guid> Register(string login, string email, string password)
        {
            return Guid.Empty;
        }

        public async Task Logon(string login, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<UserInfoDto> GetIdentityInfo(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
