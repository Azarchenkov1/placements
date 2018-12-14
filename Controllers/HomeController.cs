using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using placements.Models;

namespace placements.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        Model model;

        [HttpGet("[action]")]
        public void PlacementsDataBaseInitialize()
        {
            model = Model.ModelFactory();
        }
    }
}