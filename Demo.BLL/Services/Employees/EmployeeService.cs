using AutoMapper;
using Demo.BLL.DataTransferObject.EmployeeDtos;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Repositories;
using Demo.DataAccess.Repositories.EmployeeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Employees
{
    public class EmployeeService(IUnitOfWork _unitOfWork, IMapper _mapper) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            //var employees = _employeeRepository.GetAll(E=>E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
                employees = _unitOfWork.employeeRepository.GetAll();
            else
                employees = _unitOfWork.employeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            return employeesDto;

        }

        public EmployeeDetailsDto? GetEmployeeId(int id)
        {
            var employee = _unitOfWork.employeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDto>(employee);
        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            // Convert CreateEmployeeDTO To Employee
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            // Add Employee To Database
            _unitOfWork.employeeRepository.Add(employee);  // Add Locally 
            // Delete // Updated 
            return _unitOfWork.SaveChange();
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto);
            // Update Employee In Database
            _unitOfWork.employeeRepository.Update(employee);
            return _unitOfWork.SaveChange();
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                // Delete Employee From Database

                _unitOfWork.employeeRepository.Update(employee);

                return _unitOfWork.SaveChange() > 0 ? true : false;


            }
        }
    }
}
