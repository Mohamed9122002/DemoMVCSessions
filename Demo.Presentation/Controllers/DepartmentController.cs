using Demo.BLL.DataTransferObject;
using Demo.BLL.Services;
using Demo.Presentation.ViewModels.DepartmentViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService ,ILogger<DepartmentController> _logger,IWebHostEnvironment _environment) : Controller
    {
        // Get All Department 
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAll();
            return View(departments);
        }
        #region Create Department 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] 
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (ModelState.IsValid) // server side validation 
            {
                try
                {
                    int result = _departmentService.CreateDepartment(departmentDto);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can't be Created");
                      
                    }
                }
                catch (Exception ex)
                {
                    // Log Ex 
                    if (_environment.IsDevelopment())
                    {
                        //1.Development => Log Error In Console 
                        //Console.WriteLine(ex);
                        ModelState.AddModelError(string.Empty,ex.Message);
                      
                    }
                    else
                    {
                        //2.Deployment => Log Error File | Table in Database => return Error  View 
                        _logger.LogError(ex.Message);
                    }
                }
            }
                return View (departmentDto); 
            
        }
        #endregion
        #region Details Department   
        [HttpGet] 
        public IActionResult Details(int? Id )
        {
            if (!Id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(Id.Value);
            if (department is null) return NotFound();
            return View(department);
        }
        #endregion
        #region Edit Department 
        [HttpGet]
        public IActionResult Edit(int ? Id )
        {
            if (!Id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(Id.Value);
            if (department is null) return NotFound();
            var departmentViewModel = new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.DateOfCreation,
            };
            return View(departmentViewModel);
        }
        [HttpPost]
        public IActionResult Edit (DepartmentEditViewModel departmentEditViewModel,[FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var UpdatedDepartment = new UpdatedDepartmentDto()
                    {
                        Id = id,
                        Code = departmentEditViewModel.Code,
                        Name = departmentEditViewModel.Name,
                        Description = departmentEditViewModel.Description,
                        DateOfCreation = departmentEditViewModel.DateOfCreation,
                    };
                    int result = _departmentService.UpdatedDepartment(UpdatedDepartment);
                    if (result > 0) return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department is Not Updated");
                    }
                }
                catch (Exception ex)
                {

                    // Log Ex 
                    if (_environment.IsDevelopment())
                    {
                        //1.Development => Log Error In Console 
                        //Console.WriteLine(ex);
                        ModelState.AddModelError(string.Empty, ex.Message);

                    }
                    else
                    {
                        //2.Deployment => Log Error File | Table in Database => return Error  View 
                        _logger.LogError(ex.Message);
                        return View("ErrorView", ex);
                    }

                }
            }

            return View(departmentEditViewModel);
        }
        #endregion

    }
}
