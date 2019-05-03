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
    public class utDirector
    {
        DirectorList directors;

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
            response = client.GetAsync("Director").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Proces response
                result = response.Content.ReadAsStringAsync().Result;

                //Put json into the director list
                items = (JArray)JsonConvert.DeserializeObject(result);
                directors = items.ToObject<DirectorList>();
            }

            //Assert
            Assert.IsTrue(directors.Count > 0);
        }

        [TestMethod]
        public void GetOne()
        {
            //Setup
            Director director = new Director();
            Director retrievedDirector = new Director();
            DirectorList directors = new DirectorList();
            directors.Load();
            director = directors.FirstOrDefault(c => c.FirstName == "Broderick");

            //Act
            if (director != null)
            {

                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.GetAsync("Director/" + director.Id).Result;

                string result = response.Content.ReadAsStringAsync().Result;

                retrievedDirector = JsonConvert.DeserializeObject<Director>(result);
            }

            //Assert
            Assert.IsTrue(director.FirstName == retrievedDirector.FirstName && !string.IsNullOrEmpty(retrievedDirector.FirstName));
        }

        [TestMethod]
        public void Insert()
        {
            //Setup
            Director director = new Director
            {
                FirstName = "SLTEST",
                LastName = "SLTEST",
                Bio = "SLTEST",
                BirthDate = Convert.ToDateTime("3000-01-01")
            };
            DirectorList directors = new DirectorList();
            directors.Load();
            int originalCount = directors.Count();



            //Act
            HttpClient client = InitializeClient();
            //Serialize a director object that we're trying to insert
            string serializedDirector = JsonConvert.SerializeObject(director);
            var content = new StringContent(serializedDirector);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("Director", content).Result;

            //Assert
            directors.Clear();
            directors.Load();
            Assert.AreEqual(originalCount + 1, directors.Count);
        }

        [TestMethod]
        public void Update()
        {

            DirectorList directors = new DirectorList();
            directors.Load();
            Director director = directors.FirstOrDefault(c => c.FirstName == "SLTEST");
            Director retrievedDirector = new Director();
            if (director != null)
            {
                retrievedDirector.Id = director.Id;

                director.FirstName = "SLTEST1";

                //Act
                HttpClient client = InitializeClient();
                //Serialize a question object that we're trying to insert
                string serializedDirector = JsonConvert.SerializeObject(director);
                var content = new StringContent(serializedDirector);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Director/" + director.Id, content).Result;

                retrievedDirector.LoadById();
            }
            //Assert
            Assert.AreEqual(director.FirstName, retrievedDirector.FirstName);
        }


        [TestMethod]
        public void Delete()
        {
            //Setup
            DirectorList directors = new DirectorList();
            directors.Load();
            int originalCount = directors.Count();
            Director director = directors.FirstOrDefault(c => c.FirstName == "SLTEST1");


            //Act
            if (director != null)
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Director/" + director.Id).Result;
            }

            //Assert
            directors.Clear();
            directors.Load();
            Assert.AreEqual(originalCount - 1, directors.Count);
        }
    }
}
