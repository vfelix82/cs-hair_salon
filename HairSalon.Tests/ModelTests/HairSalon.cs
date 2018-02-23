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
        public void HairSalon_GetAll_Test()
        {
            Stylist.GetAll();
        }
        // [TestMethod]
        // public void Hairsalon_Client_Save_Test()
        // {
        //   //Arrange
        //   Clients testClients = new Clients("Janet Jackson");
        //   testClients.Save();
        //
        //   //Act
        //   List<Stylist> result = Stylist.GetAll();
        //   List<Stylist> testStylist = new List<Stylist>{testStylist};
        //
        //   //Assert
        //   CollectionAssert.AreEqual(testStylist, result);
        // }
     }
}
