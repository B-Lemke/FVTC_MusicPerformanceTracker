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
    /// Interaction logic for ManageComposer.xaml
    /// </summary>
    public partial class ManageComposer : Window
    {
        ComposerList composers;
        LocationList locations;
        Location location;
        GenderList genders;
        Gender gender;
        RaceList races;
        Race race;

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-apikey", "12345");
            client.BaseAddress = new Uri("http://musicperformancetracker.azurewebsites.net/api/");
            return client;
        }

        private void Rebind()
        {
            cboComposer.ItemsSource = null;
            cboComposer.ItemsSource = composers;
            cboComposer.DisplayMemberPath = "FullName";
            cboComposer.SelectedValuePath = "Id";


            cboLocation.ItemsSource = null;
            cboLocation.ItemsSource = locations;
            cboLocation.DisplayMemberPath = "Description";
            cboLocation.SelectedValuePath = "Id";

            cboGender.ItemsSource = null;
            cboGender.ItemsSource = genders;
            cboGender.DisplayMemberPath = "Description";
            cboGender.SelectedValuePath = "Id";

            cboRace.ItemsSource = null;
            cboRace.ItemsSource = races;
            cboRace.DisplayMemberPath = "Description";
            cboRace.SelectedValuePath = "Id";
        }

        public ManageComposer()
        {
            InitializeComponent();
            composers = new ComposerList();
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
                HttpResponseMessage composerResponse;

                //Call the API
                composerResponse = client.GetAsync("Composer").Result;

                if (composerResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Process response
                    result = composerResponse.Content.ReadAsStringAsync().Result;

                    //Put json into a piece list
                    items = (JArray)JsonConvert.DeserializeObject(result);
                    composers = items.ToObject<ComposerList>();
                }
                else
                {
                    throw new Exception("Error: " + composerResponse.ReasonPhrase);
                }


                string locationResult;
                dynamic locationItems;
                HttpResponseMessage locationResponse;

                //Call the API
                locationResponse = client.GetAsync("Location").Result;

                if (locationResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Process response
                    locationResult = locationResponse.Content.ReadAsStringAsync().Result;

                    //Put json into a piece list
                    locationItems = (JArray)JsonConvert.DeserializeObject(locationResult);
                    locations = locationItems.ToObject<LocationList>();
                }
                else
                {
                    throw new Exception("Error: " + locationResponse.ReasonPhrase);
                }



                string genderResults;
                dynamic genderItems;
                HttpResponseMessage genderResponse;

                //Call the API
                genderResponse = client.GetAsync("Gender").Result;

                if (genderResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Process response
                    genderResults = genderResponse.Content.ReadAsStringAsync().Result;

                    //Put json into a piece list
                    genderItems = (JArray)JsonConvert.DeserializeObject(genderResults);
                    genders = genderItems.ToObject<GenderList>();
                }
                else
                {
                    throw new Exception("Error: " + genderResponse.ReasonPhrase);
                }


                string raceResult;
                dynamic raceItems;
                HttpResponseMessage raceResponse;

                //Call the API
                raceResponse = client.GetAsync("Race").Result;

                if (raceResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Process response
                    raceResult = raceResponse.Content.ReadAsStringAsync().Result;

                    //Put json into a piece list
                    raceItems = (JArray)JsonConvert.DeserializeObject(raceResult);
                    races = raceItems.ToObject<RaceList>();
                }
                else
                {
                    throw new Exception("Error: " + raceResponse.ReasonPhrase);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboComposer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboComposer.SelectedItem != null)
            {
                Composer composer = composers.ElementAt(cboComposer.SelectedIndex);

                // Change the boxes on screen based on selected object
                txtFirstName.Text = composer.FirstName;
                txtLastName.Text = composer.LastName;
                txtBio.Text = composer.Bio;

                cboLocation.SelectedItem = locations.FirstOrDefault(l => l.Id == composer.LocationId);

                cboGender.SelectedItem = genders.FirstOrDefault(g => g.Id == composer.GenderId);

                cboRace.SelectedItem = races.FirstOrDefault(r => r.Id == composer.RaceId);
            }

            btnSave.Content = "Save Composer";
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            // Clear out all the values on the screen
            cboComposer.SelectedItem = null;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtBio.Text = string.Empty;
            cboLocation.SelectedItem = null;
            cboGender.SelectedItem = null;
            cboRace.SelectedItem = null;

            btnSave.Content = "Add Composer";
        }

        private void btnDeleteComposer_Click(object sender, RoutedEventArgs e)
        {
            // Deletes the composer
            if (cboComposer.SelectedItem != null)
            {
                // Call the API if the selected item isn't null to delete it
                HttpClient client = InitializeClient();
                HttpResponseMessage response = new HttpResponseMessage();

                Composer composer = composers.ElementAt(cboComposer.SelectedIndex);

                response = client.DeleteAsync("Composer/" + composer.Id).Result;
                composers.Remove(composer);

                Rebind();

                // Clear text boxes
                btnNew_Click(sender, e);
            }
        }

        private void btnClearLocation_Click(object sender, RoutedEventArgs e)
        {
            cboLocation.SelectedItem = null;
        }

        private void btnClearGender_Click(object sender, RoutedEventArgs e)
        {
            cboGender.SelectedItem = null;
        }

        private void btnClearRace_Click(object sender, RoutedEventArgs e)
        {
            cboRace.SelectedItem = null;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((string)btnSave.Content == "Add Composer")
                {
                    // Add the composer
                    // Make sure that the fields are filled out that cannot be null
                    if (string.IsNullOrEmpty(txtFirstName.Text))
                    {
                        throw new Exception("Composer must have a first name");
                    }

                    if (string.IsNullOrEmpty(txtLastName.Text))
                    {
                        throw new Exception("Composer must have a last name");
                    }

                    // Create and set values on a composer
                    Composer composer = new Composer();

                    composer.FirstName = txtFirstName.Text;
                    composer.LastName = txtLastName.Text;

                    if (!string.IsNullOrEmpty(txtBio.Text))
                    {
                        composer.Bio = txtBio.Text;
                    }
                    else
                    {
                        composer.Bio = string.Empty;
                    }
                    
                    if (cboLocation.SelectedValue != null)
                    {
                        composer.LocationId = (Guid)cboLocation.SelectedValue;
                    }


                    if (cboGender.SelectedValue != null)
                    {
                        composer.GenderId = (Guid)cboGender.SelectedValue;
                    }


                    if (cboRace.SelectedValue != null)
                    {
                        composer.RaceId = (Guid)cboRace.SelectedValue;
                    }

                    // Send it to the api
                    HttpClient client = InitializeClient();
                    string serializedComposer = JsonConvert.SerializeObject(composer);
                    var content = new StringContent(serializedComposer);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PostAsync("Composer", content).Result;

                    if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        // get the new composer id
                        response = client.GetAsync("Composer?name=" + composer.LastName).Result;
                        string result = response.Content.ReadAsStringAsync().Result;
                        Composer retrievedComposer = JsonConvert.DeserializeObject<Composer>(result);

                        // Save the id so that we can update it
                        composer.Id = retrievedComposer.Id;

                        composers.Add(composer);
                        Rebind();

                        cboComposer.SelectedIndex = composers.FindIndex(c => c == composer);
                    }
                    else
                    {
                        throw new Exception("Composer could not be inserted");
                    }
                }
                else
                {
                    // Update the composer

                    // Make sure that the fields are filled out that cannot be null
                    if (string.IsNullOrEmpty(txtFirstName.Text))
                    {
                        throw new Exception("Composer must have a first name");
                    }

                    if (string.IsNullOrEmpty(txtLastName.Text))
                    {
                        throw new Exception("Composer must have a last name");
                    }

                    // Create and set values on a composer
                    Composer composer = new Composer();
                    composer = composers[cboComposer.SelectedIndex];

                    composer.FirstName = txtFirstName.Text;
                    composer.LastName = txtLastName.Text;

                    if (!string.IsNullOrEmpty(txtBio.Text))
                    {
                        composer.Bio = txtBio.Text;
                    }
                    else
                    {
                        composer.Bio = string.Empty;
                    }

                    if (cboLocation.SelectedValue != null)
                    {
                        composer.LocationId = (Guid)cboLocation.SelectedValue;
                    }


                    if (cboGender.SelectedValue != null)
                    {
                        composer.GenderId = (Guid)cboGender.SelectedValue;
                    }


                    if (cboRace.SelectedValue != null)
                    {
                        composer.RaceId = (Guid)cboRace.SelectedValue;
                    }


                    // Send it to the api
                    HttpClient client = InitializeClient();
                    string serializedComposer = JsonConvert.SerializeObject(composer);
                    var content = new StringContent(serializedComposer);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PutAsync("Composer/" + composer.Id, content).Result;

                    if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        //Save index, refresh screen, and reselect where we are.
                        var index = cboComposer.SelectedIndex;
                        Rebind();
                        cboComposer.SelectedIndex = index;


                        MessageBox.Show("Composer Saved.", "Success");
                    }
                    else
                    {
                        throw new Exception("Composer could not be updated");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }

        private void BtnManageLocations_Click(object sender, RoutedEventArgs e)
        {
            ManageSimple manageSimple = new ManageSimple(ScreenMode.Location);
            manageSimple.ShowDialog();
        }

        private void BtnManageGenders_Click(object sender, RoutedEventArgs e)
        {
            ManageSimple manageSimple = new ManageSimple(ScreenMode.Gender);
            manageSimple.ShowDialog();
        }

        private void BtnManageRaces_Click(object sender, RoutedEventArgs e)
        {
            ManageSimple manageSimple = new ManageSimple(ScreenMode.Race);
            manageSimple.ShowDialog();
        }
    }
}
