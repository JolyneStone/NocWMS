using System.ComponentModel.DataAnnotations;

namespace KiraNet.Camellia.AuthorizationServer.Models.ViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "UserName")]
        public string Username { get; set; }

        [Display(Name = "Alias")]
        public string Alias { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
