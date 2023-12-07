using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace ProjetNetApple.ViewModels
{
    [AllowAnonymous]
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
