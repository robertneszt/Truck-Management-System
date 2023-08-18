using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace TMS_APP.Models
{
    
    public class ApplicationUser: IdentityUser
    {
        [AllowNull]
        public string? FirstName { get; set; }
        [AllowNull]
        public string? LastName { get; set; }
        [AllowNull]
        public Constants.UserStatus Status { get; set; }
        [AllowNull]

        public DateTime? DateOfBirth { get; set; }
        [AllowNull]
        public DateTime? HireDate { get; set; }
        [AllowNull]
        public string? Gender { get; set; }
        [AllowNull]
        public double? PayRate { get; set; }
        [AllowNull]
        public bool Availability { get; set; }
        [AllowNull]
        public string? Role { get; set; }
    }
}
