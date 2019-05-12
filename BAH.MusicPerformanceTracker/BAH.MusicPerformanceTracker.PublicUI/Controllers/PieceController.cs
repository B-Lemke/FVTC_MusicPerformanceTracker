using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BAH.MusicPerformanceTracker.BL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAH.MusicPieceTracker.PublicUI.Controllers
{
    public class PieceController : Controller
    {
        PieceList pieces;

        // GET: Piece
        public ActionResult Index()
        {
            pieces = new PieceList();

            //Initialize Cient
            HttpClient client = InitializeClient();

            //Call the API
            HttpResponseMessage response = client.GetAsync("Piece").Result;

            //Deserialize the json
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            pieces = items.ToObject<PieceList>();

            return View(pieces);
        }

        // GET: Piece/Details/5
        public ActionResult Details(Guid id)
        {
            Piece piece;

            //Initialize Cient
            HttpClient client = InitializeClient();

            //Call the API
            HttpResponseMessage response = client.GetAsync("Piece/" + id).Result;

            //Deserialize the json
            string result = response.Content.ReadAsStringAsync().Result;
            piece = JsonConvert.DeserializeObject<Piece>(result);

            


            return View(piece);
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-apikey", "12345");
            client.BaseAddress = new Uri("http://musicperformancetracker.azurewebsites.net/api/");
            return client;
        }

    }
}
