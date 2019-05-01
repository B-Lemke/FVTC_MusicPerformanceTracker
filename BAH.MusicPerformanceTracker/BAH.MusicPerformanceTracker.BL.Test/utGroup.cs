using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.BL.Test
{
    [TestClass]
    public class utGroup
    {
        [TestMethod]
        public void LoadTest()
        {
            GroupList groups = new GroupList();
            groups.Load();
            Assert.AreEqual(3, groups.Count);
        }


        [TestMethod]
        public void InsertTest()
        {
            Group group = new Group();
            group.Name = "BL Test";
            group.Description = "BL Test";
            group.FoundedDate = Convert.ToDateTime("05/01/3000");

            int results = group.Insert();
            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void UpdateTest()
        {
            Group group = new Group();
            GroupList groups = new GroupList();
            groups.Load();
            group = groups.FirstOrDefault(c => c.Name == "BL Test");

            group.Description = "BL Test Update";
            int results = group.Update();

            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void LoadById()
        {
            Group group = new Group();
            GroupList groups = new GroupList();
            groups.Load();
            group = groups.FirstOrDefault(c => c.Name == "BL Test");

            Group newGroup = new Group { Id = group.Id };
            newGroup.LoadById();

            Assert.AreEqual(group.Name, newGroup.Name);
        }


        [TestMethod]
        public void DeleteTest()
        {
            Group group = new Group();
            GroupList groups = new GroupList();
            groups.Load();
            group = groups.FirstOrDefault(c => c.Name == "BL Test");

            int results = group.Delete();

            Assert.IsTrue(results == 1);
        }
    }
}
