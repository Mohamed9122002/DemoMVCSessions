using Demo.BLL.DataTransferObject.EmployeeDtos;
using Demo.BLL.Services.Employees;
using Demo.DataAccess.Data.Migrations;
using Microsoft.AspNetCore.Mvc;

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


    }
}
