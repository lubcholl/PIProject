using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;


namespace ws
{

    public class logininfo
    {
        public string? idn { get; set; }
        public string? name { get; set; }
        public string? plannumb { get; set; }
    }
    [Route("api/[controller]")]
    public class StudLogin : Controller
    {

        // GET api/values/5
        [HttpGet]
        [Authorize]
        public IActionResult Get(string idn, string password)
        {
            string _connstr;

            IConfiguration configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();
            _connstr = configuration.GetSection("ConnectionStrings:studentsconn").Value;
            MySqlConnection conn = new MySqlConnection(_connstr);
            conn.Open();
            string sql = "select * from students where (idn=@idn) and (password=@password)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@idn", idn);
            cmd.Parameters.AddWithValue("@password", password);

            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                logininfo stdata = new logininfo();
                if (!reader.IsDBNull("idn"))
                    stdata.idn = reader.GetString("idn");

                stdata.plannumb = "1111";

                if (!reader.IsDBNull("name"))
                    stdata.name = reader.GetString("name");
                conn.Close();
                return Ok(JsonConvert.SerializeObject(stdata));
            }
            else
            { return NotFound(); }
        }


    }
}