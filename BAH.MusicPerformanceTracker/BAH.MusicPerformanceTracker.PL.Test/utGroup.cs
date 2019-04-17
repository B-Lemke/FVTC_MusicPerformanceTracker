using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.PL.Test
{
    [TestClass]
    public class utGroup
    {
        [TestMethod]
        public void LoadTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                int expected = 3;

                var groups = dc.tblGroups;

                int actual = groups.Count();

                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void InsertTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblGroup group = new tblGroup();
                group.Id = Guid.NewGuid();
                group.Name = "Test Group";
                group.Description = "Test Description";
                group.FoundedDate = DateTime.Now;

                dc.tblGroups.Add(group);

                dc.SaveChanges();

                tblGroup retrievedGroup = dc.tblGroups.FirstOrDefault(a => a.Name == "Test Group");

                Assert.AreEqual(group.Id, retrievedGroup.Id);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblGroup group = dc.tblGroups.FirstOrDefault(a => a.Name == "Test Group");

                group.Name = "Updated Test Group";

                dc.SaveChanges();

                tblGroup retrievedGroup = dc.tblGroups.FirstOrDefault(a => a.Name == "Updated Test Group");

                Assert.IsNotNull(retrievedGroup);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblGroup group = dc.tblGroups.FirstOrDefault(a => a.Name == "Updated Test Group");

                dc.tblGroups.Remove(group);

                dc.SaveChanges();

                tblGroup retrievedGroup = dc.tblGroups.FirstOrDefault(a => a.Name == "Updated Test Group");

                Assert.IsNull(retrievedGroup);
            }
        }
    }
}
