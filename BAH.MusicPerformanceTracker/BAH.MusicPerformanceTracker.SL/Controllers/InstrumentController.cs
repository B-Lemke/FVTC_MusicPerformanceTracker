using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class InstrumentController : ApiController
    {
        // GET: api/Instrument
        public IEnumerable<Instrument> Get()
        {
            InstrumentList instruments = new InstrumentList();
            instruments.Load();
            return instruments;
        }

        // GET: api/Instrument/5
        public Instrument Get(Guid id)
        {
            Instrument instrument = new Instrument { Id = id };
            instrument.LoadById();
            return instrument;
        }

        // POST: api/Instrument
        public void Post(Instrument instrument)
        {
            instrument.Insert();
        }

        // PUT: api/Instrument/5
        public void Put(Guid id, Instrument instrument)
        {
            instrument.Update();
        }

        // DELETE: api/Instrument/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
