using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerController : ControllerBase
    {
        /*
        // GET api/volunteer
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Console.WriteLine("consola writeline pruebas ");
            Debug.WriteLine("=================>>>>> GET VOLUNTEER STARTS ");

            String Output = "Volunteer@data.net";
            List<String> listofVolunteer = new List<String>();
            listofVolunteer.Add(Output);
            try
            {
                string connetionString;
                SqlConnection cnn;

                  connetionString =
                    @"Server=tcp:gci-fiu.database.windows.net,1433;Initial Catalog=GCI-DB;Persist Security Info=False;User ID=gciAdmin;Password=eiLiDYvy!pi9;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            // user: gciAdmin

            // pass: eiLiDYvy!pi9
                cnn = new SqlConnection(connetionString);

                cnn.Open();

                SqlCommand command;
                SqlDataReader dataReader;
                String sql;
                sql = "Select email from Volunteer;";

                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Output = Output + " " + dataReader.GetValue(0);
                    listofVolunteer.Add("" + dataReader.GetValue(0));
                }

                dataReader.Close();

                cnn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                Debug.WriteLine("=================>>>>> GET VOLUNTEER SQL EXCEPTION " + e.ToString());
            }


            Debug.WriteLine("=================>>>>> GET VOLUNTEER ENDS ");
            return listofVolunteer;

           // return new string[] { "volunter1", "volunteer2" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Debug.WriteLine("=================>>>>> POST VOLUNTEER STARTS ");
            Random random = new Random();
            int anynumber = random.Next(100, 999);

            Debug.WriteLine("================= " + anynumber + " >>>>> " + value);

            String emailx = "baseEmail-" + value;
            try
            {
                string connetionString;
                SqlConnection cnn;
                
                connetionString =
                   @"Server=tcp:gci-fiu.database.windows.net,1433;Initial Catalog=GCI-DB;Persist Security Info=False;User ID=gciAdmin;Password=eiLiDYvy!pi9;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                cnn = new SqlConnection(connetionString);

                cnn.Open();

                SqlCommand command;
                // panther_id, major
                String sql;

                sql =
                "INSERT INTO Volunteer(panther_id, email, major) VALUES('" + "11" + "', '" + emailx + "', '" +  "somemajor" + "')";
                command = new SqlCommand(sql, cnn);
                int numberOfLinesExececuted = command.ExecuteNonQuery();

                cnn.Close();
            }
            catch (SqlException e)
            {
               
                Debug.WriteLine("=================>>>>> POST VALUES SQL EXCEPTION " + e.ToString());
                Console.WriteLine(e.ToString());

            }
            Debug.WriteLine("=================>>>>> POST VOLUNTEER ENDS ");
        }

    */
    }

}