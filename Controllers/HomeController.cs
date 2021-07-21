using GigHubMVC.Data;
using GigHubMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GigHubMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _conntext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext conntext)
        {
            _logger = logger;
            _conntext = conntext;
        }

        public IActionResult Index()
        {
            var upcomingGigs = _conntext.Gigs
                .Include(g => g.Artist)
                .Where(m => m.Datetime > DateTime.Now);
            return View(upcomingGigs);
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
