using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        //Retrieve the location from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblLocation location = dc.tblLocations.FirstOrDefault(l => l.Id == this.Id);

                    //Set this location's properties
                    this.Description = location.Description;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Insert the location into the db, and generate a new Id for it.
        public int Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Create a new Id
                    this.Id = Guid.NewGuid();

                    //Set the properties
                    tblLocation location = new tblLocation
                    {
                        Id = this.Id,
                        Description = this.Description
                    };

                    //Add it to the table and save changes
                    dc.tblLocations.Add(location);
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
                    tblLocation location = dc.tblLocations.FirstOrDefault(l => l.Id == this.Id);

                    //Update the properties
                    location.Description = this.Description;

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
                    tblLocation location = dc.tblLocations.FirstOrDefault(l => l.Id == this.Id);

                    //Remove the location
                    dc.tblLocations.Remove(location);

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


    public class LocationList : List<Location>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblLocations;
                    foreach (tblLocation l in results)
                    {
                        Location location = new Location
                        {
                            Id = l.Id,
                            Description = l.Description
                        };

                        this.Add(location);
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
