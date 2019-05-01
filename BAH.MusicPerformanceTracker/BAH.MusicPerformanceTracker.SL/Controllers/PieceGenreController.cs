using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class PieceGenreController : ApiController
    {
        public void Post(PieceGenre pieceGenre)
        {
            pieceGenre.Add();
        }

        public void Delete(Guid pieceId, Guid genreId)
        {
            PieceGenre pieceGenre = new PieceGenre
            {
                GenreId = genreId,
                PieceId = pieceId
            };

            pieceGenre.Delete();
        }
    }
}
