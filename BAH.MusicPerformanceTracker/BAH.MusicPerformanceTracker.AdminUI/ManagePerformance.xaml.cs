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
    /// Interaction logic for ManagePerformance.xaml
    /// </summary>
    public partial class ManagePerformance : Window
    {
        PerformanceList performances;


        public ManagePerformance()
        {
            InitializeComponent();

            performances = new PerformanceList();

            Load();

            Rebind();
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
                HttpResponseMessage performanceResponse;

                //Call the API
                performanceResponse = client.GetAsync("Performance").Result;

                if (performanceResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Process response
                    result = performanceResponse.Content.ReadAsStringAsync().Result;

                    //Put json into a performance list
                    items = (JArray)JsonConvert.DeserializeObject(result);
                    performances = items.ToObject<PerformanceList>();
                }
                else
                {
                    throw new Exception("Error: " + performanceResponse.ReasonPhrase);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }



        private void Rebind()
        {
            cboPerformance.ItemsSource = null;
            cboPerformance.ItemsSource = performances;
            cboPerformance.DisplayMemberPath = "Name";
            cboPerformance.SelectedValuePath = "Id";

        }

        private void CboPerformance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboPerformance.SelectedItem != null)
            {
                Performance performance = performances.ElementAt(cboPerformance.SelectedIndex);
                //Change the boxes on screen based onthe selected object.
                txtDescription.Text = performance.Description;
                txtLocation.Text = performance.Location;
                txtName.Text = performance.Name;
                dtpDate.SelectedDate = performance.PerformanceDate;

                dgvPerformancePieces.ItemsSource = null;
                dgvPerformancePieces.ItemsSource = performance.PerfromancePieces;

                dgvPerformancePieces.Columns[0].Visibility = Visibility.Hidden;
                dgvPerformancePieces.Columns[1].Visibility = Visibility.Hidden;
                dgvPerformancePieces.Columns[3].Visibility = Visibility.Hidden;
                dgvPerformancePieces.Columns[5].Visibility = Visibility.Hidden;
                dgvPerformancePieces.Columns[7].Visibility = Visibility.Hidden;




                btnSavePerformance.Content = "Save Performance";






            }
        }

        private void BtnNewPerformance_Click(object sender, RoutedEventArgs e)
        {
            //Clear out all values on the screen
            cboPerformance.SelectedItem = null;
            txtName.Text = String.Empty;
            txtDescription.Text = String.Empty;
            txtLocation.Text = String.Empty;
            dtpDate.SelectedDate = null;

            btnSavePerformance.Content = "Add Performance";
        }

        private void BtnDeletePerformance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboPerformance.SelectedItem != null)
                {
                    //Call the API if the selected item isn't null to delete it
                    HttpClient client = InitializeClient();
                    HttpResponseMessage response = new HttpResponseMessage();

                    Performance performance = performances.ElementAt(cboPerformance.SelectedIndex);

                    response = client.DeleteAsync("Performance/" + performance.Id).Result;
                    performances.Remove(performance);

                    Rebind();
                }

                //Clear the text boxes
                BtnNewPerformance_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }

        private void BtnSavePerformance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((string)btnSavePerformance.Content == "Add Performance")
                {
                    //Add the performance

                    //Make sure that the fields are filled out that cannot be null
                    if (string.IsNullOrEmpty(txtName.Text))
                    {
                        throw new Exception("Performance must have a name");
                    }

                    //Create and set values on a Performance
                    Performance performance = new Performance();

                    if(performances.Any(p => p.Name == txtName.Text))
                    {
                        throw new Exception("This name has already been used.");
                    }
                    else
                    {
                        performance.Name = txtName.Text;
                    }


                    if (!string.IsNullOrEmpty(txtDescription.Text))
                    {

                        performance.Description = txtDescription.Text;
                    }
                    else
                    {
                        performance.Description = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(txtLocation.Text))
                    {

                        performance.Location = txtLocation.Text;
                    }
                    else
                    {
                        performance.Description = string.Empty;
                    }

                    if (dtpDate.SelectedDate.HasValue)
                    {
                        performance.PerformanceDate = dtpDate.SelectedDate.Value;
                    }

                    //Send it to the API
                    HttpClient client = InitializeClient();
                    string serializedPerformance = JsonConvert.SerializeObject(performance);
                    var content = new StringContent(serializedPerformance);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PostAsync("Performance", content).Result;

                    if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        //TO DO IN THE FUTURE. RETURN THE ID IN THE RESPONSE FROM THE ONE BEFORE. BUT WE CANNOT CURRENTLY DO THAT.

                        //Get the new performance Id
                        response = client.GetAsync("Performance?name=" + performance.Name).Result;
                        string result = response.Content.ReadAsStringAsync().Result;
                        Performance retrievedPerformance = JsonConvert.DeserializeObject<Performance>(result);

                        //Save the Id so that we can update it.
                        performance.Id = retrievedPerformance.Id;


                        performances.Add(performance);
                        Rebind();


                        //Select the inserted performance
                        cboPerformance.SelectedIndex = performances.FindIndex(p => p == performance);
                    }
                    else
                    {
                        throw new Exception("Performance could not be inserted");
                    }

                }
                else
                {
                    //Update the performance

                    //Make sure that the fields are filled out that cannot be null
                    if (string.IsNullOrEmpty(txtName.Text))
                    {
                        throw new Exception("Performance must have a name");
                    }

                    //Create and set values on a Performance
                    Performance performance = new Performance();

                    performance = performances[cboPerformance.SelectedIndex];

                    //Check for duplicate names. Allow for 1 which will be this item.
                    var performanceMatches = performances.Select(p => p.Name).Where(p => p == txtName.Text);
                    int performanceMatchCount = performanceMatches.Count();
                    if (performanceMatchCount > 0)
                    {
                        if (performances[cboPerformance.SelectedIndex].Name == txtName.Text && performanceMatchCount == 1)
                        {
                            performance.Name = txtName.Text;
                        }
                        else
                        {
                            throw new Exception("This name has already been used.");
                        }
                    }
                    else
                    {
                        performance.Name = txtName.Text;
                    }

                    if (!string.IsNullOrEmpty(txtDescription.Text))
                    {

                        performance.Description = txtDescription.Text;
                    }
                    else
                    {
                        performance.Description = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(txtLocation.Text))
                    {

                        performance.Location = txtLocation.Text;
                    }
                    else
                    {
                        performance.Description = string.Empty;
                    }

                    if (dtpDate.SelectedDate.HasValue)
                    {
                        performance.PerformanceDate = dtpDate.SelectedDate.Value;
                    }

                    //Send it to the API
                    HttpClient client = InitializeClient();
                    string serializedPerformance = JsonConvert.SerializeObject(performance);
                    var content = new StringContent(serializedPerformance);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PutAsync("Performance/" + performance.Id, content).Result;

                    if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        //Save index, refresh screen, and reselect where we are.
                        var index = cboPerformance.SelectedIndex;
                        Rebind();
                        cboPerformance.SelectedIndex = index;
                    }
                    else
                    {
                        throw new Exception("Performance could not be updated");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }

        private void BtnAddPiece_Click(object sender, RoutedEventArgs e)
        {
            int index = cboPerformance.SelectedIndex;
            ManagePerformancePiece managePerformancePiece = new ManagePerformancePiece(performances[index].Id, performances[index].Name);
            managePerformancePiece.ShowDialog();

            //Get the new performance piece data
            Load();
            Rebind();
            cboPerformance.SelectedIndex = index;
        }

        private void BtnRemovePiece_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (cboPerformance.SelectedItem != null && dgvPerformancePieces.SelectedItem != null)
                {
                    int index = cboPerformance.SelectedIndex;
                    PerformancePiece pp = performances[index].PerfromancePieces[dgvPerformancePieces.SelectedIndex];

                    //Call the API if the selected item isn't null to delete it and its related PieceWriters and PieceGenres
                    HttpClient client = InitializeClient();
                    HttpResponseMessage response = new HttpResponseMessage();

                   
                    response = client.DeleteAsync("PerformancePiece/" + pp.Id).Result;
                    performances[index].PerfromancePieces.Remove(pp);

                    Rebind();
                    cboPerformance.SelectedIndex = index;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
