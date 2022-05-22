using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SC.SenseTower.Accounts.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IMediator mediator;
        protected readonly ILogger logger;

        public BaseController(IMediator mediator, ILogger logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }
    }
}
