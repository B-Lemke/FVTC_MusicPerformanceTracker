using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.BL.Test
{
    [TestClass]
    public class utPiece
    {
        [TestMethod]
        public void LoadTest()
        {
            PieceList pieces = new PieceList();
            pieces.Load();
            Assert.AreEqual(3, pieces.Count);
        }


        [TestMethod]
        public void InsertTest()
        {
            Piece piece = new Piece();
            piece.Name = "Test";
            piece.GradeLevel = "Test";
            piece.PerformanceNotes = "Test";
            

            int results = piece.Insert();
            Assert.IsTrue(results == 1);
        }



        [TestMethod]
        public void UpdateTest()
        {
            Piece piece = new Piece();
            PieceList pieces = new PieceList();
            pieces.Load();
            piece = pieces.FirstOrDefault(p => p.Name == "Test");

            piece.PerformanceNotes = "Testing";
            int results = piece.Update();

            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void LoadById()
        {
            Piece piece = new Piece();
            PieceList pieces = new PieceList();
            pieces.Load();
            piece = pieces.FirstOrDefault(p => p.Name == "Test");

            Piece newPiece = new Piece { Id = piece.Id };
            newPiece.LoadById();

            Assert.AreEqual(piece.Name, newPiece.Name);
        }


        [TestMethod]
        public void DeleteTest()
        {
            Piece piece = new Piece();
            PieceList pieces = new PieceList();
            pieces.Load();
            piece = pieces.FirstOrDefault(p => p.Name == "Test");

            int results = piece.Delete();

            Assert.IsTrue(results == 1);
        }
    }
}
