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
    public class utPiece
    {
        PieceList pieces;

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
            response = client.GetAsync("Piece").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Proces response
                result = response.Content.ReadAsStringAsync().Result;

                //Put json into the piece list
                items = (JArray)JsonConvert.DeserializeObject(result);
                pieces = items.ToObject<PieceList>();
            }

            //Assert
            Assert.IsTrue(pieces.Count > 0);
        }

        [TestMethod]
        public void GetOne()
        {
            //Setup
            Piece piece = new Piece();
            Piece retrievedPiece = new Piece();
            PieceList pieces = new PieceList();
            pieces.Load();
            piece = pieces.FirstOrDefault(p => p.Name == "Lincolnshire Posy");

            //Act
            if(piece != null)
            {

                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.GetAsync("Piece/" + piece.Id).Result;

                string result = response.Content.ReadAsStringAsync().Result;

                retrievedPiece = JsonConvert.DeserializeObject<Piece>(result);
            }

            //Assert
            Assert.IsTrue(piece.Name == retrievedPiece.Name && !string.IsNullOrEmpty(retrievedPiece.Name));
        }

        [TestMethod]
        public void Insert()
        {
            //Setup
            Piece piece = new Piece
            {
                Name = "SLTEST",
                GradeLevel = "3",
                PerformanceNotes = "SLTEST",
                YearWritten = 4012
            };
            PieceList pieces = new PieceList();
            pieces.Load();
            int originalCount = pieces.Count();



            //Act
            HttpClient client = InitializeClient();
            //Serialize a piece object that we're trying to insert
            string serializedPiece = JsonConvert.SerializeObject(piece);
            var content = new StringContent(serializedPiece);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("Piece", content).Result;

            //Assert
            pieces.Clear();
            pieces.Load();
            Assert.AreEqual(originalCount + 1, pieces.Count);
        }

        [TestMethod]
        public void Update()
        {

            PieceList pieces = new PieceList();
            pieces.Load();
            Piece piece = pieces.FirstOrDefault(p => p.Name == "SLTEST");
            Piece retrievedPiece = new Piece();
            if(piece != null)
            {
                retrievedPiece.Id = piece.Id;

                piece.Name = "SLTEST1";

                //Act
                HttpClient client = InitializeClient();
                //Serialize a question object that we're trying to insert
                string serializedPiece = JsonConvert.SerializeObject(piece);
                var content = new StringContent(serializedPiece);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Piece/" + piece.Id, content).Result;

                retrievedPiece.LoadById();
            }
            //Assert
            Assert.AreEqual(piece.Name, retrievedPiece.Name);
        }


        [TestMethod]
        public void Delete()
        {
            //Setup
            PieceList pieces = new PieceList();
            pieces.Load();
            int originalCount = pieces.Count();
            Piece piece = pieces.FirstOrDefault(p => p.Name == "SLTEST1");


            //Act
            if (piece != null)
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Piece/" + piece.Id).Result;
            }

            //Assert
            pieces.Clear();
            pieces.Load();
            Assert.AreEqual(originalCount - 1, pieces.Count);
        }

    }
}
