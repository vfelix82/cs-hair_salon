using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class HomeControllerTest
    {

        [TestMethod]
        public void Index_ReturnsCorrectView_True()
        {
            HomeController controller = new HomeController();
            IActionResult actionResult = controller.Index();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

    }
}
