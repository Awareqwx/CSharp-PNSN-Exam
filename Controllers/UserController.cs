using System;
using System.Linq;
using ProfessionalNetwork.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ProfessionalNetwork.Controllers
{
    public class UserController : Controller
    {

        private NetworkContext _context;
        private readonly DbConnector _dbConnector;

        public UserController(DbConnector connect, NetworkContext context)
        {
            _dbConnector = connect;
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(ViewUser newUser)
        {
            if(ModelState.IsValid)
            {
                if(_context.Users.Where(user => user.Email == newUser.Email).Count() != 0)
                {
                    ModelState.AddModelError("Email", "Email already exists!");
                }
                else
                {
                    PasswordHasher<User> hasher = new PasswordHasher<User>();
                    User user = new User
                    {
                        Name = newUser.Name,
                        Email = newUser.Email,
                        Description = newUser.Description
                    };
                    user.PasswordHash = hasher.HashPassword(user, newUser.Password);
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    user.created_at = DateTime.Now;
                    user.updated_at = DateTime.Now;
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("currUser", user.UserId);
                    return Redirect("/professional_profile");
                }
            }
            return View("Index", newUser);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            User test;
            try
            {
                test = _context.Users.Single(user => user.Email == email);
            }
            catch
            {
                ViewBag.LoginError = "Email does not exist";
                return View("Index");
            }
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            if(hasher.VerifyHashedPassword(test, test.PasswordHash, password) == PasswordVerificationResult.Success)
            {
                HttpContext.Session.SetInt32("currUser", test.UserId);
                return Redirect("/professional_profile");
            }
            else
            {
                ViewBag.LoginError = "Incorrect Password";
                return View("Index");
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}