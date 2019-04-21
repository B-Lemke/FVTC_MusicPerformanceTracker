using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.SL.Controllers
{
    public class GenreController : ApiController
    {
        // GET: api/Genre
        public IEnumerable<Genre> Get()
        {
            GenreList genres = new GenreList();
            genres.Load();
            return genres;
        }

        // GET: api/Genre/5
        public Genre Get(Guid id)
        {
            Genre genre = new Genre { Id = id };
            genre.LoadById();
            return genre;
        }

        // POST: api/Genre
        public void Post(Genre genre)
        {
            genre.Insert();
        }

        // PUT: api/Genre/5
        public void Put(Guid id, Genre genre)
        {
            genre.Update();
        }

        // DELETE: api/Genre/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
