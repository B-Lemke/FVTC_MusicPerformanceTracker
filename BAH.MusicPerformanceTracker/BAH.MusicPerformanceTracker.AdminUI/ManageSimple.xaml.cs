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

namespace BAH.MusicPerformanceTracker.AdminUI
{
    public enum ScreenMode
    {
        Location = 1,
        Gender = 2,
        Race = 3
    }

    /// <summary>
    /// Interaction logic for ManageSimple.xaml
    /// </summary>
    public partial class ManageSimple : Window
    {
        ScreenMode screenmode;
        LocationList locations;
        GenderList genders;
        RaceList races;

        public ManageSimple(ScreenMode screenMode)
        {
            InitializeComponent();

            screenmode = screenMode;

            Rebind();
            lblAttribute.Content = screenMode.ToString() + "s:";
            Title = "Manage " + screenMode.ToString() + "s";
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-apikey", "12345");
            client.BaseAddress = new Uri("http://musicperformancetracker.azurewebsites.net/api/");
            return client;
        }

        private void Rebind()
        {
            switch (screenmode)
            {
                case ScreenMode.Location:
                    locations = new LocationList();
                    locations.Load();

                    // Bind to the datagrid
                    cboAttribute.ItemsSource = null;
                    cboAttribute.ItemsSource = locations;
                    break;

                case ScreenMode.Gender:

                    genders = new GenderList();
                    genders.Load();
                    cboAttribute.ItemsSource = null;
                    cboAttribute.ItemsSource = genders;
                    break;

                case ScreenMode.Race:

                    races = new RaceList();
                    races.Load();
                    cboAttribute.ItemsSource = null;
                    cboAttribute.ItemsSource = races;
                    break;
            }

            cboAttribute.DisplayMemberPath = "Description";
            cboAttribute.SelectedValuePath = "Id";
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ScreenMode.Location == screenmode)
                {
                    if (string.IsNullOrEmpty(txtDescription.Text))
                    {
                        throw new Exception("Please enter a location");
                    }

                    Location location = new Location();

                    if (locations.Any(l => l.Description == txtDescription.Text))
                    {
                        throw new Exception("This location has already exists.");
                    }
                    else
                    {
                        location.Description = txtDescription.Text;
                    }

                    //Send it to the API
                    HttpClient client = InitializeClient();
                    string serializedLocation = JsonConvert.SerializeObject(location);
                    var content = new StringContent(serializedLocation);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PostAsync("Location", content).Result;

                    if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        //Get the new location Id
                        response = client.GetAsync("Location?description=" + location.Description).Result;
                        string result = response.Content.ReadAsStringAsync().Result;
                        Location retrievedLocation = JsonConvert.DeserializeObject<Location>(result);

                        //Save the Id so that we can update it.
                        location.Id = retrievedLocation.Id;


                        locations.Add(location);
                        Rebind();


                        //Select the inserted location
                        cboAttribute.SelectedIndex = locations.FindIndex(p => p == location); 
                    }
                    else
                    {
                        throw new Exception("Location could not be inserted");
                    }
                }
                else if (ScreenMode.Gender == screenmode)
                {
                    if (string.IsNullOrEmpty(txtDescription.Text))
                    {
                        throw new Exception("Please enter a gender");
                    }

                    Gender gender = new Gender();

                    if (genders.Any(l => l.Description == txtDescription.Text))
                    {
                        throw new Exception("This gender has already exists.");
                    }
                    else
                    {
                        gender.Description = txtDescription.Text;
                    }

                    //Send it to the API
                    HttpClient client = InitializeClient();
                    string serializedGender = JsonConvert.SerializeObject(gender);
                    var content = new StringContent(serializedGender);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PostAsync("Gender", content).Result;

                    if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        //Get the new location Id
                        response = client.GetAsync("Gender?description=" + gender.Description).Result;
                        string result = response.Content.ReadAsStringAsync().Result;
                        Gender retrievedGender = JsonConvert.DeserializeObject<Gender>(result);

                        //Save the Id so that we can update it.
                        gender.Id = retrievedGender.Id;


                        genders.Add(gender);
                        Rebind();


                        //Select the inserted location
                        cboAttribute.SelectedIndex = genders.FindIndex(p => p == gender);
                    }
                    else
                    {
                        throw new Exception("Gender could not be inserted");
                    }
                }
                else if (ScreenMode.Race == screenmode)
                {
                    if (string.IsNullOrEmpty(txtDescription.Text))
                    {
                        throw new Exception("Please enter a race");
                    }

                    Race race = new Race();

                    if (genders.Any(l => l.Description == txtDescription.Text))
                    {
                        throw new Exception("This race has already exists.");
                    }
                    else
                    {
                        race.Description = txtDescription.Text;
                    }

                    //Send it to the API
                    HttpClient client = InitializeClient();
                    string serializedRace = JsonConvert.SerializeObject(race);
                    var content = new StringContent(serializedRace);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PostAsync("Race", content).Result;

                    if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        //Get the new location Id
                        response = client.GetAsync("Race?description=" + race.Description).Result;
                        string result = response.Content.ReadAsStringAsync().Result;
                        Race retrievedRace = JsonConvert.DeserializeObject<Race>(result);

                        //Save the Id so that we can update it.
                        race.Id = retrievedRace.Id;


                        races.Add(race);
                        Rebind();


                        //Select the inserted location
                        cboAttribute.SelectedIndex = races.FindIndex(p => p == race);
                    }
                    else
                    {
                        throw new Exception("Race could not be inserted");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboAttribute.SelectedIndex > -1)
                {
                    if (ScreenMode.Location == screenmode)
                    {
                        if (string.IsNullOrEmpty(txtDescription.Text))
                        {
                            throw new Exception("Please enter a location");
                        }

                        Location location = new Location();
                        location = locations[cboAttribute.SelectedIndex];

                        var locationMatches = locations.Select(l => l.Description).Where(l => l == txtDescription.Text);
                        int locationMatchCount = locationMatches.Count();
                        if (locationMatchCount > 0)
                        {
                            if (locations[cboAttribute.SelectedIndex].Description == txtDescription.Text && locationMatchCount == 1)
                            {
                                location.Description = txtDescription.Text;
                            }
                            else
                            {
                                throw new Exception("This location has already exists.");
                            }
                        }
                        
                        //Send it to the API
                        HttpClient client = InitializeClient();
                        string serializedLocation = JsonConvert.SerializeObject(location);
                        var content = new StringContent(serializedLocation);
                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        HttpResponseMessage response = client.PutAsync("Location/" + location.Id, content).Result;

                        if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            // Save index, refresh screen, and reselect where we are.
                            var index = cboAttribute.SelectedIndex;
                            Rebind();
                            cboAttribute.SelectedIndex = index;
                        }
                        else
                        {
                            throw new Exception("Location could not be updated");
                        }
                    }
                    else if (ScreenMode.Gender == screenmode)
                    {
                        if (string.IsNullOrEmpty(txtDescription.Text))
                        {
                            throw new Exception("Please enter a gender");
                        }

                        Gender gender = new Gender();
                        gender = genders[cboAttribute.SelectedIndex];

                        var genderMatches = genders.Select(l => l.Description).Where(l => l == txtDescription.Text);
                        int genderMatchCount = genderMatches.Count();
                        if (genderMatchCount > 0)
                        {
                            if (genders[cboAttribute.SelectedIndex].Description == txtDescription.Text && genderMatchCount == 1)
                            {
                                gender.Description = txtDescription.Text;
                            }
                            else
                            {
                                throw new Exception("This gender has already exists.");
                            }
                        }

                        //Send it to the API
                        HttpClient client = InitializeClient();
                        string serializedGender = JsonConvert.SerializeObject(gender);
                        var content = new StringContent(serializedGender);
                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        HttpResponseMessage response = client.PutAsync("Gender/" + gender.Id, content).Result;

                        if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            // Save index, refresh screen, and reselect where we are.
                            var index = cboAttribute.SelectedIndex;
                            Rebind();
                            cboAttribute.SelectedIndex = index;
                        }
                        else
                        {
                            throw new Exception("Gender could not be updated");
                        }
                    }
                    else if (ScreenMode.Race == screenmode)
                    {
                        if (string.IsNullOrEmpty(txtDescription.Text))
                        {
                            throw new Exception("Please enter a race");
                        }

                        Race race = new Race();
                        race = races[cboAttribute.SelectedIndex];

                        var raceMatches = races.Select(l => l.Description).Where(l => l == txtDescription.Text);
                        int raceMatchCount = raceMatches.Count();
                        if (raceMatchCount > 0)
                        {
                            if (races[cboAttribute.SelectedIndex].Description == txtDescription.Text && raceMatchCount == 1)
                            {
                                race.Description = txtDescription.Text;
                            }
                            else
                            {
                                throw new Exception("This race has already exists.");
                            }
                        }

                        //Send it to the API
                        HttpClient client = InitializeClient();
                        string serializedRace = JsonConvert.SerializeObject(race);
                        var content = new StringContent(serializedRace);
                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        HttpResponseMessage response = client.PutAsync("Race/" + race.Id, content).Result;

                        if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            // Save index, refresh screen, and reselect where we are.
                            var index = cboAttribute.SelectedIndex;
                            Rebind();
                            cboAttribute.SelectedIndex = index;
                        }
                        else
                        {
                            throw new Exception("Race could not be updated");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboAttribute.SelectedIndex > -1)
                {
                    if (ScreenMode.Location == screenmode)
                    {
                        if (cboAttribute.SelectedItem != null)
                        {
                            //Call the API if the selected item isn't null to delete it
                            HttpClient client = InitializeClient();
                            HttpResponseMessage response = new HttpResponseMessage();

                            Location location = locations.ElementAt(cboAttribute.SelectedIndex);

                            response = client.DeleteAsync("Location/" + location.Id).Result;
                            locations.Remove(location);

                            Rebind();
                        }
                    }
                    else if (ScreenMode.Gender == screenmode)
                    {
                        if (cboAttribute.SelectedItem != null)
                        {
                            //Call the API if the selected item isn't null to delete it
                            HttpClient client = InitializeClient();
                            HttpResponseMessage response = new HttpResponseMessage();

                            Gender gender = genders.ElementAt(cboAttribute.SelectedIndex);

                            response = client.DeleteAsync("Gender/" + gender.Id).Result;
                            genders.Remove(gender);

                            Rebind();
                        }
                    }
                    else if (ScreenMode.Race == screenmode)
                    {
                        if (cboAttribute.SelectedItem != null)
                        {
                            //Call the API if the selected item isn't null to delete it
                            HttpClient client = InitializeClient();
                            HttpResponseMessage response = new HttpResponseMessage();

                            Race race = races.ElementAt(cboAttribute.SelectedIndex);

                            response = client.DeleteAsync("Race/" + race.Id).Result;
                            races.Remove(race);

                            Rebind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }
    }
}
