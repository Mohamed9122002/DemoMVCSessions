using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.AuthModel
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
