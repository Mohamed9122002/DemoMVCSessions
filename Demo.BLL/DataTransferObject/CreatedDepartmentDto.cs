using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DataTransferObject
{
    public class CreatedDepartmentDto
    {
        [Required]

        public string  Name { get; set; } = string.Empty;
        [Required]
        [Range(10,int.MaxValue)]
        public string Code { get; set; } = string.Empty;
        public DateOnly DateOfCreation { get; set; } 
        public string? Description { get; set; } 
    }
}
