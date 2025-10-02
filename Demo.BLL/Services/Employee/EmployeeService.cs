using Demo.BLL.DataTransferObject.EmployeeDtos;
using Demo.DataAccess.Repositories.EmployeeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Employee
{
    public class EmployeeService(IEmployeeRepository _employeeRepository) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking =false)
        {
            var Employees = _employeeRepository.GetAll(withTracking);
            return Employees.Select(E => new EmployeeDto()
            {
                Id = E.Id,
                Name = E.Name,
                Age = E.Age,
                Email = E.Email,
                IsActive = E.IsActive,
                Salary = E.Salary,
                EmployeeType = E.EmployeeType.ToString(),
                Gender = E.Gender.ToString()
            });
        }

        public EmployeeDetailsDto? GetEmployeeId(int id)
        {
            var employee = _employeeRepository.GetById(id);

            return employee is null ? null : new EmployeeDetailsDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                Address = employee.Address,
                IsActive = employee.IsActive,
                Email = employee.Email,
                HiringDate = DateOnly.FromDateTime(employee.HiringDate),
                PhoneNumber = employee.PhoneNumber,
                EmployeeType = employee.EmployeeType.ToString(),
                Gender = employee.Gender.ToString(),
                CreatedBy = 1,
                CreatedOn = DateOnly.FromDateTime(DateTime.Now),
                LastModifiedBy = 1,
                LastModifiedOn = DateOnly.FromDateTime(DateTime.Now),
            };
        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }



        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }
    }
}
