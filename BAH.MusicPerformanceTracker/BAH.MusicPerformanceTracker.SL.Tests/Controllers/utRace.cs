using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;

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
            client.BaseAddress = new Uri("http://musicperformancetracker.azurewebsites.net/api/");
            return client;
        }

        [TestMethod]
        public void GetAll()
        {
            //Setup
            HttpClient client = InitializeClient();
            string result;
            dynamic items;
            HttpResponseMessage response;

            //Act
            response = client.GetAsync("Race").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Proces response
                result = response.Content.ReadAsStringAsync().Result;

                //Put json into the race list
                items = (JArray)JsonConvert.DeserializeObject(result);
                races = items.ToObject<RaceList>();
            }

            //Assert
            Assert.IsTrue(races.Count > 0);
        }

        [TestMethod]
        public void GetOne()
        {
            //Setup
            Race race = new Race();
            Race retrievedRace = new Race();
            RaceList races = new RaceList();
            races.Load();
            race = races.FirstOrDefault(r => r.Description == "White");

            //Act
            if(race != null)
            {

                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.GetAsync("Race/" + race.Id).Result;

                string result = response.Content.ReadAsStringAsync().Result;

                retrievedRace = JsonConvert.DeserializeObject<Race>(result);
            }

            //Assert
            Assert.IsTrue(race.Description == retrievedRace.Description && !string.IsNullOrEmpty(retrievedRace.Description));
        }

        [TestMethod]
        public void Insert()
        {
            //Setup
            Race race = new Race
            {
                Description = "SLTEST"
            };
            RaceList races = new RaceList();
            races.Load();
            int originalCount = races.Count();



            //Act
            HttpClient client = InitializeClient();
            //Serialize a race object that we're trying to insert
            string serializedRace = JsonConvert.SerializeObject(race);
            var content = new StringContent(serializedRace);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("Race", content).Result;

            //Assert
            races.Clear();
            races.Load();
            Assert.AreEqual(originalCount + 1, races.Count);
        }

        [TestMethod]
        public void Update()
        {

            RaceList races = new RaceList();
            races.Load();
            Race race = races.FirstOrDefault(r => r.Description == "SLTEST");
            Race retrievedRace = new Race();
            if(race != null)
            {
                retrievedRace.Id = race.Id;

                race.Description = "SLTEST1";

                //Act
                HttpClient client = InitializeClient();
                //Serialize a question object that we're trying to insert
                string serializedRace = JsonConvert.SerializeObject(race);
                var content = new StringContent(serializedRace);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Race/" + race.Id, content).Result;

                retrievedRace.LoadById();
            }
            //Assert
            Assert.AreEqual(race.Description, retrievedRace.Description);
        }


        [TestMethod]
        public void Delete()
        {
            //Setup
            RaceList races = new RaceList();
            races.Load();
            int originalCount = races.Count();
            Race race = races.FirstOrDefault(r => r.Description == "SLTEST1");


            //Act
            if (race != null)
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Race/" + race.Id).Result;
            }

            //Assert
            races.Clear();
            races.Load();
            Assert.AreEqual(originalCount - 1, races.Count);
        }

    }
}
