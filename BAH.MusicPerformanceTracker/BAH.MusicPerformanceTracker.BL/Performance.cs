using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;
using BAH.Utilities.Reporting;
using System.Net.Mail;

namespace BAH.MusicPerformanceTracker.BL
{
    public class Performance
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Performance Date")]
        public DateTime PerformanceDate { get; set; }
        public string Location { get; set; }
        public string PDFPath { get; set; }
        public PerformancePieceList PerfromancePieces { get; set; }


        public Performance()
        {
            PerfromancePieces = new PerformancePieceList();
        }

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

                    LoadPerformancePieces();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Retrieve the performance from the database with this Id
        public void LoadByName()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblPerformance performance = dc.tblPerformances.FirstOrDefault(p => p.Name == this.Name);

                    //Set this performance's properties
                    this.Id = performance.Id;
                    this.Description = performance.Description;
                    this.Location = performance.Location;
                    this.PerformanceDate = performance.PerformanceDate;
                    this.PDFPath = performance.pdfPath;
                    this.Name = performance.Name;

                    LoadPerformancePieces();
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

                    // Send an email to inform that a new performance was added
                    // Password: $haphir0MT
                    MailMessage mail = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("shapiromusictracker@gmail.com");
                    mail.To.Add("huntersiebers12@gmail.com");
                    mail.Subject = "New Performance Added!";
                    mail.Body = "A new performance was added! Check it out here: http://shapiro.azurewebsites.net";

                    smtpClient.Port = 587;
                    smtpClient.Credentials = new System.Net.NetworkCredential("shapiromusictracker@gmail.com", "$haphir0MT");
                    smtpClient.EnableSsl = true;

                    smtpClient.Send(mail);

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



        public void LoadPerformancePieces()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = from p in dc.tblPerformancePieces
                                  join d in dc.tblDirectors on p.DirectorId equals d.Id
                                  join pi in dc.tblPieces on p.PieceId equals pi.Id
                                  join g in dc.tblGroups on p.GroupId equals g.Id
                                  join pe in dc.tblPerformances on p.PerformanceId equals pe.Id
                                  where p.PerformanceId == this.Id
                                  select new
                                  {
                                      p.Id,
                                      p.DirectorId,
                                      p.GroupId,
                                      p.MP3Path,
                                      p.Notes,
                                      p.PerformanceId,
                                      p.PieceId,
                                      directorName = d.FirstName + " " + d.LastName,
                                      pieceName = pi.Name,
                                      performanceName = pe.Name,
                                      groupName = g.Name
                                  };

                        
                        //dc.tblPerformancePieces.Where(pp => pp.PerformanceId == this.Id);
                    foreach (var p in results)
                    {
                        PerformancePiece performancePiece = new PerformancePiece
                        {
                            Id = p.Id,
                            DirectorId = p.DirectorId.GetValueOrDefault(),
                            GroupId = p.GroupId,
                            MP3Path = p.MP3Path,
                            Notes = p.Notes,
                            PerformanceId = p.PerformanceId,
                            PieceId = p.PieceId,
                            PerformanceName = p.performanceName,
                            PieceName = p.pieceName,
                            GroupName = p.groupName,
                            DirectorName = p.directorName
                        };

                        this.PerfromancePieces.Add(performancePiece);
                    }
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

        public void Export()
        {

            try
            {
                string[,] data = new string[this.Count + 1, 4];
                int counter = 0;

                data[counter, 0] = "Performance Name";
                data[counter, 1] = "Description";
                data[counter, 2] = "Date";
                data[counter, 3] = "Location";
                counter++;

                foreach (var p in this)
                {
                    data[counter, 0] = p.Name;
                    data[counter, 1] = p.Description;
                    data[counter, 2] = p.PerformanceDate.ToString();
                    data[counter, 3] = p.Location;
                    counter++;
                }

                Excel.Export("Performances_" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year + ".xlsx", data);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

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
                        performance.LoadPerformancePieces();
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
