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
                
                return RedirectToAction("Index"); // Redirect to home page
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        private bool IsValidUser(string email, string password)
        {
            User user = _unitOfWork.driver.Get(c=>c.email==email);
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
          return View();
        }

        public IActionResult Account()
        {
           
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
