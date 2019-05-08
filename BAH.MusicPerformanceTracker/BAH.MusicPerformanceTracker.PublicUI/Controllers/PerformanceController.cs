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
    public class PerformanceController : Controller
    {
        PerformanceList performances;

        // GET: Performance
        public ActionResult Index()
        {
            performances = new PerformanceList();

            //Initialize Cient
            HttpClient client = InitializeClient();

            //Call the API
            HttpResponseMessage response = client.GetAsync("Performance").Result;

            //Deserialize the json
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            performances = items.ToObject<PerformanceList>();

            return View(performances);
        }

        // GET: Performance/Details/5
        public ActionResult Details(Guid id)
        {
            Performance performance;

            //Initialize Cient
            HttpClient client = InitializeClient();

            //Call the API
            HttpResponseMessage response = client.GetAsync("Performance/" + id).Result;

            //Deserialize the json
            string result = response.Content.ReadAsStringAsync().Result;
            performance = JsonConvert.DeserializeObject<Performance>(result);

            return View(performance);
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
