using Demo.BLL.Services.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeeController(IEmployeeService _employeeService ,ILogger<EmployeeController> _logger ,IWebHostEnvironment _environment) :Controller
    {
        public IActionResult Index()
        {
            
            return View (); 
            //return View(_employeeService.GetAllEmployees(false));

        }


    }
}
