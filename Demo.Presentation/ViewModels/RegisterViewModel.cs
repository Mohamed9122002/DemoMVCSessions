using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Demo.Presentation.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name Can Not Be Null ")]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Can Not Be Null ")]
        [MaxLength(50)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Can Not Be Null ")]
        [MaxLength(50)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }

    }
}
