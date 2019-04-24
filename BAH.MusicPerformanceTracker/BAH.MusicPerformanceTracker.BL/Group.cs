using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayName("Founded Date")]
        public DateTime FoundedDate { get; set; }


        //Retrieve the group from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblGroup group = dc.tblGroups.FirstOrDefault(p => p.Id == this.Id);

                    //Set this group's properties
                    this.Name = group.Name;
                    this.Description = group.Description;
                    if (group.FoundedDate.HasValue)
                    {
                        this.FoundedDate = group.FoundedDate.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Insert the group into the db, and generate a new Id for it.
        public int Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Create a new Id
                    this.Id = Guid.NewGuid();

                    //Set the properties
                    tblGroup group = new tblGroup
                    {
                        Id = this.Id,
                        Name = this.Name,
                        Description = this.Description,
                        FoundedDate = this.FoundedDate,
                    };

                    //Add it to the table and save changes
                    dc.tblGroups.Add(group);
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
                    tblGroup group = dc.tblGroups.FirstOrDefault(c => c.Id == this.Id);

                    //Update the properties
                    group.Name = this.Name;
                    group.Description = this.Description;
                    group.FoundedDate = this.FoundedDate;

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
                    tblGroup group = dc.tblGroups.FirstOrDefault(p => p.Id == this.Id);

                    //Remove the group
                    dc.tblGroups.Remove(group);

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

    public class GroupList : List<Group>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblGroups;
                    foreach (tblGroup c in results)
                    {
                        Group group = new Group
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Description = c.Description
                        };

                        //Set the end date
                        if (c.FoundedDate.HasValue)
                        {
                            group.FoundedDate = c.FoundedDate.Value;
                        }

                        this.Add(group);
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