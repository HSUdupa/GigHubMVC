using GigHubMVC.Data;
using GigHubMVC.DTO;
using GigHubMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace GigHubMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AttendanceController : ControllerBase
    {
        private ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpPost]
        public IActionResult Post(AttendanceDTO  dto )
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             
              if (_context.Attendances.Any(a => a.AttendeeId == UserId && a.GigId == dto.gigId))
                  return BadRequest("Attendence already exists..");
              var attendData = new Attendance
              {
                  AttendeeId = UserId,
                  GigId = dto.gigId
              };
              _context.Attendances.Add(attendData);
              _context.SaveChanges();
            
            return Ok();  
        }
    }
}
