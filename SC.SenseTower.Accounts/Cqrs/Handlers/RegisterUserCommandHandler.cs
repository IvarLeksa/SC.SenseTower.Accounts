using AutoMapper;
using MediatR;
using SC.SenseTower.Accounts.Cqrs.Commands;
using SC.SenseTower.Accounts.Dto.Identity;
using SC.SenseTower.Accounts.Services;
using SC.SenseTower.Common.Exceptions;
using SC.SenseTower.Common.Extensions;

namespace SC.SenseTower.Accounts.Cqrs.Handlers
{
    public class RegisterUserCommandHandler : BaseHandler, IRequestHandler<RegisterUserCommand, UserInfoDto?>
    {
        private readonly IdentityService identityService;
        
        public RegisterUserCommandHandler(
            ILogger<RegisterUserCommandHandler> logger,
            IMapper mapper,
            IdentityService identityService) : base(logger, mapper)
        {
            this.identityService = identityService;
        }

        public async Task<UserInfoDto?> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await identityService.Register(request.Login, request.Email, request.Password, cancellationToken).ConfigureAwait(false);
            if (!result.Succeeded)
                throw new ScException(result.GetMessages());

            var response = await identityService.Logon(request.Login, request.Password, cancellationToken).ConfigureAwait(false);
            if (response == null)
                throw new ScException("Ошибка входа в приложение.");

            var userInfo = await identityService.GetIdentityInfo(response.AccessToken, cancellationToken).ConfigureAwait(false);
            return mapper.Map<UserInfoDto>(userInfo);
        }
    }
}
