using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class GenderController : ApiController
    {
        // GET: api/Gender
        public IEnumerable<Gender> Get()
        {
            GenderList genders = new GenderList();
            genders.Load();
            return genders;
        }

        // GET: api/Gender/5
        public Gender Get(Guid id)
        {
            Gender gender = new Gender { Id = id };
            gender.LoadById();
            return gender;
        }
    }
}
