using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class LocaitonController : ApiController
    {
        // GET: api/Locaiton
        public IEnumerable<Location> Get()
        {
            LocationList locations = new LocationList();
            locations.Load();
            return locations;
        }

        // GET: api/Locaiton/5
        public Location Get(Guid id)
        {
            Location location = new Location { Id = id };
            location.LoadById();
            return location;
        }

        // POST: api/Locaiton
        public void Post(Location location)
        {
            location.Insert();
        }

        // PUT: api/Locaiton/5
        public void Put(Guid id, Location location)
        {
            location.Update();
        }

        // DELETE: api/Locaiton/5
        public void Delete(Guid id)
        { 
            Get(id).Delete();
        }
    }
}
