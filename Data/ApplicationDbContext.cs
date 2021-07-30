using GigHubMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GigHubMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       
        public DbSet<Gig> Gigs { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            //builder.Entity<Attendance>().HasOne(a => a.Gig).WithMany().OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<Attendance>().HasKey(a => new {a.AttendeeId , a.GigId });
            base.OnModelCreating(builder);
        }
    }
}
