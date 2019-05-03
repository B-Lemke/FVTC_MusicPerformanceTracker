using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class GroupMemberController : ApiController
    {
        // GET: api/GroupMember
        public IEnumerable<GroupMember> Get()
        {
            GroupMemberList groupMembers = new GroupMemberList();
            groupMembers.Load();
            return groupMembers;
        }

        // GET: api/GroupMember/5
        public GroupMember Get(Guid id)
        {
            GroupMember groupMember = new GroupMember { Id = id };
            groupMember.LoadById();
            return groupMember;
        }

        // POST: api/GroupMember
        public void Post(GroupMember groupMember)
        {
            groupMember.Insert();
        }

        // Put: api/GroupMember
        public void Put(Guid id, GroupMember groupMember)
        {
            groupMember.Update();
        }

        // DELETE: api/GroupMember/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
