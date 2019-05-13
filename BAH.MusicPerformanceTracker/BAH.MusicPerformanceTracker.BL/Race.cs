using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class Race
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        //Retrieve the race from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblRace race = dc.tblRaces.FirstOrDefault(r => r.Id == this.Id);

                    //Set this race's properties
                    this.Description = race.Description;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Insert the race into the db, and generate a new Id for it.
        public int Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Create a new Id
                    this.Id = Guid.NewGuid();

                    //Set the properties
                    tblRace race = new tblRace
                    {
                        Id = this.Id,
                        Description = this.Description
                    };

                    //Add it to the table and save changes
                    dc.tblRaces.Add(race);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Retrieve the race from the database with this Id
        public void LoadByDescription()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblRace race = dc.tblRaces.FirstOrDefault(p => p.Description == this.Description);

                    //Set this race's properties
                    this.Id = race.Id;
                    this.Description = race.Description;
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
                    tblRace race = dc.tblRaces.FirstOrDefault(r => r.Id == this.Id);

                    //Update the properties
                    race.Description = this.Description;

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
                    tblRace race = dc.tblRaces.FirstOrDefault(r => r.Id == this.Id);

                    //Remove the race
                    dc.tblRaces.Remove(race);

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


    public class RaceList : List<Race>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblRaces;
                    foreach (tblRace r in results)
                    {
                        Race race = new Race
                        {
                            Id = r.Id,
                            Description = r.Description
                        };

                        this.Add(race);
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
