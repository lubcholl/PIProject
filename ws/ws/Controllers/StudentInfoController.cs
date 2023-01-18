using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using ws.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ws
{
    [Route("api/[controller]")]
    public class StudentInfoController : Controller
    {
        // GET api/values/5
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

            using (MySqlConnection connection = new MySqlConnection(_connstr))
            {
                connection.Open();

                string sql = "select * from students where idn=@idn";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@idn", idn);


                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    student stdata = new student();
                    if (!reader.IsDBNull("idn"))
                        stdata.idn = reader.GetString("idn");
                    if (!reader.IsDBNull("firstname"))
                        stdata.firstname = reader.GetString("firstname");
                    if (!reader.IsDBNull("secondname"))
                        stdata.secondname = reader.GetString("secondname");
                    if (!reader.IsDBNull("lastname"))
                        stdata.lastname = reader.GetString("lastname");
                    if (!reader.IsDBNull("name"))
                        stdata.name = reader.GetString("name");
                    if (!reader.IsDBNull("email"))
                        stdata.email = reader.GetString("email");
                    if (!reader.IsDBNull("vtuemail"))
                        stdata.vtuemail = reader.GetString("vtuemail");
                    if (!reader.IsDBNull("statusdescr"))
                        stdata.statusdescr = reader.GetString("statusdescr");
                    if (!reader.IsDBNull("spec_name"))
                        stdata.spec_name = reader.GetString("spec_name");
                    if (!reader.IsDBNull("form_name"))
                        stdata.form_name = reader.GetString("form_name");
                    if (!reader.IsDBNull("StudyType_Name"))
                        stdata.StudyType_Name = reader.GetString("StudyType_Name");
                    if (!reader.IsDBNull("fn"))
                        stdata.fn = reader.GetString("fn");
                    if (!reader.IsDBNull("group"))
                        stdata.group = reader.GetInt16("group");

                    connection.Close();
                    return Ok(JsonConvert.SerializeObject(stdata));
                }
                else { return NotFound(); }
                
            }
        }
       
    }
}