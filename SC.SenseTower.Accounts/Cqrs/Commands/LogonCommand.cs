using MediatR;
using SC.SenseTower.Accounts.Dto.Identity;
using System.ComponentModel.DataAnnotations;

namespace SC.SenseTower.Accounts.Cqrs.Commands
{
    public class LogonCommand : IRequest<UserInfoDto>
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
