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
        public void GetAll_ReturnsCity_CityList()
        {
          //Arrange
          List<City> newCity = new List<City> {};

          //Act
          List<City> result = City.GetAll();

          //Assert
          CollectionAssert.AreEqual(newCity, result);
        }
     }
}
