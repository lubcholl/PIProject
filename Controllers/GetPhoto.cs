using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ws
{
    //[Authorize]
    [Route("api/[controller]")]
    public class GetPhoto : Controller
    {
       
        // GET api/values/5
        [HttpGet("{idn}")]
        [Authorize]
        public IActionResult Get(string idn)
        {
            
            var path = Environment.CurrentDirectory+"/images/" + idn + ".jpg";
            FileInfo img = new FileInfo(path);
            if (img.Exists)
            {   
                Byte[] imageBytes= new Byte[0];
                try
                {
                    imageBytes = System.IO.File.ReadAllBytes(path);
                }
                catch
                {  }
                return File(imageBytes, "image/jpeg");
             }
             else
             {return NotFound();}
        }
    }
}

