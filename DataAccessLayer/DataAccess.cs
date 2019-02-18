using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using placements.Controllers;
using placements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace placements.DataAccessLayer
{
    public static class DataAccess
    {
        static Model model = Model.ModelFactory();

        static int pageSize = 10;

        public static List<Placement> GetPlacements(int page, string header, string type, string location)
        {
            var SampleQuery = (from query_placement in model.PlasementList
                               where header == null || query_placement.header.Contains(header)
                               where type == null || query_placement.type.Contains(type)
                               where location == null || query_placement.location.Contains(location)
                               select new
                               {
                                   query_placement.id,
                                   query_placement.header,
                                   query_placement.mainphoto,
                                   query_placement.type,
                                   query_placement.location
                          }).Skip(page * pageSize - pageSize).Take(pageSize).ToList();

            List<Placement> Sample = new List<Placement>();

            SampleQuery.ForEach(i =>
            {
                Placement placement = new Placement();

                placement.id = i.id;
                placement.header = i.header;
                placement.mainphoto = i.mainphoto;
                placement.type = i.type;
                placement.location = i.location;

                Sample.Add(placement);
            });

            return Sample;
        }
    }
}
