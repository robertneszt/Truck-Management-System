using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TMS_APP.Constants;

namespace TMS_APP.Models
{
    public class Trip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName ("Customer Name")]
        public string CustomerName { get; set; }
                
        [DisplayName("Pick up Address")]
        public string PickupLocationAddress { get; set; }
        [DisplayName("Pick City")]
        public string PickupLocationCity { get; set; }
      
        [DisplayName("Pick Country")]
        public string PickupLocationCountry { get; set; }
       
        [DisplayName("Delivery Address")]
        public string DeliveryLocationAddress { get; set; }
       
        [DisplayName("Delivery City")]
        public string DeliveryLocationCity { get; set; }
        
        [DisplayName("Delivery Country")]
        public string DeliveryLocationCountry { get; set; }
      
        [DisplayName("Pick Date")]
        public DateTime PickupDate { get; set; }
      
        [DisplayName("Delivery Date")]
        public DateTime DeliveryDate { get; set; }
       
        [DisplayName("Weight")]
        public decimal ShipmentWeight { get; set; }
       
        [DisplayName("Amount")]
        public decimal TotalAmount { get; set; }
      
        [DisplayName("Quantity")]
        public decimal Quantity { get; set; }
       
        [DisplayName("Status")]
        public TripStatus Status { get; set; }

        [DisplayName("Driver Name")]
        public string DriverName { get; set; }

        [DisplayName("Driver ID")]
        public int DriverId { get; set; }
        [ForeignKey("DriverId")]
      
        public Driver driver { get; set; }

    }
}
