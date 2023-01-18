using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ws.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ws.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        
        // POST api/values
        [HttpPost]
        //[Authorize]
        public IActionResult Post([FromBody] student ReqBody)
        {
            string idn = ReqBody.idn;
            string firstName = ReqBody.firstname;
            string secondName = ReqBody.secondname;
            string lastName = ReqBody.lastname;
            string name = ReqBody.name;
            string email = ReqBody.email;
            string vtuEmail = ReqBody.vtuemail;
            string status = ReqBody.statusdescr;
            string specName = ReqBody.spec_name;
            string formName = ReqBody.form_name;
            string studyType = ReqBody.StudyType_Name;
            string fn = ReqBody.fn;
            int? group = ReqBody.group;

            string _connstr;
            IConfiguration configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();
            _connstr = configuration.GetSection("ConnectionStrings:studentsconn").Value;


            MySqlConnection conn = new MySqlConnection(_connstr);
            conn.Open();
            string sql = "insert into students (idn, firstname, secondname, lastname, name, email, vtuemail, statusdescr, spec_name, form_name, StudyType_Name, fn, group, password) values (@idn, @firstname, @secondname, @lastname, @name, @email, @vtuemail, @statusdescr, @spec_name, @form_name, @StudyType_Name, @fn, @group, @password)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@idn", idn);
            cmd.Parameters.AddWithValue("@firstname", firstName);
            cmd.Parameters.AddWithValue("@secondname", secondName);
            cmd.Parameters.AddWithValue("@lastname", lastName);
            cmd.Parameters.AddWithValue("@name", name ?? firstName + " " + secondName + " " + lastName);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@vtuemail", vtuEmail);
            cmd.Parameters.AddWithValue("@statusdescr", status);
            cmd.Parameters.AddWithValue("@spec_name", specName);
            cmd.Parameters.AddWithValue("@form_name", formName);
            cmd.Parameters.AddWithValue("@StudyType_Name", studyType);
            cmd.Parameters.AddWithValue("@fn", fn);
            cmd.Parameters.AddWithValue("@group", group);
            cmd.Parameters.AddWithValue("@password", fn);

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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

