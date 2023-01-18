using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X500;
using ws.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ws
{

    [Route("api/[controller]")]
    public class Semoc : Controller
    {


        // GET: api/values
        //[Authorize]
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

            MySqlDataAdapter da = new MySqlDataAdapter("select * from semoc where idn=@idn", conn);

            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Parameters.AddWithValue("@idn", idn);

            DataTable dt = new DataTable();

            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return Ok (JsonConvert.SerializeObject(dt));
            }
            else
            {
                return NoContent();
            }
        }

        // DELETE api/values/5
        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int ocid)
        {
            if (ocid <= 0)
                return BadRequest("Not a valid ocid");

            string _connstr;
            IConfiguration configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();
            _connstr = configuration.GetSection("ConnectionStrings:studentsconn").Value;

            MySqlConnection conn = new MySqlConnection(_connstr);
            conn.Open();
            string sql = "delete from semoc where (ocid=@ocid)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
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

        // PUT api/values/5
        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody] semoc ReqBody)
        {
            if (!ModelState.IsValid)
            { return BadRequest("Not a valid model"); }

            string idn = ReqBody.idn;
            string titul = ReqBody.titul;
            string discname = ReqBody.discname;
            int semes = ReqBody.SEMES;
            double oc = ReqBody.oc;
            string planNumber = ReqBody.nplan;
            string protocolNumber = ReqBody.protnumb;

            string _connstr;
            IConfiguration configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();
            _connstr = configuration.GetSection("ConnectionStrings:studentsconn").Value;

            MySqlConnection conn = new MySqlConnection(_connstr);
            conn.Open();
            string sql = "insert into semoc (idn, titul, discname, semes, oc, nplan, protnumb) values (@idn, @titul, @discname, @semes, @oc, @planNumber, @protocolNumber)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@idn", idn);
            cmd.Parameters.AddWithValue("@titul", titul);
            cmd.Parameters.AddWithValue("@discname", discname);
            cmd.Parameters.AddWithValue("@semes", semes);
            cmd.Parameters.AddWithValue("@oc", oc);
            cmd.Parameters.AddWithValue("@planNumber", planNumber);
            cmd.Parameters.AddWithValue("@protocolNumber", protocolNumber);
            int sqlResponse = cmd.ExecuteNonQuery();
            conn.Close();
            if (sqlResponse == 1)
            {
                return Ok();
            } else
            {
                return NoContent();
            }
        }
    }
}

