using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ws
{
    
    public class reqbody
    {
        public int ocid { get; set; }
        public double oc { get; set; }
    }

    [Route("api/[controller]")]
    public class Setoc : Controller
    {
        // POST api/values
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]reqbody ReqBody)
        {
            int ocid= ReqBody.ocid;
            double oc = ReqBody.oc;

            string _connstr;
            IConfiguration configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();
            _connstr = configuration.GetSection("ConnectionStrings:studentsconn").Value;

            MySqlConnection conn = new MySqlConnection(_connstr);
            conn.Open();
            string sql = "update semoc set oc=@oc where ocid=@ocid";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@oc", oc);
            cmd.Parameters.AddWithValue("@ocid", ocid);
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
    }
}

