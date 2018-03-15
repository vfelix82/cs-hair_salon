using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;


namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTests : IDisposable
    {

        public StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=victor_felix_test;";
        }

        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
            Specialty.DeleteAll();
        }

        // Stylist Test //


        [TestMethod]
        public void Save_StylistSavesToDatabase()
        {

            Stylist testStylist = new Stylist("Heidi");
            testStylist.Save();

            List<Stylist> result = Stylist.GetAll();
            List<Stylist> testList = new List<Stylist>{testStylist};

            CollectionAssert.AreEqual(result, testList);
         }

         [TestMethod]
         public void Find_FindsStylistInDatabase()
         {
             Stylist testStylist = new Stylist("Minna");
             testStylist.Save();

             Stylist foundStylist = Stylist.Find(testStylist.GetId());

             Assert.AreEqual(testStylist, foundStylist);
         }

         // Client Test //

         [TestMethod]
         public void Save_ClientToDatabase()
         {
             Client testClient = new Client("Fox", 1);
             testClient.Save();

             List<Client> testList = new List<Client>{testClient};
             List<Client> result = Client.GetAll();

             CollectionAssert.AreEqual(testList, result);
         }

         // Specialy Test //


         [TestMethod]
         public void Save_SpecialtyToDatabase()
         {
             Specialty testSpecialty = new Specialty("Jeremy");
             testSpecialty.Save();

             List<Specialty> result = Specialty.GetAll();
             List<Specialty> testList = new List<Specialty>{testSpecialty};

             CollectionAssert.AreEqual(testList, result);
         }

     }
}
