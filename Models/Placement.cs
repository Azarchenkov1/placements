﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace placements.Models
{
    public class Placement
    {
        [Key]
        public int id { get; set; }
        public string header { get; set; }
        public string type { get; set; }
        public string location { get; set; }
        public string entity { get; set; }
        public string size { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }

        public User owner { get; set; }
        public string owner_credentials { get; set; }

        public string mainphoto { get; set; }
        public string image_2 { get; set; }
        public string image_3 { get; set; }
        public string image_4 { get; set; }
        public string image_5 { get; set; }
    }
}