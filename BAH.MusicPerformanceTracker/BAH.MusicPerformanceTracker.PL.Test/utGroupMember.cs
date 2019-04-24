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
                groupmember.StartDate = Convert.ToDateTime("04/24/3000");
                groupmember.EndDate = DateTime.Now.AddDays(2);
                groupmember.Instrument = dc.tblInstruments.FirstOrDefault(p => p.Description == "Saxophone").Id;

                dc.tblGroupMembers.Add(groupmember);

                dc.SaveChanges();

                tblGroupMember retrievedGroupMember = dc.tblGroupMembers.FirstOrDefault(a => a.StartDate == groupmember.StartDate);

                Assert.AreEqual(groupmember.Id, retrievedGroupMember.Id);
            }
        }

        
        [TestMethod]
        public void UpdateTest()
        {
            using (MusicEntities dc = new MusicEntities()) 
            {
                DateTime startTime = Convert.ToDateTime("04/24/3000");

                tblGroupMember groupmember = dc.tblGroupMembers.FirstOrDefault(a => a.StartDate == startTime);

                groupmember.EndDate = DateTime.Now.AddDays(5);

                dc.SaveChanges();

                tblGroupMember retrievedGroupMember = dc.tblGroupMembers.FirstOrDefault(a => a.StartDate == groupmember.StartDate);

                Assert.IsNotNull(retrievedGroupMember);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                DateTime startTime = Convert.ToDateTime("04/24/3000");

                tblGroupMember groupmember = dc.tblGroupMembers.FirstOrDefault(a => a.StartDate == startTime);

                dc.tblGroupMembers.Remove(groupmember);

                dc.SaveChanges();

                tblGroupMember retrievedGroupMember = dc.tblGroupMembers.FirstOrDefault(a => a.StartDate == groupmember.StartDate);

                Assert.IsNull(retrievedGroupMember);
            }
        }
        
    }
}
