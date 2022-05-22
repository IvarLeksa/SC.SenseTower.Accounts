using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.SenseTower.Accounts.Constants;
using SC.SenseTower.Accounts.Cqrs.Requests;
using SC.SenseTower.Common.Attributes;

namespace SC.SenseTower.Accounts.Controllers
{
    [Route(ApiConstants.API_ROOT_SEGMENT + "[controller]/[action]")]
    public class UserInfoController : BaseController
    {
        public UserInfoController(IMediator mediator, ILogger<UserInfoController> logger) : base(mediator, logger)
        {
        }

        [HttpGet, CommonException, AllowAnonymous]
        public async Task<IActionResult> IsLoginFree(string login)
        {
            return Ok(await mediator.Send(new CheckLoginRequest { Login = login }));
        }

        [HttpGet, CommonException, AllowAnonymous]
        public async Task<IActionResult> IsEmailFree(string email)
        {
            return Ok(await mediator.Send(new CheckEmailRequest { Email = email }));
        }
    }
}
