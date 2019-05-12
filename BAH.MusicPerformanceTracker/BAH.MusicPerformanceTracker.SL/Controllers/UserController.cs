using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;


namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class UserController : ApiController
    {
        User user;

        public User Get(string userName, string userPass)
        {
            User user = new User { UserId = userName, UserPass = userPass };
            user.Login();
            return user;
        }




    }
}
