using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication4.Controllers;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        HomeController controller = new HomeController();
        ViewResult result = controller.ProjectDetails(3) as ViewResult;
        Assert.AreEqual("ProjectDetails/3", result.ViewName);
    }
    }
}
