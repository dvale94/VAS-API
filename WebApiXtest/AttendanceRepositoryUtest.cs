using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
using Xunit;

namespace WebApiXtest
{
    public class AttendanceRepositoryUtest
    {
        public AttendanceRepositoryUtest()
        {
            IEnumerable<Attendance> attendances = new List<Attendance>
            {

               new Attendance
               {
                   Id = new Guid("08400c9a-39ac-4fb8-5fe6-08d7678c622a"),
                   date = DateTime.UtcNow,
                   Pantherid = "1234567",
                   SignInTime = DateTime.UtcNow,
                   SignOutTime = DateTime.UtcNow,
                   Notes = "a note"
               },
                new Attendance
               {
                   Id = new Guid("08400c9a-39ac-4fb8-5fe6-08d7678c622b"),
                   date = DateTime.UtcNow,
                   Pantherid = "12344543",
                   SignInTime = DateTime.UtcNow,
                   SignOutTime = DateTime.UtcNow,
                   Notes = "a note"
               }

            };
            var attent = Task.FromResult(attendances);
            // Mock the Attendance Repository using Moq

            Mock<IAttendanceRepository> mockAttendanceRepository = new Mock<IAttendanceRepository>();// Returns((int i) => products.Where(x => x.ProductId == i).Single());
            // Return Attendance with id

            mockAttendanceRepository.Setup(mr => mr.GetAttendanceWithID(It.IsAny<Guid>())).Returns((Guid i) => Task.FromResult(attendances.FirstOrDefault()));


            // Complete the setup of our Mock Attendance Repository

            this.MockAttendanceRepository = mockAttendanceRepository.Object;
        }
        public readonly IAttendanceRepository MockAttendanceRepository;


       

       [Fact]
        public void CanReturnAttendanceWithID()

        {

                Attendance testAttendance = MockAttendanceRepository.GetAttendanceWithID(new Guid("08400c9a-39ac-4fb8-5fe6-08d7678c622a")).Result;



                Assert.NotNull(testAttendance); // Test if null

                Assert.IsType<Attendance>(testAttendance); // Test type

                Assert.Equal("1234567", testAttendance.Pantherid); // Verify it is the right product

            
        }
    }
}
