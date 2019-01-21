using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace placements.Models
{
    public class Session
    {
        public int user_id { get; set; }
        public string jwt_token { get; set; }
    }
}
