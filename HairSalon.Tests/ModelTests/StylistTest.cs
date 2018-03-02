using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;


namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTest
    {

        [TestMethod]
        public void GetName_ReturnsStylistName_String()
        {
            string testName = "Victor";
            Stylist testStylist = new Stylist(testName);

            string result = testStylist.GetName();

            Assert.AreEqual(testName, result);
        }
        
        [TestMethod]
        public void Save_StylistSavesToDatabase_Stylist()
        {

            Stylist testStylist = new Stylist("Victor");
            testStylist.Save();

            List<Stylist> result = Stylist.GetAll();
            List<Stylist> testList = new List<Stylist>{testStylist};

            CollectionAssert.AreEqual(result, testList);
         }

         [TestMethod]
         public void Find_FindsStylistInDatabase_Stylist()
         {
             Stylist testStylist = new Stylist("Victor");
             testStylist.Save();

             Stylist foundStylist = Stylist.Find(testStylist.GetId());

             Assert.AreEqual(testStylist, foundStylist);
         }

     }
}
