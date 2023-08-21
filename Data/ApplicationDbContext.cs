using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMS_APP.Constants;
using TMS_APP.Models;

namespace TMS_APP.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
      
        public DbSet<Trip> Trip { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Pay> Pay { get; set; }


    }
}               