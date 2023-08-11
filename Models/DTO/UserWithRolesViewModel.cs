using TMS_APP.Constants;

namespace TMS_APP.Models.DTO
{
    public class UserWithRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }     
        public ApplicationUser User { get;set;}
        public IList<string> Roles { get; set;}
        public string Email { get; set; }
        public string Status { get; set; }
                
    }
}
