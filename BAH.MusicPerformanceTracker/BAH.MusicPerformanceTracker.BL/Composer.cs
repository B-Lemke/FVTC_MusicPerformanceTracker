using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class Composer
    {
        public Guid Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        [DisplayName("Gener")]
        public Guid GenderId { get; set; }
        [DisplayName("Race")]
        public Guid RaceId { get; set; }
        [DisplayName("Location")]
        public Guid LocationId { get; set; }

        public string Bio { get; set; }



        //Retrieve the composer from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblComposer composer = dc.tblComposers.FirstOrDefault(c => c.Id == this.Id);

                    //Set this composer's properties
                    this.FirstName = composer.FirstName;
                    this.LastName = composer.LastName;
                    this.GenderId = composer.GenderId.HasValue ? composer.GenderId.Value : Guid.Empty;
                    this.RaceId = composer.RaceId.HasValue ? composer.RaceId.Value : Guid.Empty;
                    this.LocationId = composer.LocationId.HasValue ? composer.LocationId.Value : Guid.Empty;
                    this.Bio = composer.Bio;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Insert the composer into the db, and generate a new Id for it.
        public int Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Create a new Id
                    this.Id = Guid.NewGuid();

                    //Set the properties
                    tblComposer composer = new tblComposer
                    {
                        Id = this.Id,
                        FirstName = this.FirstName,
                        LastName = this.LastName,
                        GenderId = this.GenderId,
                        LocationId = this.LocationId,
                        RaceId = this.RaceId,
                        Bio = this.Bio
                    };

                    //Add it to the table and save changes
                    dc.tblComposers.Add(composer);
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
                    tblComposer composer = dc.tblComposers.FirstOrDefault(c => c.Id == this.Id);

                    //Update the properties
                    composer.FirstName = this.FirstName;
                    composer.LastName = this.LastName;
                    composer.GenderId = this.GenderId;
                    composer.LocationId = this.LocationId;
                    composer.RaceId = this.RaceId;
                    composer.Bio = this.Bio;

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
                    tblComposer composer = dc.tblComposers.FirstOrDefault(c => c.Id == this.Id);

                    //Remove the composer
                    dc.tblComposers.Remove(composer);

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


    public class ComposerList : List<Composer>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblComposers;
                    foreach (tblComposer c in results)
                    {
                        Composer composer = new Composer
                        {
                            Id = c.Id,
                            Bio = c.Bio,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            GenderId = c.GenderId.HasValue ? c.GenderId.Value : Guid.Empty,
                            LocationId = c.LocationId.HasValue ? c.LocationId.Value : Guid.Empty,
                            RaceId = c.RaceId.HasValue ? c.RaceId.Value : Guid.Empty

                        };

                        this.Add(composer);
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
