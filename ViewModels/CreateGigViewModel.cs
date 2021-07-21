using GigHubMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GigHubMVC.ViewModels
{
    public class CreateGigViewModel
    {
        [Required(ErrorMessage = "Please enter Venue")]
        [Display(Name = "Venue")]
        public string Venue { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }
        public int Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        
    }
}
