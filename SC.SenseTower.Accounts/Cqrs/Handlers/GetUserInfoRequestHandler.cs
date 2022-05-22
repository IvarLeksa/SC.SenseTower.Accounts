using AutoMapper;
using MediatR;
using SC.SenseTower.Accounts.Cqrs.Requests;
using SC.SenseTower.Accounts.Dto.Identity;
using SC.SenseTower.Accounts.Services;

namespace SC.SenseTower.Accounts.Cqrs.Handlers
{
    public class GetUserInfoRequestHandler : BaseHandler, IRequestHandler<GetUserInfoRequest, UserInfoDto?>
    {
        private readonly IdentityService identityService;

        public GetUserInfoRequestHandler(
            ILogger<GetUserInfoRequestHandler> logger,
            IMapper mapper,
            IdentityService identityService) : base(logger, mapper)
        {
            this.identityService = identityService;
        }

        public async Task<UserInfoDto?> Handle(GetUserInfoRequest request, CancellationToken cancellationToken)
        {
            var userInfo = await identityService.GetIdentityInfo(request.Token, cancellationToken).ConfigureAwait(false);
            return mapper.Map<UserInfoDto>(userInfo);
        }
    }
}
