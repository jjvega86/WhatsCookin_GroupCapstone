using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class Cook
    {
        [Key]
        public int CookId { get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        [NotMapped]
        public IList<string> SelectedTags { get; set; }
        [NotMapped]
        public IList<SelectListItem> AllTags { get; set; }
        public Cook()
        {
            SelectedTags = new List<string>();
            AllTags = new List<SelectListItem>();
        }

        public ICollection<CookTag> CookTag { get; set; }
        public ICollection<CookSavedRecipes> CookSavedRecipes { get; set; }




        //[ForeignKey("User")]
        //public int UserId { get; set; }
        //public User User { get; set; }
    }
}
