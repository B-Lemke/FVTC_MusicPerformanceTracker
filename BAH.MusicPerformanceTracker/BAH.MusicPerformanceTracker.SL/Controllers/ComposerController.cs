using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class ComposerController : ApiController
    {
        // GET: api/Composer
        public IEnumerable<Composer> Get()
        {
            ComposerList composers = new ComposerList();
            composers.Load();
            return composers;
        }

        // GET: api/Composer/5
        public Composer Get(Guid id)
        {
            Composer composer = new Composer { Id = id };
            composer.LoadById();
            return composer;
        }

        // GET: api/Composer?name={name}
        public Composer GetByName(string name)
        {
            Composer composer = new Composer { LastName = name };
            composer.LoadByName();
            return composer;
        }

        // POST: api/Composer
        public void Post(Composer composer)
        {
            composer.Insert();
        }

        // PUT: api/Composer/5
        public void Put(Guid id, Composer composer)
        {
            composer.Update();
        }

        // DELETE: api/Composer/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
