using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.AuthModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Is Required ")]
        [EmailAddress(ErrorMessage = "Invaild Email")]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Can Not Be Null ")]
        [MaxLength(50)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
