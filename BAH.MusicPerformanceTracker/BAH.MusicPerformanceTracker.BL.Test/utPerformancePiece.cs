using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.BL.Test
{
    [TestClass]
    public class utPerformancePiece
    {
        [TestMethod]
        public void LoadTest()
        {
            PerformancePieceList PerformancePieces = new PerformancePieceList();
            PerformancePieces.Load();
            Assert.AreEqual(6, PerformancePieces.Count);
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

            PerformanceList performances = new PerformanceList();
            Performance performance = new Performance();
            performances.Load();
            performance = performances.FirstOrDefault(c => c.Name == "Dream Concert");

            DirectorList directors = new DirectorList();
            Director director = new Director();
            directors.Load();
            director = directors.FirstOrDefault(c => c.FirstName == "Broderick");

            PerformancePiece performancePiece = new PerformancePiece();
            performancePiece.GroupId = group.Id;
            performancePiece.PerformanceId = performance.Id;
            performancePiece.DirectorId = director.Id;
            performancePiece.PieceId = piece.Id;
            performancePiece.Notes = "BL Test";
            performancePiece.MP3Path = "BL Test";

            int results = performancePiece.Insert();
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
            Group group = new Group();
            groups.Load();
            group = groups.FirstOrDefault(c => c.Name == "Jazz Ensemble");

            PerformanceList performances = new PerformanceList();
            Performance performance = new Performance();
            performances.Load();
            performance = performances.FirstOrDefault(c => c.Name == "Dream Concert");

            DirectorList directors = new DirectorList();
            Director director = new Director();
            directors.Load();
            director = directors.FirstOrDefault(c => c.FirstName == "Eric");

            PerformancePiece performancePiece = new PerformancePiece();
            PerformancePieceList performancePieces = new PerformancePieceList();
            performancePieces.Load();
            performancePiece = performancePieces.FirstOrDefault(p => p.GroupId == group.Id && p.PieceId == piece.Id && p.PerformanceId == performance.Id);
            performancePiece.Notes = "BL Test";
            performancePiece.MP3Path = "BL Test";

            performancePiece.DirectorId = director.Id;
            int results = performancePiece.Update();

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
            Group group = new Group();
            groups.Load();
            group = groups.FirstOrDefault(c => c.Name == "Jazz Ensemble");

            PerformanceList performances = new PerformanceList();
            Performance performance = new Performance();
            performances.Load();
            performance = performances.FirstOrDefault(c => c.Name == "Dream Concert");

            DirectorList directors = new DirectorList();
            Director director = new Director();
            directors.Load();
            director = directors.FirstOrDefault(c => c.FirstName == "Eric");

            PerformancePiece performancePiece = new PerformancePiece();
            PerformancePieceList performancePieces = new PerformancePieceList();
            performancePieces.Load();
            performancePiece = performancePieces.FirstOrDefault(p => p.GroupId == group.Id && p.PieceId == piece.Id && p.PerformanceId == performance.Id);
            performancePiece.Notes = "BL Test";
            performancePiece.MP3Path = "BL Test";

            PerformancePiece newperformancePiece = new PerformancePiece { Id = performancePiece.Id };
            newperformancePiece.LoadById();

            Assert.AreEqual(performancePiece.DirectorId, newperformancePiece.DirectorId);
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
            Group group = new Group();
            groups.Load();
            group = groups.FirstOrDefault(c => c.Name == "Jazz Ensemble");

            PerformanceList performances = new PerformanceList();
            Performance performance = new Performance();
            performances.Load();
            performance = performances.FirstOrDefault(c => c.Name == "Dream Concert");

            DirectorList directors = new DirectorList();
            Director director = new Director();
            directors.Load();
            director = directors.FirstOrDefault(c => c.FirstName == "Eric");

            PerformancePiece performancePiece = new PerformancePiece();
            PerformancePieceList performancePieces = new PerformancePieceList();
            performancePieces.Load();
            performancePiece = performancePieces.FirstOrDefault(p => p.GroupId == group.Id && p.PieceId == piece.Id && p.PerformanceId == performance.Id);
            performancePiece.Notes = "BL Test";
            performancePiece.MP3Path = "BL Test";

            int results = performancePiece.Delete();

            Assert.IsTrue(results == 1);
        }
    }
}
