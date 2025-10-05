using Demo.BLL.DataTransferObject;
using Demo.BLL.Factories;
using Demo.DataAccess.Repositories;
using Demo.DataAccess.Repositories.DepartmentRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services
{
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {
        // Constructor Mapping 
        // Extension Method Mapping
        // get All 
        public IEnumerable<DepartmentDto> GetAll()
        {
            var departments = _unitOfWork.departmentRepository.GetAll();
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
            var department = _unitOfWork.departmentRepository.GetById(id);
            if (department == null)
                throw new Exception($"Department with Id {id} not found.");

            //return new DepartmentDetailsDto(department); 
            return department.ToDepartmentDetailsDto();
        }
        // Create New Department 
        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            // reverse Mapping 
            //_departmentRepository.Add(departmentDto);
            var department = departmentDto.ToEntity();
            _unitOfWork.departmentRepository.Add(department);
            return _unitOfWork.SaveChange();
        }
        // Updated Department 
        public int UpdatedDepartment(UpdatedDepartmentDto departmentDto)
        {
            _unitOfWork.departmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChange();
        }
        // Delete Department 
        public bool DepleteDepartment(int id)
        {
            var department = _unitOfWork.departmentRepository.GetById(id);
            if (department == null)
                throw new Exception($"Department with Id {id} not found.");
            _unitOfWork.departmentRepository.Remove(department);
            var result = _unitOfWork.SaveChange();
            return result > 0 ? true : false;
        }

    }
}
