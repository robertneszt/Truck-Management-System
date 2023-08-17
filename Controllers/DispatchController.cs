using Microsoft.AspNetCore.Mvc;
using TMS_APP.Data;
using TMS_APP.Models;
using TMS_APP.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace YourApplication.Controllers
{
    public class DispatchController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;

        public DispatchController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        // Display the list of all the drivers
        public IActionResult DriverManagement()
        {
            List<Driver> drivers = _dbContext.Drivers.ToList();
            return View("DriverManagement", drivers);
        }

        // Update a driver's pay rate
        [HttpPost]
        public IActionResult UpdatePayRate(int driverId, decimal newPayRate)
        {
            Driver driver = _dbContext.Drivers.Find(driverId);
            if (driver != null)
            {
                driver.PayRate = newPayRate;
                _dbContext.Drivers.Update(driver);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("DriverManagement");
        }


        // Add a driver to the work pool (active vs inactive)
        public IActionResult AddDriverToWorkPool(int driverId)
        {
            Driver driver = _dbContext.Drivers.Find(driverId);
            if (driver != null)
            {
                driver.Availability = true;
                _dbContext.Drivers.Update(driver);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("DriverManagement");
        }

        public IActionResult RemoveDriverFromWorkPool(int driverId)
        {
            Driver driver = _dbContext.Drivers.Find(driverId);
            if (driver != null)
            {
                driver.Availability = false;
                _dbContext.Drivers.Update(driver);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("DriverManagement");
        }

    }
}


