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
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        
        public TripsController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
              return _context.Trip != null ? 
                          View(await _context.Trip.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Trip'  is null.");
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Trip == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip
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
            var users = await _context.Users.ToListAsync();




            var TripDirverView = new DriverTripViewModel() {
                trip = new Trip(),
                DriverList = users.Select(u => new SelectListItem
                {
                    Text = u.FirstName,
                    Value = u.Id.ToString()
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
                _context.Add(trip);
                await _context.SaveChangesAsync();
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
                trip.DriverId = driverTripView.User.Id;
                
                _context.Add(driverTripView.trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trip == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerName,PickupLocationAddress,PickupLocationCity,PickupLocationCountry,DeliveryLocationAddress,DeliveryLocationCity,DeliveryLocationCountry,PickupDate,DeliveryDate,ShipmentWeight,TotalAmount,Quantity,Status,DriverName")] Trip trip)
        {
            if (id != trip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.Id))
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
            return View(trip);
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Trip == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip
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
            if (_context.Trip == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Trip'  is null.");
            }
            var trip = await _context.Trip.FindAsync(id);
            if (trip != null)
            {
                _context.Trip.Remove(trip);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(int id)
        {
          return (_context.Trip?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
