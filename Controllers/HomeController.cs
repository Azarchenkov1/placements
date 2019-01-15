﻿using System;
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
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            Console.WriteLine("time mark1<---------------||");


            List<Placement> PlasementList = new List<Placement>();
            var Data = await (from query_placement in model.PlasementList
                              select new
                              {
                                  query_placement.id,
                                  query_placement.header,
                                  query_placement.image_1,
                                  query_placement.type,
                                  query_placement.location,
                                  query_placement.entity,
                                  query_placement.size,
                                  query_placement.fromDate,
                                  query_placement.toDate,
                                  query_placement.image_2,
                                  query_placement.image_3,
                                  query_placement.image_4,
                                  query_placement.image_5
                              }
                              ).ToListAsync();

            Console.WriteLine("time mark2<---------------||");

            Data.ForEach(i =>
            {
                Console.WriteLine("time mark3<---------------||");
                Placement placement = new Placement();
                placement.id = i.id;
                placement.header = i.header;
                placement.image_1 = i.image_1;
                placement.type = i.type;
                placement.location = i.location;
                placement.entity = i.entity;
                placement.size = i.size;
                placement.fromDate = i.fromDate;
                placement.toDate = i.toDate;
                placement.image_2 = i.image_2;
                placement.image_3 = i.image_3;
                placement.image_4 = i.image_4;
                placement.image_5 = i.image_5;
                PlasementList.Add(placement);
                Console.WriteLine("time mark4<---------------||");
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
                string name = Request.Form.Files[0].Name; //Data can be undefined inside foreach!
                Console.WriteLine("received image: " + name + "<---------------||");
                foreach(var placement in model.PlasementList)
                {
                    Console.WriteLine("foreach mainphoto is: " + placement.mainphoto + "<---------------||");
                    if(placement.mainphoto == name && placement.image_1 == null)
                    {
                        Console.WriteLine("if(placement.mainphoto == name && placement.image_1 == null) => true<---------------||");

                        byte[] bytearray = null;
                        using (var readstream = Request.Form.Files[0].OpenReadStream())
                        using (var memorystream = new MemoryStream())
                        {
                            readstream.CopyTo(memorystream);
                            bytearray = memorystream.ToArray();
                            Console.WriteLine(Convert.ToBase64String(bytearray) + "BYTEARRAY<---------------||");
                            placement.image_1 = Convert.ToBase64String(bytearray);
                            Console.WriteLine(placement.image_1 + "IMAGE_1<---------------||");
                        }

                        bytearray = null;
                        using (var readstream = Request.Form.Files[1].OpenReadStream())
                        using (var memorystream = new MemoryStream())
                        {
                            readstream.CopyTo(memorystream);
                            bytearray = memorystream.ToArray();
                            placement.image_2 = Convert.ToBase64String(bytearray);

                        }

                        bytearray = null;
                        using (var readstream = Request.Form.Files[2].OpenReadStream())
                        using (var memorystream = new MemoryStream())
                        {
                            readstream.CopyTo(memorystream);
                            bytearray = memorystream.ToArray();
                            placement.image_3 = Convert.ToBase64String(bytearray);

                        }

                        bytearray = null;
                        using (var readstream = Request.Form.Files[3].OpenReadStream())
                        using (var memorystream = new MemoryStream())
                        {
                            readstream.CopyTo(memorystream);
                            bytearray = memorystream.ToArray();
                            placement.image_4 = Convert.ToBase64String(bytearray);

                        }

                        bytearray = null;
                        using (var readstream = Request.Form.Files[4].OpenReadStream())
                        using (var memorystream = new MemoryStream())
                        {
                            readstream.CopyTo(memorystream);
                            bytearray = memorystream.ToArray();
                            placement.image_5 = Convert.ToBase64String(bytearray);
                        }
                    }
                }
                model.SaveChanges();
                Console.WriteLine("images successfuly saved, returning response<---------------||");
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
                    Console.WriteLine("received image:" + file.Name + "<---------------||");
                    byte[] bytearray = null;
                    using (var readstream = file.OpenReadStream())
                    using (var memorystream = new MemoryStream())
                    {
                        readstream.CopyTo(memorystream);
                        bytearray = memorystream.ToArray();
                        bytelist.Add(bytearray);
                    }
                }
                Console.WriteLine("Images successfuly saved to bytelist, send response with it<---------------||");
                return Json(bytelist);
            }
            else
            {
                Console.WriteLine("Bad request, no images");
                return Json("Bad response, no images");
            }
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody]User user)
        {
            Console.WriteLine("Incoming client request<---------------||");
            if (user == null)
            {
                return BadRequest("Invalid client request<---------------||");
            }

            Console.WriteLine(user.userLogin + "<---------------||");
            Console.WriteLine(user.userPassword + "<---------------||");

            if(user.userLogin == "testlogin" && user.userPassword == "testpassword")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jmf84jfjg@testKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://locahost:3000",
                    audience: "http://localhost:3000",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signinCredentials
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                Console.WriteLine("successful  response<---------------||");
                return Ok(new { Token = tokenString });
            }
            else
            {
                Console.WriteLine("error<---------------||");
                return Unauthorized();
            }
        }
    }
}