using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TMS_APP.Constants;
using TMS_APP.Data;
using TMS_APP.Models;
using TMS_APP.Models.DTO;
using TMS_APP.Repository.IRepository;

namespace TMS_APP.Controllers
{
    public class TripsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserWithRolesController> _logger;
        private readonly ApplicationDbContext _dbcontext;
        public List<ApplicationUser> Users;
        public TripsController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager,
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

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            var userDriver = new List<UserWithRolesViewModel>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Driver") && user.Availability == true)
                {
                    userDriver.Add(new UserWithRolesViewModel
                    {
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        UserName = user.UserName,
                        LastName = user.LastName,
                        Availability = user.Availability,
                        PayRate = user.PayRate,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email

                    });
                }

            }
            List<DriverTripViewModel> tripViewModels = new List<DriverTripViewModel>();
            List<Trip> tripList = await _dbcontext.Trip.ToListAsync();

            foreach (var trip in tripList)
            {
                var TripDirverView = new DriverTripViewModel()
                {
                    trip = trip,
                    DriverList = userDriver.Select(u => new SelectListItem
                    {
                        Text = u.UserName.ToString(),
                        Value = u.UserId.ToString()
                    })

                };
                tripViewModels.Add(TripDirverView);
            }
           

            return tripViewModels != null ?
                          View(tripViewModels):
                          Problem("Entity set 'ApplicationDbContext.Trip'  is null.");

        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Trips/Create
        public async Task<IActionResult> Create()
        {
            var userDriver = new List<UserWithRolesViewModel>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Driver") && user.Availability == true)
                {
                    userDriver.Add(new UserWithRolesViewModel
                    {
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        UserName = user.UserName,
                        LastName = user.LastName,
                        Availability = user.Availability,
                        PayRate = user.PayRate,
                        /*PhoneNumber = user.PhoneNumber,*/
                        Email = user.Email
                       
                    });
                }

            }

            var TripDirverView = new DriverTripViewModel() {
                trip = new Trip(),
                DriverList = userDriver.Select(u => new SelectListItem
                {
                    Text = u.UserName.ToString(),
                    Value = u.UserId.ToString()
                })
            
            };       

            return View(TripDirverView);

        }

        // POST: Trips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]

        //Weiguang modified the create method

        public async Task<IActionResult> Create(DriverTripViewModel? driverTripView, string DriverId)
        {
            if (ModelState.IsValid)
            {
                try
                {

                Trip trip = driverTripView.trip;
                    if (trip != null)
                    {
                        if (DriverId != null)
                        {
                            trip.DriverId = DriverId;
                            var driver = await _userManager.FindByIdAsync(trip.DriverId);
                            if (driver != null)
                            {
                                trip.DriverName = driver?.FirstName + " " + driver?.LastName;
                                trip.Status = TripStatus.PendingAssign;
                                _dbcontext.Update(trip);
                                await _dbcontext.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                trip.DriverId = null;
                                trip.DriverName = null;
                                trip.Status = TripStatus.Unassigned;
                                _dbcontext.Update(trip);
                                await _dbcontext.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return View();
        }

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _dbcontext.Trip == null)
            {
                return NotFound();
            }

            var userDriver = new List<UserWithRolesViewModel>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Driver")&&user.Availability==true)
                {
                    userDriver.Add(new UserWithRolesViewModel
                    {
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        UserName = user.UserName,
                        LastName = user.LastName,
                        Availability = user.Availability,
                       /* PayRate = user.PayRate,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email*/

                    });

                }

            }

            var theTrip = await _dbcontext.Trip.FindAsync(id);
            if (theTrip == null)
            {
                return NotFound();
            }

            var TripDirverView = new DriverTripViewModel()
            {
                trip = theTrip,
                DriverList = userDriver.Select(u => new SelectListItem
                {
                    Text = u.UserName.ToString(),
                    Value = u.UserId.ToString()
                })

            };

            
            return View(TripDirverView);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DriverTripViewModel TripDirverView)
        {
            if (id != TripDirverView.trip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Trip trip = TripDirverView.trip;
                    trip.DriverId = TripDirverView.trip.DriverId;

                    var driver = await _userManager.FindByIdAsync(trip.DriverId);
                    if (driver != null)
                    {
                        trip.DriverName = driver?.LastName + "" + driver?.LastName;
                        trip.Status = Constants.TripStatus.Assigned;
                        _dbcontext.Update(trip);
                        await _dbcontext.SaveChangesAsync();
                        
                    }
                    trip.Status = Constants.TripStatus.Unassigned;
                    _dbcontext.Update(trip);
                    await _dbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(TripDirverView.trip.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(TripDirverView);
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_dbcontext.Trip == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Trip'  is null.");
            }
            var trip = await _dbcontext.Trip.FindAsync(id);
            if (trip != null)
            {
                _dbcontext.Trip.Remove(trip);
            }
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> UpdateStatus(int? tripId, DriverTripViewModel TripDirverView, TripStatus newStatus)
        {
            if (tripId != TripDirverView.TripId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var trip = await _dbcontext.Trip.FindAsync(tripId);
                    if (trip != null)
                    {
                        trip.Status = newStatus;

                        if (trip.Status == TripStatus.Unassigned)
                        {
                            trip.DriverId = null;
                            trip.DriverName = null;

                            _dbcontext.Update(trip);
                            await _dbcontext.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }else if(trip.Status == TripStatus.InProgress)
                        {
                            List<Trip> myTrips = await _dbcontext.Trip.Where(c => c.Status == TripStatus.InProgress).ToListAsync();
                            if (myTrips.FirstOrDefault(c => c.DriverId == trip.DriverId)!=null)
                            {
                                ViewBag.Message = string.Format("The Driver has another trip in progress!");
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                _dbcontext.Update(trip);
                                await _dbcontext.SaveChangesAsync();
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            _dbcontext.Update(trip);
                            await _dbcontext.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }

                    }
                        
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(TripDirverView.trip.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<ActionResult> UpdateDriver(int? tripId, DriverTripViewModel TripDirverView,string selectedDriver)
        {
            if (tripId != TripDirverView.TripId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var trip = await _dbcontext.Trip.FindAsync(tripId);
                    if (trip != null)
                    {
                        trip.DriverId = selectedDriver;
                        var driver = await _userManager.FindByIdAsync(trip.DriverId);
                        if (driver != null)
                        {
                            trip.DriverName = driver?.FirstName + " " + driver?.LastName;
                            trip.Status = TripStatus.PendingAssign;
                            _dbcontext.Update(trip);
                            await _dbcontext.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(TripDirverView.trip.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("Index");
        }

        private bool TripExists(int id)
        {
          return (_dbcontext.Trip?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
