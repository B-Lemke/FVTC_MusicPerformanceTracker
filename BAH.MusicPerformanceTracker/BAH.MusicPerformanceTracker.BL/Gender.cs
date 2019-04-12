using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class Gender
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        //Retrieve the gender from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblGender gender = dc.tblGenders.FirstOrDefault(g => g.Id == this.Id);

                    //Set this gender's properties
                    this.Description = gender.Description;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Insert the gender into the db, and generate a new Id for it.
        public int Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Create a new Id
                    this.Id = Guid.NewGuid();

                    //Set the properties
                    tblGender gender = new tblGender
                    {
                        Id = this.Id,
                        Description = this.Description
                    };

                    //Add it to the table and save changes
                    dc.tblGenders.Add(gender);
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
                    tblGender gender = dc.tblGenders.FirstOrDefault(g => g.Id == this.Id);

                    //Update the properties
                    gender.Description = this.Description;

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
                    tblGender gender = dc.tblGenders.FirstOrDefault(g => g.Id == this.Id);

                    //Remove the gender
                    dc.tblGenders.Remove(gender);

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


    public class GenderList : List<Gender>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblGenders;
                    foreach (tblGender g in results)
                    {
                        Gender gender = new Gender
                        {
                            Id = g.Id,
                            Description = g.Description
                        };

                        this.Add(gender);
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
