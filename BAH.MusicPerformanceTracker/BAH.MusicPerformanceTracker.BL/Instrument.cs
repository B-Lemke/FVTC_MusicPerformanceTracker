using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class Instrument
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        //Retrieve the instrument from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblInstrument instrument = dc.tblInstruments.FirstOrDefault(i => i.Id == this.Id);

                    //Set this instrument's properties
                    this.Description = instrument.Description;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Insert the instrument into the db, and generate a new Id for it.
        public int Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Create a new Id
                    this.Id = Guid.NewGuid();

                    //Set the properties
                    tblInstrument instrument = new tblInstrument
                    {
                        Id = this.Id,
                        Description = this.Description
                    };

                    //Add it to the table and save changes
                    dc.tblInstruments.Add(instrument);
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
                    tblInstrument instrument = dc.tblInstruments.FirstOrDefault(i => i.Id == this.Id);

                    //Update the properties
                    instrument.Description = this.Description;

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
                    tblInstrument instrument = dc.tblInstruments.FirstOrDefault(i => i.Id == this.Id);

                    //Remove the instrument
                    dc.tblInstruments.Remove(instrument);

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


    public class InstrumentList : List<Instrument>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblInstruments;
                    foreach (tblInstrument i in results)
                    {
                        Instrument instrument = new Instrument
                        {
                            Id = i.Id,
                            Description = i.Description
                        };

                        this.Add(instrument);
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
