using System;

namespace BowllingAlleyASP.NETCoreWebApp.Models
{
    public class BookingSlots
    {
        public TimeSpan SlotEndTime { get; set; }

        public int SlotId { get; set; }

        public TimeSpan SlotStartTime { get; set; }
    }
}
