using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS_APP.Constants;
using TMS_APP.Data;
/*using TMS_APP.Migrations;*/
using TMS_APP.Models;
using TMS_APP.Models.DTO;


namespace TMS_APP.Controllers
{

    public class UserWithRolesController : Controller
    {   
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager; 
        private readonly ILogger<UserWithRolesController> _logger;
        private readonly ApplicationDbContext _dbcontext;
        public List<ApplicationUser> Users;

        public UserWithRolesController(UserManager<ApplicationUser> userManager,
                                  RoleManager<IdentityRole> roleManager,
                                  SignInManager<ApplicationUser> signInManager,
                                  ILogger<UserWithRolesController> logger,
                                  ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _dbcontext = context;
        }




        // GET: UserRoleController
        public async Task<IActionResult> ListUsersWithRoles()
        {
            var usersWithRoles = new List<UserWithRolesViewModel>();
            var users = await _dbcontext.Users.ToListAsync();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                usersWithRoles.Add(new UserWithRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName=user.FirstName,
                    LastName=user.LastName,
                    DateOfBirth=user.DateOfBirth,
                    HireDate=user.HireDate,
                    Email = user.Email,
                    Roles = roles,
                    Status = user.Status.ToString(),
                    PayRate=user.PayRate
                });
            }
                return View(usersWithRoles);
            
        }
        // GET: UserWithRolesController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                // Handle scenario when user is not found
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var userWithRoleViewModel = new UserWithRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Status = user.Status.ToString(),
                DateOfBirth = user.DateOfBirth,
                HireDate = user.HireDate,
                Gender = user.Gender,
                PayRate = user.PayRate,
                Availability = user.Availability,
                Roles = roles, // Join the roles using a separator  in the view page <p>Roles: @string.Join(", ", Model.Roles)</p>
                Password = user.PasswordHash
            };

            return View(userWithRoleViewModel);
        }




        // GET: UserRoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRolesController/Create
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

        // GET: UserWithRolesController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            var payRate = user.PayRate ?? 0.0;
            var availability = user.Availability;
            var userName = user.UserName;


            var UserRolemodel = new UserWithRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,             
                DateOfBirth = user.DateOfBirth,
                HireDate = user.HireDate,
                Gender = user.Gender,
                PayRate = user.PayRate,
                Availability = user.Availability,
                /*Roles = roles, */// Join the roles using a separator  in the view page <p>Roles: @string.Join(", ", Model.Roles)</p>
                Password = user.PasswordHash,
                Status = user.Status.ToString() ?? "Suspended", // Assuming you've added a property for user status
                AllRoles = roles,
                UserRoles = userRoles
            };

            return View(UserRolemodel);
        }

        // POST: UserRoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserWithRolesViewModel model)
        {

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            // //update the role of the user

            //var currentRoles = await _userManager.GetRolesAsync(user);
            //var newRolesToAdd = model.SelectedRoles;

            //If you want to set a default value for model.SelectedRoles, it's important to ensure that you're assigning a valid collection(list, array, etc.) of role names.If you want to set a default role when no roles are selected, you might consider something like this:

            //if (model.SelectedRoles == null || !model.SelectedRoles.Any())
            //{
            //    model.SelectedRoles = new List<string> { Constants.Roles.Driver.ToString() }; // Assigning a default role as driver
            //}

            //var newRolesToAdd = model.SelectedRoles.Except(currentRoles);
            //var rolesToRemove = currentRoles.Except(model.SelectedRoles);
            //await _userManager.AddToRolesAsync(user, newRolesToAdd);
            //await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            //var selectedRole = model.SelectedRoles;
            //await _userManager.RemoveFromRolesAsync(user, currentRoles);
            //await _userManager.AddToRolesAsync(user, selectedRole);


            //// option 2
            //if (model.SelectedRoles == null)
            //{
            //    // If model.SelectedRoles is null, initialize it with an empty collection
            //    model.SelectedRoles = new List<string>();
            //}

            //var newRolesToAdd = model.SelectedRoles.Except(currentRoles);
            //var rolesToRemove = currentRoles.Except(model.SelectedRoles);

            //await _userManager.AddToRolesAsync(user, newRolesToAdd);
            //await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            //// option 3 
            ////

            var currentRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = model.UserRoles.Except(currentRoles);
            var rolesToRemove = currentRoles.Except(model.UserRoles);

            await _userManager.AddToRolesAsync(user, rolesToAdd);
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

 
            ////user.UserName = model.UserName;
            user.PayRate = model.PayRate;
            user.Availability = model.Availability;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DateOfBirth = model.DateOfBirth;
            user.HireDate = model.HireDate;
            user.Gender = model.Gender;
            user.PayRate = model.PayRate;

         

            //var changePasswordResult = await _userManager.ChangePasswordAsync(user, CurrentPassword, model.NewPassword);
            //if (!changePasswordResult.Succeeded)
            //{
            //    // Handle password change failure
            //}

            //user.PasswordHash = model.NewPassword;

            // Parse the user's status string to UserStatus enum
            if (Enum.TryParse(model.Status, out UserStatus statusEnum))
            {
                user.Status = statusEnum;
            }
            else
            {
                // Handle the case where parsing fails, set a default status or handle an error
                user.Status = UserStatus.Suspended; // Replace DefaultStatus with the default value
            }

            // Update the user's properties to the database
            var result = await _userManager.UpdateAsync(user);
            await _dbcontext.SaveChangesAsync(); // Make sure to await this call

            if (result.Succeeded)
            {
                // Successfully updated the user's properties
                return RedirectToAction("ListUsersWithRoles"); // 
            }
            else
            {
                // Failed to update the user's properties, handle the error
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                // You might return the view with the model to display error messages
                return View(model); ;



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
