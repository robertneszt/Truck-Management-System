using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using TMS_APP.Constants;

namespace TMS_APP.Models.DTO
{
    public class UserWithRolesViewModel
    {
        public string UserId { get; set; }
        [AllowNull]
        public string? UserName { get; set; }
        [AllowNull]
        public string? FirstName { get; set; }
        [AllowNull]
        public string? LastName { get; set; }
        [AllowNull]
        public string? PhoneNumber { get; set; }
        [AllowNull]
        public ApplicationUser? User { get; set; }
        [AllowNull]
        public IList<string>? Roles { get; set; }
        [AllowNull]
        public string? Email { get; set; }
        [AllowNull]
        public double? PayRate { get; set; }
        [AllowNull]
        public string? Status { get; set; }
        [AllowNull]
        public bool Availability { get; set; }
        [AllowNull]
        public List<IdentityRole>? AllRoles { get; set; }
        [AllowNull]
        public IList<string>? UserRoles { get; set; }
        [AllowNull]
        public IEnumerable<string>? SelectedRoles { get; set; }

    }
}
