using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class ComposerTypeController : ApiController
    {
        // GET: api/ComposerType
        public IEnumerable<ComposerType> Get()
        {
            ComposerTypeList ctl = new ComposerTypeList();
            ctl.Load();
            return ctl;
        }

        // GET: api/ComposerType/5
        public ComposerType Get(Guid id)
        {
            ComposerType composerType = new ComposerType { Id = id };
            composerType.LoadById();
            return composerType;
        }

        // POST: api/ComposerType
        public void Post(ComposerType composerType)
        {
            composerType.Insert();
        }

        // PUT: api/ComposerType/5
        public void Put(Guid id, ComposerType composerType)
        {
            composerType.Update();
        }

        // DELETE: api/ComposerType/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
