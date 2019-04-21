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
    public class utComposerType
    {
        ComposerTypeList composerTypes;

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
            response = client.GetAsync("ComposerType").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Proces response
                result = response.Content.ReadAsStringAsync().Result;

                //Put json into the composerType list
                items = (JArray)JsonConvert.DeserializeObject(result);
                composerTypes = items.ToObject<ComposerTypeList>();
            }

            //Assert
            Assert.IsTrue(composerTypes.Count > 0);
        }

        [TestMethod]
        public void GetOne()
        {
            //Setup
            ComposerType composerType = new ComposerType();
            ComposerType retrievedComposerType = new ComposerType();
            ComposerTypeList composerTypes = new ComposerTypeList();
            composerTypes.Load();
            composerType = composerTypes.FirstOrDefault(ct => ct.Description == "Composer");

            //Act
            if(composerType != null)
            {

                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.GetAsync("ComposerType/" + composerType.Id).Result;

                string result = response.Content.ReadAsStringAsync().Result;

                retrievedComposerType = JsonConvert.DeserializeObject<ComposerType>(result);
            }

            //Assert
            Assert.IsTrue(composerType.Description == retrievedComposerType.Description && !string.IsNullOrEmpty(retrievedComposerType.Description));
        }

        [TestMethod]
        public void Insert()
        {
            //Setup
            ComposerType composerType = new ComposerType
            {
                Description = "SLTEST"
            };
            ComposerTypeList composerTypes = new ComposerTypeList();
            composerTypes.Load();
            int originalCount = composerTypes.Count();



            //Act
            HttpClient client = InitializeClient();
            //Serialize a composerType object that we're trying to insert
            string serializedComposerType = JsonConvert.SerializeObject(composerType);
            var content = new StringContent(serializedComposerType);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("ComposerType", content).Result;

            //Assert
            composerTypes.Clear();
            composerTypes.Load();
            Assert.AreEqual(originalCount + 1, composerTypes.Count);
        }

        [TestMethod]
        public void Update()
        {

            ComposerTypeList composerTypes = new ComposerTypeList();
            composerTypes.Load();
            ComposerType composerType = composerTypes.FirstOrDefault(ct => ct.Description == "SLTEST");
            ComposerType retrievedComposerType = new ComposerType();
            if(composerType != null)
            {
                retrievedComposerType.Id = composerType.Id;

                composerType.Description = "SLTEST1";

                //Act
                HttpClient client = InitializeClient();
                //Serialize a question object that we're trying to insert
                string serializedComposerType = JsonConvert.SerializeObject(composerType);
                var content = new StringContent(serializedComposerType);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("ComposerType/" + composerType.Id, content).Result;

                retrievedComposerType.LoadById();
            }
            //Assert
            Assert.AreEqual(composerType.Description, retrievedComposerType.Description);
        }


        [TestMethod]
        public void Delete()
        {
            //Setup
            ComposerTypeList composerTypes = new ComposerTypeList();
            composerTypes.Load();
            int originalCount = composerTypes.Count();
            ComposerType composerType = composerTypes.FirstOrDefault(ct => ct.Description == "SLTEST1");


            //Act
            if (composerType != null)
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("ComposerType/" + composerType.Id).Result;
            }

            //Assert
            composerTypes.Clear();
            composerTypes.Load();
            Assert.AreEqual(originalCount - 1, composerTypes.Count);
        }

    }
}
