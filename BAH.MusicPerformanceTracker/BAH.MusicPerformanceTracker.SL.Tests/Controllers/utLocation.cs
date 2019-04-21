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
    public class utLocation
    {
        LocationList locations;

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
            response = client.GetAsync("Location").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Proces response
                result = response.Content.ReadAsStringAsync().Result;

                //Put json into the location list
                items = (JArray)JsonConvert.DeserializeObject(result);
                locations = items.ToObject<LocationList>();
            }

            //Assert
            Assert.IsTrue(locations.Count > 0);
        }

        [TestMethod]
        public void GetOne()
        {
            //Setup
            Location location = new Location();
            Location retrievedLocation = new Location();
            LocationList locations = new LocationList();
            locations.Load();
            location = locations.FirstOrDefault(l => l.Description == "Russia");

            //Act
            if(location != null)
            {

                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.GetAsync("Location/" + location.Id).Result;

                string result = response.Content.ReadAsStringAsync().Result;

                retrievedLocation = JsonConvert.DeserializeObject<Location>(result);
            }

            //Assert
            Assert.IsTrue(location.Description == retrievedLocation.Description && !string.IsNullOrEmpty(retrievedLocation.Description));
        }

        [TestMethod]
        public void Insert()
        {
            //Setup
            Location location = new Location
            {
                Description = "SLTEST"
            };
            LocationList locations = new LocationList();
            locations.Load();
            int originalCount = locations.Count();



            //Act
            HttpClient client = InitializeClient();
            //Serialize a location object that we're trying to insert
            string serializedLocation = JsonConvert.SerializeObject(location);
            var content = new StringContent(serializedLocation);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("Location", content).Result;

            //Assert
            locations.Clear();
            locations.Load();
            Assert.AreEqual(originalCount + 1, locations.Count);
        }

        [TestMethod]
        public void Update()
        {

            LocationList locations = new LocationList();
            locations.Load();
            Location location = locations.FirstOrDefault(l => l.Description == "SLTEST");
            Location retrievedLocation = new Location();
            if(location != null)
            {
                retrievedLocation.Id = location.Id;

                location.Description = "SLTEST1";

                //Act
                HttpClient client = InitializeClient();
                //Serialize a question object that we're trying to insert
                string serializedLocation = JsonConvert.SerializeObject(location);
                var content = new StringContent(serializedLocation);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Location/" + location.Id, content).Result;

                retrievedLocation.LoadById();
            }
            //Assert
            Assert.AreEqual(location.Description, retrievedLocation.Description);
        }


        [TestMethod]
        public void Delete()
        {
            //Setup
            LocationList locations = new LocationList();
            locations.Load();
            int originalCount = locations.Count();
            Location location = locations.FirstOrDefault(l => l.Description == "SLTEST1");


            //Act
            if (location != null)
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Location/" + location.Id).Result;
            }

            //Assert
            locations.Clear();
            locations.Load();
            Assert.AreEqual(originalCount - 1, locations.Count);
        }

    }
}
