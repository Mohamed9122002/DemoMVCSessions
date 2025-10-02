using AutoMapper;
using Demo.BLL.DataTransferObject.EmployeeDtos;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Repositories.EmployeeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Employees
{
    public class EmployeeService(IEmployeeRepository _employeeRepository ,IMapper _mapper) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking =false)
        {
            var employees = _employeeRepository.GetAll(withTracking);
            //return Employees.Select(E => new EmployeeDto()
            //{
            //    Id = E.Id,
            //    Name = E.Name,
            //    Age = E.Age,
            //    Email = E.Email,
            //    IsActive = E.IsActive,
            //    Salary = E.Salary,
            //    EmployeeType = E.EmployeeType.ToString(),
            //    Gender = E.Gender.ToString()
            //});
            var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            return employeesDto;
        }

        public EmployeeDetailsDto? GetEmployeeId(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDto>(employee);
        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            // Convert CreateEmployeeDTO To Employee
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            // Add Employee To Database
            return _employeeRepository.Add(employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto);
            // Update Employee In Database
            return _employeeRepository.Update(employee);
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                // Delete Employee From Database

                return _employeeRepository.Update(employee) > 0 ? true : false;

            }
        }



    }
}
