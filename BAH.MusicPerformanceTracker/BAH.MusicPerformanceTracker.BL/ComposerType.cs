using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class ComposerType
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        //Retrieve the composerType from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblComposerType composerType = dc.tblComposerTypes.FirstOrDefault(ct => ct.Id == this.Id);

                    //Set this composerType's properties
                    this.Description = composerType.Description;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Insert the composerType into the db, and generate a new Id for it.
        public int Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Create a new Id
                    this.Id = Guid.NewGuid();

                    //Set the properties
                    tblComposerType composerType = new tblComposerType
                    {
                        Id = this.Id,
                        Description = this.Description
                    };

                    //Add it to the table and save changes
                    dc.tblComposerTypes.Add(composerType);
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
                    tblComposerType composerType = dc.tblComposerTypes.FirstOrDefault(ct => ct.Id == this.Id);

                    //Update the properties
                    composerType.Description = this.Description;

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
                    tblComposerType composerType = dc.tblComposerTypes.FirstOrDefault(ct => ct.Id == this.Id);

                    //Remove the composerType
                    dc.tblComposerTypes.Remove(composerType);

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


    public class ComposerTypeList : List<ComposerType>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblComposerTypes;
                    foreach(tblComposerType ct in results)
                    {
                        ComposerType composerType = new ComposerType
                        {
                            Id = ct.Id,
                            Description = ct.Description
                        };

                        this.Add(composerType);
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
