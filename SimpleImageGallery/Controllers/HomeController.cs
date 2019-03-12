using Microsoft.AspNetCore.Mvc;

namespace SimpleImageGallery.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
