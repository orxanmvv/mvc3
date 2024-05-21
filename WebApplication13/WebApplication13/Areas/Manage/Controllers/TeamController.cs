using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication13.DAL;
using WebApplication13.Models;

namespace WebApplication13.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TeamController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var teams = _appDbContext.Teams.ToList();
            return View(teams);
        }
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(Teams teams)
        {
            if (!teams.ImgFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImgFile", "duzgun daxil edilmeyib");

                return View();
            }
            string path = _webHostEnvironment.WebRootPath + @"\Upload\manage\";
            string Filename = Guid.NewGuid() + teams.ImgFile.FileName;
            using (FileStream fileStream = new FileStream(path + Filename, FileMode.Create))
            {
                teams.ImgFile.CopyTo(fileStream);
            }
            Teams teams1 = new Teams()
            {
                FullName = teams.FullName,
                Job = teams.Job,
                Description = teams.Description,
                ImgUrl = Filename

            };
            _appDbContext.Teams.Add(teams1);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int id)
        {
            var existingItem = await _appDbContext.Teams.FirstOrDefaultAsync(p => p.Id == id);
            if (existingItem != null)
            {
                _appDbContext.Remove(existingItem);
                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var existingData = await _appDbContext.Teams.FirstOrDefaultAsync(p => p.Id == id);
            return View(existingData);
        }

        [HttpPost]

        public IActionResult Update(Teams teams)
        {
            if (!ModelState.IsValid)
            {
                return View(teams);
            }
            if (teams.ImgFile != null)
            {
                string path = _webHostEnvironment.WebRootPath + @"\Upload\manage\";
                string Filename = Guid.NewGuid() + teams.ImgFile.FileName;
                using (FileStream fileStream = new FileStream(path + Filename, FileMode.Create))
                {
                    teams.ImgFile.CopyTo(fileStream);
                }
                teams.ImgUrl = Filename;

            }

            _appDbContext.Teams.Update(teams);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}

