using Microsoft.AspNetCore.Mvc;
using TMS_APP.Models;
using TMS_APP.Models.DTO;
using TMS_APP.Repository.IRepository;

namespace TMS_APP.Controllers
{
    public class DriverController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DriverController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {
            // Simulate user authentication
            if (IsValidUser(model.email, model.password))
            {
                HttpContext.Session.SetString("UserEmail", model.email);
                //HttpContext.Session.SetInt32("UserId", 123);
                return RedirectToAction("Index"); // Redirect to home page
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        private bool IsValidUser(string email, string password)
        {
            User user = _unitOfWork.user.Get(c=>c.email==email);
            if (user == null|| user.password != password)
            {
               return false;
            }
            else {
                return true;
            }
            // Simulate user validation
         
        }

        public IActionResult Index()
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");

            User user = _unitOfWork.user.Get(c => c.email == userEmail);
           
            return View(user);
        }

        public IActionResult Account()
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");

            User user = _unitOfWork.user.Get(c => c.email == userEmail);

            Driver driver = _unitOfWork.driver.Get(d => d.UserId == user.Id);
            return View(driver);

        }
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
