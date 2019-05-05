using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.BL.Test
{
    [TestClass]
    public class utPerformance
    {
        [TestMethod]
        public void LoadTest()
        {
            PerformanceList performances = new PerformanceList();
            performances.Load();
            Assert.AreEqual(6, performances.Count);
        }
    }
}
