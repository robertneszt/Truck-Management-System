using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TMS_APP.Models
{
    public class Pay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [AllowNull]
        public int? TripId { get; set; }
        [AllowNull]
        [ValidateNever]
        public Trip? Trip { get; set; }
        [AllowNull]
        public double? PayRate { get; set; }
        [AllowNull]
        public double? EstimateDistance { get; set; }
        [AllowNull]
        public double? ConfirmedDistance { get; set; }
        [AllowNull]
        public double? PayAdjuestment { get; set; }
        [AllowNull]
        public double? FinalPay { get; set; }
        [AllowNull]
        public string? Note { get; set; }
    }
}
