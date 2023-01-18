using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ws
{
    public class resetpassbody
    {
        public string idn { get; set; }
        public string newpass { get; set; }
    }
    [Route("api/[controller]")]
    public class ResetPassword : Controller
    {
        // POST api/values
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] resetpassbody ReqBody)
        {
            string idn = ReqBody.idn;
            string newpass = ReqBody.newpass;
            

            string _connstr;
            IConfiguration configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();
            _connstr = configuration.GetSection("ConnectionStrings:studentsconn").Value;

            MySqlConnection conn = new MySqlConnection(_connstr);
            conn.Open();
            string sql = "update students set password=@password where (idn=@idn) ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@idn", idn);
            cmd.Parameters.AddWithValue("@password", newpass);

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

