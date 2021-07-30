using GigHubMVC.Data;
using GigHubMVC.Models;
using GigHubMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                Genres = _context.Genres.ToList(),
                Heading = "Create a Gig"
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

        public IActionResult Mine()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = _context.Gigs.Where(g => g.ArtistId == UserId && g.Datetime > DateTime.Now)
                .Include(g => g.Genre)
                .ToList();
            return View(model);
        }


        [Authorize]
        public IActionResult Edit(int id)

        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var modelGig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == UserId);
            var viewModel = new CreateGigViewModel
            {
                Id = modelGig.Id,
                Genres = _context.Genres.ToList(),
                Venue = modelGig.Venue,
                Genre = modelGig.GenreId,
                Date = modelGig.Datetime.ToString("dd MMM yyyy"),
                Time = modelGig.Datetime.ToString("hh:mm"),
                Heading="Edit a Gig"
            };
            return View("Create",viewModel);
        }


        [Authorize]
        [HttpPost]

        public IActionResult Edit(CreateGigViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _context.Genres.ToList();
                return View("Create", model);
            }


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = _context.Gigs.Single(g => g.Id == model.Id && g.ArtistId == userId);

                data.Datetime = DateTime.Parse(string.Format("{0} {1}", model.Date, model.Time));
                data.GenreId = model.Genre;
                data.Venue = model.Venue;
           
           
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gig");
        }
    }
}
