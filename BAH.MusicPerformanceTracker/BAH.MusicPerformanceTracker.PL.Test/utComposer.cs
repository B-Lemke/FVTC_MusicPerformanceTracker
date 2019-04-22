using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.PL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.PL.Test
{
    [TestClass]
    public class utComposer
    {
        [TestMethod]
        public void LoadTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                int expected = 5;

                var composers = dc.tblComposers;

                int actual = composers.Count();

                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void InsertTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblComposer composer = new tblComposer();
                composer.Id = Guid.NewGuid();
                composer.FirstName = "PL Test First Name";
                composer.LastName = "PL Test Last Name";
                composer.GenderId = dc.tblGenders.FirstOrDefault(p => p.Description == "Male").Id;
                composer.RaceId = dc.tblRaces.FirstOrDefault(p => p.Description == "Other").Id;
                composer.LocationId = dc.tblLocations.FirstOrDefault(p => p.Description == "Wisconsin").Id;
                composer.Bio = "PL Test Bio";

                dc.tblComposers.Add(composer);

                dc.SaveChanges();

                tblComposer retrievedComposer = dc.tblComposers.FirstOrDefault(a => a.GenderId == composer.GenderId && a.RaceId == composer.RaceId && a.LocationId == composer.LocationId);

                Assert.AreEqual(composer.Id, retrievedComposer.Id);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblComposer composer = dc.tblComposers.FirstOrDefault(a => a.FirstName == "PL Test First Name");

                composer.LastName = "PL Updated Test Last Name";

                dc.SaveChanges();

                tblComposer retrievedComposer = dc.tblComposers.FirstOrDefault(a => a.FirstName == "PL Test First Name");

                Assert.IsNotNull(retrievedComposer);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblComposer composer = dc.tblComposers.FirstOrDefault(a => a.FirstName == "PL Test First Name");

                dc.tblComposers.Remove(composer);

                dc.SaveChanges();

                tblComposer retrievedComposer = dc.tblComposers.FirstOrDefault(a => a.FirstName == "PL Test First Name");

                Assert.IsNull(retrievedComposer);
            }
        }
    }
}
