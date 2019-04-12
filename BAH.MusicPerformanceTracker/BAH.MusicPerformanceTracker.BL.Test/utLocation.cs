using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.BL.Test
{
    [TestClass]
    public class utLocation
    {
        [TestMethod]
        public void LoadTest()
        {
            LocationList locations = new LocationList();
            locations.Load();
            Assert.AreEqual(5, locations.Count);
        }


        [TestMethod]
        public void InsertTest()
        {
            Location location = new Location();
            location.Description = "Test";
            int results = location.Insert();
            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void UpdateTest()
        {
            Location location = new Location();
            LocationList locations = new LocationList();
            locations.Load();
            location = locations.FirstOrDefault(l => l.Description == "Test");

            location.Description = "Update";
            int results = location.Update();

            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void LoadById()
        {
            Location location = new Location();
            LocationList locations = new LocationList();
            locations.Load();
            location = locations.FirstOrDefault(l => l.Description == "Update");

            Location newLocation = new Location { Id = location.Id };
            newLocation.LoadById();

            Assert.AreEqual(location.Description, newLocation.Description);
        }


        [TestMethod]
        public void DeleteTest()
        {
            Location location = new Location();
            LocationList locations = new LocationList();
            locations.Load();
            location = locations.FirstOrDefault(l => l.Description == "Update");

            int results = location.Delete();

            Assert.IsTrue(results == 1);
        }
    }
}
