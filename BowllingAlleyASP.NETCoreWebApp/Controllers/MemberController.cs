using Microsoft.AspNetCore.Mvc;
using BowlingAlleyDAL.Models;
using BowlingAlleyDAL;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace BowllingAlleyASP.NETCoreWebApp.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMapper _mapper;
        private readonly BowlingAlleyDBContext _context;
        BowlingAlleyRepository repObj;
        public MemberController(IMapper mapper, BowlingAlleyDBContext context)
        {
            _mapper = mapper;
            _context = context;
            repObj = new BowlingAlleyRepository();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetFreeSlots()
        {
            List<Models.BookingSlots> bookingslots = new List<Models.BookingSlots>();
            var freeslots = repObj.GetFreeSlots();

            foreach(var slots in freeslots)
            {
                bookingslots.Add(_mapper.Map<Models.BookingSlots>(slots));
            }
            return View(bookingslots);
        }

        public IActionResult BookSlot(Models.BookingSlots bookSlots)
        {
            return View(bookSlots);
        }

        public IActionResult ConfirmBooking(int slotId , int empId)
        {
          int status =   repObj.BookSlots(slotId,empId);
            if (status > 0)
            {
                return View("Success");
            }
            else
            {
                return View("Exception");
            }
        }

    }
}
