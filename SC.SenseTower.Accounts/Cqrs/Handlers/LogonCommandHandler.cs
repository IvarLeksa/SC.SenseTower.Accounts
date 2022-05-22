using AutoMapper;
using MediatR;
using SC.SenseTower.Accounts.Cqrs.Commands;
using SC.SenseTower.Accounts.Dto.Identity;
using SC.SenseTower.Accounts.Services;
using SC.SenseTower.Common.Exceptions;

namespace SC.SenseTower.Accounts.Cqrs.Handlers
{
    public class LogonCommandHandler : BaseHandler, IRequestHandler<LogonCommand, UserInfoDto?>
    {
        private readonly IdentityService identityService;

        public LogonCommandHandler(
            ILogger<LogonCommandHandler> logger,
            IMapper mapper,
            IdentityService identityService) : base(logger, mapper)
        {
            this.identityService = identityService;
        }

        public async Task<UserInfoDto?> Handle(LogonCommand request, CancellationToken cancellationToken)
        {
            var response = await identityService.Logon(request.Login, request.Password, cancellationToken).ConfigureAwait(false);
            if (response == null)
                throw new ScException("Ошибка входа в приложение.");
            if (response.IsError)
                throw new ScException($"Ошибка входа в приложение: {response.Error}");

            var userInfo = await identityService.GetIdentityInfo(response.AccessToken, cancellationToken).ConfigureAwait(false);
            return mapper.Map<UserInfoDto>(userInfo);
        }
    }
}
