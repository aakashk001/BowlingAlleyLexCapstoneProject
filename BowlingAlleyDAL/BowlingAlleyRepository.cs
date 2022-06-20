using System;
using System.Collections.Generic;
using System.Text;
using BowlingAlleyDAL.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace BowlingAlleyDAL
{
    public class BowlingAlleyRepository
    {
        BowlingAlleyDBContext context;

        public BowlingAlleyRepository()
        {
            context = new BowlingAlleyDBContext();
        }
        public List<Reservations> GetReservedSlots()
        {
            List<Reservations> reservedSlots = null;
            try
            {
                reservedSlots = context.Reservations
                                    .Where(r => r.ReservedOn == DateTime.Today && r.Status == 0)
                                    .ToList();
            }
            catch (Exception )
            {
                reservedSlots = null;
            }
            return reservedSlots;
        }
        public int ApproveOrReject(int reservationId, int slotStatus)
        {
            int status = 0;
            Reservations reservation = null;
            try
            {
                reservation = context.Reservations.Find(reservationId);
                reservation.Status = slotStatus;
                context.SaveChanges();
                status = 1;
            }
            catch (Exception )
            {
                status = -99;
            }
            return status;
        }
        public List<ReservationDetails> GetAllRejectedSlots()
        {
            List<ReservationDetails> reservationDetails = null;
            try
            {
                reservationDetails = context.ReservationDetails.FromSqlRaw("SELECT * FROM ufn_FetchAllRejectedSlots()").ToList();
            }
            catch (Exception )
            {
                reservationDetails = null;
            }
            return reservationDetails;


        }

        public List<BookingSlots> GetFreeSlots()
        {
            List<Reservations> reservations = null;
            List<BookingSlots> slots = null;
            List<BookingSlots> freeSlots = null;
            try
            {
                reservations = context.Reservations.Where(r => r.ReservedOn == DateTime.Today).ToList();
                slots = context.BookingSlots.ToList();
                freeSlots = context.BookingSlots.ToList();
                if (reservations != null)
                {
                    foreach (var r in reservations)
                    {
                        foreach (var s in slots)
                        {
                            if (r.SlotId == s.SlotId)
                            {
                                BookingSlots slot = s;
                                freeSlots.Remove(slot);
                            }

                        }
                    }
                }
                else
                {
                    freeSlots = null;
                }
            }
            catch (Exception )
            {

                freeSlots = null;
            }
            return freeSlots;
        }
        public int BookSlots(int slotId, int empId)
        {
            int result = 0;
            try
            {
                SqlParameter prmSlotId = new SqlParameter("@SlotId", slotId);
                SqlParameter prmEmpId = new SqlParameter("@EmpId", empId);
                SqlParameter prmReservationId = new SqlParameter("@ReservationId", System.Data.SqlDbType.Int);
                prmReservationId.Direction = System.Data.ParameterDirection.Output;

                result = context.Database.ExecuteSqlRaw("EXEC usp_BookSlots @SlotId, @EmpId, @ReservationId OUT", new[] { prmSlotId, prmEmpId, prmReservationId });
                empId = Convert.ToInt32(prmReservationId.Value);
            }
            catch (Exception)
            {
                result = -99;
                empId = 0;
            }
            return result;
        }
    }
}
