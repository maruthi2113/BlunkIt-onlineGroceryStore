using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace BlinkIt.Models.ViewModels.Account
{
    public class RegisterViewModel
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Doesn't match")]
        public string Confirmpassword { get; set; }
        [Required]
        public string Role { get;set; }

        

    }
}
