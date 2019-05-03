using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class PerformanceController : ApiController
    {
        // GET: api/Performance
        public IEnumerable<Performance> Get()
        {
            PerformanceList performances = new PerformanceList();
            performances.Load();
            return performances;
        }

        // GET: api/Performance/5
        public Performance Get(Guid id)
        {
            Performance performance = new Performance { Id = id };
            performance.LoadById();
            return performance;
        }

        // POST: api/Performance
        public void Post(Performance performance)
        {
            performance.Insert();
        }

        // Put: api/Performance
        public void Put(Guid id, Performance performance)
        {
            performance.Update();
        }

        // DELETE: api/Performance/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
