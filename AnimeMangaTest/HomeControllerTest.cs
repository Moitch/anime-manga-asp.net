using COMP2084_Assignment1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnimeMangaTest
{
    [TestClass]
    public class HomeControllerTest
    {
        //[TestMethod]
       // public void IndexReturnsResult()
       // {
            //arrage
          //  var controller = new HomeController();
            //act
           // var result = controller.Index();
            //assert
           //Assert.IsNotNull(result);
       // }
        [TestMethod]
        public void IndexWithLoggerReturnsResult()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            ILogger<HomeController> logger = new Logger<HomeController>(loggerFactory);
            HomeController controller = new HomeController(logger);

            var result = controller.Index();

            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void PrivacyReturnsResult()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            ILogger<HomeController> logger = new Logger<HomeController>(loggerFactory);
            HomeController controller = new HomeController(logger);

            var result = controller.Privacy();

            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void PrivacyLoadsPrivacyView()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            ILogger<HomeController> logger = new Logger<HomeController>(loggerFactory);
            HomeController controller = new HomeController(logger);

            var result = (ViewResult)controller.Privacy();

            Assert.AreEqual("Privacy", result.ViewName);

        }
    }
}
