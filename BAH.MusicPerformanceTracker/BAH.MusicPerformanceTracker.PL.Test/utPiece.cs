using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.PL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.PL.Test
{
    [TestClass]
    public class utPiece
    {
        [TestMethod]
        public void LoadTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                int expected = 3;

                var pieces = dc.tblPieces;

                int actual = pieces.Count();

                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void InsertTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblPiece piece = new tblPiece();
                piece.Id = Guid.NewGuid();
                piece.Name = "PL Test";
                piece.GradeLevel = "PL Test 4.5";
                piece.YearWritten = 2019;
                piece.PefromanceNotes = "PL Test";

                dc.tblPieces.Add(piece);

                dc.SaveChanges();

                tblPiece retrievedPiece = dc.tblPieces.FirstOrDefault(a => a.Name == "PL Test");

                Assert.AreEqual(piece.Id, retrievedPiece.Id);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblPiece piece = dc.tblPieces.FirstOrDefault(a => a.Name == "PL Test");

                piece.GradeLevel = "PL Updated Test 6";

                dc.SaveChanges();

                tblPiece retrievedPiece = dc.tblPieces.FirstOrDefault(a => a.Name == "PL Test");

                Assert.IsNotNull(retrievedPiece);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblPiece piece = dc.tblPieces.FirstOrDefault(a => a.Name == "PL Test");

                dc.tblPieces.Remove(piece);

                dc.SaveChanges();

                tblPiece retrievedPiece = dc.tblPieces.FirstOrDefault(a => a.Name == "PL Test");

                Assert.IsNull(retrievedPiece);
            }
        }
    }
}
