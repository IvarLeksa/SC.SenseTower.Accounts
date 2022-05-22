using AutoMapper;
using MediatR;
using SC.SenseTower.Accounts.Cqrs.Requests;
using SC.SenseTower.Accounts.Services;

namespace SC.SenseTower.Accounts.Cqrs.Handlers
{
    public class CheckEmailRequestHandler : BaseHandler, IRequestHandler<CheckEmailRequest, bool>
    {
        private readonly IdentityService identityService;

        public CheckEmailRequestHandler(
            ILogger<CheckEmailRequestHandler> logger,
            IMapper mapper,
            IdentityService identityService) : base(logger, mapper)
        {
            this.identityService = identityService;
        }

        public async Task<bool> Handle(CheckEmailRequest request, CancellationToken cancellationToken)
        {
            logger.LogTrace($"Запрошена проверка email \"{request.Email}\"");
            var result = await identityService.IsEmailFree(request.Email, cancellationToken).ConfigureAwait(false);
            logger.LogTrace($"Проверка email \"{request.Email}\" завершена с результатом {result}");
            return result;
        }
    }
}
