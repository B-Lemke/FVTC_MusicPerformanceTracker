using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class Performer
    {
        public Guid Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Full Name")]
        public string FullName => $"{FirstName} {LastName}";


        //Retrieve the performer from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblPerformer performer = dc.tblPerformers.FirstOrDefault(p => p.Id == this.Id);

                    //Set this performer's properties
                    this.FirstName = performer.FirstName;
                    this.LastName = performer.LastName;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Insert the performer into the db, and generate a new Id for it.
        public int Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Create a new Id
                    this.Id = Guid.NewGuid();

                    //Set the properties
                    tblPerformer performer = new tblPerformer
                    {
                        Id = this.Id,
                        FirstName = this.FirstName,
                        LastName = this.LastName,
                    };

                    //Add it to the table and save changes
                    dc.tblPerformers.Add(performer);
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
                    tblPerformer performer = dc.tblPerformers.FirstOrDefault(c => c.Id == this.Id);

                    //Update the properties
                    performer.FirstName = this.FirstName;
                    performer.LastName = this.LastName;

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
                    tblPerformer performer = dc.tblPerformers.FirstOrDefault(p => p.Id == this.Id);

                    //Remove the performer
                    dc.tblPerformers.Remove(performer);

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

    public class PerformerList : List<Performer>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblPerformers;
                    foreach (tblPerformer p in results)
                    {
                        Performer performer = new Performer
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            LastName = p.LastName
                        };

                        this.Add(performer);
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
