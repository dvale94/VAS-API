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
   public class TeamRepositoryUtest
    {
        public TeamRepositoryUtest()
        {
            IEnumerable<Team> teams = new List<Team>
            {

                new Team {
                    Id = new Guid("08400c9a-39ac-4fb8-5fe6-08d7678c622a"),
                    Description ="describe ",
                    Teamnumber = "122"

                },

                new Team {
                    Id = new Guid("08400c9a-39ac-4fb8-5fe6-08d7678c622b"),
                    Description ="description",
                    Teamnumber = "321"

                }

            };
            var tt = Task.FromResult(teams);
            // Mock the Team Repository using Moq

            Mock<ITeamRepository> mockTeamRepository = new Mock<ITeamRepository>();
            // Return all the teams

            mockTeamRepository.Setup(mr => mr.GetTeams()).Returns(tt);


            // Complete the setup of our Mock Teams Repository

            this.MockTeamsRepository = mockTeamRepository.Object;
        }
        public readonly ITeamRepository MockTeamsRepository;

        [Fact]
        public void CanReturnAllTeams()

        {

            IList<Team> testTams = (System.Collections.Generic.IList<WebApi.Models.Team>)this.MockTeamsRepository.GetTeams().Result;

            Assert.NotNull(testTams); // Test if null

            Assert.Equal(2, testTams.Count); // Verify the correct Number

        }
    }
}
