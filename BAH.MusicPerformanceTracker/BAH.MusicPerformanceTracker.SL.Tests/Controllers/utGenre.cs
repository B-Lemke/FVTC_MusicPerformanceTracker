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
    public class utGenre
    {
        GenreList genres;

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
            response = client.GetAsync("Genre").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Proces response
                result = response.Content.ReadAsStringAsync().Result;

                //Put json into the genre list
                items = (JArray)JsonConvert.DeserializeObject(result);
                genres = items.ToObject<GenreList>();
            }

            //Assert
            Assert.IsTrue(genres.Count > 0);
        }

        [TestMethod]
        public void GetOne()
        {
            //Setup
            Genre genre = new Genre();
            Genre retrievedGenre = new Genre();
            GenreList genres = new GenreList();
            genres.Load();
            genre = genres.FirstOrDefault(g => g.Description == "Dance");

            //Act
            if(genre != null)
            {

                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.GetAsync("Genre/" + genre.Id).Result;

                string result = response.Content.ReadAsStringAsync().Result;

                retrievedGenre = JsonConvert.DeserializeObject<Genre>(result);
            }

            //Assert
            Assert.IsTrue(genre.Description == retrievedGenre.Description && !string.IsNullOrEmpty(retrievedGenre.Description));
        }

        [TestMethod]
        public void Insert()
        {
            //Setup
            Genre genre = new Genre
            {
                Description = "SLTEST"
            };
            GenreList genres = new GenreList();
            genres.Load();
            int originalCount = genres.Count();



            //Act
            HttpClient client = InitializeClient();
            //Serialize a genre object that we're trying to insert
            string serializedGenre = JsonConvert.SerializeObject(genre);
            var content = new StringContent(serializedGenre);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("Genre", content).Result;

            //Assert
            genres.Clear();
            genres.Load();
            Assert.AreEqual(originalCount + 1, genres.Count);
        }

        [TestMethod]
        public void Update()
        {

            GenreList genres = new GenreList();
            genres.Load();
            Genre genre = genres.FirstOrDefault(g => g.Description == "SLTEST");
            Genre retrievedGenre = new Genre();
            if(genre != null)
            {
                retrievedGenre.Id = genre.Id;

                genre.Description = "SLTEST1";

                //Act
                HttpClient client = InitializeClient();
                //Serialize a question object that we're trying to insert
                string serializedGenre = JsonConvert.SerializeObject(genre);
                var content = new StringContent(serializedGenre);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Genre/" + genre.Id, content).Result;

                retrievedGenre.LoadById();
            }
            //Assert
            Assert.AreEqual(genre.Description, retrievedGenre.Description);
        }


        [TestMethod]
        public void Delete()
        {
            //Setup
            GenreList genres = new GenreList();
            genres.Load();
            int originalCount = genres.Count();
            Genre genre = genres.FirstOrDefault(g => g.Description == "SLTEST1");


            //Act
            if (genre != null)
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Genre/" + genre.Id).Result;
            }

            //Assert
            genres.Clear();
            genres.Load();
            Assert.AreEqual(originalCount - 1, genres.Count);
        }

    }
}
