using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.BL.Test
{
    [TestClass]
    public class utComposerType
    {
        [TestMethod]
        public void LoadTest()
        {
            ComposerTypeList composerTypes = new ComposerTypeList();
            composerTypes.Load();
            Assert.AreEqual(2, composerTypes.Count);
        }


        [TestMethod]
        public void InsertTest()
        {
            ComposerType composerType = new ComposerType();
            composerType.Description = "Test";
            int results = composerType.Insert();
            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void UpdateTest()
        {
            ComposerType composerType = new ComposerType();
            ComposerTypeList composerTypes = new ComposerTypeList();
            composerTypes.Load();
            composerType = composerTypes.FirstOrDefault(c => c.Description == "Test");

            composerType.Description = "Update";
            int results = composerType.Update();

            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void LoadById()
        {
            ComposerType composerType = new ComposerType();
            ComposerTypeList composerTypes = new ComposerTypeList();
            composerTypes.Load();
            composerType = composerTypes.FirstOrDefault(c => c.Description == "Update");

            ComposerType newComposerType = new ComposerType { Id = composerType.Id };
            newComposerType.LoadById();

            Assert.AreEqual(composerType.Description, newComposerType.Description);
        }


        [TestMethod]
        public void DeleteTest()
        {
            ComposerType composerType = new ComposerType();
            ComposerTypeList composerTypes = new ComposerTypeList();
            composerTypes.Load();
            composerType = composerTypes.FirstOrDefault(c => c.Description == "Update");

            int results = composerType.Delete();

            Assert.IsTrue(results == 1);
        }
    }
}
