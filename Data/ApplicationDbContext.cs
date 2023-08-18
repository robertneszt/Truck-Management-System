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
        public DbSet<User> Users2 { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

   

      /*  protected override void OnModelCreating(ModelBuilder modelBuilder)
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
                },
                 new Driver
                 {
                     _id = 1002,
                     UserId = 101,
                     PayRate = 10,
                     Availability = true
                 }
                );
            modelBuilder.Entity<Trip>().HasData(
                new Trip
                {
                    Id =1,
                    CustomerName = "John Doe",
                    PickupLocationAddress = "123 Main St",
                    PickupLocationCity = "Exampleville",
                    PickupLocationCountry = "Country A",
                    DeliveryLocationAddress = "456 Elm St", // Provide a value for this property
                    DeliveryLocationCity = "Destination City",
                    DeliveryLocationCountry = "Country B",
                    PickupDate = DateTime.Now,
                    DeliveryDate = DateTime.Now.AddDays(1),
                    ShipmentWeight = 100.5m,
                    TotalAmount = 250.75m,
                    Quantity = 3,
                    Status = TripStatus.Assigned,
                    DriverName = "Jane Smith",
                    DriverId = 1001 // Assuming you have a driver with this ID
                },
                new Trip
                {
                    Id = 2,
                    CustomerName = "Alice Johnson",
                    PickupLocationAddress = "789 Oak St",
                    PickupLocationCity = "Sampletown",
                    PickupLocationCountry = "Country C",
                    DeliveryLocationAddress = "987 Maple St",
                    DeliveryLocationCity = "Destinationville",
                    DeliveryLocationCountry = "Country D",
                    PickupDate = DateTime.Now.AddDays(2),
                    DeliveryDate = DateTime.Now.AddDays(3),
                    ShipmentWeight = 75.0m,
                    TotalAmount = 150.25m,
                    Quantity = 2,
                    Status = TripStatus.Unassigned,
                    DriverName=" Smith John",
                    DriverId=1002

                }
                );
        }
*/


    }
}               