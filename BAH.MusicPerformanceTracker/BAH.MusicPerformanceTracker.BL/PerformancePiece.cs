using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class PerformancePiece
    {
        public Guid Id { get; set; }
        public string Notes { get; set; }
        [DisplayName("MP3")]
        public string MP3Path { get; set; }
        [DisplayName("Director")]
        public Guid DirectorId { get; set; }
        [DisplayName("Group")]
        public Guid GroupId { get; set; }
        [DisplayName("Performance")]
        public Guid PerformanceId { get; set; }
        [DisplayName("Piece")]
        public Guid PieceId { get; set; }


        //Retrieve the performance from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblPerformancePiece performancepiece = dc.tblPerformancePieces.FirstOrDefault(p => p.Id == this.Id);

                    //Set this performancepiece's properties
                    this.Notes = performancepiece.Notes;
                    this.MP3Path = performancepiece.MP3Path;
                    this.GroupId = performancepiece.GroupId != null ? performancepiece.GroupId : Guid.Empty;
                    this.PieceId = performancepiece.PieceId != null ? performancepiece.PieceId : Guid.Empty;
                    this.PerformanceId = performancepiece.PerformanceId != null ? performancepiece.PerformanceId : Guid.Empty;
                    if (performancepiece.DirectorId.HasValue)
                    {
                        this.DirectorId = performancepiece.DirectorId.Value;
                    }
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
                    tblPerformancePiece performancePiece = new tblPerformancePiece
                    {
                        Id = this.Id,
                        Notes = this.Notes,
                        MP3Path = this.MP3Path,
                        GroupId = this.GroupId,
                        PerformanceId = this.PerformanceId,
                        DirectorId = this.DirectorId,
                        PieceId = this.PieceId
                        
                    };

                    //Add it to the table and save changes
                    dc.tblPerformancePieces.Add(performancePiece);
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
                    tblPerformancePiece performancePiece = dc.tblPerformancePieces.FirstOrDefault(c => c.Id == this.Id);

                    //Update the properties
                    performancePiece.Notes = this.Notes;
                    performancePiece.MP3Path = this.MP3Path;
                    performancePiece.GroupId = this.GroupId;
                    performancePiece.DirectorId = this.DirectorId;
                    performancePiece.PieceId = this.PieceId;
                    performancePiece.PerformanceId = this.PerformanceId;

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
                    tblPerformancePiece performancePiece = dc.tblPerformancePieces.FirstOrDefault(p => p.Id == this.Id);

                    //Remove the performance
                    dc.tblPerformancePieces.Remove(performancePiece);

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

    public class PerformancePieceList : List<PerformancePiece>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblPerformancePieces;
                    foreach (tblPerformancePiece c in results)
                    {
                        PerformancePiece performancePiece = new PerformancePiece
                        {
                            Id = c.Id,
                            Notes = c.Notes,
                            MP3Path = c.MP3Path,
                            GroupId = c.GroupId != null ? c.GroupId : Guid.Empty,
                            PerformanceId = c.PerformanceId != null ? c.PerformanceId : Guid.Empty,
                            PieceId = c.PieceId != null ? c.PieceId : Guid.Empty

                        };

                        //Set the end date
                        if (c.DirectorId.HasValue)
                        {
                            performancePiece.DirectorId = c.DirectorId.Value;
                        }

                        this.Add(performancePiece);
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
