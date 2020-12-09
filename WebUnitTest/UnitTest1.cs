using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication4.Controllers;
using WebApplication4.Models;

namespace WebUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        ApplicationDbContext db = new ApplicationDbContext();
        [TestMethod]
        public async Task TestMethod1()
        {
            UserController controller = new UserController();

            ViewResult result = await controller.Index("1f362d82-846f-4c9f-8081-922446ef1ee7") as ViewResult;
            Assert.AreEqual("Index", result.ViewName);

        }
    }
}
