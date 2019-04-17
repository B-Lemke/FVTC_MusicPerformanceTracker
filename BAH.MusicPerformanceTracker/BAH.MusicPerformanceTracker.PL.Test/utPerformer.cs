using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.PL.Test
{
    [TestClass]
    public class utPerformer
    {
        [TestMethod]
        public void LoadTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                int expected = 7;

                var performers = dc.tblPerformers;

                int actual = performers.Count();

                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void InsertTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblPerformer performer = new tblPerformer();
                performer.Id = Guid.NewGuid();
                performer.FirstName = "First Name Test";
                performer.LastName = "Last Name Test";

                dc.tblPerformers.Add(performer);

                dc.SaveChanges();

                tblPerformer retrievedPerformer = dc.tblPerformers.FirstOrDefault(a => a.FirstName == "First Name Test");

                Assert.AreEqual(performer.Id, retrievedPerformer.Id);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblPerformer performer = dc.tblPerformers.FirstOrDefault(a => a.FirstName == "First Name Test");

                performer.LastName = "Last Name Updated";

                dc.SaveChanges();

                tblPerformer retrievedPerformer = dc.tblPerformers.FirstOrDefault(a => a.LastName == "Last Name Updated");

                Assert.IsNotNull(retrievedPerformer);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblPerformer performer = dc.tblPerformers.FirstOrDefault(a => a.LastName == "Last Name Updated");

                dc.tblPerformers.Remove(performer);

                dc.SaveChanges();

                tblPerformer retrievedPerformer = dc.tblPerformers.FirstOrDefault(a => a.LastName == "Last Name Updated");

                Assert.IsNull(retrievedPerformer);
            }
        }
    }
}
