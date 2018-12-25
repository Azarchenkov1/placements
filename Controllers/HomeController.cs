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
using System.Drawing;

namespace placements.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        Model model = Model.ModelFactory();
        string photoPath = "ClientApp/src/assets/photos";

        [HttpGet("[action]")]
        public async Task<IActionResult> Main()
        {
            List<Placement> PlasementList = new List<Placement>();
            var Data = await (from query_placement in model.PlasementList
                              select new
                              {
                                  query_placement.id,
                                  query_placement.header,
                                  query_placement.mainphoto,
                                  query_placement.type,
                                  query_placement.location,
                                  query_placement.entity,
                                  query_placement.size,
                                  query_placement.fromDate,
                                  query_placement.toDate,
                                  query_placement.photo2,
                                  query_placement.photo3,
                                  query_placement.photo4,
                                  query_placement.photo5
                              }
                              ).ToListAsync();
            Data.ForEach(i =>
            {
                Placement placement = new Placement();
                placement.id = i.id;
                placement.header = i.header;
                placement.mainphoto = i.mainphoto;
                placement.type = i.type;
                placement.location = i.location;
                placement.entity = i.entity;
                placement.size = i.size;
                placement.fromDate = i.fromDate;
                placement.toDate = i.toDate;
                placement.photo2 = i.photo2;
                placement.photo3 = i.photo3;
                placement.photo4 = i.photo4;
                placement.photo5 = i.photo5;
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
                    string fullPath = Path.Combine(photoPath, fileName);
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

        [HttpPost("[action]"), DisableRequestSizeLimit]
        public ActionResult returnimagelist()
        {
            Console.WriteLine("incoming post request received: api/home/returnimagelist<---------------||");
            List<byte[]> bytelist = new List<byte[]>();
            byte[] globalbytearray = null;
            if(Request.Form.Files != null)
            {
                foreach(IFormFile file in Request.Form.Files)
                {
                    Console.WriteLine(file.Name);
                    byte[] bytearray = null;
                    using (var readstream = file.OpenReadStream())
                    using (var memorystream = new MemoryStream())
                    {
                        readstream.CopyTo(memorystream);
                        bytearray = memorystream.ToArray();
                        //string stringimage = Convert.ToBase64String(bytearray);
                        Console.WriteLine(bytearray);
                        //Console.WriteLine(stringimage);
                        bytelist.Add(bytearray);
                        globalbytearray = bytearray;
                    }
                }
                Console.WriteLine("Images successfuly saved to bytelist, send response with it");
                return Json(globalbytearray);
            }
            else
            {
                Console.WriteLine("Bad request, no images");
                return Json("Bad response, no images");
            }
        }
    }
}