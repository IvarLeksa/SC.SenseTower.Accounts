using MediatR;
using SC.SenseTower.Accounts.Dto.Identity;

namespace SC.SenseTower.Accounts.Cqrs.Requests
{
    public class GetUserInfoRequest : IRequest<UserInfoDto?>
    {
        public string Token { get; set; }
    }
}
