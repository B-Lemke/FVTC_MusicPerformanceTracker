using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.BL.Test
{
    [TestClass]
    public class utGenre
    {
        [TestMethod]
        public void LoadTest()
        {
            GenreList genres = new GenreList();
            genres.Load();
            Assert.AreEqual(23, genres.Count);
        }


        [TestMethod]
        public void InsertTest()
        {
            Genre genre = new Genre();
            genre.Description = "Test";
            int results = genre.Insert();
            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void UpdateTest()
        {
            Genre genre = new Genre();
            GenreList genres = new GenreList();
            genres.Load();
            genre = genres.FirstOrDefault(g => g.Description == "Test");

            genre.Description = "Update";
            int results = genre.Update();

            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void LoadById()
        {
            Genre genre = new Genre();
            GenreList genres = new GenreList();
            genres.Load();
            genre = genres.FirstOrDefault(g => g.Description == "Update");

            Genre newGenre = new Genre { Id = genre.Id };
            newGenre.LoadById();

            Assert.AreEqual(genre.Description, newGenre.Description);
        }


        [TestMethod]
        public void DeleteTest()
        {
            Genre genre = new Genre();
            GenreList genres = new GenreList();
            genres.Load();
            genre = genres.FirstOrDefault(g => g.Description == "Update");

            int results = genre.Delete();

            Assert.IsTrue(results == 1);
        }
    }
}
