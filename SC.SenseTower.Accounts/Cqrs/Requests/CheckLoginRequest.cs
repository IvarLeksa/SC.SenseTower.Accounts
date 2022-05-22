using MediatR;

namespace SC.SenseTower.Accounts.Cqrs.Requests
{
    public class CheckLoginRequest : IRequest<bool>
    {
        public string Login { get; set; } = string.Empty;
    }
}
