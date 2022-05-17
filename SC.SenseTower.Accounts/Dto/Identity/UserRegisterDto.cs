using System.ComponentModel.DataAnnotations;

namespace SC.SenseTower.Accounts.Dto.Identity
{
    public class UserRegisterDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
