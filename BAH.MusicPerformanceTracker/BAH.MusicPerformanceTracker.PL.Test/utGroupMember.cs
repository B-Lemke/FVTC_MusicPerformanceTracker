using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.PL.Test
{
    [TestClass]
    public class utGroupMember
    {
        [TestMethod]
        public void LoadTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                int expected = 7;

                var groupmembers = dc.tblGroupMembers;

                int actual = groupmembers.Count();

                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void InsertTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblGroupMember groupmember = new tblGroupMember();
                groupmember.Id = Guid.NewGuid();
                groupmember.PerformerId = dc.tblPerformers.FirstOrDefault(p => p.FirstName == "Hunter").Id;
                groupmember.GroupId = dc.tblGroups.FirstOrDefault(p => p.Name == "Concert Band").Id;
                groupmember.StartDate = DateTime.Now;
                groupmember.EndDate = DateTime.Now.AddDays(2);
                groupmember.Instrument = dc.tblInstruments.FirstOrDefault(p => p.Description == "Saxophone").Id;

                dc.tblGroupMembers.Add(groupmember);

                dc.SaveChanges();

                tblGroupMember retrievedGroupMember = dc.tblGroupMembers.FirstOrDefault(a => a.PerformerId == groupmember.PerformerId && a.Instrument == groupmember.Instrument);

                Assert.AreEqual(groupmember.Id, retrievedGroupMember.Id);
            }
        }

        /*
        [TestMethod]
        public void UpdateTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblGroupMember groupmember = dc.tblGroupMembers.FirstOrDefault(a => a.Name == "Test GroupMember");

                groupmember.Name = "Updated Test GroupMember";

                dc.SaveChanges();

                tblGroupMember retrievedGroupMember = dc.tblGroupMembers.FirstOrDefault(a => a.Name == "Updated Test GroupMember");

                Assert.IsNotNull(retrievedGroupMember);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblGroupMember groupmember = dc.tblGroupMembers.FirstOrDefault(a => a.Name == "Updated Test GroupMember");

                dc.tblGroupMembers.Remove(groupmember);

                dc.SaveChanges();

                tblGroupMember retrievedGroupMember = dc.tblGroupMembers.FirstOrDefault(a => a.Name == "Updated Test GroupMember");

                Assert.IsNull(retrievedGroupMember);
            }
        }
        */
    }
}
