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

        // GET: api/Gender?description={description}
        public Gender GetByDescription(string description)
        {
            Gender gender = new Gender { Description = description };
            gender.LoadByDescription();
            return gender;
        }

        // POST: api/Locaiton
        public void Post(Gender gender)
        {
            gender.Insert();
        }

        // PUT: api/Locaiton/5
        public void Put(Guid id, Gender gender)
        {
            gender.Update();
        }

        // DELETE: api/Locaiton/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
