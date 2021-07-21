using GigHubMVC.Data;
using GigHubMVC.Models;
using GigHubMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GigHubMVC.Controllers
{
    public class GigController : Controller
    {
        private ApplicationDbContext _context;


        public GigController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Create()

        {
            var viewModel = new CreateGigViewModel
            {
                Genres = _context.Genres.ToList()
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]

        public IActionResult Create(CreateGigViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _context.Genres.ToList();
                return View("Create", model);
            }


            var artist = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var gig = new Gig
            {
                ArtistId = artist,
                Datetime = DateTime.Parse(string.Format("{0} {1}", model.Date, model.Time)),
                GenreId = model.Genre,
                Venue = model.Venue
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
