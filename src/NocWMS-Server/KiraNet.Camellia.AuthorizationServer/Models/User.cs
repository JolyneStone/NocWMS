using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace KiraNet.Camellia.AuthorizationServer.Models
{
    public class User : IdentityUser
    {
        [StringLength(20)]
        public string Alias { get; set; }
    }
}
