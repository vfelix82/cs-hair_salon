using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
namespace HairSalon.Tests
{
    [TestClass]
    public class HairSalonTests
    {
        [TestMethod]
        public void GetAll_DbStartsEmpty_0()
        {
          //Arrange
          Stylist testStylist = new Stylist();

          //Act
          string result = Stylist.GetAll().Count;

          //Assert
          Assert.AreEqual(0, result);
        }
     }
}
