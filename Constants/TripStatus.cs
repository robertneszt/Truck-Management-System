using System.ComponentModel.DataAnnotations;

namespace TMS_APP.Constants
{
    public enum TripStatus
    {
        Complete=0,
        Delevered=1,
        /* [Display(Name = "In Progress")]*/
        InProgress=2,
        /* [Display(Name = "Assigned")]*/
        PickedUp=3,
        Assigned=4,
        /* [Display(Name = "Unassigned")]*/
        PendingAssign = 5,
        Unassigned =6
    }
}
