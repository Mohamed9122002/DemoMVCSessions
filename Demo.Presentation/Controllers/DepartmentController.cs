using Demo.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController (IDepartmentService _departmentService) :Controller
    {
        // Get All Department 
        [HttpGet]
        public IActionResult  Index()
        {
            var departments = _departmentService.GetAll();
            return View(departments);
        }
    }
}
