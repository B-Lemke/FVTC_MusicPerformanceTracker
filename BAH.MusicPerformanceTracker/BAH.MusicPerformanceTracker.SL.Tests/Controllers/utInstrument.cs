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
    public class utInstrument
    {
        InstrumentList instruments;

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
            response = client.GetAsync("Instrument").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Proces response
                result = response.Content.ReadAsStringAsync().Result;

                //Put json into the instrument list
                items = (JArray)JsonConvert.DeserializeObject(result);
                instruments = items.ToObject<InstrumentList>();
            }

            //Assert
            Assert.IsTrue(instruments.Count > 0);
        }

        [TestMethod]
        public void GetOne()
        {
            //Setup
            Instrument instrument = new Instrument();
            Instrument retrievedInstrument = new Instrument();
            InstrumentList instruments = new InstrumentList();
            instruments.Load();
            instrument = instruments.FirstOrDefault(ct => ct.Description == "Saxophone");

            //Act
            if(instrument != null)
            {

                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.GetAsync("Instrument/" + instrument.Id).Result;

                string result = response.Content.ReadAsStringAsync().Result;

                retrievedInstrument = JsonConvert.DeserializeObject<Instrument>(result);
            }

            //Assert
            Assert.IsTrue(instrument.Description == retrievedInstrument.Description && !string.IsNullOrEmpty(retrievedInstrument.Description));
        }

        [TestMethod]
        public void Insert()
        {
            //Setup
            Instrument instrument = new Instrument
            {
                Description = "SLTEST"
            };
            InstrumentList instruments = new InstrumentList();
            instruments.Load();
            int originalCount = instruments.Count();



            //Act
            HttpClient client = InitializeClient();
            //Serialize a instrument object that we're trying to insert
            string serializedInstrument = JsonConvert.SerializeObject(instrument);
            var content = new StringContent(serializedInstrument);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("Instrument", content).Result;

            //Assert
            instruments.Clear();
            instruments.Load();
            Assert.AreEqual(originalCount + 1, instruments.Count);
        }

        [TestMethod]
        public void Update()
        {

            InstrumentList instruments = new InstrumentList();
            instruments.Load();
            Instrument instrument = instruments.FirstOrDefault(ct => ct.Description == "SLTEST");
            Instrument retrievedInstrument = new Instrument();
            if(instrument != null)
            {
                retrievedInstrument.Id = instrument.Id;

                instrument.Description = "SLTEST1";

                //Act
                HttpClient client = InitializeClient();
                //Serialize a question object that we're trying to insert
                string serializedInstrument = JsonConvert.SerializeObject(instrument);
                var content = new StringContent(serializedInstrument);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Instrument/" + instrument.Id, content).Result;

                retrievedInstrument.LoadById();
            }
            //Assert
            Assert.AreEqual(instrument.Description, retrievedInstrument.Description);
        }


        [TestMethod]
        public void Delete()
        {
            //Setup
            InstrumentList instruments = new InstrumentList();
            instruments.Load();
            int originalCount = instruments.Count();
            Instrument instrument = instruments.FirstOrDefault(ct => ct.Description == "SLTEST1");


            //Act
            if (instrument != null)
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Instrument/" + instrument.Id).Result;
            }

            //Assert
            instruments.Clear();
            instruments.Load();
            Assert.AreEqual(originalCount - 1, instruments.Count);
        }

    }
}
