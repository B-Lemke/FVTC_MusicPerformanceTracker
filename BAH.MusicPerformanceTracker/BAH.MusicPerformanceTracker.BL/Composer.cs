using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;

namespace BAH.MusicPerformanceTracker.BL
{
    public class Composer
    {
        public Guid Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        [DisplayName("Gender")]
        public Guid GenderId { get; set; }
        [DisplayName("Race")]
        public Guid RaceId { get; set; }
        [DisplayName("Location")]
        public Guid LocationId { get; set; }


        [DisplayName("Gender")]
        public string Gender { get; set; }
        [DisplayName("Race")]
        public string Race { get; set; }
        [DisplayName("Location")]
        public string Location { get; set; }

        public string Bio { get; set; }

        public PieceList PiecesWritten { get; set; }

        //Retrieve the composer from the database with this Id
        public void LoadById()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Retrieve from the db
                    var composer = (from c in dc.tblComposers
                                    join g in dc.tblGenders on c.GenderId equals g.Id
                                    join l in dc.tblLocations on c.LocationId equals l.Id
                                    join r in dc.tblRaces on c.RaceId equals r.Id
                                    where c.Id == this.Id
                                    select new
                                    {
                                        c.Id,
                                        c.FirstName,
                                        c.LastName,
                                        c.GenderId,
                                        c.RaceId,
                                        c.LocationId,
                                        c.Bio,
                                        GenderDescription = g.Description,
                                        LocationDecsription = l.Description,
                                        RaceDescription = r.Description
                                    }).FirstOrDefault();

                    //Set this composer's properties
                    this.FirstName = composer.FirstName;
                    this.LastName = composer.LastName;
                    this.GenderId = composer.GenderId.HasValue ? composer.GenderId.Value : Guid.Empty;
                    this.RaceId = composer.RaceId.HasValue ? composer.RaceId.Value : Guid.Empty;
                    this.LocationId = composer.LocationId.HasValue ? composer.LocationId.Value : Guid.Empty;
                    this.Bio = composer.Bio;
                    this.Gender = composer.GenderDescription;
                    this.Location = composer.LocationDecsription;
                    this.Race = composer.RaceDescription;

                    LoadPieces();
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
                    tblComposer composer = dc.tblComposers.FirstOrDefault(p => p.LastName == this.LastName);

                    //Set this performance's properties
                    this.Id = composer.Id;
                    this.FirstName = composer.FirstName;
                    this.LastName = composer.LastName;
                    this.Bio = composer.Bio;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Insert the composer into the db, and generate a new Id for it.
        public int Insert()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    //Create a new Id
                    this.Id = Guid.NewGuid();

                    //Set the properties
                    tblComposer composer = new tblComposer
                    {
                        Id = this.Id,
                        FirstName = this.FirstName,
                        LastName = this.LastName,
                        GenderId = this.GenderId,
                        LocationId = this.LocationId,
                        RaceId = this.RaceId,
                        Bio = this.Bio
                    };

                    //Add it to the table and save changes
                    dc.tblComposers.Add(composer);
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
                    tblComposer composer = dc.tblComposers.FirstOrDefault(c => c.Id == this.Id);

                    //Update the properties
                    composer.FirstName = this.FirstName;
                    composer.LastName = this.LastName;
                    composer.GenderId = this.GenderId;
                    composer.LocationId = this.LocationId;
                    composer.RaceId = this.RaceId;
                    composer.Bio = this.Bio;

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
                    tblComposer composer = dc.tblComposers.FirstOrDefault(c => c.Id == this.Id);

                    //Remove the composer
                    dc.tblComposers.Remove(composer);

                    //Save the changes
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadPieces()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    this.PiecesWritten = new PieceList();

                    var results = from pw in dc.tblPieceWriters
                                  join p in dc.tblPieces on pw.PieceId equals p.Id
                                  where pw.ComposerId == this.Id
                                  select new
                                  {
                                      p.Name,
                                      p.Id
                                  };

                    foreach(var piece in results)
                    {
                        Piece newPiece = new Piece
                        {
                            Id = piece.Id,
                            Name = piece.Name
                        };

                        this.PiecesWritten.Add(newPiece);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    public class ComposerList : List<Composer>
    {
        public void Load()
        {
            try
            {
                using (MusicEntities dc = new MusicEntities())
                {
                    var results = (from c in dc.tblComposers
                                   join g in dc.tblGenders on c.GenderId equals g.Id
                                   join l in dc.tblLocations on c.LocationId equals l.Id
                                   join r in dc.tblRaces on c.RaceId equals r.Id
                                   select new
                                   {
                                       c.Id,
                                       c.FirstName,
                                       c.LastName,
                                       c.GenderId,
                                       c.RaceId,
                                       c.LocationId,
                                       c.Bio,
                                       GenderDescription = g.Description,
                                       LocationDecsription = l.Description,
                                       RaceDescription = r.Description
                                   }).ToList();

                    foreach (var c in results)
                    {
                        Composer composer = new Composer
                        {
                            Id = c.Id,
                            Bio = c.Bio,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            GenderId = c.GenderId.HasValue ? c.GenderId.Value : Guid.Empty,
                            LocationId = c.LocationId.HasValue ? c.LocationId.Value : Guid.Empty,
                            RaceId = c.RaceId.HasValue ? c.RaceId.Value : Guid.Empty,
                            Gender = c.GenderDescription,
                            Location = c.LocationDecsription,
                            Race = c.RaceDescription

                        };

                        this.Add(composer);
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
