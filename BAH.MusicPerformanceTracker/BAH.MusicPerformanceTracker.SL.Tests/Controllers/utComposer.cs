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
    public class utComposer
    {
        ComposerList composers;

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
            response = client.GetAsync("Composer").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Proces response
                result = response.Content.ReadAsStringAsync().Result;

                //Put json into the composer list
                items = (JArray)JsonConvert.DeserializeObject(result);
                composers = items.ToObject<ComposerList>();
            }

            //Assert
            Assert.IsTrue(composers.Count > 0);
        }

        [TestMethod]
        public void GetOne()
        {
            //Setup
            Composer composer = new Composer();
            Composer retrievedComposer = new Composer();
            ComposerList composers = new ComposerList();
            composers.Load();
            composer = composers.FirstOrDefault(c => c.FirstName == "Alex");

            //Act
            if(composer != null)
            {

                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.GetAsync("Composer/" + composer.Id).Result;

                string result = response.Content.ReadAsStringAsync().Result;

                retrievedComposer = JsonConvert.DeserializeObject<Composer>(result);
            }

            //Assert
            Assert.IsTrue(composer.FirstName == retrievedComposer.FirstName && !string.IsNullOrEmpty(retrievedComposer.FirstName));
        }

        [TestMethod]
        public void Insert()
        {
            //Setup
            Composer composer = new Composer
            {
                FirstName = "SLTEST",
                LastName = "SLTEST",
                Bio = "SLTEST"
            };
            ComposerList composers = new ComposerList();
            composers.Load();
            int originalCount = composers.Count();



            //Act
            HttpClient client = InitializeClient();
            //Serialize a composer object that we're trying to insert
            string serializedComposer = JsonConvert.SerializeObject(composer);
            var content = new StringContent(serializedComposer);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("Composer", content).Result;

            //Assert
            composers.Clear();
            composers.Load();
            Assert.AreEqual(originalCount + 1, composers.Count);
        }

        [TestMethod]
        public void Update()
        {

            ComposerList composers = new ComposerList();
            composers.Load();
            Composer composer = composers.FirstOrDefault(c => c.FirstName == "SLTEST");
            Composer retrievedComposer = new Composer();
            if(composer != null)
            {
                retrievedComposer.Id = composer.Id;

                composer.FirstName = "SLTEST1";

                //Act
                HttpClient client = InitializeClient();
                //Serialize a question object that we're trying to insert
                string serializedComposer = JsonConvert.SerializeObject(composer);
                var content = new StringContent(serializedComposer);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Composer/" + composer.Id, content).Result;

                retrievedComposer.LoadById();
            }
            //Assert
            Assert.AreEqual(composer.FirstName, retrievedComposer.FirstName);
        }


        [TestMethod]
        public void Delete()
        {
            //Setup
            ComposerList composers = new ComposerList();
            composers.Load();
            int originalCount = composers.Count();
            Composer composer = composers.FirstOrDefault(c => c.FirstName == "SLTEST1");


            //Act
            if (composer != null)
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Composer/" + composer.Id).Result;
            }

            //Assert
            composers.Clear();
            composers.Load();
            Assert.AreEqual(originalCount - 1, composers.Count);
        }

    }
}
