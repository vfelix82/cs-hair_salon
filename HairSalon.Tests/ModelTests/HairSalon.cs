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
        //   Stylist testStylist = new Stylist("Janet Jackson");
        //   testStylist.Save();
        //
        //   CollectionAssert.AreEqual(testStylist);
        // }
     }
}
