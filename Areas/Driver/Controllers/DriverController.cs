using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TMS_APP.Constants;
using TMS_APP.Data;
using TMS_APP.Models;
using TMS_APP.Models.DTO;
using TMS_APP.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using TMS_APP.Controllers;
using System.Security.Claims;

namespace TMS_APP.Areas.Driver.Controllers
{
    [Area("Driver")]
    [Authorize(Roles=RoleD.Role_Driver)]
    public class DriverController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserWithRolesController> _logger;
        public List<ApplicationUser> Users;

        public DriverController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ApplicationDbContext context, UserManager<ApplicationUser> userManager,
                                  RoleManager<IdentityRole> roleManager,
                                  SignInManager<ApplicationUser> signInManager,
                                  ILogger<UserWithRolesController> logger)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;

        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

/*
        [HttpPost]
        public IActionResult Login(User model)
        {
            // Simulate user authentication
            if (IsValidUser(model.email, model.password))
            {
                HttpContext.Session.SetString("UserEmail", model.email);
                HttpContext.Session.SetString("Role", "Driver");
                HttpContext.Session.SetInt32("UserId", 123);
                return RedirectToAction("Index"); // Redirect to home page
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        private bool IsValidUser(string email, string password)
        {
            User user = _unitOfWork.user.Get(c => c.email == email);
            if (user == null || user.password != password)
            {
                return false;
            }
            else
            {
                return true;
            }
            // Simulate user validation

        }*/

        public async Task<IActionResult> Index()
        {
            //string userEmail = HttpContext.Session.GetString("UserEmail");
          /*  var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail != null)
            {
                ApplicationUser? user =await _userManager.FindByEmailAsync(userEmail);
                return View(user);
            }*/

            var user = _userManager.GetUserAsync(User).Result;

            return View(user);
           

            
        }

        [AuthorizeUser]
        public async Task<IActionResult> Account()
        {
            var user = _userManager.GetUserAsync(User).Result;

            return View(user);

        }

        [AuthorizeUser]
        public IActionResult Edit()
        {
            /*if (HttpContext.Session == null)
            {
                return RedirectToAction("Login");
            }
            string userEmail = HttpContext.Session.GetString("UserEmail");

            User user = _unitOfWork.user.Get(c => c.email == userEmail);

            Driver driver = _unitOfWork.driver.Get(d => d.UserId == user.Id);*/

            var user = _userManager.GetUserAsync(User).Result;

            return View(user);

        }

        [HttpPost]

        public IActionResult Edit(ApplicationUser obj)
        {
/*
            string userEmail = HttpContext.Session.GetString("UserEmail");
            User user = _unitOfWork.user.Get(c => c.email == userEmail);
            Driver driver = _unitOfWork.driver.Get(d => d.UserId == user.Id);*/

            var user = _userManager.GetUserAsync(User).Result;

            if (ModelState.IsValid)
            {
                user.FirstName = obj.FirstName;
                user.lastName = obj.lastName;
                user.Email = obj.Email;
                user.PhoneNumber = obj.PhoneNumber;
                user.Availability = obj.Availability;
                _unitOfWork.Save();
                TempData["success"] = "You account updated successfully";
                return RedirectToAction("Account");
            }
            return View();
        }


        [AuthorizeUser]
        public IActionResult pwdReset()
        {
            if (HttpContext.Session == null)
            {
                return RedirectToAction("Login");
            }
            return View();

        }

        [HttpPost]

        public IActionResult pwdReset(ChangePasswordViewModel model)
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");

            if (ModelState.IsValid)
            {
                // Simulate resetting the password
                User user = _unitOfWork.user.Get(c => c.email == userEmail);
                if (user != null)
                {
                    user.password = model.NewPassword;
                    _unitOfWork.Save();
                    return RedirectToAction("Login", "Driver"); // Redirect to login page after resetting
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return View(model); ;

        }



        public async Task<IActionResult> Trips()
        {
            /*string userEmail = HttpContext.Session.GetString("UserEmail");
            User user = _unitOfWork.user.Get(c => c.email == userEmail);
            
                Driver driver= _unitOfWork.driver.Get(c => c.UserId == user.Id);

                Trip trip = _context.Trip.FirstOrDefault(c => c.DriverId == driver._id);*/


            return _context.Trip != null ?
                        View(await _context.Trip.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Trip'  is null.");
        }

       /* public async Task<IActionResult> MyTrips()
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");
            User user = _unitOfWork.user.Get(c => c.email == userEmail);

            Driver driver = _unitOfWork.driver.Get(c => c.UserId == user.Id);

            *//* Trip trip = _context.Trip.FirstOrDefault(c => c.DriverId == driver._id);*//*


            return _context.Trip.Where(c => c.DriverId == driver._id) != null ?
                        View(await _context.Trip.Where(c => c.DriverId == driver._id).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Trip'  is null.");
        }*/

        /* internal class AuthorizeUserAttribute : Attribute
         {
         }*/

        internal class AuthorizeUserAttribute : Attribute
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new RedirectToActionResult("Login", "Driver", null);
                }
            }
        }
    }
}
