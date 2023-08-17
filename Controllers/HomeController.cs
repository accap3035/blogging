using Blogging_Project.Data;
using Blogging_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blogging_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BloggingDbContext _db;
        public HomeController(ILogger<HomeController> logger, BloggingDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                Guid userId = new Guid(HttpContext.Session.GetString("UserId"));
                var todos = this._db.Todos.Where(t => t.Owner.Id == userId).ToList();
                return View(todos);
            }
            else 
            {

                return RedirectToAction("Login", "User");
            }
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("Login", "User");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}