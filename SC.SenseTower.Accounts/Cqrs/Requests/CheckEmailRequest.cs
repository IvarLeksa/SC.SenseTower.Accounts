using MediatR;

namespace SC.SenseTower.Accounts.Cqrs.Requests
{
    public class CheckEmailRequest : IRequest<bool>
    {
        public string Email { get; set; } = string.Empty;
    }
}
