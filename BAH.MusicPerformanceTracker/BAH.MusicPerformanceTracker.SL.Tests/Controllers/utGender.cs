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
    public class utGender
    {
        GenderList genders;

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
            response = client.GetAsync("Gender").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Proces response
                result = response.Content.ReadAsStringAsync().Result;

                //Put json into the gender list
                items = (JArray)JsonConvert.DeserializeObject(result);
                genders = items.ToObject<GenderList>();
            }

            //Assert
            Assert.IsTrue(genders.Count > 0);
        }

        [TestMethod]
        public void GetOne()
        {
            //Setup
            Gender gender = new Gender();
            Gender retrievedGender = new Gender();
            GenderList genders = new GenderList();
            genders.Load();
            gender = genders.FirstOrDefault(g => g.Description == "Male");

            //Act
            if(gender != null)
            {

                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.GetAsync("Gender/" + gender.Id).Result;

                string result = response.Content.ReadAsStringAsync().Result;

                retrievedGender = JsonConvert.DeserializeObject<Gender>(result);
            }

            //Assert
            Assert.IsTrue(gender.Description == retrievedGender.Description && !string.IsNullOrEmpty(retrievedGender.Description));
        }

        [TestMethod]
        public void Insert()
        {
            //Setup
            Gender gender = new Gender
            {
                Description = "SLTEST"
            };
            GenderList genders = new GenderList();
            genders.Load();
            int originalCount = genders.Count();



            //Act
            HttpClient client = InitializeClient();
            //Serialize a gender object that we're trying to insert
            string serializedGender = JsonConvert.SerializeObject(gender);
            var content = new StringContent(serializedGender);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("Gender", content).Result;

            //Assert
            genders.Clear();
            genders.Load();
            Assert.AreEqual(originalCount + 1, genders.Count);
        }

        [TestMethod]
        public void Update()
        {

            GenderList genders = new GenderList();
            genders.Load();
            Gender gender = genders.FirstOrDefault(g => g.Description == "SLTEST");
            Gender retrievedGender = new Gender();
            if(gender != null)
            {
                retrievedGender.Id = gender.Id;

                gender.Description = "SLTEST1";

                //Act
                HttpClient client = InitializeClient();
                //Serialize a question object that we're trying to insert
                string serializedGender = JsonConvert.SerializeObject(gender);
                var content = new StringContent(serializedGender);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Gender/" + gender.Id, content).Result;

                retrievedGender.LoadById();
            }
            //Assert
            Assert.AreEqual(gender.Description, retrievedGender.Description);
        }


        [TestMethod]
        public void Delete()
        {
            //Setup
            GenderList genders = new GenderList();
            genders.Load();
            int originalCount = genders.Count();
            Gender gender = genders.FirstOrDefault(g => g.Description == "SLTEST1");


            //Act
            if (gender != null)
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Gender/" + gender.Id).Result;
            }

            //Assert
            genders.Clear();
            genders.Load();
            Assert.AreEqual(originalCount - 1, genders.Count);
        }

    }
}
