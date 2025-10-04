using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DataTransferObject.EmployeeDtos
{
    public class CreatedEmployeeDto
    {
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "Name Must be less than 50 Characters")]
        [MinLength(5, ErrorMessage = "Name Must be More than 5 Characters")]
        public string Name { get; set; } = null!;
        [Range(22, 50, ErrorMessage = "Age Must be Between 18 and 60")]
        public int Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int? DepartmentId { get; set; }
    }
}
