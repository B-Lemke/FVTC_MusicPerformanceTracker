using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        //Retrieve the genre from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    tblGenre genre = dc.tblGenres.FirstOrDefault(g => g.Id == this.Id);

                    //Set this genre's properties
                    this.Description = genre.Description;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Insert the genre into the db, and generate a new Id for it.
        public int Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Create a new Id
                    this.Id = Guid.NewGuid();

                    //Set the properties
                    tblGenre genre = new tblGenre
                    {
                        Id = this.Id,
                        Description = this.Description
                    };

                    //Add it to the table and save changes
                    dc.tblGenres.Add(genre);
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
                    tblGenre genre = dc.tblGenres.FirstOrDefault(g => g.Id == this.Id);

                    //Update the properties
                    genre.Description = this.Description;

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
                    tblGenre genre = dc.tblGenres.FirstOrDefault(g => g.Id == this.Id);

                    //Remove the genre
                    dc.tblGenres.Remove(genre);

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


    public class GenreList : List<Genre>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = dc.tblGenres;
                    foreach(tblGenre g in results)
                    {
                        Genre genre = new Genre
                        {
                            Id = g.Id,
                            Description = g.Description
                        };

                        this.Add(genre);
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
                    var results = from pg in dc.tblPieceGenres
                                  join g in dc.tblGenres on pg.GenreId equals g.Id
                                  where pg.PieceId == id
                                  select new
                                  {
                                      g.Id,
                                      g.Description
                                  };

                    foreach (var g in results)
                    {
                        Genre genre = new Genre
                        {
                            Id = g.Id,
                            Description = g.Description
                        };

                        this.Add(genre);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class PieceGenre
    {
        public Guid Id { get; set; }
        public Guid PieceId { get; set; }
        public Guid GenreId { get; set; }

        public void Add()
        {
            using (MusicEntities dc = new MusicEntities())
            {

                //Set the properties
                tblPieceGenre pieceGenre = new tblPieceGenre
                {
                    Id = Guid.NewGuid(),
                    PieceId = PieceId,
                    GenreId = GenreId
                };

                //Add it to the table and save changes
                dc.tblPieceGenres.Add(pieceGenre);
                dc.SaveChanges();
            }
        }

        public void Delete()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                tblPieceGenre pieceGenre = dc.tblPieceGenres.FirstOrDefault(pg => pg.PieceId == PieceId && pg.GenreId == GenreId);
                dc.tblPieceGenres.Remove(pieceGenre);
                dc.SaveChanges();
            }
        }
    }
}
