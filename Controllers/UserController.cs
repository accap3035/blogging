using Blogging_Project.Data;
using Blogging_Project.Models.Domain;
using Blogging_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text.RegularExpressions;
using BCrypt.Net;

namespace Blogging_Project.Controllers
{
    public class UserController : Controller
    {
        private BloggingDbContext _db;
        public UserController(BloggingDbContext bloggingDbContext)
        {
            this._db = bloggingDbContext;
        }

        #region Validation Area
        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool IsValidPassword(string password)
        {
            var passwordRegex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{8,}$");
            return passwordRegex.IsMatch(password);
        }


        #endregion


        #region Password
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        #endregion



        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }
            
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }
        [HttpPost]
        [ActionName("Register")]
        public IActionResult Add(UserRegisterRequest userRegisterRequest) {

            if (userRegisterRequest.FirstName != null && userRegisterRequest.LastName != null && userRegisterRequest.Email != null && userRegisterRequest.Password != null)
            {
                if (IsValidEmail(userRegisterRequest.Email))
                {
                    if (IsValidPassword(userRegisterRequest.Password))
                    {
                        var user = new User
                        {
                            FirstName = userRegisterRequest.FirstName,
                            LastName = userRegisterRequest.LastName,
                            Email = userRegisterRequest.Email,
                            Password = HashPassword(userRegisterRequest.Password),
                            Admin = false
                        };
                        _db.Users.Add(user);
                        _db.SaveChanges();
                        return RedirectToAction("Login", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Try stronger password");
                        return View();
                    }
                }
                else {
                    ModelState.AddModelError("", "Invalid email!");
                    return View();
                }
                
            }
            else {
                ModelState.AddModelError("", "All fields must be filled!");
                return View();
            }
            
        }
        [HttpPost]
        [ActionName("Login")]
        public  IActionResult Signin(UserLoginRequest userLoginRequest)
        {
            var email = userLoginRequest.Email;
            var password = userLoginRequest.Password;
            if (email != null && password != null)
            {
                if (IsValidEmail(email))
                {
                    var user = this._db.Users.Where(u => u.Email == email).ToList();

                    if (user.Count == 1 && VerifyPassword(password, user[0].Password))
                    {
                        // Successful login
                        // You can set up a user session or use ASP.NET Identity here
                        HttpContext.Session.SetString("UserId", user[0].Id.ToString());
                        return RedirectToAction("", "Home");
                    }
                    else
                    {
                        // Invalid login
                        ModelState.AddModelError("", "Username or password is incorrect!");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email address!");
                    return View();
                }
                
                
            }
            else
            {
                // Invalid login
                ModelState.AddModelError("", "Both username and password must be set");
                return View();
            }
        }
    }
}
