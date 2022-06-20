using AutoMapper;
using BowlingAlleyDAL.Models;

namespace BowllingAlleyASP.NETCoreWebApp.BowlingAlleyASP.NET_Repository
{
    public class BowlllingAlleyMapper:Profile
    {
        public BowlllingAlleyMapper()
        {
            CreateMap<Reservations, Models.Reservations>();
            CreateMap<ReservationDetails, Models.ReservationDetails>();
            CreateMap<BookingSlots, Models.BookingSlots>();
        }
    }
}
