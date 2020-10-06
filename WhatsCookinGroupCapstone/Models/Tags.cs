using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class Tags
    {
        [Key]
        public int TagsId { get; set; }
        public string Name { get; set; }
        public bool Preference { get; set; }

        public ICollection<RecipeTags> RecipeTags { get; set; }

    }
}
