using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.BL.Test
{
    [TestClass]
    public class utGroupMemeber
    {
        [TestMethod]
        public void LoadTest()
        {
            GroupMemberList GroupMembers = new GroupMemberList();
            GroupMembers.Load();
            Assert.AreEqual(9, GroupMembers.Count);
        }
        
        [TestMethod]
        public void InsertTest()
        {
            //Load up Guids
            GroupList groups = new GroupList();
            Group group = new Group();
            groups.Load();
            group = groups.FirstOrDefault(c => c.Name == "Jazz Ensemble");

            PerformerList performers = new PerformerList();
            Performer performer = new Performer();
            performers.Load();
            performer = performers.FirstOrDefault(c => c.FirstName == "Hunter");

            InstrumentList instruments = new InstrumentList();
            Instrument instrument = new Instrument();
            instruments.Load();
            instrument = instruments.FirstOrDefault(c => c.Description == "Saxophone");

            
            GroupMember groupmember = new GroupMember();
            groupmember.GroupId = group.Id;
            groupmember.PerformerId = performer.Id;
            groupmember.Instrument = instrument.Id;
            groupmember.StartDate = Convert.ToDateTime("04/24/3000");
            groupmember.EndDate = Convert.ToDateTime("04/25/3000");

            int results = groupmember.Insert();
            Assert.IsTrue(results == 1);
        }
        
        [TestMethod]
        public void UpdateTest()
        {
            //Load up Guids
            PerformerList performers = new PerformerList();
            Performer performer = new Performer();
            performers.Load();
            performer = performers.FirstOrDefault(p => p.FirstName == "Hunter");

            GroupList groups = new GroupList();
            Group group = new Group();
            groups.Load();
            group = groups.FirstOrDefault(c => c.Name == "Jazz Ensemble");

            InstrumentList instruments = new InstrumentList();
            Instrument instrument = new Instrument();
            instruments.Load();
            instrument = instruments.FirstOrDefault(c => c.Description == "Euphonium");

            GroupMember groupMember = new GroupMember();
            GroupMemberList groupMembers = new GroupMemberList();
            groupMembers.Load();
            groupMember = groupMembers.FirstOrDefault(p => p.GroupId == group.Id && p.PerformerId == performer.Id);
            groupMember.StartDate = Convert.ToDateTime("04/24/3000");
            groupMember.EndDate = Convert.ToDateTime("04/25/3000");

            groupMember.Instrument = instrument.Id;
            int results = groupMember.Update();

            Assert.IsTrue(results == 1);
        }
        
        [TestMethod]
        public void LoadById()
        {
            //Load up Guids
            PerformerList performers = new PerformerList();
            Performer performer = new Performer();
            performers.Load();
            performer = performers.FirstOrDefault(p => p.FirstName == "Hunter");

            GroupList groups = new GroupList();
            Group group = new Group();
            groups.Load();
            group = groups.FirstOrDefault(c => c.Name == "Jazz Ensemble");

            InstrumentList instruments = new InstrumentList();
            Instrument instrument = new Instrument();
            instruments.Load();
            instrument = instruments.FirstOrDefault(c => c.Description == "Euphonium");

            GroupMember groupMember = new GroupMember();
            GroupMemberList groupMembers = new GroupMemberList();
            groupMembers.Load();
            groupMember = groupMembers.FirstOrDefault(p => p.GroupId == group.Id && p.PerformerId == performer.Id);
            groupMember.StartDate = Convert.ToDateTime("04/24/3000");
            groupMember.EndDate = Convert.ToDateTime("04/25/3000");

            GroupMember newgroupmember = new GroupMember { Id = groupMember.Id };
            newgroupmember.LoadById();

            Assert.AreEqual(groupMember.Instrument, newgroupmember.Instrument);
        }
        
        [TestMethod]
        public void DeleteTest()
        {
            //Load up Guids
            PerformerList performers = new PerformerList();
            Performer performer = new Performer();
            performers.Load();
            performer = performers.FirstOrDefault(p => p.FirstName == "Hunter");

            GroupList groups = new GroupList();
            Group group = new Group();
            groups.Load();
            group = groups.FirstOrDefault(c => c.Name == "Jazz Ensemble");

            InstrumentList instruments = new InstrumentList();
            Instrument instrument = new Instrument();
            instruments.Load();
            instrument = instruments.FirstOrDefault(c => c.Description == "Euphonium");

            GroupMember groupMember = new GroupMember();
            GroupMemberList groupMembers = new GroupMemberList();
            groupMembers.Load();
            groupMember = groupMembers.FirstOrDefault(p => p.GroupId == group.Id && p.PerformerId == performer.Id);
            groupMember.StartDate = Convert.ToDateTime("04/24/3000");
            groupMember.EndDate = Convert.ToDateTime("04/25/3000");

            int results = groupMember.Delete();

            Assert.IsTrue(results == 1);
        }
    }
}
