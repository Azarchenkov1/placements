using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using placements.Models;

namespace placements.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        Model model = Model.ModelFactory();

        [HttpGet("[action]")]
        public async Task<IActionResult> Main()
        {
            List<Placement> PlasementList = new List<Placement>();
            var Data = await (from query_placement in model.PlasementList
                              select new
                              {
                                  query_placement.id,
                                  query_placement.header,
                                  query_placement.type,
                                  query_placement.location,
                                  query_placement.entity,
                                  query_placement.size,
                                  query_placement.fromDate,
                                  query_placement.toDate
                              }
                              ).ToListAsync();
            Data.ForEach(i =>
            {
                Placement placement = new Placement();
                placement.id = i.id;
                placement.header = i.header;
                placement.type = i.type;
                placement.location = i.location;
                placement.entity = i.entity;
                placement.size = i.size;
                placement.fromDate = i.fromDate;
                placement.toDate = i.toDate;
                PlasementList.Add(placement);
            });
            return Json(PlasementList);
        }

        [HttpPost("[action]")]
        public IActionResult newplacement([FromBody]Placement placement)
        {
            Console.WriteLine("incoming post request received: api/home/newplacement<---------------||");
            if(placement != null)
            {
                Console.WriteLine("data was received, saving to database and send successful response<---------------||");
                model.PlasementList.Add(placement);
                model.SaveChanges();
                return Json("successful response");
            }
            else
            {
                Console.WriteLine("data does not received<---------------||");
                return Json("invalid response");
            }
        }
    }
}