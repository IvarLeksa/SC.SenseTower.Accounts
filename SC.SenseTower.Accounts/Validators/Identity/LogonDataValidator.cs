using FluentValidation;
using SC.SenseTower.Accounts.Cqrs.Commands;

namespace SC.SenseTower.Accounts.Validators.Identity
{
    public class LogonDataValidator : AbstractValidator<LogonCommand>
    {
        public LogonDataValidator()
        {
            RuleFor(x => x.Login).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
