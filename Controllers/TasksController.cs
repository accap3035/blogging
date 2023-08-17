using Blogging_Project.Data;
using Blogging_Project.Models.Domain;
using Blogging_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogging_Project.Controllers
{
    public class TasksController : Controller
    {
        private BloggingDbContext _db;

        public TasksController(BloggingDbContext db)
        {
            this._db = db;
        }

        [HttpGet]
        public IActionResult Add()
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

        [HttpPost]
        public IActionResult Add(TodoAddRequest todoAddRequest)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                var title = todoAddRequest.Title;
                var desc = todoAddRequest.Description;
                var deadlineDate = todoAddRequest.DeadlineDate;
                var deadlineTime = todoAddRequest.DeadlineTime;

                if (title != null && desc != null && deadlineDate != null && deadlineTime != null)
                {
                    Guid sessionUserId = Guid.Parse(HttpContext.Session.GetString("UserId"));

                    var Todo = new Todo
                    {
                        Title = todoAddRequest.Title,
                        Description = todoAddRequest.Description,
                        DeadlineDate = (DateTime)todoAddRequest.DeadlineDate,
                        DeadlineTime = (TimeSpan)todoAddRequest.DeadlineTime,
                        Owner = this._db.Users.FirstOrDefault(u => u.Id == sessionUserId)
                    };
                    this._db.Todos.Add(Todo);
                    this._db.SaveChanges();
                    ModelState.Clear();
                    return View();
                }
                else
                {

                    return View();
                }

            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                Guid taskId = new Guid(id);
                // Retrieve the TODO item from the database using the ID
                var todo = this._db.Todos.Where(t => t.Id == taskId).ToList();

                if (todo == null)
                {
                    return NotFound(); // TODO item not found
                }
                return View(todo);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        [HttpPost]
        public IActionResult Edit()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (!Guid.TryParse(Request.Form["id"], out Guid id))
            {
                // Handle invalid ID here
                return RedirectToAction("Index", "Home");
            }

            var editedTodo = _db.Todos.FirstOrDefault(t => t.Id == id);

            if (editedTodo == null)
            {
                // Handle todo not found
                return RedirectToAction("Index", "Home");
            }

            // Update the properties of the editedTodo based on form data
            editedTodo.Title = Request.Form["title"];
            editedTodo.Description = Request.Form["description"];
            if (DateTime.TryParse(Request.Form["deadlineDate"], out DateTime deadlineDate))
            {
                // Parsing successful, set the property
                editedTodo.DeadlineDate = deadlineDate;
            }
            else
            {
                // Handle parsing failure, maybe provide an error message
                ModelState.AddModelError("deadlineDate", "Invalid date format");
                // Redirect back to the edit form with error message
                return View("Edit", editedTodo);
                // Parse and set the deadlineDate and deadlineTime properties

            }
            if (TimeSpan.TryParse(Request.Form["deadlineTime"], out TimeSpan deadlineTime))
            {
                // Parsing successful, set the property
                editedTodo.DeadlineTime = deadlineTime;
            }
            else
            {
                // Handle parsing failure, maybe provide an error message
                ModelState.AddModelError("deadlineTime", "Invalid time format");
                // Redirect back to the edit form with error message
                return View("Edit", editedTodo);
                // Parse and set the deadlineDate and deadlineTime properties

            }
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                Guid taskId = new Guid(id);
                // Retrieve the TODO item from the database using the ID
                var todo = this._db.Todos.Find(taskId);

                if (todo == null)
                {
                    return NotFound(); // TODO item not found
                }
                else
                {
                    _db.Todos.Remove(todo);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }

    }
}
