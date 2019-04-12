using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.BL.Test
{
    [TestClass]
    public class utGender
    {
        [TestMethod]
        public void LoadTest()
        {
            GenderList genders = new GenderList();
            genders.Load();
            Assert.AreEqual(3, genders.Count);
        }


        [TestMethod]
        public void InsertTest()
        {
            Gender gender = new Gender();
            gender.Description = "Test";
            int results = gender.Insert();
            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void UpdateTest()
        {
            Gender gender = new Gender();
            GenderList genders = new GenderList();
            genders.Load();
            gender = genders.FirstOrDefault(g => g.Description == "Test");

            gender.Description = "Update";
            int results = gender.Update();

            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void LoadById()
        {
            Gender gender = new Gender();
            GenderList genders = new GenderList();
            genders.Load();
            gender = genders.FirstOrDefault(g => g.Description == "Update");

            Gender newGender = new Gender { Id = gender.Id };
            newGender.LoadById();

            Assert.AreEqual(gender.Description, newGender.Description);
        }


        [TestMethod]
        public void DeleteTest()
        {
            Gender gender = new Gender();
            GenderList genders = new GenderList();
            genders.Load();
            gender = genders.FirstOrDefault(g => g.Description == "Update");

            int results = gender.Delete();

            Assert.IsTrue(results == 1);
        }
    }
}
