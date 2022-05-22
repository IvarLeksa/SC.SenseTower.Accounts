using AutoMapper;

namespace SC.SenseTower.Accounts.Cqrs.Handlers
{
    public class BaseHandler
    {
        protected readonly ILogger logger;
        protected readonly IMapper mapper;

        public BaseHandler(ILogger logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}
