using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class PerformancePieceController : ApiController
    {
        // GET: api/PerformancePiece
        public IEnumerable<PerformancePiece> Get()
        {
            PerformancePieceList performancePieces = new PerformancePieceList();
            performancePieces.Load();
            return performancePieces;
        }

        // GET: api/PerformancePiece/5
        public PerformancePiece Get(Guid id)
        {
            PerformancePiece performancePiece = new PerformancePiece { Id = id };
            performancePiece.LoadById();
            return performancePiece;
        }

        // POST: api/PerformancePiece
        public void Post(PerformancePiece performancePiece)
        {
            performancePiece.Insert();
        }

        // Put: api/PerformancePiece
        public void Put(Guid id, PerformancePiece performancePiece)
        {
            performancePiece.Update();
        }

        // DELETE: api/PerformancePiece/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
