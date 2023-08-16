using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMS_APP.Models;

namespace TMS_APP.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<User> Users2 { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(
               new User { Id = 100, firstName = "danny",lastName="yang", email = "user@example.com",password="password",phone="87654321",role=User.Roles.Driver },
               new User { Id = 101, firstName = "danny2", lastName = "yang", email = "user2@example.com", password = "password2", phone = "87654321", role = User.Roles.Admin }
             

               );
            modelBuilder.Entity<Driver>().HasData(
                new Driver
                {
                    _id = 1001,
                   UserId=100,
                   PayRate=10,
                   Availability=true
                }
                );
    }



    }
}               