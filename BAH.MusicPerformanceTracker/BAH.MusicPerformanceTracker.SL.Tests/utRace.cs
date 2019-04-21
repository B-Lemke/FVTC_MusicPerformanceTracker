using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BAH.MusicPerformanceTracker.SL.Tests
{
    [TestClass]
    public class utRace
    {
        RaceList races;

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-apikey", "12345");
            client.BaseAddress = new Uri("http://localhost:59933/api/");
            return client;
        }

        [TestMethod]
        public void GetAll()
        {
            HttpClient client = InitializeClient();
            string result;
            dynamic items;
            HttpResponseMessage response;

            //Call the API
            response = client.GetAsync("Race").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Proces response
                result = response.Content.ReadAsStringAsync().Result;

                //Put json into the color list
                items = (JArray)JsonConvert.DeserializeObject(result);
                races = items.ToObject<RaceList>();
            }

            Assert.IsTrue(races.Count > 0);
        }



    }
}
