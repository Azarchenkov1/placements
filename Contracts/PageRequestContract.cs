using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace placements.Contracts
{
    public class PageRequestContract
    {
        public string page { get; set; }
        public string header { get; set; }
        public string type { get; set; }
        public string location { get; set; }
    }
}
