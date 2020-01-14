using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using WebApi.Models.Person;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        // GET api/values
        /// <summary>
        /// get harcoded values
        /// </summary>
        /// <returns>Dummy Values</returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
             return new string[] { "value1", "valuedos" };
        }
        /*
        private IConfiguration _configuration { get; }
        private IPersonProvider _personProvider { get; }
        public ValuesController(IConfiguration configuration, IPersonProvider personProvider)
        {
           this._configuration = configuration;
           this._personProvider = personProvider;
        }

            // TODO change server connection
            // TODO change app service in devops
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Console.WriteLine("consola writeline pruebas ");
            Debug.WriteLine("<<<<<<================== GET VALUES START ++++++++++++>>>>>>>>>>");
            String Output = "hardcoded@data.net";
            List<String> listofEmails = new List<String>();
            listofEmails.Add(Output);
            try
            {
                string connetionString;
                SqlConnection cnn;

                // connetionString = @"Server=(localdb)\MSSQLLocalDB;Database=Outreachdb;Trusted_Connection=True;MultipleActiveResultSets=true";
               // connetionString =
               //     @"Server=tcp:outreachserver.database.windows.net,1433;Initial Catalog=OutreachDB;Persist Security Info=False;User ID=admindb;Password=ramonserver1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                 connetionString = _configuration["OutreachConnectionString"];

                cnn = new SqlConnection(connetionString);

                cnn.Open();

                SqlCommand command;
                SqlDataReader dataReader;
                String sql;
                sql = "Select Email from Person;";

                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Output = Output + " " + dataReader.GetValue(0);
                    listofEmails.Add("" +dataReader.GetValue(0));
                }

                dataReader.Close();

                cnn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                Debug.WriteLine("=================>>>>> GET VALUES SQL EXCEPTION " + e.ToString());
            }



            Debug.WriteLine("=================>>>>> GET VALUES ENDS ");

            IEnumerable<Person> pers = this._personProvider.GetPersons();

            foreach(Person p in pers)
            {
                Debug.WriteLine("====>>>>> DAPPER SAYS email " + p.Email + "  password " + p.Password);
            }


            // return new string[] { "value1", Output };
            return listofEmails;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Random random = new Random();
            int anynumber =  random.Next(1000, 9999);

            Debug.WriteLine("================= " + anynumber + " >>>>> " + value);

            String emailx =  "baseEmail-" + value;
            try
            {
                string connetionString;
                SqlConnection cnn;


                // connetionString = @"Server=(localdb)\MSSQLLocalDB;Database=Outreachdb;Trusted_Connection=True;MultipleActiveResultSets=true";
                // connetionString =
                  //  @"Server=tcp:outreachserver.database.windows.net,1433;Initial Catalog=OutreachDB;Persist Security Info=False;User ID=admindb;Password=ramonserver1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                connetionString = _configuration["OutreachConnectionString"];
                cnn = new SqlConnection(connetionString);

                cnn.Open();

                SqlCommand command;

                String sql;

                sql =
                "INSERT INTO Person(Email, Password) VALUES('" + emailx + "', '" + anynumber + "')";
                command = new SqlCommand(sql, cnn);
                int numberOfLinesExececuted= command.ExecuteNonQuery();

                cnn.Close();
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
                Console.WriteLine(e.ToString());
            }

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
