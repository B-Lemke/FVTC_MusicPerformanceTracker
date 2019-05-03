using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class GroupController : ApiController
    {
        // GET: api/Group
        public IEnumerable<Group> Get()
        {
            GroupList groups = new GroupList();
            groups.Load();
            return groups;
        }

        // GET: api/Group/5
        public Group Get(Guid id)
        {
            Group group = new Group { Id = id };
            group.LoadById();
            return group;
        }

        // POST: api/Group
        public void Post(Group group)
        {
            group.Insert();
        }

        // Put: api/Group
        public void Put(Guid id, Group group)
        {
            group.Update();
        }

        // DELETE: api/Group/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
