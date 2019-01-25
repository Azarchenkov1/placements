using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace placements.Controllers
{
    public static class Library
    {
        public static string Converter(IFormFile photo)
        {
            byte[] byteArray = null;
            using (var readStream = photo.OpenReadStream())
            using (var memoryStream = new MemoryStream())
            {
                readStream.CopyTo(memoryStream);
                byteArray = memoryStream.ToArray();
                return Convert.ToBase64String(byteArray);
            }
        }
    }
}
