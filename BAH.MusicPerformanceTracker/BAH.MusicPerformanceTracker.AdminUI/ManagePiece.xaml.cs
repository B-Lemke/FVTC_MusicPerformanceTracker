using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BAH.MusicPerformanceTracker.BL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAH.MusicPerformanceTracker.AdminUI
{
    /// <summary>
    /// Interaction logic for ManagePiece.xaml
    /// </summary>
    public partial class ManagePiece : Window
    {
        PieceList pieces;
        GenreList genres;
        ComposerList composers;

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-apikey", "12345");
            client.BaseAddress = new Uri("http://musicperformancetracker.azurewebsites.net/api/");
            return client;
        }

        public ManagePiece()
        {
            InitializeComponent();

            pieces = new PieceList();

            Load();

            Rebind();
        }

        private void Load()
        {
            try
            {
                HttpClient client = InitializeClient();

                string result;
                dynamic items;
                HttpResponseMessage pieceResponse;

                //Call the API
                pieceResponse = client.GetAsync("Piece").Result;

                if(pieceResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Process response
                    result = pieceResponse.Content.ReadAsStringAsync().Result;

                    //Put json into a piece list
                    items = (JArray)JsonConvert.DeserializeObject(result);
                    pieces = items.ToObject<PieceList>();
                }
                else
                {
                    throw new Exception("Error: " + pieceResponse.ReasonPhrase);
                }


                string genreResult;
                dynamic genreItems;
                HttpResponseMessage genreResponse;

                //Call the API
                genreResponse = client.GetAsync("Genre").Result;

                if (genreResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Process response
                    genreResult = genreResponse.Content.ReadAsStringAsync().Result;

                    //Put json into a piece list
                    genreItems = (JArray)JsonConvert.DeserializeObject(genreResult);
                    genres = genreItems.ToObject<GenreList>();
                }
                else
                {
                    throw new Exception("Error: " + genreResponse.ReasonPhrase);
                }



                string composerResult;
                dynamic composerItems;
                HttpResponseMessage composerResponse;

                //Call the API
                composerResponse = client.GetAsync("Composer").Result;

                if (composerResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Process response
                    composerResult = composerResponse.Content.ReadAsStringAsync().Result;

                    //Put json into a piece list
                    composerItems = (JArray)JsonConvert.DeserializeObject(composerResult);
                    composers = composerItems.ToObject<ComposerList>();
                }
                else
                {
                    throw new Exception("Error: " + composerResponse.ReasonPhrase);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Rebind()
        {
            cboPiece.ItemsSource = null;
            cboPiece.ItemsSource = pieces;
            cboPiece.DisplayMemberPath = "Name";
            cboPiece.SelectedValuePath = "Id";


            lstGenre.ItemsSource = null;
            lstGenre.ItemsSource = genres;
            lstGenre.DisplayMemberPath = "Description";
            lstGenre.SelectedValuePath = "Id";

            lstComposer.ItemsSource = null;
            lstComposer.ItemsSource = composers;
            lstComposer.DisplayMemberPath = "FullName";
            lstComposer.SelectedValuePath = "Id";

            lstArranger.ItemsSource = null;
            lstArranger.ItemsSource = composers;
            lstArranger.DisplayMemberPath = "FullName";
            lstArranger.SelectedValuePath = "Id";
        }

        private void CboPiece_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Piece piece = pieces.ElementAt(cboPiece.SelectedIndex);
            //Change the boxes on screen based onthe selected object.
            txtGradeLevel.Text = piece.GradeLevel;
            txtName.Text = piece.Name;
            txtPerformanceNotes.Text = piece.PerformanceNotes;
            txtYearWritten.Text = piece.YearWritten.ToString();

            lstGenre.SelectedItems.Clear();
            foreach(Genre genre in piece.Genres)
            {
                Genre genreToAdd = genres.FirstOrDefault(g => g.Id == genre.Id);
                lstGenre.SelectedItems.Add(genreToAdd);
            }

        }
    }
}
