using System;

namespace BowllingAlleyASP.NETCoreWebApp.Models
{
    public class Reservations
    {
        public int ReservationId { get; set; }
        public int ReservedBy { get; set; }
        public DateTime ReservedOn { get; set; }
        public int SlotId { get; set; }
        public int? Status { get; set; }
    }
}
