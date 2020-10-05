using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class Tags
    {
        [PrimaryKey]
        public int TagId { get; set; }
        public string Name { get; set; }
        public bool Preference { get; set; }

    }
}
