using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.PL.Test
{
    [TestClass]
    public class utPerformancePiece
    {
        [TestMethod]
        public void LoadTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                int expected = 6;

                var performancepieces = dc.tblPerformancePieces;

                int actual = performancepieces.Count();

                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void InsertTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblPerformancePiece performancepiece = new tblPerformancePiece();
                performancepiece.Id = Guid.NewGuid();
                performancepiece.DirectorId = dc.tblDirectors.FirstOrDefault(p => p.FirstName == "Broderick").Id;
                performancepiece.GroupId = dc.tblGroups.FirstOrDefault(p => p.Name == "Jazz Ensemble").Id;
                performancepiece.PerformanceId = dc.tblPerformances.FirstOrDefault(p => p.Name == "Spring Concert").Id;
                performancepiece.PieceId = dc.tblPieces.FirstOrDefault(p => p.Name == "Rock Music").Id;
                performancepiece.Notes = "PL Test";
                performancepiece.MP3Path = "PL Test";

                dc.tblPerformancePieces.Add(performancepiece);

                dc.SaveChanges();

                tblPerformancePiece retrievedPerformancePiece = dc.tblPerformancePieces.FirstOrDefault(a => a.Notes == "PL Test");

                Assert.AreEqual(performancepiece.Id, retrievedPerformancePiece.Id);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblPerformancePiece performancepiece = dc.tblPerformancePieces.FirstOrDefault(a => a.Notes == "PL Test");

                performancepiece.MP3Path = "PL Updated Test";

                dc.SaveChanges();

                tblPerformancePiece retrievedPerformancePiece = dc.tblPerformancePieces.FirstOrDefault(a => a.Notes == "PL Test");

                Assert.IsNotNull(retrievedPerformancePiece);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblPerformancePiece performancepiece = dc.tblPerformancePieces.FirstOrDefault(a => a.Notes == "PL Test");

                dc.tblPerformancePieces.Remove(performancepiece);

                dc.SaveChanges();

                tblPerformancePiece retrievedPerformancePiece = dc.tblPerformancePieces.FirstOrDefault(a => a.Notes == "PL Test");

                Assert.IsNull(retrievedPerformancePiece);
            }
        }
    }
}
