using GigHubMVC.Controllers;
using GigHubMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace GigHubMVC.ViewModels
{
    public class CreateGigViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Venue")]
        [Display(Name = "Venue")]
        public string Venue { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }
        public int Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<GigController, IActionResult>> update = (c => c.Edit(this));
                Expression<Func<GigController, IActionResult>> create = (c => c.Create(this));
                var action = Id != 0 ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
           
        }
        
    }
}
