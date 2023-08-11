using Microsoft.AspNetCore.Identity;


namespace TMS_APP.Models
{
    
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public Constants.UserStatus Status { get;set;}

        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public string Gender { get; set; }  
        public decimal PayRate { get; set; }
        public bool Availability { get; set; }
    }
}
