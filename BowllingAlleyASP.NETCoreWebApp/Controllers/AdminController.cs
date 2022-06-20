using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BowlingAlleyDAL;
using BowlingAlleyDAL.Models;
using System.Collections.Generic;
using BowllingAlleyASP.NETCoreWebApp.Models;

namespace BowllingAlleyASP.NETCoreWebApp.Controllers
{
    public class AdminController : Controller

        
    {
        private readonly IMapper _mapper;
        private readonly BowlingAlleyDBContext _context;
        BowlingAlleyRepository repObj;

        public AdminController(IMapper mapper,BowlingAlleyDBContext context)
        {
            _context = context;
            _mapper = mapper;
            repObj = new BowlingAlleyRepository();
        }

        public IActionResult Index()
    {
        return View();
    }
    public IActionResult GetAllReservationDetails()
    {
        List<Models.Reservations> ListofReservation = new List<Models.Reservations>();
        var NoofReservation = repObj.GetReservedSlots();
            foreach(var reservation in NoofReservation)
            {
               ListofReservation.Add(_mapper.Map<Models.Reservations>(reservation));

            }
            return View(ListofReservation);

        }

        public IActionResult Approve(int ReservationId)
        { 
            var status = repObj.ApproveOrReject(ReservationId,1);
            if(status == 1)
            {
                return View("Success");
            }
            else
            {
                return View("Some Thing Went Wrong!!!!!");
            }
        }
        public IActionResult Reject(int ReservationId)
        {
            var status = repObj.ApproveOrReject(ReservationId,-1);
            if (status == 1)
            {
                return View("Success");
            }
            else
            {
                return View("Some Thing Went Wrong!!!!!");
            }
        }
        
        public IActionResult GetRejectedSlots()
        {
         var status =  repObj.GetAllRejectedSlots();
            List<Models.ReservationDetails> RejectedList = new List<Models.ReservationDetails>();

            foreach(var details in status)
            {
              RejectedList.Add(_mapper.Map<Models.ReservationDetails>(details));
            }
            return View(RejectedList);
        }



    }
}
