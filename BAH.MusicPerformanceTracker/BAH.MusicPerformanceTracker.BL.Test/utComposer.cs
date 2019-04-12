using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.BL.Test
{
    [TestClass]
    public class utComposer
    {
        [TestMethod]
        public void LoadTest()
        {
            ComposerList composers = new ComposerList();
            composers.Load();
            Assert.AreEqual(5, composers.Count);
        }


        [TestMethod]
        public void InsertTest()
        {
            Composer composer = new Composer();
            composer.FirstName = "Test";
            composer.LastName = "Test";
            composer.Bio = "Test";
            
            int results = composer.Insert();
            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void UpdateTest()
        {
            Composer composer = new Composer();
            ComposerList composers = new ComposerList();
            composers.Load();
            composer = composers.FirstOrDefault(c => c.FirstName == "Test");

            composer.Bio = "Testing";
            int results = composer.Update();

            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void LoadById()
        {
            Composer composer = new Composer();
            ComposerList composers = new ComposerList();
            composers.Load();
            composer = composers.FirstOrDefault(c => c.FirstName == "Test");

            Composer newComposer = new Composer { Id = composer.Id };
            newComposer.LoadById();

            Assert.AreEqual(composer.FirstName, newComposer.FirstName);
        }


        [TestMethod]
        public void DeleteTest()
        {
            Composer composer = new Composer();
            ComposerList composers = new ComposerList();
            composers.Load();
            composer = composers.FirstOrDefault(c => c.FirstName == "Test");

            int results = composer.Delete();

            Assert.IsTrue(results == 1);
        }

    }
}
