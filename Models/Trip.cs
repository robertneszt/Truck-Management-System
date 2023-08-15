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
        public string PickupLocationCountry { get; set; }
        //[Required]
        public string DeliveryLocationAddress { get; set; }
        //[Required]
        public string DeliveryLocationCity { get; set; }
        //[Required]
        public string DeliveryLocationCountry { get; set; }
        //[Required]
        public DateTime PickupDate { get; set; }
        //[Required]
        public DateTime DeliveryDate { get; set; }
        //[Required]
        public decimal ShipmentWeight { get; set; }
        //[Required]
        public decimal TotalAmount { get; set; }
        //[Required]
        public decimal Quantity { get; set; }
        //[Required]
        public TripStatus Status { get; set; }





    }
}
