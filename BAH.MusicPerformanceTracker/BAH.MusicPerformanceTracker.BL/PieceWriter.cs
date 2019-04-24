using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class PieceWriter
    {
        private string pieceWriterPefromanceNotes;

        public Guid Id { get; set; }
        public Guid PieceId { get; set; }
        public Guid ComposerId { get; set; }
        public Guid ComposerTypeId { get; set; }

        //Retrieve the pieceWriter from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblPieceWriter pieceWriter = dc.tblPieceWriters.FirstOrDefault(p => p.Id == this.Id);

                    //Set this pieceWriter's properties
                    this.ComposerId = pieceWriter.ComposerId;
                    this.ComposerTypeId = pieceWriter.ComposerTypeId;
                    this.PieceId = pieceWriter.PieceId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Insert the pieceWriter into the db, and generate a new Id for it.
        public int Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Create a new Id
                    this.Id = Guid.NewGuid();

                    //Set the properties
                    tblPieceWriter pieceWriter = new tblPieceWriter
                    {
                        Id = this.Id,
                        ComposerTypeId = this.ComposerTypeId,
                        ComposerId = this.ComposerId,
                        PieceId = this.PieceId,
                    };

                    //Add it to the table and save changes
                    dc.tblPieceWriters.Add(pieceWriter);
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
                    tblPieceWriter pieceWriter = dc.tblPieceWriters.FirstOrDefault(p => p.Id == this.Id);

                    //Update the properties
                    pieceWriter.Id = this.Id;
                    pieceWriter.PieceId = this.PieceId;
                    pieceWriter.ComposerId = this.ComposerId;
                    pieceWriter.ComposerTypeId = this.ComposerTypeId;

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
                    tblPieceWriter pieceWriter = dc.tblPieceWriters.FirstOrDefault(p => p.Id == this.Id);

                    //Remove the pieceWriter
                    dc.tblPieceWriters.Remove(pieceWriter);

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


    public class PieceWriterList : List<PieceWriter>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblPieceWriters;
                    foreach (tblPieceWriter p in results)
                    {
                        PieceWriter pieceWriter = new PieceWriter
                        {
                            Id = p.Id,
                            ComposerTypeId = p.ComposerTypeId,
                            ComposerId = p.ComposerId,
                            PieceId = p.PieceId
                        };

                        this.Add(pieceWriter);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void LoadByPieceId(Guid id)
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblPieceWriters.Where(pw => pw.PieceId == id);
                    foreach (tblPieceWriter p in results)
                    {
                        PieceWriter pieceWriter = new PieceWriter
                        {
                            Id = p.Id,
                            ComposerTypeId = p.ComposerTypeId,
                            ComposerId = p.ComposerId,
                            PieceId = p.PieceId
                        };

                        this.Add(pieceWriter);
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
