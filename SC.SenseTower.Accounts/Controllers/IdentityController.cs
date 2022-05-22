using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.SenseTower.Accounts.Constants;
using SC.SenseTower.Accounts.Cqrs.Commands;
using SC.SenseTower.Accounts.Cqrs.Requests;
using SC.SenseTower.Accounts.Models.Identity;
using SC.SenseTower.Common.Attributes;

namespace SC.SenseTower.Accounts.Controllers
{
    [Route(ApiConstants.API_ROOT_SEGMENT + "[controller]/[action]")]
    public class IdentityController : BaseController
    {
        public IdentityController(
            IMediator mediator,
            ILogger<IdentityController> logger) : base(mediator, logger)
        {
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost, CommonException, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(command, cancellationToken));
        }

        [HttpGet]
        public IActionResult Logon()
        {
            var model = new LogonViewModel();
            return View(model);
        }

        [HttpPost, CommonException, AllowAnonymous]
        public async Task<IActionResult> Logon(LogonCommand command, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(command, cancellationToken));
        }

        [HttpGet, CommonException]
        public async Task<IActionResult> GetInfo(CancellationToken cancellationToken)
        {
            var token = HttpContext.Request.Headers.Authorization[1];
            return Ok(await mediator.Send(new GetUserInfoRequest { Token = token }, cancellationToken));
        }
    }
}
