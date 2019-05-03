using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class PerformerController : ApiController
    {
        // GET: api/Performer
        public IEnumerable<Performer> Get()
        {
            PerformerList performers = new PerformerList();
            performers.Load();
            return performers;
        }

        // GET: api/Performer/5
        public Performer Get(Guid id)
        {
            Performer performer = new Performer { Id = id };
            performer.LoadById();
            return performer;
        }

        // POST: api/Performer
        public void Post(Performer performer)
        {
            performer.Insert();
        }

        // Put: api/Performer
        public void Put(Guid id, Performer performer)
        {
            performer.Update();
        }

        // DELETE: api/Performer/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
