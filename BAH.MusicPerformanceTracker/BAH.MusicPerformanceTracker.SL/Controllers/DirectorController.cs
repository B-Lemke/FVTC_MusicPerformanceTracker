using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class DirectorController : ApiController
    {
        // GET: api/Director
        public IEnumerable<Director> Get()
        {
            DirectorList directors = new DirectorList();
            directors.Load();
            return directors;
        }

        // GET: api/Director/5
        public Director Get(Guid id)
        {
            Director director = new Director { Id = id };
            director.LoadById();
            return director;
        }

        // POST: api/Director
        public void Post(Director director)
        {
            director.Insert();
        }

        // Put: api/Director
        public void Put(Guid id, Director director)
        {
            director.Update();
        }

        // DELETE: api/Director/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
