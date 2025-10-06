using Demo.BLL.DataTransferObject;
using Demo.BLL.Services;
using Demo.Presentation.ViewModels.DepartmentViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    [Authorize]
    public class DepartmentController(IDepartmentService _departmentService ,ILogger<DepartmentController> _logger,IWebHostEnvironment _environment) : Controller
    {
        // Get All Department 
        [HttpGet]
        public IActionResult Index()
        {
            //ViewData["Message"] = "HelloFromViewData";
            //ViewBag.Message = "Hello From ";
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
        public IActionResult Create(DepartmentViewModel departmentView)
        {
            if (ModelState.IsValid) // server side validation 
            {
                try
                {
                    var departmentDto = new CreatedDepartmentDto()
                    {
                        Name = departmentView.Name,
                        Code = departmentView.Code, 
                        DateOfCreation = departmentView.DateOfCreation, 
                        Description = departmentView.Description, 
                    };
                    int result = _departmentService.CreateDepartment(departmentDto);
                    string Message;
                    if (result > 0)
                        Message = $"Department {departmentView.Name}Is Created Successful";
                    else
                        Message = $"Department {departmentView.Name} can not Be Created";
                    TempData["Mes"] =Message;
                    return RedirectToAction(nameof(Index));
               
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
                return View (departmentView); 
            
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
            var departmentViewModel = new DepartmentViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.DateOfCreation,
            };
            return View(departmentViewModel);
        }
        [HttpPost]
        public IActionResult Edit (DepartmentViewModel departmentEditViewModel,[FromRoute] int id)
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
        #region Delete Department 

        [HttpPost]
         public IActionResult Delete (int id)
        {
            if(id == 0) return BadRequest();
            try
            {
                bool Deleted = _departmentService.DepleteDepartment(id);
                if (Deleted) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department is Not Deleted");
                    return RedirectToAction(nameof(Delete),new {id });
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    //1.Development => Log Error In Console 
                    //Console.WriteLine(ex);
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //2.Deployment => Log Error File | Table in Database => return Error  View 
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        #endregion

    }
}
