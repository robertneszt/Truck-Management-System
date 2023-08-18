using Microsoft.AspNetCore.Mvc;
using TMS_APP.Data;
using TMS_APP.Models;
using TMS_APP.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TMS_APP.Models.DTO;
using TMS_APP.Controllers;

namespace YourApplication.Controllers
{
    public class DispatchController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserWithRolesController> _logger;
        private readonly ApplicationDbContext _dbcontext;
        public List<ApplicationUser> Users;
        public DispatchController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager,
                                  RoleManager<IdentityRole> roleManager,
                                  SignInManager<ApplicationUser> signInManager,
                                  ILogger<UserWithRolesController> logger
                                  )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _dbcontext = dbContext;


        }

        // Display the list of all the drivers
        public async Task<IActionResult> DriverManagement()
        {
            /*List<ApplicationUser> drivers = _dbContext.Users.ToList().FindAll(x => x.Role == "Driver");
            return View("DriverManagement", drivers);*/

            var userDriver = new List<UserWithRolesViewModel>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Driver"))
                {
                    userDriver.Add(new UserWithRolesViewModel
                    {
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Availability = user.Availability,
                        PayRate = user.PayRate,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email
                    });
                }
               
            }
            return View(userDriver);
        }

        // Update a driver's pay rate
        [HttpPost]
        public async Task<ActionResult> UpdatePayRate(string driverId, double newPayRate)
        {
           var driver =  await _userManager.FindByIdAsync(driverId);
/*           var driver2 =   _dbContext.Users.FirstOrDefault(u => u.Id == driverId);
*/            if (driver != null)
            {
                driver.PayRate = (double)newPayRate;
                await _userManager.UpdateAsync(driver);
               // _dbContext.SaveChanges();
            }
            return RedirectToAction("DriverManagement");
        }


        // Add a driver to the work pool (active vs inactive)
        public async Task<ActionResult> AddDriverToWorkPool(string driverId)
        {
            var driver = await _userManager.FindByIdAsync(driverId);
            if (driver != null)
            {
                driver.Availability = true;
                /*_dbContext.Drivers.Update(driver);
                 */
                await _userManager.UpdateAsync(driver);
               /* _dbContext.SaveChanges();*/
            }
            return RedirectToAction("DriverManagement");
        }

        public async Task<ActionResult> RemoveDriverFromWorkPool(string driverId)
        {
            var driver = await _userManager.FindByIdAsync(driverId);
            if (driver != null)
            {
                driver.Availability = false;
                /*                _dbContext.Drivers.Update(driver);
                               _dbContext.SaveChanges();*/

                await _userManager.UpdateAsync(driver);
            }
            return RedirectToAction("DriverManagement");
        }

    }

}


