using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class Preferences
    {
        [Key]
        public int PreferencesId { get; set; }
        public bool isAPreference { get; set; }

        public ICollection<Preferences> Preference { get; set; }
    }
}
