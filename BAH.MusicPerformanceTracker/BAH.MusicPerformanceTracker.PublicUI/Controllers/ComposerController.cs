using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BAH.MusicPerformanceTracker.BL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAH.MusicPerformanceTracker.PublicUI.Controllers
{
    public class ComposerController : Controller
    {
        ComposerList composers;

        // GET: Composer
        public ActionResult Index()
        {
            composers = new ComposerList();

            //Initialize Cient
            HttpClient client = InitializeClient();

            //Call the API
            HttpResponseMessage response = client.GetAsync("Composer").Result;

            //Deserialize the json
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            composers = items.ToObject<ComposerList>();

            return View(composers);
        }

        // GET: Composer/Details/5
        public ActionResult Details(Guid id)
        {
            Composer composer;

            //Initialize Cient
            HttpClient client = InitializeClient();

            //Call the API
            HttpResponseMessage response = client.GetAsync("Composer/" + id).Result;

            //Deserialize the json
            string result = response.Content.ReadAsStringAsync().Result;
            composer = JsonConvert.DeserializeObject<Composer>(result);

            return View(composer);
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