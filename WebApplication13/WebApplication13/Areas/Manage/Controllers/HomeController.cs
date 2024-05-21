using Microsoft.AspNetCore.Mvc;

namespace WebApplication13.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
