using Blogging_Project.Data;
using Blogging_Project.Models.Domain;
using Blogging_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blogging_Project.Controllers
{
    public class AdminTagsController : Controller
    {
        private BloggingDbContext _bloggingDbContext;
        public AdminTagsController(BloggingDbContext bloggingDbContext)
        {
            _bloggingDbContext  = bloggingDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var user = this._bloggingDbContext.Users.Where(u => u.Id.ToString() == userId).ToList();
          
            if (user[0].Admin)
            {
                return View("Add");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
        [HttpPost]
        [ActionName("Add")]
        public IActionResult SubmitTag(AddTagRequest addTagRequest)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var user = this._bloggingDbContext.Users.Where(u => u.Id.ToString() == userId).ToList();
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            if (user[0].Admin) { 
                // Mapping AddTagRequest view to Tag domain model
                
                _bloggingDbContext.Tags.Add(tag);
                _bloggingDbContext.SaveChanges();
                return View("Add");
            }else{
                return RedirectToAction("Index","Home");
            }
            
        }
    }
}
