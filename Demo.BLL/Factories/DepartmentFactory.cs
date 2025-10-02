using Demo.BLL.DataTransferObject;
using Demo.DataAccess.Models.DepartmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Factories
{
   public static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department d)
        {
            return new DepartmentDto()
            {
                DeptId = d.Id,
                Name = d.Name,
                Code = d.Code,
                Description = d.Description,
                DateOfCreation = DateOnly.FromDateTime((DateTime)d.CreatedOn)
            };
        }
        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedBy = department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = DateOnly.FromDateTime((DateTime)department.LastModifiedOn),
                CreatedOn = DateOnly.FromDateTime((DateTime)department.CreatedOn),
            };
        }
        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }

        public static Department ToEntity(this UpdatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly()),
            };
        }
    }
}
