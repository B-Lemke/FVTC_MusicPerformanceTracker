using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.PL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.PL.Test
{
    [TestClass]
    public class utPerformance
    {
        [TestMethod]
        public void LoadTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                int expected = 3;

                var performances = dc.tblPerformances;

                int actual = performances.Count();

                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void InsertTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblPerformance performance = new tblPerformance();
                performance.Id = Guid.NewGuid();
                performance.Name = "PL Test";
                performance.PerformanceDate = DateTime.Now;
                performance.Location = "PL Test";
                performance.pdfPath = "PL Test";
                performance.Description = "PL Test";

                dc.tblPerformances.Add(performance);

                dc.SaveChanges();

                tblPerformance retrievedPerformance = dc.tblPerformances.FirstOrDefault(a => a.Name == "PL Test");

                Assert.AreEqual(performance.Id, retrievedPerformance.Id);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblPerformance performance = dc.tblPerformances.FirstOrDefault(a => a.Name == "PL Test");

                performance.Location = "PL Updated Test"; ;

                dc.SaveChanges();

                tblPerformance retrievedPerformance = dc.tblPerformances.FirstOrDefault(a => a.Name == "PL Test");

                Assert.IsNotNull(retrievedPerformance);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblPerformance performance = dc.tblPerformances.FirstOrDefault(a => a.Name == "PL Test");

                dc.tblPerformances.Remove(performance);

                dc.SaveChanges();

                tblPerformance retrievedPerformance = dc.tblPerformances.FirstOrDefault(a => a.Name == "PL Test");

                Assert.IsNull(retrievedPerformance);
            }
        }
    }
}
