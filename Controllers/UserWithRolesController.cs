using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS_APP.Constants;
using TMS_APP.Data;
using TMS_APP.Models;
using TMS_APP.Models.DTO;

namespace TMS_APP.Controllers
{

    public class UserWithRolesController : Controller
    {   
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager; 
        private readonly ILogger<UserWithRolesController> _logger;
        private readonly ApplicationDbContext _context;
        public List<ApplicationUser> Users;

        public UserWithRolesController(UserManager<IdentityUser> userManager,
                                  RoleManager<IdentityRole> roleManager,
                                  SignInManager<IdentityUser> signInManager,
                                  ILogger<UserWithRolesController> logger,
                                  ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _context = context;
        }

        


        // GET: UserRoleController
        public async Task<IActionResult> ListUsersWithRoles()
        {
            var usersWithRoles = new List<UserWithRolesViewModel>();

            var users = await _context.Users.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                usersWithRoles.Add(new UserWithRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email= user.Email,
                    Roles = roles,
                    Status= user.Status.ToString()
                });
            }

            return View(usersWithRoles);
        }

        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: UserRoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserRoleController/Edit/5
        public ActionResult Edit(int id)
        {


            return View();
        }

        // POST: UserRoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserRoleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserRoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }

}
