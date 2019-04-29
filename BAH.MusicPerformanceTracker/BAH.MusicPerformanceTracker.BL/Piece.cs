using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class Piece
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string GradeLevel { get; set; }
        public int YearWritten { get; set; }
        public string PerformanceNotes { get; set; }
        public GenreList Genres { get; set; }
        public PieceWriterList PieceWriters {get;set;}


        public Piece()
        {
            Genres = new GenreList();
            PieceWriters = new PieceWriterList();
        }


        //Retrieve the piece from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblPiece piece = dc.tblPieces.FirstOrDefault(p => p.Id == this.Id);

                    //Set this piece's properties
                    this.Name = piece.Name;
                    if (piece.YearWritten.HasValue)
                    {
                        this.YearWritten = piece.YearWritten.Value;
                    }
                    else
                    {
                        piece.YearWritten = -1;
                    }
                    this.GradeLevel = piece.GradeLevel;
                    this.PerformanceNotes = piece.PefromanceNotes;
                    LoadPieceWriters();
                    LoadGenres();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Retrieve the piece from the database with this Id
        public void LoadByName()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblPiece piece = dc.tblPieces.FirstOrDefault(p => p.Name == this.Name);

                    //Set this piece's properties
                    this.Id = piece.Id;
                    if (piece.YearWritten.HasValue)
                    {
                        this.YearWritten = piece.YearWritten.Value;
                    }
                    else
                    {
                        piece.YearWritten = -1;
                    }
                    this.GradeLevel = piece.GradeLevel;
                    this.PerformanceNotes = piece.PefromanceNotes;
                    LoadPieceWriters();
                    LoadGenres();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadPieceWriters()
        {
            this.PieceWriters = new PieceWriterList();
            this.PieceWriters.LoadByPieceId(this.Id);
        }

        //Insert the piece into the db, and generate a new Id for it.
        public int Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Create a new Id
                    this.Id = Guid.NewGuid();

                    //Set the properties
                    tblPiece piece = new tblPiece
                    {
                        Id = this.Id,
                        Name = this.Name,
                        GradeLevel = this.GradeLevel,
                        PefromanceNotes = this.PerformanceNotes,
                    };

                    //Set the year written
                    if (piece.YearWritten >= 0)
                    {
                        piece.YearWritten = this.YearWritten;
                    }
                    else
                    {
                        piece.YearWritten = null;
                    }

                    //Add it to the table and save changes
                    dc.tblPieces.Add(piece);
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
                    tblPiece piece = dc.tblPieces.FirstOrDefault(p => p.Id == this.Id);

                    //Update the properties
                    piece.Id = this.Id;
                    piece.Name = this.Name;
                    piece.GradeLevel = this.GradeLevel;
                    piece.PefromanceNotes = this.PerformanceNotes;
                    //Set the year written
                    if (piece.YearWritten >= 0)
                    {
                        piece.YearWritten = this.YearWritten;
                    }
                    else
                    {
                        piece.YearWritten = null;
                    }

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
                    tblPiece piece = dc.tblPieces.FirstOrDefault(p => p.Id == this.Id);

                    //Remove the piece
                    dc.tblPieces.Remove(piece);

                    //Save the changes
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Load the genres onto this piece
        public void LoadGenres()
        {
            Genres = new GenreList();
            Genres.LoadByPieceId(this.Id);
        }
    }


    public class PieceList : List<Piece>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblPieces;
                    foreach (tblPiece p in results)
                    {
                        Piece piece = new Piece
                        {
                            Id = p.Id,
                            Name = p.Name,
                            GradeLevel = p.GradeLevel,
                            PerformanceNotes = p.PefromanceNotes
                        };
                        //Set the year written
                        if (p.YearWritten.HasValue)
                        {
                            piece.YearWritten = p.YearWritten.Value;
                        } else
                        {
                            piece.YearWritten = -1;
                        }
                        piece.LoadPieceWriters();
                        piece.LoadGenres();
                        this.Add(piece);

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
