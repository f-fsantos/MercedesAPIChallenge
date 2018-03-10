using System;

namespace Mercedes_Benz.io_Challenge.ViewModels
{
    public class CancelBookingViewModel
    {
        public string Id { get; set; }
        public string CancelledReason{ get; set; }
        public DateTime CancelledAt { get; set; }
    }
}
