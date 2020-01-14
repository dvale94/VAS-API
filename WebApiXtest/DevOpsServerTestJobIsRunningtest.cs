using System;
using System.Linq;
using WebApi.Controllers;
using Xunit;

// http://www.williablog.net/williablog/post/2009/12/15/Mock-a-database-repository-using-Moq.aspx
namespace WebApiXtest
{
    public class DevOpsServerTestJobIsRunningtest
    {
        // Test to check that DevOps server Pipeline is picking up the test project
        ValuesController vc = new ValuesController();

        [Fact]
        public void GetReturnsCorrectValue()
        {

            var valist = vc.Get();
            var vals = valist.Value.ToList().FirstOrDefault();

            Assert.Equal("value1", vals);
        }
    }
}
