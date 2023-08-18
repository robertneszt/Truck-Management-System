using System.ComponentModel.DataAnnotations;

namespace TMS_APP.Constants
{
    public enum TripStatus
    {
        Complete=0,
       /* [Display(Name = "In Progress")]*/
        InProgress=1,
       /* [Display(Name = "Assigned")]*/
        Assigned=2,
      /*  [Display(Name = "Unassigned")]*/
        Unassigned=3,
    }
}
