using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X500;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ws
{
    public class imgreqbody
    {
        public string idn { get; set; }
        public string ImageData { get; set; }
    }

    [Route("api/[controller]")]
    public class UploadPhoto : Controller
    {
        // POST api/values
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] imgreqbody ReqBody)
        {
            try
            {
                string filepath = Environment.CurrentDirectory + "/images/" + ReqBody.idn + ".jpg";
                var bytess = Convert.FromBase64String(ReqBody.ImageData );
                using (var imageFile = new FileStream(filepath, FileMode.Create))
                {
                    imageFile.Write(bytess, 0, bytess.Length);
                    imageFile.Flush();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}

