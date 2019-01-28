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
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using placements.Contracts;

namespace placements.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        Model model = Model.ModelFactory();
        string photoPath = "ClientApp/src/assets/photos";
        static List<Session> sessionList = new List<Session>();

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
                                  query_placement.mainphoto,
                                  query_placement.type,
                                  query_placement.location,
                                  query_placement.entity,
                                  query_placement.size,
                                  query_placement.fromDate,
                                  query_placement.toDate,
                                  query_placement.owner_credentials
                              }
                              ).ToListAsync();

            Console.WriteLine("time mark2<---------------||");

            Data.ForEach(i =>
            {
                Console.WriteLine("time mark3<---------------||");
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
                placement.owner_credentials = i.owner_credentials;

                PlasementList.Add(placement);
                Console.WriteLine("time mark4<---------------||");
            });
            return Json(PlasementList);
        }

        [HttpPost("[action]")]
        public IActionResult placements([FromBody]PageRequestContract pageRequestContract)
        {
            Console.WriteLine("incoming post request received: api/home/placements<---------------||");
            if (pageRequestContract != null)
            {
                Console.WriteLine("page received: " + pageRequestContract.page + "<---------------||");
                List<Placement> Sample = new List<Placement>();
                int Number = Int32.Parse(pageRequestContract.page);
                int Counter = 1;
                int Top = Number * 10 + 1;
                int Bottom = Top - 10;
                Console.WriteLine("Top: " + Top + "Bottom: " + Bottom + "<---------------||");
                foreach (Placement placement in model.PlasementList)
                {
                    if (Counter >= Bottom && Counter < Top)
                    {
                        Sample.Add(placement);
                    }
                    Counter++;
                }
                foreach (Placement placement in Sample)
                {
                    placement.image_2 = null;
                    placement.image_3 = null;
                    placement.image_4 = null;
                    placement.image_5 = null;
                }
                return Json(Sample);
            }
            Console.WriteLine("invalid data received<---------------||");
            return Json("invalid data sended<---------------||");
        }

        [HttpPost("[action]")]
        public IActionResult details([FromBody]RequestedPlacementContract contract)
        {
            Console.WriteLine("incoming post request received: api/home/details<---------------||");
            if(contract != null)
            {
                foreach(Placement placement in model.PlasementList)
                {
                    if(placement.id == contract.id)
                    {
                        foreach(User user in model.UserList)
                        {
                            if(user.id.ToString() == placement.owner_credentials)
                            {
                                PlacementDetails placementDetails = new PlacementDetails();

                                placementDetails.header = placement.header;
                                placementDetails.type = placement.type;
                                placementDetails.location = placement.location;
                                placementDetails.entity = placement.entity;
                                placementDetails.size = placement.size;
                                placementDetails.fromDate = placement.fromDate;
                                placementDetails.toDate = placement.toDate;

                                placementDetails.mainphoto = placement.mainphoto;
                                placementDetails.image_2 = placement.image_2;
                                placementDetails.image_3 = placement.image_3;
                                placementDetails.image_4 = placement.image_4;
                                placementDetails.image_5 = placement.image_5;

                                placementDetails.userEmail = user.userEmail;
                                placementDetails.userPhone = user.userPhone;
                                placementDetails.userReputation = user.userReputation;
                                placementDetails.userPhoto = user.userPhoto;
                                placementDetails.userName = user.userName;
                                placementDetails.userSurName = user.userSurName;
                                placementDetails.userCity = user.userCity;

                                placementDetails.userId = user.id.ToString();

                                return Json(placementDetails);
                            }
                        }
                    }
                }
            }
            return Json("invalid request");
        }

        [HttpPost("[action]")]
        public IActionResult newplacement([FromBody]Placement placement)
        {
            Console.WriteLine("incoming post request received: api/home/newplacement<---------------||");
            if(placement != null)
            {
                bool trigger = false;
                Console.WriteLine("checking identity<---------------||");
                foreach(Session sessionInstance in sessionList)
                {
                    if(sessionInstance.jwt_token == placement.owner_credentials)
                    {
                        Console.WriteLine("identity confirmed<---------------||");
                        foreach(User user in model.UserList)
                        {
                            if(user.id == sessionInstance.user_id)
                            {
                                placement.owner = user;
                                placement.owner_credentials = placement.owner.id.ToString();
                                model.PlasementList.Add(placement);
                                trigger = true;
                                break;
                            }
                        }
                        break;
                    }
                }
                if(trigger)
                {
                    model.SaveChanges();
                    Console.WriteLine("data was received, saving to database and send successful response<---------------||");
                    return Json("successful response");
                } else {
                    Console.WriteLine("false identity, request handling declined<---------------||");
                    return Json("invalid response");
                }
            }
            else
            {
                Console.WriteLine("data does not received<---------------||");
                return Json("invalid response");
            }
        }

        [HttpPost("[action]")]
        public ActionResult deleteplacement([FromBody]DeletePlacementContract contract)
        {
            Console.WriteLine("incoming post request received: api/home/deleteplacement<---------------||");
            if(contract != null)
            {
                bool trigger = false;
                Console.WriteLine("checking identity<---------------||");
                foreach(Session sessionInstance in sessionList)
                {
                    if(sessionInstance.jwt_token == contract.jwt_token)
                    {
                        Console.WriteLine("identity confirmed<---------------||");
                        foreach(Placement placement in model.PlasementList)
                        {
                            if (placement.id == contract.placement_id && placement.owner.id == sessionInstance.user_id)
                            {
                                model.PlasementList.Remove(placement);
                                trigger = true;
                            } else {
                                foreach (User user in model.UserList)
                                {
                                    if (user.id == sessionInstance.user_id && user.userAdmin == true)
                                    {
                                        model.PlasementList.Remove(placement);
                                        trigger = true;
                                    }
                                }
                            }
                        }
                        if(trigger)
                        {
                            model.SaveChanges();
                            Console.WriteLine("data was deleted from database, send successful response<---------------||");
                            return Json("successful response");
                        } else {
                            Console.WriteLine("declared placement does not belong to this user, invalid action<---------------||");
                            return Json("invalid response, declared placement does not belong to this user");
                        }
                    }
                }
                Console.WriteLine("not authorized user<---------------||");
                return Json("invalid response, not authorized user");
            }
            Console.WriteLine("invalid request<---------------||");
            return Json("invalid response, data does not valid");
        }

        [HttpPost("[action]"), DisableRequestSizeLimit]
        public ActionResult uploadfile([FromForm]PhotoSetContract contract)
        {
            Console.WriteLine("incoming post request received: api/home/uploadfile<---------------||");
            if (contract != null)
            {
                Console.WriteLine("Contract valid<---------------||");
                bool trigger = false;
                foreach(Session sessionInstance in sessionList)
                {
                    if(sessionInstance.jwt_token == contract.jwt_token)
                    {
                        Console.WriteLine("identity confirmed<---------------||");
                        Console.WriteLine(model.PlasementList.Count() + "<---------------||");
                        foreach(Placement placement in model.PlasementList)
                        {
                            Console.WriteLine(placement.id + " id, header " + placement.header + "<---------------||");
                            Console.WriteLine(placement.owner_credentials + " == " + sessionInstance.user_id.ToString() + "<---------------||");

                            if (placement.mainphoto == Request.Form.Files[0].Name && placement.owner_credentials == sessionInstance.user_id.ToString())
                            {
                                trigger = true;
                                try
                                {
                                    placement.mainphoto = Library.Converter(Request.Form.Files[0]);
                                    placement.image_2 = Library.Converter(Request.Form.Files[1]);
                                    placement.image_3 = Library.Converter(Request.Form.Files[2]);
                                    placement.image_4 = Library.Converter(Request.Form.Files[3]);
                                    placement.image_5 = Library.Converter(Request.Form.Files[4]);
                                }
                                catch
                                {
                                    Console.WriteLine("out of range ex, continue to work<---------------||");
                                }
                            }
                        }
                    } else {
                        Console.WriteLine("wrong identity<---------------||");
                        return Json("invalid identity");
                    }
                }
                if (trigger)
                {
                    model.SaveChanges();
                    Console.WriteLine("photos successfuly saved to db<---------------||");
                    return Json("successful response");
                } else {
                    Console.WriteLine("invalid action by user<---------------||");
                    return Json("invalid action by user");
                }
            } else {
                Console.WriteLine("invalid request<---------------||");
                return Json("invalid request");
            }
        }

        [HttpPost("[action]")]
        public IActionResult Registration([FromBody]User user)
        {
            Console.WriteLine("Incoming client request: api/home/registration<---------------||");
            if (user == null)
            {
                Console.WriteLine("Invalid client request<---------------||");
                return Json("Invalid client request<---------------||");
            } else {
                Console.WriteLine("Save received data<---------------||");
                model.UserList.Add(user);
                model.SaveChanges();
                Console.WriteLine("Received data successfuly saved, sending successful response<---------------||");
                return Json("Received data successfuly saved<---------------||");
            }
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody]User user)
        {
            Console.WriteLine("Incoming client request: api/home/login<---------------||");
            if (user == null)
            {
                return BadRequest("Invalid client request<---------------||");
            }

            Console.WriteLine("userLogin" + user.userLogin + "<---------------||");
            Console.WriteLine("userPassword" + user.userPassword + "<---------------||");

            foreach (User db_user in model.UserList)
            {
                if (db_user.userLogin == user.userLogin && db_user.userPassword == user.userPassword)
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

                    Session sessionInstance = new Session();
                    sessionInstance.jwt_token = tokenString;
                    sessionInstance.user_id = db_user.id;
                    sessionList.Add(sessionInstance);

                    string user_id_value = db_user.id.ToString();
                    string isAdmin_value = db_user.userAdmin.ToString();
                    
                    Console.WriteLine("successful  response<---------------||");
                    return Ok(new { Token = tokenString, user_id = user_id_value, isAdmin = isAdmin_value});
                }
            }

            Console.WriteLine("error<---------------||");
            return Unauthorized();
            //Handler end
        }
    }
}