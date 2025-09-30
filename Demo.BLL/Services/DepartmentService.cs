using Demo.BLL.DataTransferObject;
using Demo.BLL.Factories;
using Demo.DataAccess.Repositories.DepartmentRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services
{
    public class DepartmentService(IDepartmentRepository departmentRepository) : IDepartmentService
    {
        // Constructor Mapping 
        // Extension Method Mapping
        private readonly IDepartmentRepository _departmentRepository = departmentRepository;
        // get All 
        public IEnumerable<DepartmentDto> GetAll()
        {
            var departments = _departmentRepository.GetAll();
            // Convert Department to DepartmentDto =>Mapping 
            #region Manual Mapping 
            //var departmentsDto = departments.Select(d => new DepartmentDto
            //{
            //    DeptId = d.Id,
            //    Name = d.Name,
            //    Code = d.Code,
            //    Description = d.Description,
            //    DateOfCreation = DateOnly.FromDateTime((DateTime)d.CreatedOn)
            //});
            #endregion
            // Using Factory for Mapping => Extension Method 
            return departments.Select(D => D.ToDepartmentDto());
        }
        // get by Id 
        public DepartmentDetailsDto GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department == null)
                throw new Exception($"Department with Id {id} not found.");

            //return new DepartmentDetailsDto(department); 
            return department.ToDepartmentDetailsDto();
        }
        // Create New Department 
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            // reverse Mapping 
            //_departmentRepository.Add(departmentDto);
            var department = departmentDto.ToEntity();
            return _departmentRepository.Add(department);
        }
        // Updated Department 
        public int UpdatedDepartment(UpdatedDepartmentDto departmentDto)
        {
            return _departmentRepository.Update(departmentDto.ToEntity());
        }
        // Delete Department 
        public bool DepleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department == null)
                throw new Exception($"Department with Id {id} not found.");
            var result = _departmentRepository.Remove(department);
            return result > 0 ? true : false;
        }

    }
}
