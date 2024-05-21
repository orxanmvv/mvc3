using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication13.DAL;

namespace WebApplication13.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Teams.ToList());
        }

      

     
    }
}