using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication4.Controllers;
namespace WebApplication4
{
    [TestClass]
    public class HomeControllerTest
    {
       
        [TestMethod]
        public void SampleTest()
        {
            Assert.AreEqual("HomeController", "HomeController");
        }

        [TestMethod]
        public void ReturnsView()
        {

            HomeController controllerUnderTest = new HomeController();
            var result = controllerUnderTest.ProjectDetails(2) as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
