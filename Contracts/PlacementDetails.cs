using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace placements.Contracts
{
    public class PlacementDetails
    {
        public string header { get; set; }
        public string type { get; set; }
        public string location { get; set; }
        public string entity { get; set; }
        public string size { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }

        public string mainphoto { get; set; }
        public string image_2 { get; set; }
        public string image_3 { get; set; }
        public string image_4 { get; set; }
        public string image_5 { get; set; }

        public string userEmail { get; set; }
        public string userPhone { get; set; }
        public string userReputation { get; set; }
        public string userPhoto { get; set; }
        public string userName { get; set; }
        public string userSurName { get; set; }
        public string userCity { get; set; }

        public string userId { get; set; }
    }
}
