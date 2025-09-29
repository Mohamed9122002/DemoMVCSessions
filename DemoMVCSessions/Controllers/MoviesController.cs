using Microsoft.AspNetCore.Mvc;

namespace DemoMVCSessions.Controllers
{
    // BaseURl/Movies/GetMovie/10 
    public class MoviesController :Controller
    {
        public void GetMovie (int id )
        {

        }
        public string Index()
        {
            return "Hello From Index";
        }

    }
}
