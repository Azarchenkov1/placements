using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
        string photopath = "ClientApp/src/assets/photos";

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

        [HttpPost("[action]"), DisableRequestSizeLimit]
        public ActionResult uploadfile()
        {
            Console.WriteLine("incoming post request received: api/home/uploadfile<---------------||");
            try
            {
                foreach(IFormFile file in Request.Form.Files)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(photopath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    Console.WriteLine(fileName + " has been successfuly saved in directory<---------------||");
                }
                return Json("upload successful");
            }
            catch (System.Exception ex)
            {
                return Json("Upload Failed: " + ex.Message);
            }
        }
    }
}