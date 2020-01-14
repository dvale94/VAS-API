using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
using Xunit;

namespace WebApiXtest
{
   public class SchoolRepositoryUtest
    {

        public SchoolRepositoryUtest()
        {
            IEnumerable<School> schools = new List<School>
            {
                new School
                {
                    Id = new Guid("08400c9a-39ac-4fb8-5fe6-08d7678c622a"),
                    schoolid = "2314",
                    name = "Silvanna pre School",
                    address = "8201 st 102 nw, Miami, 2134",
                    grade ="middle",
                    phonenumber ="(305)-234-5634"
                  
                }
               ,

                new School
                {
                    Id = new Guid("08400c9a-39ac-4fb8-5fe6-08d7678c622b"),
                    schoolid = "233",
                    name = "Silvanna middle School",
                    address = "8201 st 102 nw, Miami, 2134",
                    grade ="middle",
                    phonenumber ="(305)-234-3634"

                }

            };
            var schoolt = Task.FromResult(schools);

            // Mock the School Repository using Moq

            Mock<ISchoolRepository> mockSchoolRepository = new Mock<ISchoolRepository>();
            // Return all the teams

            mockSchoolRepository.Setup(mr => mr.GetSchools() ).Returns(schoolt);


            // Complete the setup of our Mock Product Repository

            this.MockSchoolRepository = mockSchoolRepository.Object;
        }
        public readonly ISchoolRepository MockSchoolRepository;


        [Fact]
        public void CanReturnAllSchools()

        {

            IList<School> testSchools = (System.Collections.Generic.IList<WebApi.Models.School>)this.MockSchoolRepository.GetSchools().Result;

            Assert.NotNull(testSchools); // Test if null

            Assert.Equal(2, testSchools.Count); // Verify the correct Number

        }
    }
}
