using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ws
{
    public class changepassbody
    {
        public string idn { get; set; }
        public string oldpass { get; set; }
        public string newpass { get; set; }
    }

    [Route("api/[controller]")]
    public class Changepass : Controller
    {
        // POST api/values
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]changepassbody  ReqBody)
        {
            string idn = ReqBody.idn;
            string newpass = ReqBody.newpass;
            string oldpass = ReqBody.oldpass;

            string _connstr;
            IConfiguration configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();
            _connstr = configuration.GetSection("ConnectionStrings:studentsconn").Value;

            MySqlConnection conn = new MySqlConnection(_connstr);
            conn.Open();
            string sql = "update students set password=@password where (idn=@idn) and (password=@oldpassword)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@idn", idn);
            cmd.Parameters.AddWithValue("@password", newpass);
            cmd.Parameters.AddWithValue("@oldpassword", oldpass );
            int numberOfRows = cmd.ExecuteNonQuery();
            conn.Close();
            if (numberOfRows == 0) {
                return Ok();
            } else
            {
                return BadRequest();
            }

        }
    }
}

