using Demo.BLL.DataTransferObject.EmployeeDtos;
using Demo.BLL.Services;
using Demo.BLL.Services.Employees;
using Demo.DataAccess.Data.Migrations;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enum;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Demo.Presentation.Controllers
{
    public class EmployeeController(IEmployeeService _employeeService, ILogger<EmployeeController> _logger, IWebHostEnvironment _environment ) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName )
        {
            var employees = _employeeService.GetAllEmployees( EmployeeSearchName);
            return View(employees);
        }
        #region Create Employee 

        public IActionResult Create([FromServices] IDepartmentService _departmentService)
        {
            ViewData["Departments"] = _departmentService.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel viewModel)
        {
            if (ModelState.IsValid) // Server Side validation
            {
                try
                {
                    var employeeDto = new CreatedEmployeeDto()
                    {
                        Name = viewModel.Name, 
                        Age = viewModel.Age, 
                        Address = viewModel.Address, 
                        Email = viewModel.Email, 
                        EmployeeType = viewModel.EmployeeType,
                        Gender = viewModel.Gender, 
                        HiringDate = viewModel.HiringDate, 
                        IsActive = viewModel.IsActive, 
                        PhoneNumber = viewModel.PhoneNumber, 
                        Salary = viewModel.Salary ,
                        DepartmentId = viewModel.DepartmentId,
                        Image = viewModel.Image,
                        
                    };
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

            return View(viewModel);

        }
        #endregion
        #region Employee Details 
        [HttpGet]
        public IActionResult Details(int? Id)
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
                new EmployeeViewModel()
                {
                    Name = employee.Name,
                    Address = employee.Address,
                    Email = employee.Email,
                    Age = employee.Age.Value,
                    Salary = employee.Salary,
                    PhoneNumber = employee.PhoneNumber,
                    IsActive = employee.IsActive,
                    HiringDate = employee.HiringDate,
                    Gender = Enum.Parse<Gender>(employee.Gender),
                    EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                    DepartmentId = employee.DepartmentId
                }
            );
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int? Id,EmployeeViewModel  viewModel)
        {
            if (!Id.HasValue) return BadRequest();

            if (!ModelState.IsValid)
                return View(viewModel);
            try
            {
                var employeeDTO = new UpdatedEmployeeDto()
                {
                    Name = viewModel.Name,
                    Age = viewModel.Age,
                    Address = viewModel.Address,
                    Email = viewModel.Email,
                    EmployeeType = viewModel.EmployeeType,
                    Gender = viewModel.Gender,
                    HiringDate = viewModel.HiringDate,
                    IsActive = viewModel.IsActive,
                    PhoneNumber = viewModel.PhoneNumber,
                    Salary = viewModel.Salary,
                    DepartmentId = viewModel.DepartmentId
                };
                var result = _employeeService.UpdateEmployee(employeeDTO);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Not Updated");
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(viewModel);
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }


            }
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool isDeleted = _employeeService.DeleteEmployee(id);
                if (isDeleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "employee Not Deleted");
                    return RedirectToAction(nameof(Delete), new { id });
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    // 1. Environment Development => Log Error in Console and Return same view with error message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // 2. Environment Deployment
                    // Log Error in File | Table in DataBase And Return Error View
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
    }
}
