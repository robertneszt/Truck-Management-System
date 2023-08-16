﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        [AuthorizeUser]
        public IActionResult Account()
        {
            if (HttpContext.Session == null)
            {
                return RedirectToAction("Login");
            }

            string userEmail = HttpContext.Session.GetString("UserEmail");

            User user = _unitOfWork.user.Get(c => c.email == userEmail);

            Driver driver = _unitOfWork.driver.Get(d => d.UserId == user.Id);
            return View(driver);

        }

        [AuthorizeUser]
        public IActionResult Edit()
        {
            if (HttpContext.Session == null)
            {
                return RedirectToAction("Login");
            }
            string userEmail = HttpContext.Session.GetString("UserEmail");

            User user = _unitOfWork.user.Get(c => c.email == userEmail);

            Driver driver = _unitOfWork.driver.Get(d => d.UserId == user.Id);
            return View(driver);

        }

        [HttpPost]

        public IActionResult Edit(Driver obj)
        {
                      
            string userEmail = HttpContext.Session.GetString("UserEmail");
            User user = _unitOfWork.user.Get(c => c.email == userEmail);
            Driver driver = _unitOfWork.driver.Get(d => d.UserId == user.Id);

            if (ModelState.IsValid)
            {
                user.firstName=obj.User.firstName; 
                user.lastName=obj.User.lastName;
                user.email=obj.User.email;
                user.phone = obj.User.phone;
                driver.Availability=obj.Availability;
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
                    return RedirectToAction("Login","Driver"); // Redirect to login page after resetting
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return View(model); ;

        }



        public IActionResult Dashboard()
        {
            return View();
        }
    }

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
