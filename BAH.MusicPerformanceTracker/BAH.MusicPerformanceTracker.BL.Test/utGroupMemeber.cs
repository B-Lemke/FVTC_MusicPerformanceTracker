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
            PieceList pieces = new PieceList();
            Piece piece = new Piece();
            pieces.Load();
            piece = pieces.FirstOrDefault(p => p.Name == "Rock Music");

            GroupList groups = new GroupList();
            Group group = new Group();
            groups.Load();
            group = groups.FirstOrDefault(c => c.Name == "Jazz Ensemble");

            PerformerList performers = new PerformerList();
            Performer performer = new Performer();
            performers.Load();
            performer = performers.FirstOrDefault(c => c.FirstName == "Hunter");

            /*
            GroupMember GroupMember = new GroupMember();
            GroupMember.GroupId = composer.Id;
            GroupMember.PieceId = piece.Id;
            GroupMember.PerformerId = ct.Id;
            

            int results = GroupMember.Insert();
            Assert.IsTrue(results == 1);
        }



        [TestMethod]
        public void UpdateTest()
        {
            //Load up Guids
            PieceList pieces = new PieceList();
            Piece piece = new Piece();
            pieces.Load();
            piece = pieces.FirstOrDefault(p => p.Name == "Rock Music");

            GroupList groups = new GroupList();
            Group composer = new Group();
            groups.Load();
            composer = groups.FirstOrDefault(c => c.FirstName == "Modest");

            PerformerList ctl = new PerformerList();
            Performer ct = new Performer();
            ctl.Load();
            ct = ctl.FirstOrDefault(c => c.Description == "Group");

            GroupMember GroupMember = new GroupMember();
            GroupMemberList GroupMembers = new GroupMemberList();
            GroupMembers.Load();
            GroupMember = GroupMembers.FirstOrDefault(p => p.GroupId == composer.Id && p.PieceId == piece.Id);

            GroupMember.PerformerId = ct.Id;
            int results = GroupMember.Update();

            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void LoadById()
        {
            //Load up Guids
            PieceList pieces = new PieceList();
            Piece piece = new Piece();
            pieces.Load();
            piece = pieces.FirstOrDefault(p => p.Name == "Rock Music");

            GroupList groups = new GroupList();
            Group composer = new Group();
            groups.Load();
            composer = groups.FirstOrDefault(c => c.FirstName == "Modest");

            GroupMember GroupMember = new GroupMember();
            GroupMemberList GroupMembers = new GroupMemberList();
            GroupMembers.Load();
            GroupMember = GroupMembers.FirstOrDefault(p => p.GroupId == composer.Id && p.PieceId == piece.Id);

            GroupMember newGroupMember = new GroupMember { Id = GroupMember.Id };
            newGroupMember.LoadById();

            Assert.AreEqual(GroupMember.GroupId, newGroupMember.GroupId);
        }


        [TestMethod]
        public void DeleteTest()
        {
            //Load up Guids
            PieceList pieces = new PieceList();
            Piece piece = new Piece();
            pieces.Load();
            piece = pieces.FirstOrDefault(p => p.Name == "Rock Music");

            GroupList groups = new GroupList();
            Group composer = new Group();
            groups.Load();
            composer = groups.FirstOrDefault(c => c.FirstName == "Modest");

            GroupMember GroupMember = new GroupMember();
            GroupMemberList GroupMembers = new GroupMemberList();
            GroupMembers.Load();
            GroupMember = GroupMembers.FirstOrDefault(p => p.GroupId == composer.Id && p.PieceId == piece.Id);

            int results = GroupMember.Delete();

            Assert.IsTrue(results == 1);
        }
        */
    }
}
