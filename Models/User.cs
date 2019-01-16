using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace placements.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string userLogin { get; set; }
        public string userPassword { get; set; }

        public bool userAdmin { get; set; }

        public string userEmail { get; set; }
        public string userPhone { get; set; }
        public string userReputation { get; set; }
        public string userPhoto { get; set; }

        public string userName { get; set; }
        public string userSurName { get; set; }
        public string userBirthDate { get; set; }
        public string userCountry { get; set; }
        public string userCity { get; set; }
    }
}
