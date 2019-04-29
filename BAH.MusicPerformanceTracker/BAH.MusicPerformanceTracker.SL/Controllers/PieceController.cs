using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class PieceController : ApiController
    {
        // GET: api/Piece
        public IEnumerable<Piece> Get()
        {
            PieceList pieces = new PieceList();
            pieces.Load();
            return pieces;
        }

        // GET: api/Piece/5
        public Piece Get(Guid id)
        {
            Piece piece = new Piece { Id = id };
            piece.LoadById();
            return piece;
        }

        // GET: api/Piece?name={name}
        public Piece GetByName(string name)
        {
            Piece piece = new Piece { Name = name };
            piece.LoadByName();
            return piece;
        }

        // POST: api/Piece
        public void Post(Piece piece)
        {
            piece.Insert();
        }

        // PUT: api/Piece/5
        public void Put(Guid id, Piece piece)
        {
            piece.Update();
        }

        // DELETE: api/Piece/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
