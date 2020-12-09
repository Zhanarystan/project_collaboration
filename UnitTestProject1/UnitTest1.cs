using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace WebApplication4
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void IndexViewResultNotNull()
        {

            U controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
