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
using System.Security.Claims;
using NuGet.Versioning;

namespace TMS_APP.Controllers
{
    
    [Authorize(Roles = RoleD.Role_Driver)]
    public class DriverController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _dbcontext;
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
            _dbcontext = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;

        }


    
        public async Task<IActionResult> Index()
        {
           //Pass pending trip to notice field

            var user =await _userManager.GetUserAsync(User);

            List<Trip> myTrips = await _dbcontext.Trip.Where(c => c.DriverId == user.Id).ToListAsync();

            var pendingTrip = myTrips.FirstOrDefault(t => t.Status == TripStatus.PendingAssign);
            return View(pendingTrip);
                      
        }

      
        public async Task<IActionResult> Account()
        {
            var user =await _userManager.GetUserAsync(User);
            return View(user);
        }

       
        public IActionResult Edit()
        {
           

            var user = _userManager.GetUserAsync(User).Result;
            return View(user);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(ApplicationUser obj)
        {
             var user =await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                user.FirstName = obj.FirstName;
                user.LastName = obj.LastName;
                user.Email = obj.Email;
                user.PhoneNumber = obj.PhoneNumber;
                user.Availability = obj.Availability;
                _unitOfWork.Save();
                TempData["success"] = "You account updated successfully";
                return RedirectToAction("Account");
            }
            return View();
        }

     
        // Weiguang 
        // Display all available Trips
        public async Task<IActionResult> Trips()
        {
            List<Trip> myTrips = await _dbcontext.Trip.Where(c => c.Status==TripStatus.Unassigned).ToListAsync();
            return myTrips != null ? View(myTrips) :
                        Problem("No Trips available now.Please check with the dispatcher!");
        }

        // Disaplay the user trips
        public async Task<IActionResult> MyTrips()
        {
            var user =await _userManager.GetUserAsync(User);
            List<Trip> myTrips = await _dbcontext.Trip.Where(c => c.DriverId == user.Id).ToListAsync();
            return myTrips!= null ? View(myTrips) :
                        Problem("You have no trips assigned yet. Please check with your dispatcher!");
        }

        // Pay Page
        public async Task<IActionResult> payAdvise() {
            var user = await _userManager.GetUserAsync(User);
            List<DriverTripViewModel> tripViewModels = new List<DriverTripViewModel>();
            List<Trip> tripList = await _dbcontext.Trip.Where(c => c.DriverId == user.Id).ToListAsync();
                var TripDirverView = new DriverTripViewModel()
                {
                    Trips = tripList,
                    User = user
                };
            return  View(TripDirverView);
        }

        // Driver Dashboard >> Trip detail page
        public async Task<IActionResult> TripDetail(int? id)
        {
            if (id == null || _dbcontext.Trip == null)
            {
                return NotFound();
            }

            var trip = await _dbcontext.Trip
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        // Reject Trip
        public async Task<IActionResult> ReleaseTrip(int? tripId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var trip = await _dbcontext.Trip.FindAsync(tripId);
                    if (trip != null)
                    {
                        trip.Status = TripStatus.Unassigned;
                        trip.DriverId = null;
                        trip.DriverName = null;
                        _dbcontext.Update(trip);
                        await _dbcontext.SaveChangesAsync();
                        return RedirectToAction("MyTrips");
                    }  
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }

            return View();
        }

        // Accept Trip
        public async Task<IActionResult> AcceptTrip(int? tripId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user =await _userManager.GetUserAsync(User);
                    var trip = await _dbcontext.Trip.FindAsync(tripId);
                    if (trip != null)
                    {
                        trip.Status = TripStatus.Unassigned;
                        trip.DriverId = user.Id;
                        trip.DriverName = user?.FirstName + " " + user?.LastName;
                        trip.Status = TripStatus.Assigned;
                        _dbcontext.Update(trip);
                        await _dbcontext.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }

            return View();
        }

        //Complete Trip:
        

        public async Task<IActionResult> CompleteTrip(int? tripId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var trip = await _dbcontext.Trip.FindAsync(tripId);
                    if (trip != null)
                    {
                        trip.Status = TripStatus.Complete;
                        _dbcontext.Update(trip);
                        await _dbcontext.SaveChangesAsync();
                        return RedirectToAction("MyTrips");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }

            return View();
        }

        // Pick up load:

        
        public async Task<IActionResult> PickUpLoad(int? tripId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var trip = await _dbcontext.Trip.FindAsync(tripId);
                    if (trip != null)
                    {
                        Pay pay = new()
                        {
                            TripId = trip.Id,
                            // Add hard coded mile before Map is ready
                            EstimateDistance= 3000
                        };
                        trip.Status = TripStatus.PickedUp;
                        _dbcontext.Update(trip);
                        await _dbcontext.SaveChangesAsync();
                        return RedirectToAction("MyTrips");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }

            return View();
        }

        //DeliveredTrip

        public async Task<IActionResult> DeliveredTrip(int? tripId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var trip = await _dbcontext.Trip.FindAsync(tripId);
                    if (trip != null)
                    {
                        trip.Status = TripStatus.Delevered;
                        _dbcontext.Update(trip);
                        await _dbcontext.SaveChangesAsync();
                        return RedirectToAction("MyTrips");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return View();
        }

        //Load in Transportation:

        public async Task<IActionResult> InTransportation(int? tripId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var trip = await _dbcontext.Trip.FindAsync(tripId);
                    if (trip != null)
                    {
                        trip.Status = TripStatus.InProgress;
                        _dbcontext.Update(trip);
                        await _dbcontext.SaveChangesAsync();
                        return RedirectToAction("MyTrips");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return View();
        }

        //



        public async Task<IActionResult> ReleaseTripAndMarkUnavailable(int? tripId, string driverId)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    var trip = await _dbcontext.Trip.FindAsync(tripId);
                    if (trip != null)
                    {
                        trip.Status = TripStatus.Unassigned;
                        trip.DriverId = null;
                        trip.DriverName = null;
                        _dbcontext.Update(trip);
                        await _dbcontext.SaveChangesAsync();
                        return RedirectToAction("MyTrips");
                    }
                    var driver = await _userManager.FindByIdAsync(driverId);
                    if(driver != null)
                    {
                        driver.Availability = false; 
                        await _dbcontext.SaveChangesAsync();
                     
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return View();
        }
    }
}
