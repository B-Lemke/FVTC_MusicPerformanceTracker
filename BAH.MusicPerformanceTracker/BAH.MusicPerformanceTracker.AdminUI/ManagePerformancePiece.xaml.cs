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
    /// Interaction logic for ManagePerformancePiece.xaml
    /// </summary>
    public partial class ManagePerformancePiece : Window
    {
        public string PerformanceName { get; set; }
        public Guid PerformanceId { get; set; }
        PieceList pieces;
        GroupList groups;
        DirectorList directors;
        PerformancePiece performancePiece = new PerformancePiece();

        public ManagePerformancePiece(Guid performanceId, string performanceName)
        {
            InitializeComponent();
            pieces = new PieceList();
            groups = new GroupList();
            directors = new DirectorList();

            PerformanceId = performanceId;
            PerformanceName = performanceName;

            Load();
            Rebind();

            lblPerformance.Content = PerformanceName;
        }

        public ManagePerformancePiece(Guid performanceId, string performanceName, PerformancePiece pp)
        {
            InitializeComponent();
            pieces = new PieceList();
            groups = new GroupList();
            directors = new DirectorList();

            PerformanceId = performanceId;
            PerformanceName = performanceName;

            Load();
            Rebind();

            lblPerformance.Content = PerformanceName;

            performancePiece = pp;
            cboDirector.SelectedValue = pp.DirectorId;
            cboGroup.SelectedValue = pp.GroupId;
            cboPiece.SelectedValue = pp.PieceId;
            txtNotes.Text = pp.Notes;

            btnSubmit.Content = "Update";
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-apikey", "12345");
            client.BaseAddress = new Uri("http://musicperformancetracker.azurewebsites.net/api/");
            return client;
        }


        private void Load()
        {
            try
            {
                HttpClient client = InitializeClient();

                string result;
                dynamic items;
                HttpResponseMessage response;

                //Call the API
                response = client.GetAsync("Piece").Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Process response
                    result = response.Content.ReadAsStringAsync().Result;

                    //Put json into a piece list
                    items = (JArray)JsonConvert.DeserializeObject(result);
                    pieces = items.ToObject<PieceList>();
                }
                else
                {
                    throw new Exception("Error: " + response.ReasonPhrase);
                }


                //Call the API
                response = client.GetAsync("Group").Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Process response
                    result = response.Content.ReadAsStringAsync().Result;

                    //Put json into a piece list
                    items = (JArray)JsonConvert.DeserializeObject(result);
                    groups = items.ToObject<GroupList>();
                }
                else
                {
                    throw new Exception("Error: " + response.ReasonPhrase);
                }
                                
                //Call the API
                response = client.GetAsync("Director").Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Process response
                    result = response.Content.ReadAsStringAsync().Result;

                    //Put json into a piece list
                    items = (JArray)JsonConvert.DeserializeObject(result);
                    directors = items.ToObject<DirectorList>();
                }
                else
                {
                    throw new Exception("Error: " + response.ReasonPhrase);
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


            cboGroup.ItemsSource = null;
            cboGroup.ItemsSource = groups;
            cboGroup.DisplayMemberPath = "Name";
            cboGroup.SelectedValuePath = "Id";

            cboDirector.ItemsSource = null;
            cboDirector.ItemsSource = directors;
            cboDirector.DisplayMemberPath = "FullName";
            cboDirector.SelectedValuePath = "Id";

        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(btnSubmit.Content.ToString() == "Submit")
                {
                    PerformancePiece performancePiece = new PerformancePiece();

                    if (cboPiece.SelectedItem != null)
                    {
                        performancePiece.DirectorId = directors[cboDirector.SelectedIndex].Id;
                    }

                    performancePiece.PieceId = pieces[cboPiece.SelectedIndex].Id;
                    performancePiece.GroupId = groups[cboGroup.SelectedIndex].Id;
                    performancePiece.PerformanceId = PerformanceId;
                    performancePiece.Notes = txtNotes.Text;

                    //Send it to the API
                    HttpClient client = InitializeClient();
                    string serializedPerformancePiece = JsonConvert.SerializeObject(performancePiece);
                    var content = new StringContent(serializedPerformancePiece);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PostAsync("PerformancePiece", content).Result;

                    this.Close();
                }
                else
                {
                    if (cboPiece.SelectedItem != null)
                    {
                        performancePiece.DirectorId = directors[cboDirector.SelectedIndex].Id;
                    }

                    performancePiece.PieceId = pieces[cboPiece.SelectedIndex].Id;
                    performancePiece.GroupId = groups[cboGroup.SelectedIndex].Id;
                    performancePiece.PerformanceId = PerformanceId;
                    performancePiece.Notes = txtNotes.Text;

                    //Send it to the API
                    HttpClient client = InitializeClient();
                    string serializedPerformancePiece = JsonConvert.SerializeObject(performancePiece);
                    var content = new StringContent(serializedPerformancePiece);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PutAsync("PerformancePiece/"+performancePiece.Id, content).Result;

                    this.Close();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            

        }
    }
}
