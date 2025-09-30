using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace DemoMVCSessions.Controllers
{
    // BaseURl/Movies/GetMovie/10 
    public class MoviesController : Controller
    {

        #region Is a method must meet certain requirements
        //The method must be public.
        //The method cannot be a static method.
        //The method cannot be an extension method.
        //The method cannot be a constructor, getter, or setter.
        //The method cannot have open generic types.
        //The method is not a method of the controller base class.
        //The method cannot contain ref or out parameters. 
        #endregion
        #region  Types of action results: 

        //ViewResult - Represents HTML and markup.
        //EmptyResult - Represents no result.
        //RedirectResult - Represents a redirection to a new URL.
        //JsonResult - Represents a JavaScript Object Notation result that can be used in an AJAX application.
        //JavaScriptResult - Represents a JavaScript script.
        //ContentResult - Represents a text result.
        //FileContentResult - Represents a downloadable file (with the binary content).
        //FilePathResult - Represents a downloadable file(with a path).
        //FileStreamResult - Represents a downloadable file(with a file stream)

        #endregion
        [HttpGet]
        //public ContentResult GetMovie(int id)
        //{
        //    //ContentResult result = new ContentResult();
        //    //result.Content = $"Movie ID = {id}";
        //    //result.ContentType = "text/html";
        //    return Content($"Movie ID = {id}","text/html");
        //}
        public IActionResult GetMovie(int id, string name)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else if (id < 10)
            {
                return NotFound();
            }
            else
            {
                return Content($"Movie With Name = {name} and Id = {id}", "text/html");
            }
        }
        public string Index()
        {
            return "Hello From Index";
        }
        #region Model Binding
        //Is the Process of automatically mapping incoming HTTP request data to action method parameters or model properties
        //Model Data Can Be 
            //Simple Data
            //Complex Data
            //Mixed Data
            //Collection
        //Value Providers [In Order] 
        //Form ,Route Data, Query String ,Request Body ,RequestHeader
        //[HttpPost]
        //public  IActionResult TestModelBinding([FromQuery] int id, string name)
        //{
        //    return Content($"Movie With Name = {name} and Id = {id}  ", "text/html");
        //}

        #endregion

    }
}
