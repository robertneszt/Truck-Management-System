using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
              return _dbcontext.Trip != null ? 
                          View(await _dbcontext.Trip.ToListAsync()) :
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
                        LastName = user.lastName,
                        Availability = user.Availability,
                        PayRate = user.PayRate,
                        PhoneNumber = user.PhoneNumber,
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

        //Weiguang commented out
       /* public async Task<IActionResult> Create([Bind("Id,CustomerName,PickupLocationAddress,PickupLocationCity,PickupLocationCountry,DeliveryLocationAddress,DeliveryLocationCity,DeliveryLocationCountry,PickupDate,DeliveryDate,ShipmentWeight,TotalAmount,Quantity,Status,DriverName")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Add(trip);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }*/


        //Weiguang modified the create method

        public async Task<IActionResult> Create(DriverTripViewModel? driverTripView)
        {
            if (ModelState.IsValid)
            {
                Trip trip = driverTripView.trip;
                trip.DriverId = driverTripView.trip.DriverId;

                var driver = await _userManager.FindByIdAsync(trip.DriverId);

                trip.DriverName=driver?.lastName+""+driver?.lastName;
                
                _dbcontext.Update(trip);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
                        LastName = user.lastName,
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

                    trip.DriverName = driver?.lastName + "" + driver?.lastName;

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

        private bool TripExists(int id)
        {
          return (_dbcontext.Trip?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
