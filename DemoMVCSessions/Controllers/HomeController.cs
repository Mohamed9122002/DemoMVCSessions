using Microsoft.AspNetCore.Mvc;

namespace DemoMVCSessions.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // return View(new Movie); // Take Model To Bind View With Model Data
            // return View ("Hamda") // Return View With Specific Name 
            return View(); // Return View With Same Name of Action 
            
        }
    }
}
