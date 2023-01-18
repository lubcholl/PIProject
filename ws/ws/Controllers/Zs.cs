using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ws
{
    [Route("api/[controller]")]
    public class Zs : Controller
    {
        // GET: api/values
        [HttpGet]
        [Authorize]
        public IActionResult Get(string idn)
        {
            string _connstr;

            IConfiguration configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();
            _connstr = configuration.GetSection("ConnectionStrings:studentsconn").Value;

            MySqlConnection conn = new MySqlConnection(_connstr);

            MySqlDataAdapter da = new MySqlDataAdapter("select * from zs where idn=@idn", conn);

            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Parameters.AddWithValue("@idn", idn);

            DataTable dt = new DataTable();

            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return Ok(JsonConvert.SerializeObject(dt));
            }
            else
            {
                return NoContent();
            }
        }

       
    }
}

