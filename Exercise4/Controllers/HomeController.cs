using System.Diagnostics;
using Exercise4.Data;
using Exercise4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exercise4.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Home/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Home/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.UserName == username && u.Password == password && !u.Lock);

            if (user != null)
            {
                // You can store user info in TempData or Session here
                TempData["UserName"] = user.UserName;
                return RedirectToAction("Index", "Home"); // or wherever you want to go next
            }

            ViewBag.ErrorMessage = "Invalid username or password, or your account is locked.";
            return View();
        }

        // GET: /Home/Index
        public IActionResult Index()
        {
            var name = TempData["UserName"];
            ViewBag.Message = $"Welcome {name}";
            return View();
        }
    }
}
