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
    }
}
