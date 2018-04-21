using System.ComponentModel.DataAnnotations;

namespace KiraNet.Camellia.AuthorizationServer.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The minimum length of the {0} is {2}, the maximun length of the {1}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Comfirm Password")]
        [Compare("Password", ErrorMessage = "The new password is inconsistend with confirmation passowd.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
