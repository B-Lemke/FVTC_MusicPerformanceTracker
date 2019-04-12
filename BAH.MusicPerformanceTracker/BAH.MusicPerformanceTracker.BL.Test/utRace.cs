using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.BL.Test
{
    [TestClass]
    public class utRace
    {
        [TestMethod]
        public void LoadTest()
        {
            RaceList races = new RaceList();
            races.Load();
            Assert.AreEqual(6, races.Count);
        }


        [TestMethod]
        public void InsertTest()
        {
            Race race = new Race();
            race.Description = "Test";
            int results = race.Insert();
            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void UpdateTest()
        {
            Race race = new Race();
            RaceList races = new RaceList();
            races.Load();
            race = races.FirstOrDefault(r => r.Description == "Test");

            race.Description = "Update";
            int results = race.Update();

            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void LoadById()
        {
            Race race = new Race();
            RaceList races = new RaceList();
            races.Load();
            race = races.FirstOrDefault(r => r.Description == "Update");

            Race newRace = new Race { Id = race.Id };
            newRace.LoadById();

            Assert.AreEqual(race.Description, newRace.Description);
        }


        [TestMethod]
        public void DeleteTest()
        {
            Race race = new Race();
            RaceList races = new RaceList();
            races.Load();
            race = races.FirstOrDefault(r => r.Description == "Update");

            int results = race.Delete();

            Assert.IsTrue(results == 1);
        }
    }
}
