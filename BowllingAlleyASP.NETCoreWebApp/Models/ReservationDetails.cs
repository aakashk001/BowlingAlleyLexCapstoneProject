using System;

namespace BowllingAlleyASP.NETCoreWebApp.Models
{
    public class ReservationDetails
    {
        public string EmpName { get; set; }
        public int ReservationId { get; set; }

        public DateTime ReservedOn { get; set; }

        public TimeSpan SlotEndTime { get; set; }
        public int SlotId { get; set; }
        public TimeSpan SlotStartTime { get; set; }
    }
}
