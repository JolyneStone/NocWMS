using System.ComponentModel.DataAnnotations;

namespace KiraNet.Camellia.AuthorizationServer.Models.ViewModels
{
    public class AddUserViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}
