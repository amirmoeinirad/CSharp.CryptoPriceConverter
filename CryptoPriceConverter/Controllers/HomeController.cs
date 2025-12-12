using Microsoft.AspNetCore.Mvc;

namespace CryptoPriceConverter.Controllers
{
    public class HomeController : Controller
    {
        // Action method for handling HTTP GET requests to the home page.
        public IActionResult Index()
        {            
            return View();
        }
    }
}
