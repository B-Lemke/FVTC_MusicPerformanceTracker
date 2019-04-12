using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.BL.Test
{
    [TestClass]
    public class utPieceWriter
    {
        [TestMethod]
        public void LoadTest()
        {
            PieceWriterList pieceWriters = new PieceWriterList();
            pieceWriters.Load();
            Assert.AreEqual(4, pieceWriters.Count);
        }


        [TestMethod]
        public void InsertTest()
        {
            //Load up Guids
            PieceList pieces = new PieceList();
            Piece piece = new Piece();
            pieces.Load();
            piece = pieces.FirstOrDefault(p => p.Name == "Rock Music");

            ComposerList composers = new ComposerList();
            Composer composer = new Composer();
            composers.Load();
            composer = composers.FirstOrDefault(c => c.FirstName == "Modest");

            ComposerTypeList ctl = new ComposerTypeList();
            ComposerType ct = new ComposerType();
            ctl.Load();
            ct = ctl.FirstOrDefault(c => c.Description == "Arranger");

            PieceWriter pieceWriter = new PieceWriter();
            pieceWriter.ComposerId = composer.Id;
            pieceWriter.PieceId = piece.Id;
            pieceWriter.ComposerTypeId = ct.Id;

            int results = pieceWriter.Insert();
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

            ComposerList composers = new ComposerList();
            Composer composer = new Composer();
            composers.Load();
            composer = composers.FirstOrDefault(c => c.FirstName == "Modest");

            ComposerTypeList ctl = new ComposerTypeList();
            ComposerType ct = new ComposerType();
            ctl.Load();
            ct = ctl.FirstOrDefault(c => c.Description == "Composer");

            PieceWriter pieceWriter = new PieceWriter();
            PieceWriterList pieceWriters = new PieceWriterList();
            pieceWriters.Load();
            pieceWriter = pieceWriters.FirstOrDefault(p => p.ComposerId == composer.Id && p.PieceId == piece.Id);

            pieceWriter.ComposerTypeId = ct.Id;
            int results = pieceWriter.Update();

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

            ComposerList composers = new ComposerList();
            Composer composer = new Composer();
            composers.Load();
            composer = composers.FirstOrDefault(c => c.FirstName == "Modest");

            PieceWriter pieceWriter = new PieceWriter();
            PieceWriterList pieceWriters = new PieceWriterList();
            pieceWriters.Load();
            pieceWriter = pieceWriters.FirstOrDefault(p => p.ComposerId == composer.Id && p.PieceId == piece.Id);

            PieceWriter newPieceWriter = new PieceWriter { Id = pieceWriter.Id };
            newPieceWriter.LoadById();

            Assert.AreEqual(pieceWriter.ComposerId, newPieceWriter.ComposerId);
        }


        [TestMethod]
        public void DeleteTest()
        {
            //Load up Guids
            PieceList pieces = new PieceList();
            Piece piece = new Piece();
            pieces.Load();
            piece = pieces.FirstOrDefault(p => p.Name == "Rock Music");

            ComposerList composers = new ComposerList();
            Composer composer = new Composer();
            composers.Load();
            composer = composers.FirstOrDefault(c => c.FirstName == "Modest");

            PieceWriter pieceWriter = new PieceWriter();
            PieceWriterList pieceWriters = new PieceWriterList();
            pieceWriters.Load();
            pieceWriter = pieceWriters.FirstOrDefault(p => p.ComposerId == composer.Id && p.PieceId == piece.Id);

            int results = pieceWriter.Delete();

            Assert.IsTrue(results == 1);
        }
    }
}
