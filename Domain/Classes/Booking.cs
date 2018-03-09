using System;

namespace Domain.Classes
{
    public class Booking
    {
        public string Id { get; set; }
        public string VehicleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime PickupDate{ get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CancelledAt{ get; set; }
        public string CancelledReason { get; set; }
    }
}
