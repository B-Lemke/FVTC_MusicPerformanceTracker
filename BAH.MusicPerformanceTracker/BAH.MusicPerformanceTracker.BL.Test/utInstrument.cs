using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using System.Linq;

namespace BAH.MusicPerformanceTracker.BL.Test
{
    [TestClass]
    public class utInstrument
    {
        [TestMethod]
        public void LoadTest()
        {
            InstrumentList instruments = new InstrumentList();
            instruments.Load();
            Assert.AreEqual(6, instruments.Count);
        }


        [TestMethod]
        public void InsertTest()
        {
            Instrument instrument = new Instrument();
            instrument.Description = "Test";
            int results = instrument.Insert();
            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void UpdateTest()
        {
            Instrument instrument = new Instrument();
            InstrumentList instruments = new InstrumentList();
            instruments.Load();
            instrument = instruments.FirstOrDefault(i => i.Description == "Test");

            instrument.Description = "Update";
            int results = instrument.Update();

            Assert.IsTrue(results == 1);
        }


        [TestMethod]
        public void LoadById()
        {
            Instrument instrument = new Instrument();
            InstrumentList instruments = new InstrumentList();
            instruments.Load();
            instrument = instruments.FirstOrDefault(i => i.Description == "Update");

            Instrument newInstrument = new Instrument { Id = instrument.Id };
            newInstrument.LoadById();

            Assert.AreEqual(instrument.Description, newInstrument.Description);
        }


        [TestMethod]
        public void DeleteTest()
        {
            Instrument instrument = new Instrument();
            InstrumentList instruments = new InstrumentList();
            instruments.Load();
            instrument = instruments.FirstOrDefault(i => i.Description == "Update");

            int results = instrument.Delete();

            Assert.IsTrue(results == 1);
        }
    }
}
