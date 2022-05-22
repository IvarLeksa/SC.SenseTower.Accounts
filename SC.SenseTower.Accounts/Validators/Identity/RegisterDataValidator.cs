using FluentValidation;
using SC.SenseTower.Accounts.Cqrs.Commands;
using SC.SenseTower.Accounts.Services;

namespace SC.SenseTower.Accounts.Validators.Identity
{
    public class RegisterDataValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterDataValidator(IdentityService identityService)
        {
            RuleFor(x => x.Login)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30)
                .Must(val => identityService.IsLoginFree(val, default).ConfigureAwait(false).GetAwaiter().GetResult())
                .WithMessage("Пользователь с таким именем входа уже зарегистрирован");
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(30);
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress()
                .MinimumLength(5)
                .MaximumLength(100)
                .Must(val => identityService.IsEmailFree(val, default).ConfigureAwait(false).GetAwaiter().GetResult())
                .WithMessage("Пользователь с таким email уже зарегистрирован");
        }
    }
}
