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
        ComposerTypeList composerTypes;

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

                if (pieceResponse.StatusCode == System.Net.HttpStatusCode.OK)
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


                string composerTypeResult;
                dynamic composerTypeItems;
                HttpResponseMessage composerTypeResponse;

                //Call the API
                composerTypeResponse = client.GetAsync("ComposerType").Result;

                if (composerTypeResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Process response
                    composerTypeResult = composerTypeResponse.Content.ReadAsStringAsync().Result;

                    //Put json into a piece list
                    composerTypeItems = (JArray)JsonConvert.DeserializeObject(composerTypeResult);
                    composerTypes = composerTypeItems.ToObject<ComposerTypeList>();
                }
                else
                {
                    throw new Exception("Error: " + composerTypeResponse.ReasonPhrase);
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
            if (cboPiece.SelectedItem != null)
            {
                Piece piece = pieces.ElementAt(cboPiece.SelectedIndex);
                //Change the boxes on screen based onthe selected object.
                txtGradeLevel.Text = piece.GradeLevel;
                txtName.Text = piece.Name;
                txtPerformanceNotes.Text = piece.PerformanceNotes;
                txtYearWritten.Text = piece.YearWritten > 0 ? piece.YearWritten.ToString() : String.Empty;

                lstGenre.SelectedItems.Clear();
                foreach (Genre genre in piece.Genres)
                {
                    Genre genreToAdd = genres.FirstOrDefault(g => g.Id == genre.Id);
                    lstGenre.SelectedItems.Add(genreToAdd);
                }

                lstComposer.SelectedItems.Clear();
                //Get the composerTypeId for composer
                Guid composerTypeGuid = composerTypes.FirstOrDefault(ct => ct.Description == "Composer").Id;
                foreach (PieceWriter pieceWriter in piece.PieceWriters)
                {
                    //If this piecewrite is a composer, select them
                    if (pieceWriter.ComposerTypeId == composerTypeGuid)
                    {
                        Composer composerToSelect = composers.FirstOrDefault(c => c.Id == pieceWriter.ComposerId);
                        lstComposer.SelectedItems.Add(composerToSelect);
                    }
                }

                lstArranger.SelectedItems.Clear();
                //Get the composerTypeId for composer
                Guid arrangerTypeId = composerTypes.FirstOrDefault(ct => ct.Description == "Arranger").Id;
                foreach (PieceWriter pieceWriter in piece.PieceWriters)
                {
                    //If this piecewrite is a composer, select them
                    if (pieceWriter.ComposerTypeId == arrangerTypeId)
                    {
                        Composer arrangerToSelect = composers.FirstOrDefault(c => c.Id == pieceWriter.ComposerId);
                        lstArranger.SelectedItems.Add(arrangerToSelect);
                    }
                }

                btnSave.Content = "Save Piece";
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            //Clear out all values on the screen
            cboPiece.SelectedItem = null;
            txtGradeLevel.Text = String.Empty;
            txtName.Text = String.Empty;
            txtPerformanceNotes.Text = String.Empty;
            txtYearWritten.Text = String.Empty;
            lstArranger.SelectedItems.Clear();
            lstComposer.SelectedItems.Clear();
            lstGenre.SelectedItems.Clear();

            btnSave.Content = "Add Piece";
        }

        private void BtnDeleteComposer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboPiece.SelectedItem != null)
                {
                    //Call the API if the selected item isn't null to delete it and its related PieceWriters and PieceGenres
                    HttpClient client = InitializeClient();
                    HttpResponseMessage response = new HttpResponseMessage();

                    Piece piece = pieces.ElementAt(cboPiece.SelectedIndex);

                    response = client.DeleteAsync("Piece/" + piece.Id).Result;
                    pieces.Remove(piece);

                    Rebind();
                }

                //Clear the text boxes
                BtnNew_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((string)btnSave.Content == "Add Piece")
                {
                    //Add the piece

                    //Make sure that the fields are filled out that cannot be null
                    if (string.IsNullOrEmpty(txtName.Text))
                    {
                        throw new Exception("Piece must have a name");
                    }

                    //Create and set values on a Piece
                    Piece piece = new Piece();

                    piece.Name = txtName.Text;

                    if (!string.IsNullOrEmpty(txtGradeLevel.Text))
                    {

                        piece.GradeLevel = txtGradeLevel.Text;
                    }
                    else
                    {
                        piece.GradeLevel = string.Empty;
                    }


                    if (!string.IsNullOrEmpty(txtPerformanceNotes.Text))
                    {

                        piece.PerformanceNotes = txtPerformanceNotes.Text;
                    }
                    else
                    {
                        piece.PerformanceNotes = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(txtYearWritten.Text))
                    {
                        int yearWritten;
                        int.TryParse(txtYearWritten.Text, out yearWritten);
                        piece.YearWritten = yearWritten;
                    }

                    //Send it to the API
                    HttpClient client = InitializeClient();
                    string serializedPiece = JsonConvert.SerializeObject(piece);
                    var content = new StringContent(serializedPiece);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PostAsync("Piece", content).Result;

                    if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {

                        //Get the new piece Id
                        response = client.GetAsync("Piece?name=" + piece.Name).Result;
                        string result = response.Content.ReadAsStringAsync().Result;
                        Piece retrievedPiece = JsonConvert.DeserializeObject<Piece>(result);

                        //Save the Id so that we can update it.
                        piece.Id = retrievedPiece.Id;

                        foreach (Composer composer in lstComposer.SelectedItems)
                        {
                            PieceWriter pieceWriter = new PieceWriter();
                            pieceWriter.ComposerId = composer.Id;
                            pieceWriter.PieceId = retrievedPiece.Id;
                            pieceWriter.ComposerTypeId = composerTypes.FirstOrDefault(ct => ct.Description == "Composer").Id;

                            //Add to the piece
                            piece.PieceWriters.Add(pieceWriter);

                            //Send it to the API
                            string serializedPieceWriter = JsonConvert.SerializeObject(pieceWriter);
                            content = new StringContent(serializedPieceWriter);
                            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                            response = client.PostAsync("PieceWriter", content).Result;
                        }

                        foreach (Composer arranger in lstArranger.SelectedItems)
                        {
                            PieceWriter pieceWriter = new PieceWriter();
                            pieceWriter.ComposerId = arranger.Id;
                            pieceWriter.PieceId = retrievedPiece.Id;
                            pieceWriter.ComposerTypeId = composerTypes.FirstOrDefault(ct => ct.Description == "Arranger").Id;

                            //Add to the piece
                            piece.PieceWriters.Add(pieceWriter);

                            //Send it to the API
                            string serializedPieceWriter = JsonConvert.SerializeObject(pieceWriter);
                            content = new StringContent(serializedPieceWriter);
                            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                            response = client.PostAsync("PieceWriter", content).Result;
                        }

                        foreach (Genre genre in lstGenre.SelectedItems)
                        {
                            PieceGenre pieceGenre = new PieceGenre
                            {
                                GenreId = genre.Id,
                                PieceId = retrievedPiece.Id
                            };

                            //Add to the piece
                            piece.Genres.Add(genre);

                            //Send it to the API
                            string serializedPieceGenre = JsonConvert.SerializeObject(pieceGenre);
                            content = new StringContent(serializedPieceGenre);
                            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                            response = client.PostAsync("PieceGenre", content).Result;
                        }

                        pieces.Add(piece);
                        Rebind();


                        //Select the inserted piece
                        cboPiece.SelectedIndex = pieces.FindIndex(p => p == piece);
                    }
                    else
                    {
                        throw new Exception("Piece could not be inserted");
                    }

                }
                else
                {
                    //Update the piece

                    //Make sure that the fields are filled out that cannot be null
                    if (string.IsNullOrEmpty(txtName.Text))
                    {
                        throw new Exception("Piece must have a name");
                    }

                    //Create and set values on a Piece
                    Piece piece = new Piece();

                    piece = pieces[cboPiece.SelectedIndex];

                    piece.Name = txtName.Text;

                    if (!string.IsNullOrEmpty(txtGradeLevel.Text))
                    {

                        piece.GradeLevel = txtGradeLevel.Text;
                    }
                    else
                    {
                        piece.GradeLevel = string.Empty;
                    }


                    if (!string.IsNullOrEmpty(txtPerformanceNotes.Text))
                    {

                        piece.PerformanceNotes = txtPerformanceNotes.Text;
                    }
                    else
                    {
                        piece.PerformanceNotes = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(txtYearWritten.Text))
                    {
                        int yearWritten;
                        int.TryParse(txtYearWritten.Text, out yearWritten);
                        piece.YearWritten = yearWritten;
                    }

                    //Send it to the API
                    HttpClient client = InitializeClient();
                    string serializedPiece = JsonConvert.SerializeObject(piece);
                    var content = new StringContent(serializedPiece);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PutAsync("Piece/" + piece.Id, content).Result;

                    if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        Guid composerTypeId = composerTypes.FirstOrDefault(ct => ct.Description == "Composer").Id;
                        List<Guid> oldComposers = new List<Guid>();
                        oldComposers = piece.PieceWriters.Where(pw => pw.ComposerTypeId == composerTypeId).Select(pw => pw.ComposerId).ToList();

                        List<Guid> selectedComposers = new List<Guid>();

                        //Add new composers
                        foreach (Composer composer in lstComposer.SelectedItems)
                        {
                            //If they are not in the old composers list, add them.
                            if (!oldComposers.Contains(composer.Id))
                            {

                                PieceWriter pieceWriter = new PieceWriter();
                                pieceWriter.ComposerId = composer.Id;
                                pieceWriter.PieceId = piece.Id;
                                pieceWriter.ComposerTypeId = composerTypeId;

                                //Add to the piece
                                piece.PieceWriters.Add(pieceWriter);

                                //Send it to the API
                                string serializedPieceWriter = JsonConvert.SerializeObject(pieceWriter);
                                content = new StringContent(serializedPieceWriter);
                                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                                response = client.PostAsync("PieceWriter", content).Result;
                            }

                            //Add to the selected composer list
                            selectedComposers.Add(composer.Id);
                        }

                        //Delete the composer peice writers that are no longer selected
                        foreach (Guid composerId in oldComposers.Except(selectedComposers))
                        {
                            PieceWriter pieceWriter = new PieceWriter();
                            pieceWriter = piece.PieceWriters.FirstOrDefault(pw => pw.ComposerId == composerId && pw.ComposerTypeId == composerTypeId);

                            response = client.DeleteAsync("PieceWriter/" + pieceWriter.Id).Result;

                            piece.PieceWriters.Remove(pieceWriter);
                        }


                        //Setup arrangers
                        Guid arrangerTypeId = composerTypes.FirstOrDefault(ct => ct.Description == "Arranger").Id;
                        List<Guid> oldArrangers = new List<Guid>();
                        oldArrangers = piece.PieceWriters.Where(pw => pw.ComposerTypeId == arrangerTypeId).Select(pw => pw.ComposerId).ToList();

                        List<Guid> selectedArrangers = new List<Guid>();

                        //Add new arrangers
                        foreach (Composer arranger in lstArranger.SelectedItems)
                        {
                            //If they are not in the old arranger list, add them.
                            if (!oldArrangers.Contains(arranger.Id))
                            {

                                PieceWriter pieceWriter = new PieceWriter();
                                pieceWriter.ComposerId = arranger.Id;
                                pieceWriter.PieceId = piece.Id;
                                pieceWriter.ComposerTypeId = arrangerTypeId;

                                //Add to the piece
                                piece.PieceWriters.Add(pieceWriter);

                                //Send it to the API
                                string serializedPieceWriter = JsonConvert.SerializeObject(pieceWriter);
                                content = new StringContent(serializedPieceWriter);
                                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                                response = client.PostAsync("PieceWriter", content).Result;
                            }

                            //Add to the selected arranger list
                            selectedArrangers.Add(arranger.Id);
                        }

                        //Delete the arranger peice writers that are no longer selected
                        foreach (Guid composerId in oldArrangers.Except(selectedArrangers))
                        {
                            PieceWriter pieceWriter = new PieceWriter();
                            pieceWriter = piece.PieceWriters.FirstOrDefault(pw => pw.ComposerId == composerId && pw.ComposerTypeId == arrangerTypeId);

                            response = client.DeleteAsync("PieceWriter/" + pieceWriter.Id).Result;

                            piece.PieceWriters.Remove(pieceWriter);
                        }



                        //Setup genres
                        List<Guid> oldGenres = new List<Guid>();
                        oldGenres = piece.Genres.Select(g => g.Id).ToList();

                        List<Guid> selectedGenres = new List<Guid>();

                        //Add new arrangers
                        foreach (Genre genre in lstGenre.SelectedItems)
                        {
                            //If they are not in the old genre list, add them.
                            if (!oldGenres.Contains(genre.Id))
                            {

                                PieceGenre pieceGenre = new PieceGenre();
                                pieceGenre.PieceId = piece.Id;
                                pieceGenre.GenreId = genre.Id;

                                //Send it to the API
                                string serializedPieceGenre = JsonConvert.SerializeObject(pieceGenre);
                                content = new StringContent(serializedPieceGenre);
                                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                                response = client.PostAsync("PieceGenre", content).Result;

                                piece.Genres.Add(genre);
                            }

                            //Add to the selected gener list
                            selectedGenres.Add(genre.Id);
                        }

                        //Delete the genres that are no longer selected
                        foreach (Guid genreId in oldGenres.Except(selectedGenres))
                        {
                            response = client.DeleteAsync("PieceGenre?pieceId=" + piece.Id + "&genreId=" + genreId).Result;

                            Genre genreToRemove = piece.Genres.FirstOrDefault(g => g.Id == genreId);
                            piece.Genres.Remove(genreToRemove);
                        }


                        //Save index, refresh screen, and reselect where we are.
                        var index = cboPiece.SelectedIndex;
                        Rebind();
                        cboPiece.SelectedIndex = index;
                    }
                    else
                    {
                        throw new Exception("Piece could not be updated");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}