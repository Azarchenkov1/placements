using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace placements.Contracts
{
    public class DeletePlacementContract
    {
        public string jwt_token { get; set; }
        public int placement_id { get; set; }
    }
}
