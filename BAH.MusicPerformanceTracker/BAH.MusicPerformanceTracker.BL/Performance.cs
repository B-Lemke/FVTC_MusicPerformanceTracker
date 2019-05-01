using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class Performance
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayName("Performance Date")]
        public DateTime PerformanceDate { get; set; }
        public string Location { get; set; }
        public string PDFPath { get; set; }


        //Retrieve the performance from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblPerformance performance = dc.tblPerformances.FirstOrDefault(p => p.Id == this.Id);

                    //Set this performance's properties
                    this.Name = performance.Name;
                    this.Description = performance.Description;
                    this.PDFPath = performance.pdfPath;
                    this.PerformanceDate = performance.PerformanceDate;
                    this.Location = performance.Location;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Insert the performance into the db, and generate a new Id for it.
        public int Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Create a new Id
                    this.Id = Guid.NewGuid();

                    //Set the properties
                    tblPerformance performance = new tblPerformance
                    {
                        Id = this.Id,
                        Name = this.Name,
                        Description = this.Description,
                        PerformanceDate = this.PerformanceDate,
                        pdfPath = this.PDFPath,
                        Location = this.Location
                    };

                    //Add it to the table and save changes
                    dc.tblPerformances.Add(performance);
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
                    tblPerformance performance = dc.tblPerformances.FirstOrDefault(c => c.Id == this.Id);

                    //Update the properties
                    performance.Name = this.Name;
                    performance.Description = this.Description;
                    performance.PerformanceDate = this.PerformanceDate;
                    performance.pdfPath = this.PDFPath;
                    performance.Location = this.Location;

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
                    tblPerformance performance = dc.tblPerformances.FirstOrDefault(p => p.Id == this.Id);

                    //Remove the performance
                    dc.tblPerformances.Remove(performance);

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

    public class PerformanceList : List<Performance>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblPerformances;
                    foreach (tblPerformance c in results)
                    {
                        Performance performance = new Performance
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Description = c.Description,
                            Location = c.Location,
                            PDFPath = c.pdfPath,
                            PerformanceDate = c.PerformanceDate
                        };

                        this.Add(performance);
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
