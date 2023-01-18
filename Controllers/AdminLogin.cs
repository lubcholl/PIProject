using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ws
{
    
    public class Adminlogininfo
    {
        public string? username { get; set; }
        public string? name { get; set; }
        public int? userid { get; set; }
    }

    [Route("api/[controller]")]
    public class AdminLogin : Controller
    {
        // GET api/values/5
        [HttpGet]
        [Authorize]
        public IActionResult Get(string Username, string Password)
        {
            string _connstr;

            IConfiguration configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();
            _connstr = configuration.GetSection("ConnectionStrings:studentsconn").Value;
            MySqlConnection conn = new MySqlConnection(_connstr);
            conn.Open();
            string sql = "select * from admins where (username=@username) and (password=@password)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", Username);
            cmd.Parameters.AddWithValue("@password", Password);

            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                Adminlogininfo admindata = new Adminlogininfo();

                if (!reader.IsDBNull("username"))
                    admindata.username = reader.GetString("username");

                if (!reader.IsDBNull("name"))
                    admindata.name = reader.GetString("name");

                if (!reader.IsDBNull("userid"))
                    admindata.userid = reader.GetInt32("userid");
                conn.Close();
                return Ok(JsonConvert.SerializeObject(admindata));
            }
            else
            { return NotFound(); }
        }

    }
}

