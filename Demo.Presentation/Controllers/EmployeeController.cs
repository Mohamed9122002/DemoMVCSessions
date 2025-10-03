using Demo.BLL.DataTransferObject.EmployeeDtos;
using Demo.BLL.Services.Employees;
using Demo.DataAccess.Data.Migrations;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Demo.Presentation.Controllers
{
    public class EmployeeController(IEmployeeService _employeeService ,ILogger<EmployeeController> _logger ,IWebHostEnvironment _environment) :Controller
    {
        public IActionResult Index()
        {
            return View(_employeeService.GetAllEmployees(false));
        }
        #region Create Employee 

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] 
        public IActionResult Create ( CreatedEmployeeDto employeeDto )
        {
            if (ModelState.IsValid) // Server Side validation
            {
                try
                {
                    int result = _employeeService.CreateEmployee(employeeDto);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Not Created");
                        //return View(employeeDTO);
                    }
                }
                catch (Exception ex)
                {
                    // log Exception
                    if (_environment.IsDevelopment())
                    {
                        // 1. Environment Development => Log Error in Console and Return same view with error message
                        ModelState.AddModelError(string.Empty, ex.Message);
                        //return View(employeeDTO);
                    }
                    else
                    {
                        // 2. Environment Deployment
                        // Log Error in File | Table in DataBase And Return Error View
                        _logger.LogError(ex.Message);
                    }
                }
            }

            return View(employeeDto);

        }
        #endregion
        #region Employee Details 
        [HttpGet]
        public IActionResult Details ( int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeId(Id.Value);
            return employee is null ? NotFound() : View(employee);
        }
        #endregion
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeId(id.Value);
            if (employee is null) return NotFound();
            // Map EmployeeDetailsDTo to UpdatedEmployeeDTO
            return View(
                new UpdatedEmployeeDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Address = employee.Address,
                    Email = employee.Email,
                    Age = employee.Age.Value,
                    Salary = employee.Salary,
                    PhoneNumber = employee.PhoneNumber,
                    IsActive = employee.IsActive,
                    HiringDate = employee.HiringDate,
                    Gender = Enum.Parse<Gender>(employee.Gender),
                    EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType)
                }
            );
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int? Id, UpdatedEmployeeDto employeeDTO)
        {
            if (!Id.HasValue || Id != employeeDTO.Id) return BadRequest();

            if (!ModelState.IsValid)
                return View(employeeDTO);
            try
            {
                var result = _employeeService.UpdateEmployee(employeeDTO);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Not Updated");
                    return View(employeeDTO);
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeDTO);
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }


            }
        }

    }
}
