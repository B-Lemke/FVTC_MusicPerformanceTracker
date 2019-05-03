using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAH.MusicPerformanceTracker.BL;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;

namespace BAH.MusicPerformanceTracker.SL.Tests.Controllers
{
    [TestClass]
    public class utPerformer
    {
        PerformerList performers;

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
            response = client.GetAsync("Performer").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Proces response
                result = response.Content.ReadAsStringAsync().Result;

                //Put json into the performer list
                items = (JArray)JsonConvert.DeserializeObject(result);
                performers = items.ToObject<PerformerList>();
            }

            //Assert
            Assert.IsTrue(performers.Count > 0);
        }

        [TestMethod]
        public void GetOne()
        {
            //Setup
            Performer performer = new Performer();
            Performer retrievedPerformer = new Performer();
            PerformerList performers = new PerformerList();
            performers.Load();
            performer = performers.FirstOrDefault(c => c.FirstName == "Hunter");

            //Act
            if (performer != null)
            {

                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.GetAsync("Performer/" + performer.Id).Result;

                string result = response.Content.ReadAsStringAsync().Result;

                retrievedPerformer = JsonConvert.DeserializeObject<Performer>(result);
            }

            //Assert
            Assert.IsTrue(performer.FirstName == retrievedPerformer.FirstName && !string.IsNullOrEmpty(retrievedPerformer.FirstName));
        }

        [TestMethod]
        public void Insert()
        {
            //Setup
            Performer performer = new Performer
            {
                FirstName = "SLTEST",
                LastName = "SLTEST"
            };
            PerformerList performers = new PerformerList();
            performers.Load();
            int originalCount = performers.Count();



            //Act
            HttpClient client = InitializeClient();
            //Serialize a performer object that we're trying to insert
            string serializedPerformer = JsonConvert.SerializeObject(performer);
            var content = new StringContent(serializedPerformer);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("Performer", content).Result;

            //Assert
            performers.Clear();
            performers.Load();
            Assert.AreEqual(originalCount + 1, performers.Count);
        }

        [TestMethod]
        public void Update()
        {

            PerformerList performers = new PerformerList();
            performers.Load();
            Performer performer = performers.FirstOrDefault(c => c.FirstName == "SLTEST");
            Performer retrievedPerformer = new Performer();
            if (performer != null)
            {
                retrievedPerformer.Id = performer.Id;

                performer.FirstName = "SLTEST1";

                //Act
                HttpClient client = InitializeClient();
                //Serialize a question object that we're trying to insert
                string serializedPerformer = JsonConvert.SerializeObject(performer);
                var content = new StringContent(serializedPerformer);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Performer/" + performer.Id, content).Result;

                retrievedPerformer.LoadById();
            }
            //Assert
            Assert.AreEqual(performer.FirstName, retrievedPerformer.FirstName);
        }


        [TestMethod]
        public void Delete()
        {
            //Setup
            PerformerList performers = new PerformerList();
            performers.Load();
            int originalCount = performers.Count();
            Performer performer = performers.FirstOrDefault(c => c.FirstName == "SLTEST1");


            //Act
            if (performer != null)
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Performer/" + performer.Id).Result;
            }

            //Assert
            performers.Clear();
            performers.Load();
            Assert.AreEqual(originalCount - 1, performers.Count);
        }
    }
}
