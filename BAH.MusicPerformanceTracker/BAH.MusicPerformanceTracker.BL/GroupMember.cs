using BAH.MusicPerformanceTracker.PL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAH.MusicPerformanceTracker.BL
{
    public class GroupMember
    {
        public Guid Id { get; set; }
        [DisplayName("Group Name")]
        public Guid GroupId { get; set; }
        [DisplayName("Name")]
        public Guid PerformerId { get; set; }
        public Guid Instrument { get; set; }
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }


        //Retrieve the groupmember from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblGroupMember groupmember = dc.tblGroupMembers.FirstOrDefault(p => p.Id == this.Id);

                    //Set this groupmember's properties
                    this.GroupId = groupmember.GroupId != null ? groupmember.GroupId : Guid.Empty;
                    this.PerformerId = groupmember.PerformerId != null ? groupmember.PerformerId : Guid.Empty;
                    this.PerformerId = groupmember.Instrument != null ? groupmember.Instrument : Guid.Empty;
                    this.StartDate = groupmember.StartDate;
                    if (groupmember.EndDate.HasValue)
                    {
                        this.EndDate = groupmember.EndDate.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Insert the groupmember into the db, and generate a new Id for it.
        public int Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Create a new Id
                    this.Id = Guid.NewGuid();

                    //Set the properties
                    tblGroupMember groupmember = new tblGroupMember
                    {
                        Id = this.Id,
                        GroupId = this.GroupId,
                        PerformerId = this.PerformerId,
                        Instrument = this.Instrument,
                        StartDate = this.StartDate,
                        EndDate = this.EndDate,
                    };

                    //Add it to the table and save changes
                    dc.tblGroupMembers.Add(groupmember);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve the record from the DB
                    tblGroupMember groupmember = dc.tblGroupMembers.FirstOrDefault(c => c.Id == this.Id);

                    //Update the properties
                    groupmember.GroupId = this.GroupId;
                    groupmember.PerformerId = this.PerformerId;
                    groupmember.Instrument = this.Instrument;
                    groupmember.StartDate = this.StartDate;
                    groupmember.EndDate = this.EndDate;

                    //Save the changes
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve it from the DB
                    tblGroupMember groupmember = dc.tblGroupMembers.FirstOrDefault(p => p.Id == this.Id);

                    //Remove the groupmember
                    dc.tblGroupMembers.Remove(groupmember);

                    //Save the changes
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class GroupMemberList : List<GroupMember>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblGroupMembers;
                    foreach (tblGroupMember c in results)
                    {
                        GroupMember groupmember = new GroupMember
                        {
                            Id = c.Id,
                            StartDate = c.StartDate,
                            GroupId = c.GroupId != null ? c.GroupId : Guid.Empty,
                            PerformerId = c.PerformerId != null ? c.PerformerId : Guid.Empty,
                            Instrument = c.Instrument != null ? c.Instrument : Guid.Empty

                        };

                        //Set the end date
                        if (c.EndDate.HasValue)
                        {
                            groupmember.EndDate = c.EndDate.Value;
                        }

                        this.Add(groupmember);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
