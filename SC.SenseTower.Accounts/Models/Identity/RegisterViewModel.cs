using System.ComponentModel.DataAnnotations;

namespace SC.SenseTower.Accounts.Models.Identity
{
    public class RegisterViewModel
    {
        [Display(Name = "Имя входа")]
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Login { get; set; }

        [Display(Name = "Email")]
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required]
        [StringLength(30, MinimumLength = 8)]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля")]
        [Required]
        [StringLength(30, MinimumLength = 8)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
