using System.ComponentModel.DataAnnotations;

namespace BlinkIt.Models.ViewModels.Account
{
    public class Forgotmodel
    {

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}
