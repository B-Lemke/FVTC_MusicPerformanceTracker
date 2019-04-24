using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.BL.Test
{
    [TestClass]
    public class utPerformer
    {
        [TestMethod]
        public void LoadTest()
        {
            PerformerList performers = new PerformerList();
            performers.Load();
            Assert.AreEqual(7, performers.Count);
        }


        [TestMethod]
        public void InsertTest()
        {
            Performer performer = new Performer();
            performer.FirstName = "BL Test";
            performer.LastName = "BL Test";

            int results = performer.Insert();
            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void UpdateTest()
        {
            Performer performer = new Performer();
            PerformerList performers = new PerformerList();
            performers.Load();
            performer = performers.FirstOrDefault(c => c.FirstName == "BL Test");

            performer.LastName = "BL Test Update";
            int results = performer.Update();

            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void LoadById()
        {
            Performer performer = new Performer();
            PerformerList performers = new PerformerList();
            performers.Load();
            performer = performers.FirstOrDefault(c => c.FirstName == "BL Test");

            Performer newPerformer = new Performer { Id = performer.Id };
            newPerformer.LoadById();

            Assert.AreEqual(performer.FirstName, newPerformer.FirstName);
        }


        [TestMethod]
        public void DeleteTest()
        {
            Performer performer = new Performer();
            PerformerList performers = new PerformerList();
            performers.Load();
            performer = performers.FirstOrDefault(c => c.FirstName == "BL Test");

            int results = performer.Delete();

            Assert.IsTrue(results == 1);
        }
    }
}
