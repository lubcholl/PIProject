using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ws
{
    [Route("api/[controller]")]
    public class GetStudents : Controller
    {


        // GET api/values/5
        //[Authorize]
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            string _connstr;
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            _connstr = configuration.GetSection("ConnectionStrings:studentsconn").Value;

            MySqlConnection conn = new MySqlConnection(_connstr);

            MySqlDataAdapter da = new MySqlDataAdapter("select * from students", conn);

            da.SelectCommand.CommandType = CommandType.Text;
       

            DataTable dt = new DataTable();

            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return Ok(JsonConvert.SerializeObject(dt));
            }
            else
            {
                return NotFound();
            }
        }

       
    }
}

