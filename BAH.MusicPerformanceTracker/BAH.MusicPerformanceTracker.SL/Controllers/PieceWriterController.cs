using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class PieceWriterController : ApiController
    {
        // GET: api/PieceWriter
        public IEnumerable<PieceWriter> Get()
        {
            PieceWriterList pwl = new PieceWriterList();
            pwl.Load();
            return pwl;
        }

        // GET: api/PieceWriter/5
        public PieceWriter Get(Guid id)
        {
            PieceWriter pieceWriter = new PieceWriter { Id = id };
            pieceWriter.LoadById();
            return pieceWriter;
        }

        // POST: api/PieceWriter
        public void Post(PieceWriter pieceWriter)
        {
            pieceWriter.Insert();
        }

        // PUT: api/PieceWriter/5
        public void Put(Guid id, PieceWriter pieceWriter)
        {
            pieceWriter.Update();
        }

        // DELETE: api/PieceWriter/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
