// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMS_APP.Models;


namespace TMS_APP.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }
       // public string FirstName { get; set; }
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [AllowNull]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "First name")]
            [AllowNull]
            public string FirstName { get; set; }
            [Display(Name = "Last name")]
            [AllowNull]
            public string LastName { get; set; }

            [Display(Name = "Date of Birth")]
            [AllowNull]
            public DateTime? DOB { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
             var foundUser =  _userManager.FindByIdAsync(user.Id);
            Username = userName;
           
            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = foundUser.Result.FirstName,
                LastName= foundUser.Result.lastName,
                DOB= foundUser.Result.DateOfBirth
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var SaveUser = await _userManager.GetUserAsync(User);

            if (SaveUser == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(SaveUser);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(SaveUser);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(SaveUser, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            var firstName = SaveUser.FirstName;
            if (Input.FirstName != firstName)
            {
                SaveUser.FirstName = Input.FirstName;
               
            }
            var lastName = SaveUser.lastName;
            if (Input.LastName  != lastName)
            {
                SaveUser.lastName = Input.LastName;

            }

            var dateOfBirth = SaveUser.DateOfBirth;
            if (Input.DOB != dateOfBirth)
            {
                SaveUser.DateOfBirth = Input.DOB;

            }
            await _userManager.UpdateAsync(SaveUser);
            await _signInManager.RefreshSignInAsync(SaveUser);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
