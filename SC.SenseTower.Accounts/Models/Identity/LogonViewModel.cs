using System.ComponentModel.DataAnnotations;

namespace SC.SenseTower.Accounts.Models.Identity
{
    public class LogonViewModel
    {
        [Display(Name = "Имя входа")]
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [Required]
        [StringLength(30, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
