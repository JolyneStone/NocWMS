using System.ComponentModel.DataAnnotations;

namespace KiraNet.Camellia.AuthorizationServer.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Alias")]
        public string Alias { get; set; }
    }
}
