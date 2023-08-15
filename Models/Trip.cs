using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TMS_APP.Constants;

namespace TMS_APP.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName ("Customer Name")]
        public string CustomerName { get; set; }

        // [Required]
        [DisplayName("Pick up Address")]
        public string PickupLocationAddress { get; set; }
        //[Required]
        [DisplayName("Pick City")]
        public string PickupLocationCity { get; set; }
        // [Required]
        [DisplayName("Pick Country")]
        public string PickupLocationCountry { get; set; }
        //[Required]
        [DisplayName("Delivery Address")]
        public string DeliveryLocationAddress { get; set; }
        //[Required]
        [DisplayName("Delivery City")]
        public string DeliveryLocationCity { get; set; }
        //[Required]
        [DisplayName("Delivery Country")]
        public string DeliveryLocationCountry { get; set; }
        //[Required]
        [DisplayName("Pick Date")]
        public DateTime PickupDate { get; set; }
        //[Required]
        [DisplayName("Delivery Date")]
        public DateTime DeliveryDate { get; set; }
        //[Required]
        [DisplayName("Weight")]
        public decimal ShipmentWeight { get; set; }
        //[Required]
        [DisplayName("Amount")]
        public decimal TotalAmount { get; set; }
        //[Required]
        [DisplayName("Quantity")]
        public decimal Quantity { get; set; }
        //[Required]
        [DisplayName("Status")]
        public TripStatus Status { get; set; }

        [DisplayName("Driver Name")]
        public string DriverName { get; set; }



    }
}
