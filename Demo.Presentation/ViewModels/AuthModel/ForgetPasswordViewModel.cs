using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.AuthModel
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Is Required ")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}

