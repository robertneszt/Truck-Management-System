using System.Diagnostics.CodeAnalysis;

namespace TMS_APP.Models.DTO
{
    public class PayViewModel
    {
        public int Id { get; set; }
        [AllowNull]
        public string? UserID { get; set; }
        [AllowNull]
        public string? UserName { get; set;}
        [AllowNull]
        public Pay? CurrentTripPay { get; set; }
        [AllowNull]
        public List<Pay>? CompletedPays  { get; set;}
        [AllowNull]
        public List<Pay>? UnfinishedPays { get; set; }
        [AllowNull]
        public List<Trip>? TotalTrips { get; set; }
    }
}
