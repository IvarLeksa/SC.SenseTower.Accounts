using Microsoft.AspNetCore.Mvc;
using SC.SenseTower.Accounts.Constants;
using SC.SenseTower.Accounts.Dto.Identity;
using SC.SenseTower.Accounts.Services;
using SC.SenseTower.Accounts.Validators.Identity;
using SC.SenseTower.Common.Attributes;

namespace SC.SenseTower.Accounts.Controllers
{
    [Route(ApiConstants.API_ROOT_SEGMENT + "[controller]/[action]")]
    public class IdentityController : Controller
    {
        private readonly IdentityService identityService;
        private readonly RegistryDataValidator registryDataValidator;

        public IdentityController(
            IdentityService identityService,
            RegistryDataValidator registryDataValidator)
        {
            this.identityService = identityService;
            this.registryDataValidator = registryDataValidator;
        }

        [HttpPost, CommonException]
        public async Task<UserInfoDto> Register(UserRegisterDto data)
        {
            if (!await registryDataValidator.ValidateAsync(data, ModelState))
                throw new Exception(string.Join("\n", registryDataValidator.GetErrors()));

            var userId = await identityService.Register(data.Login, data.Email, data.Password);
            await identityService.Logon(data.Login, data.Password);
            var result = await identityService.GetIdentityInfo(userId);
            return result;
        }
    }
}
