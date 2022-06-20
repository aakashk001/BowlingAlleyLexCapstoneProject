using BowllingAlleyASP.NETCoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BowlingAlleyDAL.Models;
using BowlingAlleyDAL;
using AutoMapper;


namespace BowllingAlleyASP.NETCoreWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BowlingAlleyDBContext _context;
        private readonly IMapper _mapper;
        BowlingAlleyRepository repObj;
        public HomeController(ILogger<HomeController> logger,BowlingAlleyDBContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            repObj = new BowlingAlleyRepository();
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        }
    }

