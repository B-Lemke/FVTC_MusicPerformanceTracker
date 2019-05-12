using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class User
    {
        public int Id { get; set; }
        [DisplayName("Password")]
        public string UserPass { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        public User()
        {

        }

        public User(string userid, string pass)
        {
            //Signing in constructor
            UserId = userid;
            UserPass = pass;
        }

        public User(int id, string userId, string userpass, string firstname, string lastname)
        {
            //Updating a user constructor
            Id = id;
            UserId = userId;
            UserPass = userpass;
            FirstName = firstname;
            LastName = lastname;
        }

        public User(string userId, string userpass, string firstname, string lastname)
        {
            //Creating a user constructor
            UserId = userId;
            UserPass = userpass;
            FirstName = firstname;
            LastName = lastname;
        }

        private string GetHash()
        {
            using (var hash = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(UserPass);
                return Convert.ToBase64String(hash.ComputeHash(hashbytes));
            }
        }

        //Mapper function
        private void Map(tblUser user)
        {
            //Move the class data to the data row object
            user.Id = this.Id;
            user.UserId = this.UserId;
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;
            user.UserPass = this.UserPass;
        }

        public void Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    tblUser newuser = new tblUser();

                    Id = dc.tblUsers.Any() ? dc.tblUsers.Max(p => p.Id) + 1 : 1;
                    UserPass = GetHash();

                    //Set the class details to the datarow
                    Map(newuser);

                    dc.tblUsers.Add(newuser);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Login()
        {
            try
            {
                if (!string.IsNullOrEmpty(UserId))
                {
                    if (!string.IsNullOrEmpty(UserPass))
                    {
                        using (MusicEntities dc = new MusicEntities())
                        {
                            tblUser user = dc.tblUsers.FirstOrDefault(u => u.UserId == UserId);

                            if (user != null)
                            {
                                if (user.UserPass == GetHash())
                                {
                                    //Success!
                                    this.FirstName = user.FirstName;
                                    this.LastName = user.LastName;
                                    this.Id = user.Id;

                                    return true;
                                }
                                else
                                {
                                    //Password in DB doesn't match the password hashed
                                    throw new Exception("Cannot login with these credentials. We are watching you. IP address logged. Goodbye.");
                                }
                            }
                            else
                            {
                                //No user found with this userid
                                throw new Exception("No user found with this userid");
                            }
                        }
                    }
                    else
                    {
                        //No password entered
                        throw new Exception("No password was set");
                    }
                }
                else
                {
                    //no userid entered
                    throw new Exception("No UserId was set");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Seed()
        {
            User user = new User("b-lemke", "maple", "Brody", "Lemke");
            user.Insert();
        }
    }
}