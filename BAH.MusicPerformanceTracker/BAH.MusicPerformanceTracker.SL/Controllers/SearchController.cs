using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class SearchController : ApiController
    {

        // GET: api/Performance
        public Searches Get()
        {
            Searches searches = new Searches();
            searches.Load();
            return searches;
        }
    }
}
