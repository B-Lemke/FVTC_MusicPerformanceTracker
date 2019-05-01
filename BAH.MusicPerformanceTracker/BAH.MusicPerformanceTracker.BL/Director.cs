using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class Director
    {
        public Guid Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Director Date")]
        public DateTime BirthDate { get; set; }
        public string Bio { get; set; }
        [DisplayName("Full Name")]
        public string FullName => $"{FirstName} {LastName}";



        //Retrieve the director from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblDirector director = dc.tblDirectors.FirstOrDefault(p => p.Id == this.Id);

                    //Set this director's properties
                    this.FirstName = director.FirstName;
                    this.LastName = director.LastName;
                    this.Bio = director.Bio;
                    this.BirthDate = director.BirthDate;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Insert the director into the db, and generate a new Id for it.
        public int Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Create a new Id
                    this.Id = Guid.NewGuid();

                    //Set the properties
                    tblDirector director = new tblDirector
                    {
                        Id = this.Id,
                        FirstName = this.FirstName,
                        LastName = this.LastName,
                        Bio = this.Bio,
                        BirthDate = this.BirthDate
                    };

                    //Add it to the table and save changes
                    dc.tblDirectors.Add(director);
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
                    tblDirector director = dc.tblDirectors.FirstOrDefault(c => c.Id == this.Id);

                    //Update the properties
                    director.FirstName = this.FirstName;
                    director.LastName = this.LastName;
                    director.Bio = this.Bio;
                    director.BirthDate = this.BirthDate;

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
                    tblDirector director = dc.tblDirectors.FirstOrDefault(p => p.Id == this.Id);

                    //Remove the director
                    dc.tblDirectors.Remove(director);

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

    public class DirectorList : List<Director>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblDirectors;
                    foreach (tblDirector c in results)
                    {
                        Director director = new Director
                        {
                            Id = c.Id,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            Bio = c.Bio,
                            BirthDate = c.BirthDate
                        };

                        this.Add(director);
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
