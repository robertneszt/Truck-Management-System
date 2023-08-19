using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using TMS_APP.Constants;

namespace TMS_APP.Models.DTO
{
    public class DriverTripViewModel
    {
        public int TripId { get; set; }
        [AllowNull]
        public string? CustomerName { get; set; }
        [DisplayName("Status")]
        public string? Status { get; set; }
       
        [AllowNull]

        [DisplayName("Driver Name")]
        public string? DriverName { get; set; }
        [AllowNull]
        public string? UserId { get; set; }
        [AllowNull]
        public Trip? trip { get; set; }
        [AllowNull]
        [ValidateNever]
        public ApplicationUser? User { get; set; }
       /* [AllowNull]
        public List<ApplicationUser> Drivers { get; set; }*/
        [AllowNull]
        [ValidateNever]
        public IEnumerable<SelectListItem> DriverList { get; set; }

        [AllowNull]
        [ValidateNever]
        public List<Trip> Trips { get; set; }
    }
}
