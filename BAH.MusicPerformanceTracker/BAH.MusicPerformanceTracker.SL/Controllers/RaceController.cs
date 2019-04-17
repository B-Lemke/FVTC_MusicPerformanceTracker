using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class RaceController : ApiController
    {
        // GET: api/Race
        public IEnumerable<Race> Get()
        {
            RaceList races = new RaceList();
            races.Load();
            return races;
        }

        // GET: api/Race/5
        public Race Get(Guid id)
        {
            Race race = new Race { Id = id };
            race.LoadById();
            return race;
        }

        // POST: api/Race
        public void Post(Race race)
        {
            race.Insert();
        }

        // PUT: api/Race/5
        public void Put(Guid id, Race race)
        {
            race.Update();
        }

        // DELETE: api/Race/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
