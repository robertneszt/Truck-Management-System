using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TMS_APP.Models
{
    public class DriverTripViewModel
    {
        public Trip trip { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> DriverList { get; set; }
    }
}
