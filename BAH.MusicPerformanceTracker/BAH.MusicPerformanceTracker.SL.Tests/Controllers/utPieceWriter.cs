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
    public class utPieceWriter
    {
        PieceWriterList pieceWriters;

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
            response = client.GetAsync("PieceWriter").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Proces response
                result = response.Content.ReadAsStringAsync().Result;

                //Put json into the pieceWriter list
                items = (JArray)JsonConvert.DeserializeObject(result);
                pieceWriters = items.ToObject<PieceWriterList>();
            }

            //Assert
            Assert.IsTrue(pieceWriters.Count > 0);
        }

        [TestMethod]
        public void GetOne()
        {
            //Setup
            PieceWriter pieceWriter = new PieceWriter();
            PieceWriter retrievedPieceWriter = new PieceWriter();
            PieceWriterList pieceWriters = new PieceWriterList();
            Piece pictures = new Piece();
            PieceList pl = new PieceList();
            pl.Load();
            pictures = pl.FirstOrDefault(p => p.Name == "Pictures At An Exhibition");

            pieceWriters.Load();

            pieceWriter = pieceWriters.FirstOrDefault(pw => pw.PieceId == pictures.Id);

            //Act
            if(pieceWriter != null)
            {

                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.GetAsync("PieceWriter/" + pieceWriter.Id).Result;

                string result = response.Content.ReadAsStringAsync().Result;

                retrievedPieceWriter = JsonConvert.DeserializeObject<PieceWriter>(result);
            }

            //Assert
            Assert.IsTrue(pieceWriter.ComposerId == retrievedPieceWriter.ComposerId && retrievedPieceWriter.ComposerId != Guid.Empty);
        }

        [TestMethod]
        public void Insert()
        {
            //Setup
            ComposerList cl = new ComposerList();
            Composer composer = new Composer();
            cl.Load();
            composer = cl.FirstOrDefault(c => c.FirstName == "Alex");

            PieceList pl = new PieceList();
            Piece piece = new Piece();
            pl.Load();
            piece = pl.FirstOrDefault(p => p.Name == "Pictures At An Exhibition");

            ComposerTypeList ctl = new ComposerTypeList();
            ComposerType composerType = new ComposerType();
            ctl.Load();
            composerType = ctl.FirstOrDefault(ct => ct.Description == "Composer");



            PieceWriter pieceWriter = new PieceWriter
            {
                ComposerId = composer.Id,
                ComposerTypeId = composerType.Id,
                PieceId = piece.Id
            };
            PieceWriterList pieceWriters = new PieceWriterList();
            pieceWriters.Load();
            int originalCount = pieceWriters.Count();



            //Act
            HttpClient client = InitializeClient();
            //Serialize a pieceWriter object that we're trying to insert
            string serializedPieceWriter = JsonConvert.SerializeObject(pieceWriter);
            var content = new StringContent(serializedPieceWriter);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("PieceWriter", content).Result;

            //Assert
            pieceWriters.Clear();
            pieceWriters.Load();
            Assert.AreEqual(originalCount + 1, pieceWriters.Count);
        }

        [TestMethod]
        public void Update()
        {
            //Setup
            ComposerList cl = new ComposerList();
            Composer composer = new Composer();
            cl.Load();
            composer = cl.FirstOrDefault(c => c.FirstName == "Alex");

            PieceList pl = new PieceList();
            Piece piece = new Piece();
            pl.Load();
            piece = pl.FirstOrDefault(p => p.Name == "Pictures At An Exhibition");

            ComposerTypeList ctl = new ComposerTypeList();
            ComposerType composerType = new ComposerType();
            ComposerType arranger = new ComposerType();
            ctl.Load();
            composerType = ctl.FirstOrDefault(ct => ct.Description == "Composer");
            arranger = ctl.FirstOrDefault(ct => ct.Description == "Arranger");

            PieceWriterList pieceWriters = new PieceWriterList();
            pieceWriters.Load();
            PieceWriter pieceWriter = pieceWriters.FirstOrDefault(pw => pw.ComposerId == composer.Id && pw.ComposerTypeId == composerType.Id && pw.PieceId == piece.Id);

            PieceWriter retrievedPieceWriter = new PieceWriter();
            if(pieceWriter != null)
            {
                retrievedPieceWriter.Id = pieceWriter.Id;

                pieceWriter.ComposerTypeId = arranger.Id;

                //Act
                HttpClient client = InitializeClient();
                //Serialize a question object that we're trying to insert
                string serializedPieceWriter = JsonConvert.SerializeObject(pieceWriter);
                var content = new StringContent(serializedPieceWriter);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("PieceWriter/" + pieceWriter.Id, content).Result;

                retrievedPieceWriter.LoadById();
            }
            //Assert
            Assert.AreEqual(pieceWriter.ComposerTypeId, retrievedPieceWriter.ComposerTypeId);
        }


        [TestMethod]
        public void Delete()
        {
            //Setup
            ComposerList cl = new ComposerList();
            Composer composer = new Composer();
            cl.Load();
            composer = cl.FirstOrDefault(c => c.FirstName == "Alex");

            PieceList pl = new PieceList();
            Piece piece = new Piece();
            pl.Load();
            piece = pl.FirstOrDefault(p => p.Name == "Pictures At An Exhibition");

            ComposerTypeList ctl = new ComposerTypeList();
            ComposerType arranger = new ComposerType();
            ctl.Load();
            arranger = ctl.FirstOrDefault(ct => ct.Description == "Arranger");

            PieceWriterList pieceWriters = new PieceWriterList();
            pieceWriters.Load();
            int originalCount = pieceWriters.Count();

            PieceWriter pieceWriter = pieceWriters.FirstOrDefault(pw => pw.ComposerId == composer.Id && pw.ComposerTypeId == arranger.Id && pw.PieceId == piece.Id);

            //Act
            if (pieceWriter != null)
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("PieceWriter/" + pieceWriter.Id).Result;
            }

            //Assert
            pieceWriters.Clear();
            pieceWriters.Load();
            Assert.AreEqual(originalCount - 1, pieceWriters.Count);
        }

    }
}
