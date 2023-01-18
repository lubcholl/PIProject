using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ws.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ws.Controllers
{

    public class ZSReqBody
    {
        public string idn { get; set; }
        public string type { get; set; }
        public string semester { get; set; }
    }

    [Route("api/[controller]")]
    public class AddCertification : Controller
    {
        //HTTP POST
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] ZSReqBody ReqBody)
        {
            string idn = ReqBody.idn;
            string semester = ReqBody.semester;
            string type = ReqBody.type;

            string _connstr;
            IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
            _connstr = configuration.GetSection("ConnectionStrings:studentsconn").Value;


            MySqlConnection conn = new MySqlConnection(_connstr);
            conn.Open();
            string sql = "insert into zs (idn, type, sem) values (@idn, @type, @sem)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@idn", idn);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@sem", semester);
            

            //int sqlResponse = cmd.ExecuteNonQuery();

            try
            {
                int sqlResponse = cmd.ExecuteNonQuery();
                conn.Close();
                if (sqlResponse == 1)
                {
                    return Ok();
                }
                else
                {
                    return NoContent();
                }
            }
            catch
            {
                return BadRequest();
            }


        }
    }
}

