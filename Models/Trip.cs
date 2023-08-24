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

        [AllowNull]
        public string? CustomerName { get; set; }
        [AllowNull]
        [DisplayName("Pick up Address")]
        public string? PickupLocationAddress { get; set; }
        [Required]
        [DisplayName("Pick City")]
        public string? PickupLocationCity { get; set; }
        [Required]
        [DisplayName("Pick State/Province")]
        public string? PickupLocationState { get; set; }
        [Required]

        [DisplayName("Pick Country")]
        public string? PickupLocationCountry { get; set; }
        [AllowNull]

        [DisplayName("Delivery Address")]
        public string? DeliveryLocationAddress { get; set; }
        [Required]

        [DisplayName("Delivery City")]
        public string? DeliveryLocationCity { get; set; }
        [Required]
        [DisplayName("Delivery State/Province")]
        public string? DeliveryLocationState { get; set; }
        [Required]

        [DisplayName("Delivery Country")]
        public string? DeliveryLocationCountry { get; set; }
        [AllowNull]

        [DisplayName("Pick Date")]
        public DateTime PickupDate { get; set; } = DateTime.Now;
        [AllowNull]
        [DisplayName("Delivery Date")]
        public DateTime DeliveryDate { get; set; } = DateTime.Now;
        [AllowNull]

        [DisplayName("Weight")]
        public decimal? ShipmentWeight { get; set; }
        [AllowNull]

        [DisplayName("Amount")]
        public decimal? TotalAmount { get; set; }
        [AllowNull]

        [DisplayName("Quantity")]
        public decimal? Quantity { get; set; }
        [AllowNull]

        [DisplayName("Status")]
        public TripStatus Status { get; set; }
        [AllowNull]

        [DisplayName("Driver ID")]
        public string? DriverId { get; set; }
        [AllowNull]

        [DisplayName("Driver Name")]
        public string? DriverName { get; set; }
        [AllowNull]
        public double? EstimateDistance { get; set; }
    }
}
